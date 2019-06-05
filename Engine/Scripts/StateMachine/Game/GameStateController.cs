public class GameStateController : IStateController {

    protected override IStateManager GetStateManager() {
        return GameStateManager.Instance;
    }

}
