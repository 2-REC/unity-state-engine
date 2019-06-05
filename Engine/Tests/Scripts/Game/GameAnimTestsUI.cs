using UnityEngine;
using UnityEngine.UI;

public class GameAnimTestsUI : MonoBehaviour {

    public GameStateController ctrl;
    public Text text;

    void Start() {
        text.text = StateIds.Name(ctrl.StateId);
    }

    public void Skip() {
        ctrl.End();
    }

}
