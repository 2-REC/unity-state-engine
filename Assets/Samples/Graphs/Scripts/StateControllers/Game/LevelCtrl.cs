
namespace SampleGraphs {

    public class LevelCtrl : GameStateController {

        public int Lives { get; private set; }

        public override void HandleMainState() {
            Lives = GetGameData().GetLives();
        }

        public void EndLevelSuccess() {
            GetGameData().SetLevelCompleted();
            GetGameData().CommitChanges();

            LoadChildState("SUCCESS");
        }

        public void EndLevelFailure() {
            GetGameData().LoseLife();
            GetGameData().CommitChanges();

            LoadChildState("FAILURE");
        }

    }

}
