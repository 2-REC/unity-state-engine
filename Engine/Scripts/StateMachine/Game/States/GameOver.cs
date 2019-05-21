using UnityEngine;

public class GameOver : GameStateController {

    public override void HandleMainState() {
        Debug.Log(GetGameData().CanContinue() ? "CONTINUE" : "GAME OVER");
    }

    // to be called to continue in state graph
    public void Continue() {
        // Update Game Data that should be saved when losing the game (with or without continues)
        //...
        // eg: high score, ...?

        if (GetGameData().CanContinue()) {
            Debug.Log("CONTINUE");

            // Update Game Data that should be saved when losing the game and have continues
            //...

            End();
        }
        else
        {
            Debug.Log("GAME OVER");

           // Update Game Data that should be saved when losing the game and have no continues
           //...

// !!!! ???? TODO: OK ? ???? !!!!
// (or SetDifficulty(-1)?
            GetGameData().SetLevel(-1);
            ChangeState(GameStateId.QUIT);
        }
    }

}
