
namespace SampleGraphs {

    public class GameOverUI : ICtrlUI {

        protected override void SetStatusText() {
            statusText.text = ((GameOverCtrl)controller).CanContinue ? "Can continue..." : "No more continues...";
        }
    }

}
