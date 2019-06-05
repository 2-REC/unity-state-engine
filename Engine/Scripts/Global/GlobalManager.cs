public class GlobalManager : IManager {

    public GlobalStateManager globalStateManager;
    public IGlobalDataManager globalDataManager;


    protected override void InstantiateStateManager() {
        Instantiate(globalStateManager);
    }

    protected override IStateManager GetStateManager() {
        return GlobalStateManager.Instance;
    }

    protected override IDataManager InstantiateDataManager() {
        return Instantiate(globalDataManager);
    }

}
