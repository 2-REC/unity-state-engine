using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
ALL STUFF RELATED TO GAME (DIFFICULTY, LEVEL, BEGIN ANIM, ETC)
SHOULDN'T BE HANDLED IN STATE MANAGERS BUT IN OTHER SCRIPTS/OBJECTS
    => other objects just have to call "End" when done with scene...
    => StateControllers are simpler: only have to call "End" - or "ChangeState" or "Leave" for specific states
    => states without children can use this script
    => states with children can derive this class and call "ChangeState"
    => states leacing the graph can derive this class and call "Leave"
*/

// !!!! TODO: use same base class for GlobalStateController & GameStateController !!!!

public class GlobalStateController : MonoBehaviour
{
    public GlobalStateId stateId { get; private set; }

    protected GlobalStateManager globalStateManager;

    void Awake()
    {
        globalStateManager = GlobalStateManager.Instance;
        globalStateManager.OnStateChange += HandleOnStateChange;

        stateId = globalStateManager.GetStateId(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
Debug.Log("GLOBAL - Start - stateId: " + stateId);
        globalStateManager.SetGlobalState(stateId);
    }

    public void End()
    {
Debug.Log("GLOBAL - End - stateId: " + stateId);
        globalStateManager.OnStateChange -= HandleOnStateChange;
        globalStateManager.NextState();
    }

    public bool HandleOnStateChange()
    {
        bool handled = false;
        GlobalStateId currentStateId = globalStateManager.currentStateId;
        if (currentStateId == stateId)
        {
            HandleMainState();
            handled = true;
        }
        else
        {
            GlobalState state = globalStateManager.GetState(stateId);
            if (state.children != null)
            {
                for (int i = 0; i < state.children.Count; ++i)
                    foreach (GlobalStateId childId in state.children)
                    {
                        if (childId == stateId)
                        {
                            globalStateManager.OnStateChange -= HandleOnStateChange;
                            SceneManager.LoadScene(globalStateManager.GetState(childId).scene);
                            handled = true;
                            break;
                        }
                    }
            }
        }

        return handled;
    }

    protected void ChangeState(GlobalStateId stateId)
    {
        globalStateManager.SetGlobalState(stateId);
    }

    protected void Leave(string exitScene)
    {
        globalStateManager.LeaveGraph(exitScene);
    }

    // should be overriden if want specific state/scene initialisations
    public virtual void HandleMainState() { }

}
