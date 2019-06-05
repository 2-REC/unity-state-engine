/*
ALL STUFF RELATED TO GAME (DIFFICULTY, LEVEL, BEGIN ANIM, ETC)
SHOULDN'T BE HANDLED IN STATE MANAGERS BUT IN OTHER SCRIPTS/OBJECTS
    => other objects just have to call "End" when done with scene...
    => StateControllers are simpler: only have to call "End" - or "ChangeState" or "Leave" for specific states
    => states without children can use this script
    => states with children can derive this class and call "ChangeState"
    => states leaving the graph can derive this class and call "Leave"
*/
public class GlobalStateController : IStateController {

    protected override IStateManager GetStateManager() {
        return GlobalStateManager.Instance;
    }

}
