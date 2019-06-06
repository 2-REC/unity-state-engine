/*
  Implement the following methods:
  - StartLevel: Executed as soon as level begins playing
  - StopLevel: Executed as soon as level ends playing

  Additionally:
  - can ignore the delays at start & end if not desired ...
  - add an AudioSource and AudioClips for the desired level sounds.
  (or code inside "AUDIO" blocks can be removed)
  (!? - music could be handled here ...?)
*/

using UnityEngine;

public class LevelController : MonoBehaviour {

    public float startDelay = 2.0f;

    //////// GAME - BEGIN
    //// HUD - BEGIN
    // The link with the Hud controller can be done differently depending on the scene structure/hierarchy.
    public HudController hud;
    //// HUD - END

    //// AUDIO - BEGIN
    public AudioClip winLevelClip = null;
    public AudioClip loseLevelClip = null;
    //// AUDIO - MID

    //// AUDIO - BEGIN
    AudioSource gameAudio;
    //// AUDIO - MID
    //////// GAME - END


    void Awake() {
        //////// GAME - BEGIN
        //// AUDIO - BEGIN
        gameAudio = GetComponent<AudioSource>();
        //// AUDIO - MID
        //////// GAME - END
    }

    public float InitLevel() {
        float delay = startDelay;

        //////// GAME - BEGIN
        //// AUDIO - BEGIN
// !!!! ???? TODO: start music? ???? !!!!
        // ... levelMusicClip
        //// AUDIO - MID

        //// HUD - BEGIN
        if (hud != null) {
            float hudDelay = hud.Begin();
            if (hudDelay > delay) {
                delay = hudDelay;
            }
        }
        //// HUD - END
        //////// GAME - END

        return delay;
    }

    public float NotifyWin() {
        float delay = 0.0f;

        //////// GAME - BEGIN
        //// AUDIO - BEGIN
        if ((gameAudio != null) && (winLevelClip != null)) {
            gameAudio.clip = winLevelClip;
            gameAudio.Play();
            if (winLevelClip.length > delay) {
                delay = winLevelClip.length;
            }
        }
        //// AUDIO - MID

        //// HUD - BEGIN
        if (hud != null) {
            float hudDelay = hud.End(true);
            if (hudDelay > delay) {
                delay = hudDelay;
            }
        }
        //// HUD - END
        //////// GAME - END

        return delay;
    }

    public float NotifyLose() {
        float delay = 0.0f;

        //////// GAME - BEGIN
        //// AUDIO - BEGIN
        if ((gameAudio != null) && (loseLevelClip != null)) {
            gameAudio.clip = loseLevelClip;
            gameAudio.Play();
            if (loseLevelClip.length > delay) {
                delay = loseLevelClip.length;
            }
        }
        //// AUDIO - MID

        //// HUD - BEGIN
        if (hud != null) {
            float hudDelay = hud.End(false);
            if (hudDelay > delay) {
                delay = hudDelay;
            }
        }
        //// HUD - END
        //////// GAME - END

        return delay;
    }


    public void StartLevel() {
        //////// GAME - BEGIN
        //...
        //////// GAME - END
    }

    public void StopLevel() {
        //////// GAME - BEGIN
        //...
        //////// GAME - END
    }


    //////// GAME - BEGIN

    //// HUD - BEGIN
    public HudController getHudController() {
        return hud;
    }
    //// HUD - END

    //////// GAME - END

}
