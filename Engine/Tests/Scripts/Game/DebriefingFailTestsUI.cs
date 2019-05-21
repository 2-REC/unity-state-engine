/*
  This script can be used to test/simulate the "Game Over" screen.
  It should be attached to a UI Canvas, and a "GameOverTests" script should be provided as input parameter,
  as well as a UI Text field to display the number of lives left.
*/
/*
using UnityEngine;
using UnityEngine.UI;

public class DebriefingFailTestsUI : MonoBehaviour {

    public DebriefingFailTests debriefingFailTests;
    public Text textNbLives;

    void Start() {
        textNbLives.text = "Lives left: " + debriefingFailTests.GetGameData().GetLives().ToString();
    }

    public void Retry() {
        debriefingFailTests.Retry();
        textNbLives.text = "Lives left: " + debriefingFailTests.GetGameData().GetLives().ToString();
    }

    public void GameOver() {
        debriefingFailTests.GameOver();
    }

}
*/
using UnityEngine;
using UnityEngine.UI;

public class DebriefingFailTestsUI : MonoBehaviour {

    public DebriefingFail debriefingFail;
    public Text textNbLives;
    public Text textContinue;

    void Start() {
        textNbLives.text = "Lives left: " + debriefingFail.GetGameData().GetLives().ToString();

        int nbLives = debriefingFail.GetGameData().GetLives();
        textNbLives.text = "Lives left: " + nbLives.ToString();
        if (nbLives > 0) {
            textContinue.text = "Have a life => RETRY";
        }
        else {
            textContinue.text = "Don't have lives => GAME OVER";
        }
    }

    public void Continue() {
        debriefingFail.Continue();
    }

}
