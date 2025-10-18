using System.Collections.Generic;

namespace SampleGraphs {

    public class SuccessCtrl : GameStateController {

        public bool GameEnd { get; private set; } = false;

        public override void HandleMainState() {
            GameEnd = GetGameData().IsGameComplete();
        }

        public void Continue() {
            if (GameEnd) {
                LoadChildState("GAME_END");
            } else {
                List<int> nextLevels = GetGameData().GetNextLevels(GetGameData().GetLevel());
                // TODO: IF NOT => ERROR!
                if (nextLevels.Count == 1) {
                    GetGameData().SetLevel(nextLevels[0]);
                }

                End();
            }
        }

    }

}
