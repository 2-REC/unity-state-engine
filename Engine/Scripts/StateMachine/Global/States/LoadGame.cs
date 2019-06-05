public class LoadGame : GlobalStateController {

    public void StartGame(string filename) {
        if(GameSessionManager.LoadGame(filename)) {
            Leave();
        }
    }

}
