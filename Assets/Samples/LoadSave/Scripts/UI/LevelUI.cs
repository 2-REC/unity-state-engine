using UnityEngine;
using UnityEngine.UI;

namespace SampleLoadSave {

    public class LevelUI : ICtrlUI {

        public Text livesText;
        public Text healthText;
        public Text pointsText;

        public GameObject savePanel;
        public Text savedGameText;

        public string exitScene;
        public int damage;
        public int points;


        protected override void SetStatusText() {
            statusText.text = ((LevelCtrl)controller).LevelName;

            livesText.text = $"Lives: {((LevelCtrl)controller).Lives}";
            healthText.text = $"Health: {((LevelCtrl)controller).Health}";
            pointsText.text = $"Points: {((LevelCtrl)controller).Points}";

            savePanel.SetActive(false);
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

        public void SavePanelOpen() {
            savedGameText.gameObject.SetActive(false);
            savePanel.SetActive(true);
        }

        public void SavePanelClose() {
            savePanel.SetActive(false);
        }

        public void Quit() {
            ((LevelCtrl)controller).Quit(exitScene);
        }

        public void SaveSlot(int slotNb) {
            ((LevelCtrl)controller).SaveGame($"_save{slotNb}");

            savedGameText.text = $"Game saved to slot {slotNb}";
            savedGameText.gameObject.SetActive(true);
        }

    }

}
