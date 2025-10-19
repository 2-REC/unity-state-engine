
namespace SampleData {

    public class GameOverCtrl : GameStateController {

        public string leaveSceneName;


        public bool CanContinue { get; private set; } = false;
        public int NbContinues { get; private set; } = 0;

        public override void HandleMainState() {
            CanContinue = GetGameData().CanContinue();
            NbContinues = GetGameData().GetContinues();
        }

        public void Continue() {
            if (CanContinue) {
                GetGameData().LoseContinue();
                End();
            }
        }

        public void Stop() {
            Leave(leaveSceneName);
        }

    }

}
