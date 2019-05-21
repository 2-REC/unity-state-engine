using System.Collections.Generic;
using UnityEngine;

// TODO: "GlobalSessionManager" & "GameSessionManager" could be derived from a common base abstract class.

public static class GameSessionManager
{
// !!!! TODO: change name !!!!
    const string GAME_FILE = "INGAME";


    private static Dictionary<string, int> fields = new Dictionary<string, int>();


    public static void Clear()
    {
// !!!! TODO: should be saved somewhere else (crypted & unaccessible) !!!!
        FileManager.Delete(GAME_FILE);
        fields.Clear();
    }

    public static void NewGame(int difficulty)
    {
        Clear();

        // load difficulty values from config file
        DifficultyData difficultyData = new DifficultyData();
        DifficultyData.DifficultyValues diff = difficultyData.GetValues(difficulty);

        SetDifficulty(difficulty);

        SetInitialField("LIVES", diff.lives);
        SetField("LIVES", diff.lives);
        SetField("CONTINUES", diff.continues);
        // no need for "INITIAL_CONTINUES"

        foreach (KeyValuePair<string, int> pair in diff.fields)
        {
            string key = pair.Key;
            int value = pair.Value;

            SetInitialField(key, value);
            SetField(key, value);
        }

        SetLevel(-1);

        Save();
    }

    public static int GetInitialField(string name, int defaultValue)
    {
        return GetField("INITIAL_" + name, defaultValue);
    }

    public static int GetInitialField(string name)
    {
        return GetInitialField(name, 0);
    }

    private static void SetInitialField(string name, int value)
    {
        SetField("INITIAL_" + name, value);
    }

    public static int GetField(string name, int defaultValue)
    {
        int value;
        if (fields.TryGetValue(name, out value))
        {
            return value;
        }
        return defaultValue;
    }

    public static int GetField(string name)
    {
        return GetField(name, 0);
    }

    public static void SetField(string name, int value)
    {
        fields[name] = value;
    }


    public static int GetLevel()
    {
        return GetField("LEVEL", -1);
    }

    public static void SetLevel(int level)
    {
        SetField("LEVEL", level);

    }

    public static int GetDifficulty()
    {
        return GetField("DIFFICULTY", -1);
    }

    private static void SetDifficulty(int difficulty)
    {
        SetField("DIFFICULTY", difficulty);
    }

    public static int GetInitialLives()
    {
        return GetField("INITIAL_LIVES", -1); // -1?
    }

    public static int GetLives()
    {
        return GetField("LIVES", -1); // -1?
    }

    public static void SetLives(int lives)
    {
        SetField("LIVES", lives);
    }

    public static int GetContinues()
    {
        return GetField("CONTINUES", -1); // -1?
    }

    public static void SetContinues(int continues)
    {
        SetField("CONTINUES", continues);
    }

    public static bool IsLevelCompleted(int level)
    {
        return (GetField("LEVEL_" + level, -1) != -1);
    }

    public static void SetLevelCompleted(int level)
    {
        SetField("LEVEL_" + level, 1);
    }

    public static void Save()
    {
// !!!! TODO: should be saved somewhere else (crypted & unaccessible) !!!!
        FileManager.Save(GAME_FILE, fields);
    }

    public static void SaveGame(string filename)
    {
        FileManager.Save(filename, fields);
    }

    public static void Load()
    {
        fields.Clear();
// !!!! TODO: should be saved somewhere else (crypted & unaccessible) !!!!
        FileManager.Load(GAME_FILE, fields);
    }

    public static bool LoadGame(string filename)
    {
        Dictionary<string, int> newFields = new Dictionary<string, int>();
        bool loaded = FileManager.Load(filename, newFields);
        if (loaded) {
            fields = newFields;
            Save();
        }
        return loaded;
    }

}
