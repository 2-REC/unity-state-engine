using System;
using UnityEngine;

public class NewGame : GlobalStateController {

    public String exitScene = "GameIntro";

    public void StartGame(int difficulty) {
        GameSessionManager.NewGame(difficulty);
        Leave(exitScene);
    }

}
