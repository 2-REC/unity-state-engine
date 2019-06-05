using UnityEngine;
using UnityEngine.UI;

public class OptionsTestsUI : MonoBehaviour {

    public GlobalStateController ctrl;
    public Text text;

    void Start() {
        text.text = StateIds.Name(ctrl.StateId);
    }

    public void ResetAll() {
        GameSessionManager.Instance.Clear();
        GlobalSessionManager.Instance.Clear();
    }

    public void OK() {
        ctrl.End();
    }

}
