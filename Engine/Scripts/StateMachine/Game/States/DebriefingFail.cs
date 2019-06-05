using UnityEngine;

public class DebriefingFail : GameStateController {

    public override void HandleMainState() {
        int lives = GetGameData().GetLives();
        Debug.Log("lives left: " + lives);

        Debug.Log(GetGameData().IsGameOver() ? "GAME_OVER" : "RETRY");
    }

    // to be called to continue in state graph
    public void Continue() {
        if (GetGameData().IsGameOver()) {
            Debug.Log("GAME_OVER");

            // save data now to avoid loss in case of crash/stop
            GetGameData().CommitChanges();

//            ChangeState(GameStateId.GAME_OVER);
//            ChangeState(StateId.Id("GAME_OVER"));
LoadChildState("GAME_OVER");
        }
        else {
            Debug.Log("RETRY");

            // save data now to avoid loss in case of crash/stop
            GetGameData().CommitChanges();

            End();
        }
    }

}
