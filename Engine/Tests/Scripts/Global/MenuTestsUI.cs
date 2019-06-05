using UnityEngine;
using UnityEngine.UI;

public class MenuTestsUI : MonoBehaviour {

    public Menu ctrl;
    public Text text;
    public GameObject buttonContinue;


    void Start () {
        text.text = StateIds.Name(ctrl.StateId);
        if (GameSessionManager.GetLevel() == -1) {
            buttonContinue.SetActive(false);
        }
    }

    public void NewGame() {
        ctrl.NewGame();
    }

    public void LoadGame() {
        ctrl.LoadGame();
    }

    public void Continue() {
        ctrl.Continue();
    }

    public void Options() {
        ctrl.Options();
    }

    public void Credits() {
        ctrl.Credits();
    }

    public void Quit() {
        ctrl.End();
    }

}
