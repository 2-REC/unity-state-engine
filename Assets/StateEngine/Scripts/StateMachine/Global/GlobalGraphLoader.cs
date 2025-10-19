using UnityEngine;

namespace StateEngine {

    public class GlobalGraphLoader : IGraphLoader {

/*
        public GlobalGraphLoader(string filename)
                : base(filename) {
        }
*/
        public GlobalGraphLoader(TextAsset xmlGraph)
                : base(xmlGraph) {
        }

        protected override bool CheckAttributes(StateData data) {
            if ((data.scene == null) || "".Equals(data.scene)) {
                Debug.Log("Invalid state!");
                return false;
            }
            return true;
        }

    }

}
