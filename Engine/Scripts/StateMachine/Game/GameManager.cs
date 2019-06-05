/*
If want to use Global data in game, uncomment the "GLOBAL_IN_GAME" blocks.
*/

//TODO: MAKE SURE THE NAME OF THE SCRIPT DOESN'T CAUSE PROBLEMS
//=> "GameManager" is a Unity reserved name...

public class GameManager : IManager {

    public GameStateManager gameStateManager;
    public IGameDataManager gameDataManager;
//////// GLOBAL_IN_GAME - BEGIN
//    public IGlobalDataManager globalDataManager;
//////// GLOBAL_IN_GAME - END


//////// GLOBAL_IN_GAME - BEGIN
/*
    protected override void InitDataManager(IStateManager stateManager) {
        if (stateManager.GetGlobalDataManager() == null) {
            IDataManager dataManager = InstantiateGlobalDataManager();
            stateManager.SetGlobalDataManager(dataManager);
//? GlobalSessionManager.Init();
            stateManager.GetGlobalDataManager().Load();
//? GameSessionManager.Load();
        }

        base.InitDataManager(stateManager);
    }
*/
//////// GLOBAL_IN_GAME - END

    protected override void InstantiateStateManager() {
        Instantiate(gameStateManager);
    }

    protected override IStateManager GetStateManager() {
        return GameStateManager.Instance;
    }

    protected override IDataManager InstantiateDataManager() {
        return Instantiate(gameDataManager);
    }


}
