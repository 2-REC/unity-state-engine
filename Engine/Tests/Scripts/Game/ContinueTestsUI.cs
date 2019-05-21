/*
  This script can be used to test/simulate the "Continues" menu.
  It should be attached to a UI Canvas, and a "Continue" script should be provided as input parameter,
  as well as a UI Text field to display the number of continues left.
*/

using UnityEngine;
using UnityEngine.UI;

public class ContinueTestsUI : MonoBehaviour {

    public Continue cont;
    public Text textNbContinues;

    void Start() {
        textNbContinues.text = "Continues left: " + cont.GetGameData().GetContinues().ToString();
    }

    public void Yes() {
        cont.Yes();
        textNbContinues.text = "Continues left: " + cont.GetGameData().GetContinues().ToString();
    }

    public void No() {
        cont.No();
    }

}
