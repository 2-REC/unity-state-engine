using UnityEngine;

public abstract class IDataManager : MonoBehaviour {

    protected bool loaded = false;


    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void Load() {
        if (loaded) {
            return;
        }

        LoadData();

        loaded = true;
    }

//OK?
    public virtual void Leave() {
        CommitChanges();

        loaded = false;
    }


    public abstract void CommitChanges();
    protected abstract void LoadData();

}
