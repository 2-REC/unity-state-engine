
namespace SampleGraphs {

    public class NewGameCtrl : GlobalStateController {

        public string gameSceneName;

        public void StartGame(int difficulty) {
            GameSessionManager.Instance.NewGame(difficulty);
            GameSessionManager.Instance.SetLevel(1);
            Leave(gameSceneName);
        }

    }

}
