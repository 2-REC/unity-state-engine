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

- [ ] Add symbolic links script for Linux ("set_links")

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
