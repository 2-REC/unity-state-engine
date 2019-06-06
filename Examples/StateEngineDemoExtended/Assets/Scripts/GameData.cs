using UnityEngine;

public class GameData : IGameDataManager {

    ////////
    private int points = 0;
    private int health = 0;
    ////////


    protected override void LoadSpecifics() {
        ////////
        points = gameSessionManager.GetField("POINTS");
        health = gameSessionManager.GetField("HEALTH");

        Debug.Log("GameData:Load - points: " + points);
        Debug.Log("GameData:Load - health: " + health);
        ////////
    }

    protected override void CommitChangesSpecifics() {
        ////////
        gameSessionManager.SetField("POINTS", points);
        gameSessionManager.SetField("HEALTH", health);

        Debug.Log("GameData:CommitChangesSpecifics - points: " + points);
        Debug.Log("GameData:CommitChangesSpecifics - health: " + health);
        ////////
    }


    protected override void ResetLifeData() {
        ////////
        health = gameSessionManager.GetInitialField("HEALTH");
        ////////
    }

    protected override void ResetContinueData() {
        ////////
        points = gameSessionManager.GetInitialField("POINTS");;
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
        return gameSessionManager.GetInitialField("HEALTH");
    }

    public int GetHealth() {
        return health;
    }

    public void SetHealth(int newHealth) {
        health = newHealth;
    }
    ////////

}
