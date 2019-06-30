TODO: CHECK VALIDITY OF TODOS...


SCRIPTS:

- GENERAL:
  - [ ] Remove TODOs
  - [ ] Remove useless "using" in scripts
  - [ ] Identation
  - [ ] Coding style
  - [ ] Add/remove comments
  - [ ] Remove/comment logs
  - [ ] Unify variables/prefabs/resources names
  - [ ] ...?
<br>LATER:
  - [ ] add a graph editor for the state transitions
  - ...

- [ ] Add symbolic links script for Linux ("set_links")

- [ ] Might not be a good thing to call "PlayerPrefs" in "SessionManager.Reset", if want to keep data persistent through different games (eg: highest scores, etc.)

- [ ] "GameData.Leave" calls "SessionManager.Save" to save the data when leaving the game graph. This can be a problem as data is not saved before, so in case of crash (or voluntary "kill" of the application) the data will not be saved (which can be seen both as a bug or as a way to cheat) => OK to keep as is?

- [ ] Files in "Resources/Xml/" should not be common!
  => MOVE OUT OF ENGINE! (& set templates/examples)

- [ ] Serialize data for save/load & options/ingame files
  https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934

- [ ] Use binary files for save/load & options/ingame files
  => Look at:
  https://answers.unity.com/questions/1219643/how-to-store-game-variables.html

- [ ] "Fix" debriefing stuff: Need to determine when it's last level (MODIFY "IsGameComplete").
  => EITHER:
  - reach a level with no "next" (=null)
  - all the levels are "completed" (=true)

- [ ] Check why calling "CommitChanges" in "DebriefingFail", but not in "Debriefing".

?
- [ ] Add "Pause", "Unpause" & "Quit" to "Level.cs" (as abstract)

?
- [ ] "StartLevel" in "LevelManager" should override method from "Level"
  (=> Add abstract method in "Level.cs")

?
- [ ] Add "NotifyHit" in "Level.cs" (common?)
- [ ] Add "NotifyHeal" in "Level.cs" (common?)

- [ ] Add comments in "scripts/GameData" of V1

- [ ] "continueScene" should be enums instead of strings!!!!

- [ ] DOCUMENTATION
  - [ ] Make doc (use, objects/projects, tests, ...)
  - [ ] Write README.md
