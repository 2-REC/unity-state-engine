using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate bool OnGameStateChangeHandler();

/*

when building states graph, should check if all the scenes exist
(& even if they are in the BuildSettings!)


to determine if scene exists, look at:
https://github.com/AdriaandeJongh/UnityTools/blob/master/SceneListCheck.cs

OR:

/// <summary>
 /// Used to get assets of a certain type and file extension from entire project
 /// </summary>
 /// <param name="type">The type to retrieve. eg typeof(GameObject).</param>
 /// <param name="fileExtension">The file extention the type uses eg ".prefab".</param>
 /// <returns>An Object array of assets.</returns>
 public static T[] GetAssetsOfType<T>(string fileExtension) where T : UnityEngine.Object
 {
     List<T> tempObjects = new List<T>();
     DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
     FileInfo[] goFileInfo = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);
     
     int i = 0; int goFileInfoLength = goFileInfo.Length;
     FileInfo tempGoFileInfo; string tempFilePath;
     T tempGO;
     for (; i < goFileInfoLength; i++)
     {
         tempGoFileInfo = goFileInfo[i];
         if (tempGoFileInfo == null)
             continue;
         
         tempFilePath = tempGoFileInfo.FullName;
         tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
         tempGO = AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(T)) as T;
         if (tempGO == null)
         {
             continue;
         }
         else if (!(tempGO is T))
         {
             continue;
         }
         
         tempObjects.Add(tempGO);
     }
     
     return tempObjects.ToArray();
 }

*/


public class GameStateManager : MonoBehaviour
{
// !!!! TODO: automate or make more user friendly !!!!
    private const string GRAPH_XML = "Xml/game_states";


    public event OnGameStateChangeHandler OnStateChange;
    public GameStateId currentStateId { get; private set; }

    private static GameStateManager instance = null;

    private GameDataManager gameData = null;
//    private GlobalDataManager globalData = null;

    private GameState[] states;
    private Stack<GameStateId> stack;

    private AsyncOperation async;
    private bool isAsync;


    public static GameStateManager Instance
    {
        get
        {
            if (GameStateManager.instance == null)
            {
                GameObject go = new GameObject("GameStateManager");
                GameStateManager.instance = go.AddComponent<GameStateManager>();
                DontDestroyOnLoad(GameStateManager.instance);
                GameStateManager.instance.Load();
            }
            return GameStateManager.instance;
        }
    }


    protected GameStateManager()
    {
        isAsync = false;

        stack = new Stack<GameStateId>();
        stack.Push(GameStateId.NONE);
        currentStateId = GameStateId.NONE;        
    }

    void Load()
    {
        states = GameLoader.LoadGameStateGraph(GRAPH_XML);
    }

    public static bool IsInstance()
    {
        return (instance != null);
    }

    public GameState GetState(GameStateId stateId)
    {
        return states[(int)stateId];
    }

    public GameStateId GetStateId(string sceneName)
    {
        foreach(GameState state in states) {
            if (GetSceneName(state) == sceneName) {
                return state.id;
            }
        }
        return GameStateId.NONE;
    }

    public void SetGameState(GameStateId stateId)
    {
        if (currentStateId != stateId)
        {
            currentStateId = stateId;
            stack.Push(stateId);
        }

        bool handled = OnStateChange();

        if (!handled)
        {
            LoadState(stateId);
        }
        else
        {
            GameState state = states[(int)currentStateId];
            if (state.children == null)
            {
                state = states[(int)state.next];
                if (GameStateId.NONE != state.id)
                {
                    AsyncLoadScene();
                }
            }
        }
    }

    public void NextState()
    {
        currentStateId = stack.Pop();

        GameState state = states[(int)currentStateId];
        GameStateId next = state.next;
        if (next != GameStateId.NONE)
        {
            LoadState(next);
        }
        else
        {
            currentStateId = stack.Peek();
            GameState currentState = states[(int)currentStateId];

            if (!currentState.restartable)
            {
                stack.Pop();

                GameStateId stateId = currentState.next;
                while (currentStateId != GameStateId.NONE && stateId == GameStateId.NONE)
                {
                    currentStateId = stack.Peek();
                    currentState = states[(int)currentStateId];
                    if (currentState.restartable)
                    {
                        LoadState(currentStateId);
                        return;
                    }
                    stack.Pop();
                    stateId = currentState.next;
                }
                LoadState(stateId);
            }
            else
            {
                LoadState(currentStateId);
            }

        }
    }

    private void LoadState(GameStateId stateId)
    {
        if (currentStateId != stateId)
        {
            currentStateId = stateId;
            stack.Push(stateId);
        }

        GameState state = states[(int)currentStateId];
        if (isAsync)
        {
Debug.Log("LoadState: ActivateScene");
            ActivateScene();
        }
        else
        {
Debug.Log("LoadState: LoadScene - scene: " + GetSceneName(state));
            SceneManager.LoadScene(GetSceneName(state));
        }
    }

    string GetSceneName(GameState state)
    {
        if (state.isLevel)
        {
            string effectiveScene = gameData.GetSceneName();
            if (effectiveScene != null && !"".Equals(effectiveScene))
            {
                return effectiveScene;
            }
        }
        return state.scene;
    }

    public void OnApplicationQuit()
    {
        GameStateManager.instance = null;
    }

    public void AsyncLoadScene()
    {
        StartCoroutine("load");
    }

    IEnumerator load()
    {
// !!!! TODO: remove when finished developing !!!!
        Debug.LogWarning("ASYNC LOAD STARTED - " + "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");

        GameState state = states[(int)currentStateId];
        state = states[(int)state.next];
Debug.Log("AsyncLoadScene - scene: " + GetSceneName(state));
        async = SceneManager.LoadSceneAsync(GetSceneName(state));

        isAsync = true;
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {
        isAsync = false;
        async.allowSceneActivation = true;
    }

    public void LeaveGraph(String exitScene)
    {
        gameData.Leave();

// !!!! ???? TODO ???? !!!!
//?        GameStateManager.instance = null;
        Destroy(gameObject);

// !!!! TODO: check that exitScene is valid !!!!
// if not ...?
        SceneManager.LoadScene(exitScene);
    }

    public void Terminate()
    {
        Destroy(gameObject);
        Application.Quit();
    }


/*
    public void SetGlobalData(GlobalDataManager globalData) {
        if (this.globalData == null) {
            this.globalData = globalData;
        }
    }

    public GlobalDataManager GetGlobalData() {
        return globalData;
    }
*/

    public void SetGameData(GameDataManager gameData) {
        if (this.gameData == null) {
            this.gameData = gameData;
        }
    }

    public GameDataManager GetGameData() {
        return gameData;
    }

}
