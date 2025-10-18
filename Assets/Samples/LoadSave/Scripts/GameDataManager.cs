
namespace SampleLoadSave {

    public class GameDataManager : IGameDataManager {
        // Declare a property for each game field defined in "values.xml".
        public int Points { get; set; } = 0;
        public int Health { get; set; } = 0;


        protected override void LoadSpecifics() {
            // Load the game field values.
            // Names must match the field names defined in "values.xml".
            Points = gameSessionManager.GetField("POINTS");
            Health = gameSessionManager.GetField("HEALTH");
        }

        protected override void CommitChangesSpecifics() {
            // Save the game field values.
            // Names must match the field names defined in "values.xml".
            gameSessionManager.SetField("POINTS", Points);
            gameSessionManager.SetField("HEALTH", Health);
        }

        protected override void ResetLifeData() {
            // Load initial game field values when lose a life.
            // Only fields that are persistent in the scope of a life.
            Health = gameSessionManager.GetInitialField("HEALTH");
        }

        protected override void ResetContinueData() {
            // Load initial game field values when lose a continue.
            // Only fields that are persistent in the scope of a continue.
            Points = gameSessionManager.GetInitialField("POINTS");
        }

    }

}
