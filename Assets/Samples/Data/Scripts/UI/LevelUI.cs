using UnityEngine.UI;

namespace SampleData {

    public class LevelUI : ICtrlUI {

        public Text descriptionText;
        public Text livesText;
        public Text healthText;
        public Text pointsText;

        public int damage;
        public int points;


        protected override void SetStatusText() {
            statusText.text = ((LevelCtrl)controller).LevelName;
            descriptionText.text = ((LevelCtrl)controller).LevelDescription;

            livesText.text = $"Lives: {((LevelCtrl)controller).Lives}";
            healthText.text = $"Health: {((LevelCtrl)controller).Health}";
            pointsText.text = $"Points: {((LevelCtrl)controller).Points}";
        }

        public void Win() {
            ((LevelCtrl)controller).EndLevelSuccess();
        }

        public void Lose() {
            ((LevelCtrl)controller).EndLevelFailure();
            livesText.text = $"Lives: {((LevelCtrl)controller).Lives}";
        }

        public void LoseHealth() {
            ((LevelCtrl)controller).LoseHealth(damage);
            healthText.text = $"Health: {((LevelCtrl)controller).Health}";
        }

        public void AddPoints() {
            ((LevelCtrl)controller).AddPoints(points);
            pointsText.text = $"Points: {((LevelCtrl)controller).Points}";
        }

    }

}
