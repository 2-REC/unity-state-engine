using UnityEngine;
using UnityEngine.UI;

public class GlobalAnimTestsUI : MonoBehaviour {

    public GlobalStateController ctrl;
    public Text text;

    void Start() {
        text.text = ctrl.stateId.ToString();
    }

    public void Skip() {
        ctrl.End();
    }

}
