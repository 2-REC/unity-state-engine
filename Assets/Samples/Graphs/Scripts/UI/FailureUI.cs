
namespace SampleGraphs {

    public class FailureUI : ICtrlUI {

        protected override void SetStatusText() {
            statusText.text = ((FailureCtrl)controller).GameOver ? "Game Over" : "Retry level";
        }
    }

}
