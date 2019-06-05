public class LoadGame : GlobalStateController {

    public void StartGame(string filename) {
        if(GameSessionManager.Instance.LoadGame(filename)) {
            Leave();
        }
    }

}
