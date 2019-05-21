using UnityEngine;

public class LevelTestsGameUI : MonoBehaviour {

    public LevelManager levelManager;

    ////////
    public int damage = 5;
    public int points = 5;
    ////////


    public void NotifyWin() {
        levelManager.NotifyWin();
    }

    public void NotifyDeath() {
        levelManager.NotifyDeath();
    }

    ////////
    public void NotifyHit() {
        levelManager.NotifyHit(damage);
    }

    public void AddPoints() {
        levelManager.AddPoints(points);
    }
    ////////

}
