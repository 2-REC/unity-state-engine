using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate bool OnGlobalStateChangeHandler();

public class GlobalStateManager : MonoBehaviour
{
// !!!! TODO: automate or make more user friendly !!!!
    private const String GRAPH_XML = "Xml/global_states";


    public event OnGlobalStateChangeHandler OnStateChange;
    public GlobalStateId currentStateId { get; private set; }

    private static GlobalStateManager instance = null;

    private GlobalDataManager globalData = null;

    private GlobalState[] states;
    private Stack<GlobalStateId> stack;

    private AsyncOperation async;
    private bool isAsync;


    public static GlobalStateManager Instance
    {
        get
        {
            if (GlobalStateManager.instance == null)
            {
                GameObject go = new GameObject("GlobalStateManager");
                GlobalStateManager.instance = go.AddComponent<GlobalStateManager>();
                DontDestroyOnLoad(GlobalStateManager.instance);
                GlobalStateManager.instance.Load();
            }
            return GlobalStateManager.instance;
        }
    }


    protected GlobalStateManager()
    {
        isAsync = false;

        stack = new Stack<GlobalStateId>();
        stack.Push(GlobalStateId.NONE);
        currentStateId = GlobalStateId.NONE;
    }

    void Load()
    {
        states = GlobalLoader.LoadGlobalStateGraph(GRAPH_XML);
    }

    public static bool IsInstance()
    {
        return (instance != null);
    }

    public GlobalState GetState(GlobalStateId stateId)
    {
        return states[(int)stateId];
    }

    public GlobalStateId GetStateId(string sceneName)
    {
        foreach(GlobalState state in states) {
            if (state.scene == sceneName) {
                return state.id;
            }
        }
        return GlobalStateId.NONE;
    }

    public void SetGlobalState(GlobalStateId stateId)
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
            GlobalState state = states[(int)currentStateId];
            if (state.children == null)
            {
                state = states[(int)state.next];
                if (GlobalStateId.NONE != state.id)
                {
                    AsyncLoadScene();
                }
            }
        }
    }

    public void NextState()
    {
        currentStateId = stack.Pop();

        GlobalState state = states[(int)currentStateId];
        GlobalStateId next = state.next;
        if (next != GlobalStateId.NONE)
        {
            LoadState(next);
        }
        else
        {
            currentStateId = stack.Peek();
            GlobalState currentState = states[(int)currentStateId];

            if (!currentState.restartable)
            {
                stack.Pop();

                GlobalStateId stateId = currentState.next;
                while (currentStateId != GlobalStateId.NONE && stateId == GlobalStateId.NONE)
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

    private void LoadState(GlobalStateId stateId)
    {
        if (currentStateId != stateId)
        {
            currentStateId = stateId;
            stack.Push(stateId);
        }

        GlobalState state = states[(int)currentStateId];
        String scene = state.scene;
        if ((scene != null) && !"".Equals(scene))
        {
            if (isAsync)
            {
Debug.Log("LoadState: ActivateScene");
                ActivateScene();
            }
            else
            {
Debug.Log("LoadState: LoadScene - scene: " + scene);
                SceneManager.LoadScene(scene);
            }
        }
        else
        {
            Terminate();
        }
    }

    public void OnApplicationQuit()
    {
Debug.Log("AsyncLoadScene - OnApplicationQuit");
        GlobalStateManager.instance = null;
    }

    public void AsyncLoadScene()
    {
        StartCoroutine("load");
    }

    IEnumerator load()
    {
// !!!! TODO: remove when finished developing !!!!
        Debug.LogWarning("ASYNC LOAD STARTED - " + "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");

        GlobalState state = states[(int)currentStateId];
        state = states[(int)state.next];
        String scene = state.scene;
Debug.Log("AsyncLoadScene - scene: " + scene);
        async = SceneManager.LoadSceneAsync(scene);
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
// !!!! ???? TODO ???? !!!!
/*
Debug.Log("!!!! TODO: write class holding the game's data !!!!");
Debug.Log("          => Serializable class");
Debug.Log("          => and here call a \"SaveData\" method");
*/

// !!!! ???? TODO ???? !!!!
//?        GlobalStateManager.instance = null;
        Destroy(gameObject);

// !!!! TODO: check that exitScene is valid !!!!
// if not ...?
        SceneManager.LoadScene(exitScene);
    }

    public void Terminate()
    {
Debug.Log("AsyncLoadScene - Terminate");
        Destroy(gameObject);
        Application.Quit();
    }


    public void SetGlobalData(GlobalDataManager globalData) {
        if (this.globalData == null) {
            this.globalData = globalData;
        }
    }

    public GlobalDataManager GetGlobalData() {
        return globalData;
    }

}
