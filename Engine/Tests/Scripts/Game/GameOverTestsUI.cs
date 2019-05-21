/*
  This script can be used to test/simulate the "Game Over" screen.
  It should be attached to a UI Canvas, and a "GameOverTests" script should be provided as input parameter,
  as well as a UI Text field to display the number of continues left.
*/
/*
using UnityEngine;
using UnityEngine.UI;

public class GameOverTestsUI : MonoBehaviour {

    public GameOverTests gameOverTests;
    public Text textNbContinues;

    void Start() {
        textNbContinues.text = "Continues left: " + gameOverTests.GetGameData().GetContinues().ToString();
    }

    public void Retry() {
        gameOverTests.Retry();
    }

    public void Finish() {
        gameOverTests.Finish();
    }

}
*/
using UnityEngine;
using UnityEngine.UI;

public class GameOverTestsUI : MonoBehaviour {

    public GameOver gameOver;
    public Text textNbContinues;
    public Text textContinue;

    void Start() {
        int nbContinues = gameOver.GetGameData().GetContinues();
        textNbContinues.text = "Continues left: " + nbContinues.ToString();
        if (nbContinues > 0) {
            textContinue.text = "Have a continue => CONTINUE";
        }
        else {
            textContinue.text = "Don't have continues => QUIT";
        }
    }

    public void Continue() {
        gameOver.Continue();
    }

}
