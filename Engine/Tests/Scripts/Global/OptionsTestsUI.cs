using UnityEngine;
using UnityEngine.UI;

public class OptionsTestsUI : MonoBehaviour {

    public GlobalStateController ctrl;
    public Text text;

    void Start() {
        text.text = ctrl.stateId.ToString();
    }

    public void ResetAll() {
        GameSessionManager.Clear();
        GlobalSessionManager.Clear();
    }

    public void OK() {
        ctrl.End();
    }

}
