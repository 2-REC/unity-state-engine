using UnityEngine;
using System.Collections;

public class DebriefingTestsUI : MonoBehaviour {

    public Debriefing debriefing;

    public void Next() {
        debriefing.Next();
    }

    public void GameEnd() {
        debriefing.GameEnd();
    }

}
