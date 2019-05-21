using UnityEngine;
using System.Collections;

/*
  To use the "GlobalDataManager" in the Game Graph:
  - "GameManager": uncomment the associated code blocks
  - "GameStateController": uncomment the accessors & declaration
  - in each scene (in Game Graph): add the "GlobalData" prefab to "GameManager" object
*/

public abstract class GlobalDataManager : MonoBehaviour {

/*
!!!! TODO: add required/desired fields/methods !!!!
*/

    private bool loaded = false;


    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Load() {
        if (loaded) {
            return;
        }

// TODO: read PlayerPrefs into variables
//...

        LoadSpecifics();

        loaded = true;
    }

    public void CommitChanges() {
        // update global values
//        GlobalSessionManager.Set...(...);
//...

        CommitChangesSpecifics();

        GlobalSessionManager.Save();
    }


    // load the global specific data
    protected abstract void LoadSpecifics();
    // save the global specific data
    protected abstract void CommitChangesSpecifics();

}
