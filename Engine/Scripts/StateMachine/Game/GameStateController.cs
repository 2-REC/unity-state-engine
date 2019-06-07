public class GameStateController : IStateController {

    protected override IStateManager GetStateManager() {
        return GameStateManager.Instance;
    }

    public IGameDataManager GetGameData() {
        return (IGameDataManager)stateManager.GetDataManager();
    }

}
