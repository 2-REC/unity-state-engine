
namespace SampleGraphs {

    public class GameOverCtrl : GameStateController {

        public bool CanContinue { get; private set; } = false;

        public override void HandleMainState() {
            CanContinue = GetGameData().CanContinue();
        }

        public void Continue() {
            if (CanContinue) {
                End();
            } else {
                LoadChildState("QUIT_GAME");
            }
        }

    }

}
