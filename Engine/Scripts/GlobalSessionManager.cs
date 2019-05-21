using System.Collections.Generic;
using UnityEngine;

// TODO: "GlobalSessionManager" & "GameSessionManager" could be derived from a common base abstract class.

public static class GlobalSessionManager
{
// !!!! TODO: change name !!!!
    const string OPTIONS_FILE = "OPTIONS";


    private static Dictionary<string, int> fields = new Dictionary<string, int>();


    public static void Clear()
    {
// !!!! TODO: should be saved somewhere else (crypted & unaccessible) !!!!
        FileManager.Delete(OPTIONS_FILE);
        fields.Clear();
    }

    public static void Init()
    {
        if (!Load())
        {
// !!!! TODO !!!!
//            // load default options from config file
//            OptionsData optionsData = new OptionsData();
//            OptionsData.OptionsValues options = optionsData.GetValues();
//            foreach (var pair in options.fields)
//            {
//                string key = pair.Key;
//                int value = pair.Value;
//                SetField(key, value);
//            }

            Save();
        }
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


    public static void Save()
    {
// !!!! TODO: should be saved somewhere else (crypted & unaccessible) !!!!
        FileManager.Save(OPTIONS_FILE, fields);
    }

    public static bool Load()
    {
        fields.Clear();
// !!!! TODO: should be saved somewhere else (crypted & unaccessible) !!!!
        return FileManager.Load(OPTIONS_FILE, fields);
    }

}
