using UnityEngine;

public class GameData : GameDataManager {

    ////////
    private int points = 0;
    private int health = 0;
    ////////


    protected override void LoadSpecifics() {
        ////////
        points = GameSessionManager.GetField("POINTS");
        health = GameSessionManager.GetField("HEALTH");

        Debug.Log("GameData:Load - points: " + points);
        Debug.Log("GameData:Load - health: " + health);
        ////////
    }

    protected override void CommitChangesSpecifics() {
        ////////
        GameSessionManager.SetField("POINTS", points);
        GameSessionManager.SetField("HEALTH", health);

        Debug.Log("GameData:CommitChangesSpecifics - points: " + points);
        Debug.Log("GameData:CommitChangesSpecifics - health: " + health);
        ////////
    }


    protected override void ResetLifeData() {
        ////////
        health = GameSessionManager.GetInitialField("HEALTH");
        ////////
    }

    protected override void ResetContinueData() {
        ////////
        points = GameSessionManager.GetInitialField("POINTS");;
        ////////
    }


    ////////
    public int GetPoints() {
        return points;
    }

    public void SetPoints(int newPoints) {
        points = newPoints;
    }

    public int GetInitialHealth() {
        return GameSessionManager.GetInitialField("HEALTH");
    }

    public int GetHealth() {
        return health;
    }

    public void SetHealth(int newHealth) {
        health = newHealth;
    }
    ////////

}
