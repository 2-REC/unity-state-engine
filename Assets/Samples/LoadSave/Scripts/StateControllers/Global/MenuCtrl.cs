
namespace SampleLoadSave {

    public class MenuCtrl : GlobalStateController {

        public string gameSceneName;


        public void NewGame(int difficulty) {
            GameSessionManager.Instance.NewGame(difficulty);
            GameSessionManager.Instance.SetLevel(1);
            Leave(gameSceneName);
        }

        public bool CheckSavedGame(int slotNb) {
            return GameSessionManager.Instance.CheckExists($"_save{slotNb}");
        }

        public void LoadGame(int slotNb) {
            if (GameSessionManager.Instance.LoadGame($"_save{slotNb}"))
                Leave(gameSceneName);
        }

        public bool CanContinue() {
            return GameSessionManager.Instance.GetLevel() != -1;
        }

        public void Continue() {
            if (!CanContinue())
                return;

            Leave(gameSceneName);
        }

    }

}
