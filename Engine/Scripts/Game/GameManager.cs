/*
If want to use Global data in game, uncomment the "GLOBAL_IN_GAME" blocks.
*/

//TODO: MAKE SURE THE NAME OF THE SCRIPT DOESN'T CAUSE PROBLEMS
//=> "GameManager" is a Unity reserved name...

public class GameManager : IManager {

    public GameStateManager gameStateManager;
    public IGameDataManager gameDataManager;
//////// GLOBAL_IN_GAME - BEGIN
    public bool useGlobalDataManager = false;
    public IGlobalDataManager globalDataManager;
//////// GLOBAL_IN_GAME - END


//////// GLOBAL_IN_GAME - BEGIN
    protected override void InitDataManager(IStateManager stateManager) {
        if (useGlobalDataManager) {
            GameStateManager gameStateManager_local = (GameStateManager)stateManager;
            if (gameStateManager_local.GetGlobalDataManager() == null) {
                IGlobalDataManager globalDataManager = InstantiateGlobalDataManager();
                gameStateManager_local.SetGlobalDataManager(globalDataManager);
//? GlobalSessionManager.Init();
                gameStateManager_local.GetGlobalDataManager().Load();
//? GameSessionManager.Load();
            }
        }
        base.InitDataManager(stateManager);
    }
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

//////// GLOBAL_IN_GAME - BEGIN
    protected IGlobalDataManager InstantiateGlobalDataManager() {
        return (useGlobalDataManager ? Instantiate(globalDataManager) : null);
    }
//////// GLOBAL_IN_GAME - END

}
