
namespace SampleData {

    public class GameEndCtrl : GameStateController {

        public void Stop() {
            Leave("Menu");
        }

    }

}