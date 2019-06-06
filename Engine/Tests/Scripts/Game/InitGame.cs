/*
TODO: rewrite this more clearly...

This script should be used in a "Game" scene (typically "Level"), to allow loading game data at the beginning.
It is useful for testing the scene, without requiring to execute the Global graph before (eg: coming from a "NewGame" state, which initialises the game data before starting the game graph).

The script requires a "Global Data Manager" to be attached to the "Game Manager", in order for the "Game Data" to be loaded.

The script execution order needs to be modified, to be between "GlobalManager" and "GameManager" (and before "Default Time").
(in "Edit" -> "Project Settings" -> "Script Execution Order")

Once the script has been executed once, it can be deactivated (or removed), and the data will persist ("GAME.json" file in the "StreaminAssets").
To reset the data to the "New Game" state, simply reactivate the script.

Alternatively, it is possible to manually add or edit the "GAME.json" file.

*/

using UnityEngine;

public class InitGame : MonoBehaviour {

    public int difficulty = 1;

    void Awake() {
        GameSessionManager.Instance.NewGame(difficulty);
    }

}
