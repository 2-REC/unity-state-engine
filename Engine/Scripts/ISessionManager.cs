using System.Collections.Generic;
using UnityEngine;

public abstract class ISessionManager : MonoBehaviour {

    private string DATA_FILE;

    protected Dictionary<string, int> fields;


    protected ISessionManager() {
        fields = new Dictionary<string, int>();
    }

    protected void SetFile(string filename) {
        DATA_FILE = filename;
    }

    public void Clear() {
//TODO: should be saved somewhere else (crypted & inaccessible)!
        FileManager.Delete(DATA_FILE);
        fields.Clear();
    }

    public int GetField(string name, int defaultValue) {
        if (fields.TryGetValue(name, out int value)) {
            return value;
        }
        return defaultValue;
    }

    public int GetField(string name) {
        return GetField(name, 0);
    }

    public void SetField(string name, int value) {
        fields[name] = value;
    }

    public void Save() {
//TODO: should be saved somewhere else (crypted & inaccessible)!
        FileManager.Save(DATA_FILE, fields);
    }

    public bool Load() {
        fields.Clear();
//TODO: should be saved somewhere else (crypted & inaccessible)!
        return FileManager.Load(DATA_FILE, fields);
    }

}
