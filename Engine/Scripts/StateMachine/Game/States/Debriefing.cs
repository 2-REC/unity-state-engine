using UnityEngine;

public class Debriefing : GameStateController {

    public override void HandleMainState() {
//TODO: ?
//        GetGameData().SetLevelCompleted();

        Debug.Log(GetGameData().IsGameComplete() ? "GAME COMPLETED" : "GAME NOT COMPLETED");
    }

    // to be called to continue in state graph
    public void Continue() {
        if (!GetGameData().IsGameComplete()) {
            Debug.Log("GAME NOT COMPLETED");

//TODO: ?
            // save data now to avoid loss in case of crash/stop
//            GetGameData().CommitChanges();

            End();
        }
        else {
            Debug.Log("GAME COMPLETED");

//TODO: ?
            // save data now to avoid loss in case of crash/stop
//            GetGameData().CommitChanges();

            LoadChildState("GAME_END");
        }
    }

//TODO: to remove! (test purpose)
// (& associated GUI button)
    public void Next() {
        End();
    }

//TODO: to remove! (test purpose)
// (& associated GUI button)
    public void GameEnd() {
        LoadChildState("GAME_END");
    }

}
