using StateEngine;
using System.Collections.Generic;

namespace SampleData {

    public class LevelCtrl : GameStateController {

        public string LevelName { get; private set; }
        public string LevelDescription { get; private set; }
        public int Lives { get; private set; }
        public int Health { get; private set; }
        public int Points { get; private set; }

        private GameDataManager _gameDataManager;

        public override void HandleMainState() {
            _gameDataManager = (GameDataManager)GetGameData();

            LevelName = GetGameData().GetLevelName();
            LevelDescription = GetGameData().GetLevelNode().Data["description"];

            Lives = _gameDataManager.GetLives();
            Health = _gameDataManager.Health;
            Points = _gameDataManager.Points;
        }

        public void EndLevelSuccess() {
            GetGameData().SetLevelCompleted();
            GetGameData().CommitChanges();

            if (_gameDataManager.IsGameComplete()) {
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

        public void EndLevelFailure() {
            GetGameData().LoseLife();
            GetGameData().CommitChanges();

            if (_gameDataManager.IsGameOver()) {
                LoadChildState("GAME_OVER");
            } else {
                End();
            }
        }

        public void LoseHealth(int damage) {
            Health -= damage;
            ((GameDataManager)GetGameData()).Health = Health;
            if (Health <= 0) {
                EndLevelFailure();
                return;
            }
        }

        public void AddPoints(int points) {
            Points += points;
            ((GameDataManager)GetGameData()).Points = Points;
        }

    }

}
