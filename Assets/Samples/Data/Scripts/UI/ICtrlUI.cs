using StateEngine;
using UnityEngine;
using UnityEngine.UI;

namespace SampleData {

    [DefaultExecutionOrder(10)]
    public abstract class ICtrlUI : MonoBehaviour {

        public IStateController controller;

        public Text statusText;

        void Start() {
            SetStatusText();
        }

        protected abstract void SetStatusText();

    }

}
