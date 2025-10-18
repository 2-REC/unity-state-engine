
namespace SampleGraphs {

    public class ContinueUI : ICtrlUI {

        protected override void SetStatusText() {
            statusText.text = "Continues left: " + ((ContinueCtrl)controller).NbContinues.ToString();
        }

        public void Yes() {
            ((ContinueCtrl)controller).Continue();
            statusText.text = "Continues left: " + ((ContinueCtrl)controller).NbContinues.ToString();
        }

        public void No() {
            ((ContinueCtrl)controller).Stop();
        }

    }

}
