/*
  To use the "GlobalDataManager" in the Game Graph:
  - "GameManager": uncomment the associated code blocks
  - "GameStateController": uncomment the accessors & declaration
  - in each scene (in Game Graph): add the "GlobalData" prefab to "GameManager" object
*/

//TODO: this class is currently useless (derived classes could directly use base class as parent)
//=> Might change...

public abstract class IGlobalDataManager : IDataManager {

    // Get global data
    protected override void LoadData() {
//TODO: OK HERE?
GlobalSessionManager.Instance.Init();

//        GlobalSessionManager.Instance.Get...();
//...

        LoadSpecifics();

//TODO: OK HERE?
GameSessionManager.Instance.Load();
    }

    // Save global data
    public override void CommitChanges() {
//        GlobalSessionManager.Instance.Set...(...);
//...

        CommitChangesSpecifics();

//        GlobalSessionManager.Save();
        GlobalSessionManager.Instance.Save();
    }

//TODO: needed?
/*
    public override void Leave() {
    }
*/

    protected abstract void LoadSpecifics();
    protected abstract void CommitChangesSpecifics();

}
