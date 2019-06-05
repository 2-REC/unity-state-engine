using UnityEngine;

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

//            ChangeState(GameStateId.END_ANIM);
//            ChangeState(StateId.Id("END_ANIM"));
LoadChildState("END_ANIM");
        }
        else {
            //Debug.Log("FAIL");

            GetGameData().LoseLife();
            EndProcessLose();
            GetGameData().CommitChanges();

//            ChangeState(GameStateId.END_ANIM_FAIL);
//            ChangeState(StateId.Id("END_ANIM_FAIL"));
LoadChildState("END_ANIM_FAIL");
        }
    }

    public void Save(string filename) {
        SaveProcess();
GetGameData().CommitChanges();
GameSessionManager.SaveGame(filename);
    }

// !!!! TODO: should also have a "QuitLevel" (& return to Map) !!!!
    public void QuitGame() {
//TODO: can be added as child?	
//        ChangeState(GameStateId.QUIT);
//        ChangeState(StateId.Id("QUIT"));
LoadChildState("QUIT");
    }


    // Level initialisations
    protected abstract void InitGame();
    // Update Game Data that should be saved when finishing the level (win or fail)
    protected abstract void EndProcess();
    // Update Game Data that should be saved when winning the level.
    protected virtual void EndProcessWin() {}
    // Update Game Data that should be saved when losing the level.
    protected virtual void EndProcessLose() {}
    // Update Game Data that should be saved when saving the level
    protected virtual void SaveProcess() {}

    public virtual void Pause() {}
    public virtual void Unpause() {}

}
