using StateEngine;

namespace SampleGraphs {

    public class GameQuitCtrl : GameStateController {

        public string gameSceneName;

        public void QuitGame() {
            Leave(gameSceneName);
        }

    }

}
