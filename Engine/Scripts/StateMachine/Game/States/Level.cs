public abstract class Level : GameStateController {

    protected int lives;


    public override void HandleMainState() {
        lives = GetGameData().GetLives();

        InitGame();
    }

    // To be called when ending the level
    protected void EndLevel(bool success) {
        EndProcess();

        if (success) {
            //Debug.Log("SUCCESS");

            GetGameData().SetLevelCompleted();
            EndProcessWin();
            GetGameData().CommitChanges();

            LoadChildState("END_ANIM");
        }
        else {
            //Debug.Log("FAIL");

            GetGameData().LoseLife();
            EndProcessLose();
            GetGameData().CommitChanges();

            LoadChildState("END_ANIM_FAIL");
        }
    }

    public void Save(string filename) {
        SaveProcess();
//TODO: OK here?
GetGameData().CommitChanges();
GameSessionManager.Instance.SaveGame(filename);
    }

//TODO: should also have a "QuitLevel" (& return to Map)
    public void QuitGame() {
        LoadChildState("QUIT");
    }


    public virtual void Pause() { }
    public virtual void Unpause() { }

    // Level initialisations
    protected abstract void InitGame();
    // Update Game Data to save when finishing the level (win or fail)
    protected abstract void EndProcess();
    // Update Game Data to save when winning the level.
    protected virtual void EndProcessWin() {}
    // Update Game Data to save when losing the level.
    protected virtual void EndProcessLose() {}
    // Update Game Data to save when saving the level
    protected virtual void SaveProcess() {}
}
