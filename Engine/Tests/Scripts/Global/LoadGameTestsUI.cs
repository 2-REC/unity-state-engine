using UnityEngine;
using UnityEngine.UI;

public class LoadGameTestsUI : MonoBehaviour {

    public LoadGame ctrl;
    public Text text;

    void Start() {
        text.text = StateIds.Name(ctrl.StateId);
    }

    public void StartGame1() {
        ctrl.StartGame("FILE_1");
    }

    public void StartGame2() {
        ctrl.StartGame("FILE_2");
    }

    public void StartGame3() {
        ctrl.StartGame("FILE_3");
    }

    public void Cancel() {
        ctrl.End();
    }

}
