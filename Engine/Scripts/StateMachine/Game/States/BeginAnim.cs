public class BeginAnim : GameStateController {

//TODO: Make class generic for other states & handle an animation object (+ add an Editor script)
    public string defaultAnim;

    private string anim;

    public override void HandleMainState() {
        //Debug.Log("BeginAnim:HandleMainState - currentState: " + gameStateManager.currentStateId);

        anim = GetGameData().GetLevelNode().BeginAnim;
        //Debug.Log("BeginAnim:Load - beginAnim: " + anim);
        if ((anim == null) || "".Equals(anim)) {
            anim = defaultAnim;
        }
    }

    public string getAnim() {
        return anim;
    }

}
