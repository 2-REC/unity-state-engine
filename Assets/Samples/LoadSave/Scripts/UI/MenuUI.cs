using UnityEngine;
using UnityEngine.UI;

namespace SampleLoadSave {

    public class MenuUI : MonoBehaviour {

        public MenuCtrl controller;

        public GameObject continueButton;
        public GameObject newGamePanel;
        public GameObject loadGamePanel;
        public Button[] loadSlotButtons;


        public void Start() {
            continueButton.SetActive(controller.CanContinue());
            newGamePanel.SetActive(false);
            loadGamePanel.SetActive(false);
        }

        public void NewGameMenuOpen() {
            newGamePanel.SetActive(true);
        }

        public void NewGameMenuClose() {
            newGamePanel.SetActive(false);
        }

        public void LoadGameMenuOpen() {
            for (int i = 0; i < loadSlotButtons.Length; ++i)
                loadSlotButtons[i].interactable = controller.CheckSavedGame(i + 1);
            loadGamePanel.SetActive(true);
        }

        public void LoadGameMenuClose() {
            loadGamePanel.SetActive(false);
        }

        public void NewGameEasy() {
            controller.NewGame(0);
        }

        public void NewGameHard() {
            controller.NewGame(1);
        }

    }

}
