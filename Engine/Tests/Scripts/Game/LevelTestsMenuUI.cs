using UnityEngine;
using UnityEngine.UI;

public class LevelTestsMenuUI : MonoBehaviour {

    public Level level;
    public GameObject panel;
    public Text text;
    public Text textLevel;

    void Awake() {
        panel.gameObject.SetActive(false);
    }

    void Start() {
        text.text = level.stateId.ToString();
        textLevel.text = level.GetGameData().GetLevelName();
    }

    public void Pause() {
        level.Pause();
        panel.gameObject.SetActive(true);
    }

    public void Unpause() {
        panel.gameObject.SetActive(false);
        level.Unpause();
    }

    public void Save1() {
        level.Save("FILE_1");
    }

    public void Save2() {
        level.Save("FILE_2");
    }

    public void Save3() {
        level.Save("FILE_3");
    }

    public void Quit() {
// TODO: should have a YES/NO pop-up box.
        level.QuitGame();
    }

}
