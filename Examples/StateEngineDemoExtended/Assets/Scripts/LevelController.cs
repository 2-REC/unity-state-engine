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

    // The link with the Hud controller can be done differently depending on the scene structure/hierarchy.
    public HudController hud;
    public float startDelay = 2.0f;

    //// AUDIO - BEGIN
    public AudioClip winLevelClip = null;
    public AudioClip loseLevelClip = null;
    //// AUDIO - MID

    //// AUDIO - BEGIN
    AudioSource gameAudio;
    //// AUDIO - MID


    void Awake() {
        //// AUDIO - BEGIN
        gameAudio = GetComponent<AudioSource>();
        //// AUDIO - MID
    }

    public float InitLevel() {
        float delay = startDelay;

        //// AUDIO - BEGIN
// !!!! ???? TODO: start music? ???? !!!!
        // ... levelMusicClip
        //// AUDIO - MID

        if (hud != null) {
            float hudDelay = hud.Begin();
            if (hudDelay > delay) {
                delay = hudDelay;
            }
        }

        return delay;
    }

    public float NotifyWin() {
        float delay = 0.0f;

        //// AUDIO - BEGIN
        if ((gameAudio != null) && (winLevelClip != null)) {
            gameAudio.clip = winLevelClip;
            gameAudio.Play();
            if (winLevelClip.length > delay) {
                delay = winLevelClip.length;
            }
        }
        //// AUDIO - MID

        if (hud != null) {
            float hudDelay = hud.End(true);
            if (hudDelay > delay) {
                delay = hudDelay;
            }
        }

        return delay;
    }

    public float NotifyLose() {
        float delay = 0.0f;

        //// AUDIO - BEGIN
        if ((gameAudio != null) && (loseLevelClip != null)) {
            gameAudio.clip = loseLevelClip;
            gameAudio.Play();
            if (loseLevelClip.length > delay) {
                delay = loseLevelClip.length;
            }
        }
        //// AUDIO - MID

        if (hud != null) {
            float hudDelay = hud.End(false);
            if (hudDelay > delay) {
                delay = hudDelay;
            }
        }

        return delay;
    }

    public HudController getHudController() {
        return hud;
    }


    public void StartLevel() {
        //...
    }

    public void StopLevel() {
        //...
    }

}
