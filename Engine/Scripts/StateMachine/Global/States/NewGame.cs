public class NewGame : GlobalStateController {

    public void StartGame(int difficulty) {
        GameSessionManager.NewGame(difficulty);
        Leave();
    }

}
