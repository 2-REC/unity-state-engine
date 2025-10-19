using StateEngine;
using System.Collections.Generic;

namespace SampleLoadSave {

    public class LevelCtrl : GameStateController {

        public string LevelName { get; private set; }
        public int Lives { get; private set; }
        public int Health { get; private set; }
        public int Points { get; private set; }

        private GameDataManager _gameDataManager;


        public override void HandleMainState() {
            _gameDataManager = (GameDataManager)GetGameData();

            LevelName = _gameDataManager.GetLevelName();

            Lives = _gameDataManager.GetLives();
            Health = _gameDataManager.Health;
            Points = _gameDataManager.Points;
        }

        public void EndLevelSuccess() {
            _gameDataManager.SetLevelCompleted();
            _gameDataManager.CommitChanges();

            if (_gameDataManager.IsGameComplete()) {
                LoadChildState("GAME_END");
            } else {
                List<int> nextLevels = GetGameData().GetNextLevels(_gameDataManager.GetLevel());
                // TODO: IF NOT => ERROR!
                if (nextLevels.Count == 1) {
                    _gameDataManager.SetLevel(nextLevels[0]);
                }

                End();
            }
        }

        public void EndLevelFailure() {
            _gameDataManager.LoseLife();

            if (_gameDataManager.IsGameOver()) {
                LoadChildState("GAME_OVER");
            } else {
                End();
            }
        }

        public void LoseHealth(int damage) {
            Health -= damage;
            _gameDataManager.Health = Health;
            if (Health <= 0) {
                EndLevelFailure();
                return;
            }
        }

        public void AddPoints(int points) {
            Points += points;
            _gameDataManager.Points = Points;
        }

        public void SaveGame(string filename) {
            GetGameData().SaveGame(filename);
        }

        public void Quit(string sceneName) {
            Leave(sceneName);
        }

    }

}
