using UnityEngine;

public class Menu : GlobalStateController
{
    public string continueScene = "Map";


    public void NewGame()
    {
        ChangeState(GlobalStateId.NEW_GAME);
    }

    public void LoadGame()
    {
        ChangeState(GlobalStateId.LOAD_GAME);
    }

    public void Continue()
    {
        Leave(continueScene);
    }

    public void Options()
    {
        ChangeState(GlobalStateId.OPTIONS);
    }

    public void Credits()
    {
        ChangeState(GlobalStateId.CREDITS);
    }

}
