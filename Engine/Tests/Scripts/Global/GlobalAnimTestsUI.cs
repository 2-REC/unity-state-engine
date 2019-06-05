using UnityEngine;
using UnityEngine.UI;

public class GlobalAnimTestsUI : MonoBehaviour {

    public GlobalStateController ctrl;
    public Text text;

    void Start() {
        text.text = StateIds.Name(ctrl.StateId);
    }

    public void Skip() {
        ctrl.End();
    }

}
