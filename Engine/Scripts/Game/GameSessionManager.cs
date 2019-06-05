using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : ISessionManager {

//TODO: change name!?
//!    const string GAME_FILE = "INGAME";


    protected static GameSessionManager instance = null;

    public static GameSessionManager Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject("GameSessionManager");
                instance = go.AddComponent<GameSessionManager>();
                DontDestroyOnLoad(instance);
//TODO: change name!?
//TODO: define elsewhere!?
                instance.SetFile("INGAME");
//TODO: call Init? Load?
            }
            return instance;
        }
    }

    public static bool IsInstance() {
        return (instance != null);
    }


    public void NewGame(int difficulty) {
        Clear();

        // load difficulty values from config file
        DifficultyData difficultyData = new DifficultyData();
        DifficultyData.DifficultyValues diff = difficultyData.GetValues(difficulty);

        SetDifficulty(difficulty);

        SetInitialField("LIVES", diff.lives);
        SetField("LIVES", diff.lives);
        SetField("CONTINUES", diff.continues);
        // no need for "INITIAL_CONTINUES"

        foreach (KeyValuePair<string, int> pair in diff.fields) {
            string key = pair.Key;
            int value = pair.Value;

            SetInitialField(key, value);
            SetField(key, value);
        }

        SetLevel(-1);

        Save();
    }

    public int GetInitialField(string name, int defaultValue) {
        return GetField("INITIAL_" + name, defaultValue);
    }

    public int GetInitialField(string name) {
        return GetInitialField(name, 0);
    }

    private void SetInitialField(string name, int value) {
        SetField("INITIAL_" + name, value);
    }

    public int GetLevel() {
        return GetField("LEVEL", -1);
    }

    public void SetLevel(int level) {
        SetField("LEVEL", level);

    }

    public int GetDifficulty() {
        return GetField("DIFFICULTY", -1);
    }

    private void SetDifficulty(int difficulty) {
        SetField("DIFFICULTY", difficulty);
    }

    public int GetInitialLives() {
        return GetField("INITIAL_LIVES", -1); // -1?
    }

    public int GetLives() {
        return GetField("LIVES", -1); // -1?
    }

    public void SetLives(int lives) {
        SetField("LIVES", lives);
    }

    public int GetContinues() {
        return GetField("CONTINUES", -1); // -1?
    }

    public void SetContinues(int continues) {
        SetField("CONTINUES", continues);
    }

    public bool IsLevelCompleted(int level) {
        return (GetField("LEVEL_" + level, -1) != -1);
    }

    public void SetLevelCompleted(int level) {
        SetField("LEVEL_" + level, 1);
    }

    public void SaveGame(string filename) {
        FileManager.Save(filename, fields);
    }

    public bool LoadGame(string filename) {
        Dictionary<string, int> newFields = new Dictionary<string, int>();
        bool loaded = FileManager.Load(filename, newFields);
        if (loaded) {
            fields = newFields;
            Save();
        }
        return loaded;
    }

}
