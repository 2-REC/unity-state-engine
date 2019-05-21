using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class GameDataManager : MonoBehaviour {

    private const string GRAPH_LEVELS = "Xml/levels";

    // list of all levels (Dictionary<int, LevelNode>)
    private Dictionary<int, LevelNode> levels;
    // list of available levels & their status (Dictionary<levelId, status>)
    private Dictionary<int, bool> availableLevels = new Dictionary<int, bool>();

    private int currentLevel = -1;
    private int lives = -1;
    private int continues = -1;

    private bool loaded = false;


    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Load() {
        if (loaded) {
            return;
        }

        currentLevel = GameSessionManager.GetLevel();
        Debug.Log("GameDataManager:Load - level: " + currentLevel);

        lives = GameSessionManager.GetLives();
        continues = GameSessionManager.GetContinues();

        LoadSpecifics();

        Debug.Log("GameDataManager:Load - lives: " + lives);
        Debug.Log("GameDataManager:Load - continues: " + continues);

        levels = GameLoader.LoadLevelGraph(GRAPH_LEVELS);

        foreach (KeyValuePair<int, LevelNode> level in levels) {
            if (GameSessionManager.IsLevelCompleted(level.Key)) {
                level.Value.completed = true;
            }
        }

/*
Debug.Log("levels:");
        foreach(KeyValuePair<int, LevelNode> levelNode in gameDataManager.levels) {
            LevelNode level = levelNode.Value;
            Debug.Log("level");
            Debug.Log("    id: " + level.id);
            Debug.Log("    scene: " + level.scene);

            if (level.next != null) {
                for (int j=0; j<level.next.Count; ++j) {
                    Debug.Log("    next: " + level.next[j]);
                }
            }
        }
*/

        SetLevels();

        loaded = true;
    }

    private void SetLevels() {
        foreach (KeyValuePair<int, LevelNode> level in levels) {
            if (level.Value.startup) {
                availableLevels.Add(level.Key, level.Value.completed);
                if (level.Value.completed) {
                    UpdateLevels(level.Key);
                }
            }
        }
    }

    private void UpdateLevels(int level) {
        List<int> next = levels[level].next;
        if (next != null) {
            for (int i = 0; i < next.Count; ++i) {
                if (!availableLevels.ContainsKey(next[i])) {
                    availableLevels.Add(next[i], levels[next[i]].completed);
                    if (levels[next[i]].completed) {
                        UpdateLevels(next[i]);
                    }
                }
            }
        }
    }

    public bool IsLevelCompleted(int level) {
        if (availableLevels.ContainsKey(level)) {
            return availableLevels[level];
        }
// !!!! ???? TODO: true or false ? ???? !!!!
        return false;
    }

    public void SetLevelCompleted() {
        if (currentLevel != -1) {
            levels[currentLevel].completed = true;
            availableLevels[currentLevel] = true;
            UpdateLevels(currentLevel);
        }
    }

    public void Leave() {
        if (currentLevel == -1) {
            GameSessionManager.Clear();
        }
        else {
            CommitChanges();
        }

        loaded = false;
        availableLevels.Clear();
    }

    public int GetLives() {
        return lives;
    }

    public int LoseLife() {
        lives -= 1;

        ResetLifeData();

        return lives;
    }

    public int GetContinues() {
        return continues;
    }

    public int LoseContinue() {
        continues -= 1;
        lives = GameSessionManager.GetInitialLives();

        ResetContinueData();

        return continues;
    }


    public int GetLevel() {
        return currentLevel;
    }

    public void SetLevel(int level) {
        currentLevel = level;
    }

    public LevelNode GetLevelNode() {
        if (currentLevel != -1) {
            return levels[currentLevel];
        }
        return null;
    }

    public string GetSceneName() {
        if (currentLevel != -1) {
            return levels[currentLevel].scene;
        }
        return null;
    }

    public string GetLevelName() {
        if (currentLevel != -1) {
            return levels[currentLevel].name;
        }
        return null;
    }

    public Dictionary<int, bool> GetAvailableLevels() {
        return availableLevels;
    }

    public bool IsGameOver() {
        return (lives <= 0);
    }

// !!!! ???? TODO: why? ???? !!!!
// CHECK THIS!
    public bool IsGameComplete() {
        return (continues <= 0);
    }

    public bool CanContinue() {
        return (continues > 0);
    }

    public void CommitChanges() {
        // update game values
        GameSessionManager.SetLives(lives);
        GameSessionManager.SetContinues(continues);


        CommitChangesSpecifics();

        GameSessionManager.SetLevel(currentLevel);

        foreach (KeyValuePair<int, LevelNode> level in levels) {
            if (level.Value.completed) {
                GameSessionManager.SetLevelCompleted(level.Key);
            }
        }
        GameSessionManager.Save();
    }


    // load the game specific data
    protected abstract void LoadSpecifics();
    // save the game specific data
    protected abstract void CommitChangesSpecifics();
    // data to be set when losing a life
    protected abstract void ResetLifeData();
    // data to be set when losing a continue
    protected abstract void ResetContinueData();

}
