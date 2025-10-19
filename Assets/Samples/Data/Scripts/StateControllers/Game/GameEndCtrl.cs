
namespace SampleData {

    public class GameEndCtrl : GameStateController {

        public string leaveSceneName;


        public void Stop() {
            Leave(leaveSceneName);
        }

    }

}
