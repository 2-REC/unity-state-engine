
namespace SampleGraphs {

    public class SuccessUI : ICtrlUI {

        protected override void SetStatusText() {
            statusText.text = ((SuccessCtrl)controller).GameEnd ? "Game end reached.\nCongratulations!" : "Next level awaits.\nKeep going!";
        }
    }

}
