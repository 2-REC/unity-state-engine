public class EndAnimFail : GameStateController {

//TODO: Make class generic for other states & handle an animation object (+ add an Editor script)
    public string defaultAnim;

    private string anim;

    public override void HandleMainState() {
        //Debug.Log("EndAnimFail:HandleMainState - currentState: " + gameStateManager.currentStateId);

        anim = GetGameData().GetLevelNode().EndAnimFail;
        //Debug.Log("EndAnimFail:Load - endAnimFail: " + anim);
        if ((anim == null) || "".Equals(anim)) {
            anim = defaultAnim;
        }
    }

    public string getAnim() {
        return anim;
    }

}
