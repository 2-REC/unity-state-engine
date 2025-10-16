
public class GameDataManager : IGameDataManager {
    // Declare a property for each game field defined in "values.xml".
    public int points { get; set; } = 0;
    public int health { get; set; } = 0;


    protected override void LoadSpecifics() {
        // Load the game field values.
        // Names must match the field names defined in "values.xml".
        points = gameSessionManager.GetField("POINTS");
        health = gameSessionManager.GetField("HEALTH");
    }

    protected override void CommitChangesSpecifics() {
        // Save the game field values.
        // Names must match the field names defined in "values.xml".
        gameSessionManager.SetField("POINTS", points);
        gameSessionManager.SetField("HEALTH", health);
    }

    protected override void ResetLifeData() {
        // Load initial game field values when lose a life.
        // Only fields that are persistent in the scope of a life.
	    health = gameSessionManager.GetInitialField("HEALTH");
    }

    protected override void ResetContinueData() {
        // Load initial game field values when lose a continue.
        // Only fields that are persistent in the scope of a continue.
        points = gameSessionManager.GetInitialField("POINTS");
    }

}
