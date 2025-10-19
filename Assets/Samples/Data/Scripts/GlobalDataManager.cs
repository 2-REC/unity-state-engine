using StateEngine;

namespace SampleData {

    public class GlobalDataManager : IGlobalDataManager {

        public int Difficulty { get; set; } = 0;

        protected override void LoadSpecifics() {
            // Load the global field values.
            Difficulty = globalSessionManager.GetField("DIFFICULTY");
        }

        protected override void CommitChangesSpecifics() {
            // Save the game field values.
            globalSessionManager.SetField("DIFFICULTY", Difficulty);
        }

    }

}
