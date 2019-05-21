using UnityEngine;

public class Continue : GameStateController {

    public override void HandleMainState() {
        Debug.Log("CONTINUES LEFT: " + GetGameData().GetContinues());
    }

    // to be called to continue in state graph & continue game
    public void Yes() {
        // Update Game Data that should be saved when losing the game and continuing
// TODO: should only be called if can continue! (to avoid negative values ...)
        int continues = GetGameData().LoseContinue();
        Debug.Log("continues left: " + continues);
        //...

        End();
    }

    // to be called to continue in state graph but stop game
    public void No() {
        // Update Game Data that should be saved when losing the game and not continuing
        //...

        ChangeState(GameStateId.QUIT);
    }

}
