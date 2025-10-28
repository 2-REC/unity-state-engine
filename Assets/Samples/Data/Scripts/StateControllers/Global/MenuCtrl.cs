using StateEngine;

namespace SampleData {

    public class MenuCtrl : GlobalStateController {

        public string gameSceneName;

        public int Difficulty { get; private set; }

        public override void HandleMainState() {
            Difficulty = ((GlobalDataManager)GetGlobalData()).Difficulty;
        }

        public void NewGame() {
            GameSessionManager.Instance.NewGame(Difficulty);
            GameSessionManager.Instance.SetLevel(1);
            Leave(gameSceneName);
        }

        public void SetDifficulty(int value) {
            Difficulty = value;
            ((GlobalDataManager)GetGlobalData()).Difficulty = value;
        }

    }

}
