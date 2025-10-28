
namespace SampleGraphs {
    public class LevelUI : ICtrlUI {

        protected override void SetStatusText() {
            statusText.text = $"Lives: {((LevelCtrl)controller).Lives}";
        }

    }

}
