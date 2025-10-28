using StateEngine;

namespace SampleGraphs {

    public class ContinueCtrl : GameStateController {

        public int NbContinues { get; private set; } = 0;

        public override void HandleMainState() {
            NbContinues = GetGameData().GetContinues();
        }

        public void Continue() {
            NbContinues = GetGameData().LoseContinue();
            End();
        }

        public void Stop() {
            LoadChildState("QUIT_GAME");
        }

    }

}
