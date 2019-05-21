using System;
using UnityEngine;

public class LoadGame : GlobalStateController {

    public String exitScene = "Map"; //?

    public void StartGame(string filename) {
        if(GameSessionManager.LoadGame(filename)) {
            Leave(exitScene);
        }
    }

}
