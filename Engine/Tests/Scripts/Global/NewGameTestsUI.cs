using UnityEngine;
using UnityEngine.UI;

public class NewGameTestsUI : MonoBehaviour {

    public NewGame ctrl;
    public Text text;

    void Start() {
        text.text = ctrl.stateId.ToString();
    }

    public void StartGameEasy() {
        ctrl.StartGame(0);
    }

    public void StartGameNormal() {
        ctrl.StartGame(1);
    }

    public void StartGameHard() {
        ctrl.StartGame(2);
    }

    public void Cancel() {
        ctrl.End();
    }

}
