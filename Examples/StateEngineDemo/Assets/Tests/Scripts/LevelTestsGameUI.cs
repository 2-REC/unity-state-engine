using UnityEngine;

public class LevelTestsGameUI : MonoBehaviour {

    public LevelManager levelManager;

    public void NotifyWin() {
        levelManager.NotifyWin();
    }

    public void NotifyDeath() {
        levelManager.NotifyDeath();
    }

}
