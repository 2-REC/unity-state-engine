/*
  This script can be used to test/simulate the "Map" screen.
  It needs to be attached to a UI Canvas, and the "Map" script has to be provided as input parameter.
  /// (+other params...)
*/

using UnityEngine;
using UnityEngine.UI;

public class MapTestsUI : MonoBehaviour {

    public Map ctrl;
    public Text text;
    public GameObject panelMap;
    public GameObject buttonLevelPrefab;
    public GameObject panelSave;


    void Start() {
        text.text = ctrl.StateId.ToString();
        panelSave.gameObject.SetActive(false);

        float positionY = 50.0f;
        foreach(int level in ctrl.GetLevels()) {
            GameObject button = (GameObject)Instantiate(buttonLevelPrefab);
            button.transform.SetParent(panelMap.transform);
            button.GetComponent<Button>().onClick.AddListener(delegate{ctrl.StartLevel(level);});
            button.transform.GetChild(0).GetComponent<Text>().text = "Level" + level;
            button.transform.localPosition = new Vector3(0.0f, positionY, 0.0f);
            positionY -= 50.0f;

/*
            // highlight if completed
            if (ctrl.GetGameData().IsLevelCompleted(level)) {
//TODO: change colour or size...
            }
*/
        }
    }


    public void OpenSaveDlg() {
        panelSave.gameObject.SetActive(true);
    }

    public void CloseSaveDlg() {
        panelSave.gameObject.SetActive(false);
    }


    public void SaveGame1() {
        ctrl.SaveGame("FILE_1");
    }
    public void SaveGame2() {
        ctrl.SaveGame("FILE_2");
    }
    public void SaveGame3() {
        ctrl.SaveGame("FILE_3");
    }

}
