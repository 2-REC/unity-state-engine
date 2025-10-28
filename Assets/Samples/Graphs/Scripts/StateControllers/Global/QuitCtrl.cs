using StateEngine;

namespace SampleGraphs {

    public class QuitCtrl : GlobalStateController {

        public void Quit() {
            Leave();
        }

    }

}
