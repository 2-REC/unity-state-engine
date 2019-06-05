using System.Collections.Generic;

public class Map : GameStateController {

//TODO: use the whole LevelNode structures?
    List<int> levels;


    public override void InitState() {
        levels = new List<int>();
        GenerateMap();
    }

    public override void HandleMainState() {
//TODO: anything to do?
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
        LoadChildState("BEGIN_ANIM");
    }

    public List<int> GetLevels() {
        return levels;
    }


    public void QuitGame() {
        LoadChildState("QUIT");
    }

    public void SaveGame(string filename) {
        GameSessionManager.Instance.SaveGame(filename);
    }

}
