using UnityEngine;
using UnityEngine.UI;

public class OptionsTestsUI : MonoBehaviour {

    public GlobalStateController ctrl;
    public Text text;

    void Start() {
        text.text = StateIds.Name(ctrl.StateId);
    }

    public void ResetAll() {
        GameSessionManager.Clear();
        GlobalSessionManager.Clear();
    }

    public void OK() {
        ctrl.End();
    }

}
