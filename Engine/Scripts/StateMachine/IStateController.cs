using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class IStateController : MonoBehaviour {

    public bool canLeaveGraph = false;
    public string exitScene;

    public int StateId { get; private set; }

    protected IStateManager gameStateManager;


    void Awake() {
        gameStateManager = GetStateManager();
        gameStateManager.OnStateChange += HandleOnStateChange;

        StateId = gameStateManager.GetStateId(SceneManager.GetActiveScene().name);

        InitState();
    }

    void Start() {
Debug.Log("GAME - Start - StateId: " + StateId);
        gameStateManager.SetState(StateId);
    }

    public void End() {
Debug.Log("GAME - End - StateId: " + StateId);
        gameStateManager.OnStateChange -= HandleOnStateChange;
        gameStateManager.NextState();
    }

    void OnDestroy() {
        gameStateManager.OnStateChange -= HandleOnStateChange;
    }

    public bool HandleOnStateChange() {
        bool handled = false;
        int currentStateId = gameStateManager.CurrentStateId;
        if (currentStateId == StateId) {
            HandleMainState();
            handled = true;
        }
        else {
//!?            GameState state = gameStateManager.GetState(StateId);
            State state = gameStateManager.GetState(StateId);
            if (state.Children != null) {
                for (int i = 0; i < state.Children.Count; ++i) {
                    foreach (int childId in state.Children) {
                        if (childId == StateId) {
                            gameStateManager.OnStateChange -= HandleOnStateChange;
                            SceneManager.LoadScene(gameStateManager.GetState(childId).Scene);
                            handled = true;
                            break;
                        }
                    }
                }
            }
        }
        return handled;
    }

/*
//TODO: CHANGE!
//    public void ChangeState(GameStateId stateId) {
    protected void ChangeState(int stateId) {
        gameStateManager.SetGameState(stateId);
    }
*/
    protected void LoadChildState(string childId) {
//!?        GameState state = gameStateManager.GetState(StateId);
        State state = gameStateManager.GetState(StateId);
        if ((state.Children != null) && state.Children.Contains(StateIds.Index(childId))) {
            gameStateManager.SetState(StateIds.Index(childId));
        }
        else {
            throw new Exception("GameStateController: 'LoadChildState' can only be called with one of its children state!");
        }
    }

/*
    protected void Leave(string exitScene) {
        gameStateManager.LeaveGraph(exitScene);
    }
*/
    protected void Leave() {
        if (!canLeaveGraph) {
            throw new Exception("GameStateController: 'Leave' cannot be called if the state cannot leave the graph!");
        }
        if (exitScene != "") {
            gameStateManager.LeaveGraph(exitScene);
        }
    }


//TODO: move to another class?
    public IGameDataManager GetGameData() {
//        return (IGameDataManager)gameStateManager.GetGameDataManager();
        return (IGameDataManager)gameStateManager.GetDataManager();
    }
/*
    public IDataManager GetGameData() {
        return gameStateManager.GetGameData();
    }
*/


    public virtual void InitState() { }
    public virtual void HandleMainState() { }

    protected abstract IStateManager GetStateManager();

}
