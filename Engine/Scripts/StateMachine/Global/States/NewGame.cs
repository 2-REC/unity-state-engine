public class NewGame : GlobalStateController {

    public void StartGame(int difficulty) {
        GameSessionManager.Instance.NewGame(difficulty);
        Leave();
    }

}
