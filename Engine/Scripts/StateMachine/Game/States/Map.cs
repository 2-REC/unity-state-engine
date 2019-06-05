using System.Collections.Generic;

public class Map : GameStateController {

// !!!! ???? TODO: use the whole LevelNode structures? ???? !!!!
    List<int> levels;

    public override void InitState() {
        levels = new List<int>();
        GenerateMap();
    }

    public override void HandleMainState() {
//...
    }

    public void GenerateMap() {
        levels.Clear();

        foreach (KeyValuePair<int, bool> level in GetGameData().GetAvailableLevels()) {
            //Debug.Log("adding level: " + level.Key);
            levels.Add(level.Key);
        }
    }

    public void StartLevel(int level) {
        GetGameData().SetLevel(level);
//        ChangeState(GameStateId.BEGIN_ANIM);
//        ChangeState(StateId.Id("BEGIN_ANIM"));
LoadChildState("BEGIN_ANIM");
    }

    public List<int> GetLevels() {
        return levels;
    }


    public void QuitGame() {
//TODO: can be added as child?	
//        ChangeState(GameStateId.QUIT);
//        ChangeState(StateId.Id("QUIT"));
//TODO: "QUIT" is not a chid => usde Next? End? or accept other than children?
LoadChildState("QUIT");
    }

    public void SaveGame(string filename) {
        GameSessionManager.SaveGame(filename);
    }

}
