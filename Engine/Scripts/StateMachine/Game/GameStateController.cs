using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// !!!! TODO: use same base class for GlobalStateController & GameStateController !!!!

public class GameStateController : MonoBehaviour
{
    public GameStateId stateId { get; private set; }

    protected GameStateManager gameStateManager;

    void Awake()
    {
        gameStateManager = GameStateManager.Instance;
        gameStateManager.OnStateChange += HandleOnStateChange;

        stateId = gameStateManager.GetStateId(SceneManager.GetActiveScene().name);

        InitState();
    }

    void Start()
    {
Debug.Log("GAME - Start - stateId: " + stateId);
        gameStateManager.SetGameState(stateId);
    }

    public void End()
    {
Debug.Log("GAME - End - stateId: " + stateId);
        gameStateManager.OnStateChange -= HandleOnStateChange;
        gameStateManager.NextState();
    }
    void OnDestroy()
    {
        gameStateManager.OnStateChange -= HandleOnStateChange;
    }

    public bool HandleOnStateChange()
    {
        bool handled = false;
        GameStateId currentStateId = gameStateManager.currentStateId;
        if (currentStateId == stateId)
        {
            HandleMainState();
            handled = true;
        }
        else
        {
            GameState state = gameStateManager.GetState(stateId);
            if (state.children != null)
            {
                for (int i = 0; i < state.children.Count; ++i)
                    foreach (GameStateId childId in state.children)
                    {
                        if (childId == stateId)
                        {
                            gameStateManager.OnStateChange -= HandleOnStateChange;
                            SceneManager.LoadScene(gameStateManager.GetState(childId).scene);
                            handled = true;
                            break;
                        }
                    }
            }
        }

        return handled;
    }

    public void ChangeState(GameStateId stateId)
    {
        gameStateManager.SetGameState(stateId);
    }

    protected void Leave(string exitScene)
    {
        gameStateManager.LeaveGraph(exitScene);
    }

    public GameDataManager GetGameData() {
        return gameStateManager.GetGameData();
    }


    public virtual void InitState() { }
    public virtual void HandleMainState() { }

}
