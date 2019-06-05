using UnityEngine;

public class GlobalSessionManager : ISessionManager {

//TODO: change name!?
//!    const string OPTIONS_FILE = "OPTIONS";


    protected static GlobalSessionManager instance = null;

    public static GlobalSessionManager Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject("GlobalSessionManager");
                instance = go.AddComponent<GlobalSessionManager>();
                DontDestroyOnLoad(instance);
//TODO: change name!?
//TODO: define elsewhere!?

                instance.SetFile("OPTIONS");
//TODO: call Init? Load?
            }
            return instance;
        }
    }

    public static bool IsInstance() {
        return (instance != null);
    }


    public void Init() {
        if (!Load()) {
//TODO: default config file
/*
            // load default options from config file
            OptionsData optionsData = new OptionsData();
            OptionsData.OptionsValues options = optionsData.GetValues();
            foreach (var pair in options.fields) {
                string key = pair.Key;
                int value = pair.Value;
                SetField(key, value);
            }
*/
            Save();
        }
    }

}
