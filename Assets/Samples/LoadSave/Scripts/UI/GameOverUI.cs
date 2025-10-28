
using UnityEngine.UI;

namespace SampleLoadSave {

    public class GameOverUI : ICtrlUI {

        public Text nbContinues;
        public Button yesButton;
        public Button noButton;
        public Button okButton;

        protected override void SetStatusText() {
            bool canContinue = ((GameOverCtrl)controller).CanContinue;
            statusText.text = canContinue ? "Continue?" : "No more continues...";

            if (canContinue) {
                nbContinues.gameObject.SetActive(true);
                nbContinues.text = $"Contines left: {((GameOverCtrl)controller).NbContinues}";

                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
            } else {
                nbContinues.gameObject.SetActive(false);

                okButton.gameObject.SetActive(true);
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
            }
        }

        public void Continue() {
            ((GameOverCtrl)controller).Continue();
            nbContinues.text = $"Contines left: {((GameOverCtrl)controller).NbContinues}";
        }

        public void Stop() {
            ((GameOverCtrl)controller).Stop();
        }

    }

}
