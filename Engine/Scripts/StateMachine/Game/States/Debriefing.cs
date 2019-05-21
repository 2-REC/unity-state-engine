using UnityEngine;

public class Debriefing : GameStateController {

    public override void HandleMainState() {
//        GetGameData().SetLevelCompleted();

        Debug.Log(GetGameData().IsGameComplete() ? "GAME COMPLETED" : "GAME NOT COMPLETED");
    }

    // to be called to continue in state graph
    public void Continue() {
        if (!GetGameData().IsGameComplete()) {
            Debug.Log("GAME NOT COMPLETED");

            // save data now to avoid loss in case of crash/stop
//            GetGameData().CommitChanges();

            End();
        }
        else {
            Debug.Log("GAME COMPLETED");

            // save data now to avoid loss in case of crash/stop
//            GetGameData().CommitChanges();

            ChangeState(GameStateId.GAME_END);
        }
    }

// !!!! TODO: remove !!!!
// (& associated GUI button)
    public void Next() {
        End();
    }

// !!!! TODO: remove !!!!
// (& associated GUI button)
    public void GameEnd() {
        ChangeState(GameStateId.GAME_END);
    }

}
