public class Menu : GlobalStateController {

    public void NewGame() {
        LoadChildState("NEW_GAME");
    }

    public void LoadGame() {
        LoadChildState("LOAD_GAME");
    }

//TODO: remove this method, and call directly "Leave" (as it is public)
    public void Continue() {
        Leave();
    }

    public void Options() {
        LoadChildState("OPTIONS");
    }

    public void Credits() {
        LoadChildState("CREDITS");
    }

}
