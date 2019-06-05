using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate bool OnStateChangeHandler();

public class IStateManager : MonoBehaviour {

//TODO: get from settings/config...?
//    public static string GRAPH_XML;


    public event OnStateChangeHandler OnStateChange;
    public int CurrentStateId { get; private set; }

    protected static IStateManager instance = null;

    protected IDataManager dataManager = null;

    private State[] states;
    private Stack<int> stack;

    private AsyncOperation async;
    private bool isAsync;


    protected IStateManager() {
        isAsync = false;

        stack = new Stack<int>();
        stack.Push(StateIds.NONE);
        CurrentStateId = StateIds.NONE;
    }

/*
//    void Load() {
    public void Load() {
        GraphLoader graphLoader = new GraphLoader(GRAPH_XML);
        states = graphLoader.LoadStateGraph();
    }
*/
    public void Load(IGraphLoader graphLoader) {
        states = graphLoader.LoadStateGraph();
    }

    public static bool IsInstance() {
        return (instance != null);
    }

    public State GetState(int stateId) {
        return states[stateId];
    }

    public int GetStateId(string sceneName) {
        foreach(State state in states) {
            if (GetSceneName(state) == sceneName) {
                return state.Id;
            }
        }
        return StateIds.NONE;
    }

    public void SetState(int stateId) {
        if (CurrentStateId != stateId) {
            CurrentStateId = stateId;
            stack.Push(stateId);
        }

        bool handled = OnStateChange();

        if (!handled) {
            LoadState(stateId);
        }
        else {
            State state = states[CurrentStateId];
            if (state.Children == null) {
                state = states[state.Next];
                if (StateIds.NONE != state.Id) {
                    AsyncLoadScene();
                }
            }
        }
    }

    public void NextState() {
        CurrentStateId = stack.Pop();

        State state = states[CurrentStateId];
        int next = state.Next;
        if (next != StateIds.NONE) {
            LoadState(next);
        }
        else {
            CurrentStateId = stack.Peek();
            State currentState = states[CurrentStateId];

            if (!currentState.Restartable) {
                stack.Pop();

                int stateId = currentState.Next;
                while ((CurrentStateId != StateIds.NONE) && (stateId == StateIds.NONE)) {
                    CurrentStateId = stack.Peek();
                    currentState = states[CurrentStateId];
                    if (currentState.Restartable) {
                        LoadState(CurrentStateId);
                        return;
                    }
                    stack.Pop();
                    stateId = currentState.Next;
                }
                LoadState(stateId);
            }
            else {
                LoadState(CurrentStateId);
            }
        }
    }

    private void LoadState(int stateId) {
        if (CurrentStateId != stateId) {
            CurrentStateId = stateId;
            stack.Push(stateId);
        }

        State state = states[CurrentStateId];
        string scene = GetSceneName(state);
        if ((scene != null) && !"".Equals(scene)) {
            if (isAsync) {
                ActivateScene();
            }
            else {
                SceneManager.LoadScene(scene);
            }
        }
        else {
            Terminate();
        }
    }

    public void OnApplicationQuit() {
        instance = null;
    }

    public void AsyncLoadScene() {
        StartCoroutine("LoadScene");
    }


    IEnumerator LoadScene() {
//TODO: remove when finished developing?
        Debug.LogWarning("ASYNC LOAD STARTED - " + "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");

        State state = states[CurrentStateId];
        state = states[state.Next];

        async = SceneManager.LoadSceneAsync(GetSceneName(state));

        isAsync = true;
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene() {
        isAsync = false;
        async.allowSceneActivation = true;
    }

    public void LeaveGraph(string exitScene) {
        dataManager.Leave();

//TODO: required?
//?        instance = null;
        Destroy(gameObject);

//TODO: check that exitScene is valid!
// if not ...?
        SceneManager.LoadScene(exitScene);
    }

    protected virtual string GetSceneName(State state) {
        return state.Scene;
    }

    public void Terminate() {
        Destroy(gameObject);
        Application.Quit();
    }


    public void SetDataManager(IDataManager dataManager) {
        if (this.dataManager == null) {
            this.dataManager = dataManager;
        }
    }

    public IDataManager GetDataManager() {
        return dataManager;
    }

}
