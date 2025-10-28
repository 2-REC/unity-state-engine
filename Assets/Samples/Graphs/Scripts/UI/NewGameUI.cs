using UnityEngine;

namespace SampleGraphs {

    public class NewGameUI : MonoBehaviour {

        public NewGameCtrl ctrl;

        public void NewGameEasy() {
            ctrl.StartGame(0);
        }

        public void NewGameNormal() {
            ctrl.StartGame(1);
        }

        public void NewGameHard() {
            ctrl.StartGame(2);
        }

    }

}
