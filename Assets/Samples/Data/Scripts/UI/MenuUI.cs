using UnityEngine.UI;

namespace SampleData {

    public class MenuUI : ICtrlUI {

        public Dropdown difficultyDropdown;

        // TODO: fill difficulty values dynamically!
        protected override void SetStatusText() {
            difficultyDropdown.value = ((MenuCtrl)controller).Difficulty;
        }

        public void SetDifficulty(Dropdown dropdown) {
            int difficulty = dropdown.value;
            ((MenuCtrl)controller).SetDifficulty(difficulty);
        }

    }

}
