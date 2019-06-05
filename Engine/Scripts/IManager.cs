using UnityEngine;

public abstract class IManager : MonoBehaviour {

    void Awake() {
        if (!IStateManager.IsInstance()) {
            InstantiateStateManager();
        }

/*
        if (SoundManager.instance == null) {
            Instantiate(soundManager);
        }
*/

        IStateManager stateManager = GetStateManager();
        InitDataManager(stateManager);
    }

    protected virtual void InitDataManager(IStateManager stateManager) {
        if (stateManager.GetDataManager() == null) {
            IDataManager dataManager = InstantiateDataManager();
            stateManager.SetDataManager(dataManager);
//? GlobalSessionManager.Init();
            stateManager.GetDataManager().Load();
//? GameSessionManager.Load();
        }
    }


    protected abstract void InstantiateStateManager();
    protected abstract IStateManager GetStateManager();
    protected abstract IDataManager InstantiateDataManager();

}
