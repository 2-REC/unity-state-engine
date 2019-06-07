using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class IStateController : MonoBehaviour {

    public bool canLeaveGraph = false;
    public string exitScene;

    public int StateId { get; private set; }

    protected IStateManager stateManager;


    void Awake() {
        stateManager = GetStateManager();
        stateManager.OnStateChange += HandleOnStateChange;

        StateId = stateManager.GetStateId(SceneManager.GetActiveScene().name);

        InitState();
    }

    void Start() {
Debug.Log("GAME - Start - StateId: " + StateId);
        stateManager.SetState(StateId);
    }

    public void End() {
Debug.Log("GAME - End - StateId: " + StateId);
        stateManager.OnStateChange -= HandleOnStateChange;
        stateManager.NextState();
    }

    void OnDestroy() {
        stateManager.OnStateChange -= HandleOnStateChange;
    }

    public bool HandleOnStateChange() {
        bool handled = false;
        int currentStateId = stateManager.CurrentStateId;
        if (currentStateId == StateId) {
            HandleMainState();
            handled = true;
        }
        else {
            State state = stateManager.GetState(StateId);
            if (state.Children != null) {
                for (int i = 0; i < state.Children.Count; ++i) {
                    foreach (int childId in state.Children) {
                        if (childId == StateId) {
                            stateManager.OnStateChange -= HandleOnStateChange;
                            SceneManager.LoadScene(stateManager.GetState(childId).Scene);
                            handled = true;
                            break;
                        }
                    }
                }
            }
        }
        return handled;
    }

    protected void LoadChildState(string childId) {
        State state = stateManager.GetState(StateId);
        if ((state.Children != null) && state.Children.Contains(StateIds.Index(childId))) {
            stateManager.SetState(StateIds.Index(childId));
        }
        else {
            throw new Exception("IStateController: 'LoadChildState' can only be called with one of its children state!");
        }
    }

    protected void Leave() {
        if (!canLeaveGraph) {
            throw new Exception("IStateController: 'Leave' cannot be called if the state cannot leave the graph!");
        }
        if (exitScene != "") {
            stateManager.LeaveGraph(exitScene);
        }
    }


    public virtual void InitState() { }
    public virtual void HandleMainState() { }

    protected abstract IStateManager GetStateManager();

}
