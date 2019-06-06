using UnityEngine;

public class GameData : IGameDataManager {

    //////// GAME - BEGIN
    private int points = 0;
    private int health = 0;
    //////// GAME - END


    protected override void LoadSpecifics() {
        //////// GAME - BEGIN
        points = gameSessionManager.GetField("POINTS");
        health = gameSessionManager.GetField("HEALTH");

        Debug.Log("GameData:Load - points: " + points);
        Debug.Log("GameData:Load - health: " + health);
        //////// GAME - END
    }

    protected override void CommitChangesSpecifics() {
        //////// GAME - BEGIN
        gameSessionManager.SetField("POINTS", points);
        gameSessionManager.SetField("HEALTH", health);

        Debug.Log("GameData:CommitChangesSpecifics - points: " + points);
        Debug.Log("GameData:CommitChangesSpecifics - health: " + health);
        //////// GAME - END
    }


    protected override void ResetLifeData() {
        //////// GAME - BEGIN
        health = gameSessionManager.GetInitialField("HEALTH");
        //////// GAME - END
    }

    protected override void ResetContinueData() {
        //////// GAME - BEGIN
        points = gameSessionManager.GetInitialField("POINTS");;
        //////// GAME - END
    }


    //////// GAME - BEGIN

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

    //////// GAME - END

}
