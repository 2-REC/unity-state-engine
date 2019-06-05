/*
TODO: move to doc
- isLevel: if "true", the scene to load depends on the "level" number
    ! - only 1 element can have this property set to "true"!
- restartable: if "true" and have children, will go to "next" state when coming back from a children state
*/

public class GameState : State {

    public bool IsLevel { get; private set; }


    public GameState(int id, string scene, int next)
            : base(id, scene, next) {
        IsLevel = false;
    }

    public void SetIsLevel(bool isLevel) {
        IsLevel = isLevel;
    }

}
