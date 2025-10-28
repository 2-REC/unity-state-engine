using StateEngine;

namespace SampleGraphs {

    public class FailureCtrl : GameStateController {

        public bool GameOver { get; private set; } = false;

        public override void HandleMainState() {
            GameOver = GetGameData().IsGameOver();
        }

        public void Continue() {
            if (GameOver) {
                LoadChildState("GAME_OVER");
            } else {
                End();
            }
        }

    }

}
