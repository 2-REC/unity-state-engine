public class EndAnim : GameStateController {

//TODO: Make class generic for other states & handle an animation object (+ add an Editor script)
    public string defaultAnim;

    private string anim;

    public override void HandleMainState() {
        //Debug.Log("EndAnim:HandleMainState - currentState: " + gameStateManager.currentStateId);

        anim = GetGameData().GetLevelNode().EndAnim;
        //Debug.Log("EndAnim:Load - endAnim: " + anim);
        if ((anim == null) || "".Equals(anim)) {
            anim = defaultAnim;
        }
    }

    public string getAnim() {
        return anim;
    }

}
