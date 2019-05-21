using UnityEngine;
using UnityEngine.UI;

public class BriefingTestsUI : MonoBehaviour {

//    public Briefing briefing;
    public GameStateController controller;
    public Text textLevel;

    void Start () {
        textLevel.text = controller.GetGameData().GetLevelName();
    }

    public void Go() {
        controller.End();
    }

}
