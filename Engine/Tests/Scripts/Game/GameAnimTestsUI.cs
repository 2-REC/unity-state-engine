using UnityEngine;
using UnityEngine.UI;

public class GameAnimTestsUI : MonoBehaviour {

    public GameStateController ctrl;
    public Text text;

    void Start() {
        text.text = ctrl.stateId.ToString();
    }

    public void Skip() {
        ctrl.End();
    }

}
