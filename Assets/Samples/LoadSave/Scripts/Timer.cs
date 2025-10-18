/* Script calling a method after a timeout delay. */

using UnityEngine;
using UnityEngine.Events;

namespace SampleLoadSave {

    public class Timer : MonoBehaviour {

        public UnityEvent method;
        public float delay = 3f;

        void Start() {
            if (delay > 0) {
                Invoke(nameof(End), delay);
            }
        }

        void End() {
            method.Invoke();
        }

    }

}
