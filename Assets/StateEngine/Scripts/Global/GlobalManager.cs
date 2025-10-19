using UnityEngine;

namespace StateEngine {

    public class GlobalManager : IManager {

        public GlobalStateManager globalStateManager;
        public IGlobalDataManager globalDataManager;

        public TextAsset globalStatesGraph;
        public TextAsset gameData;

        protected override void InstantiateStateManager() {
            GlobalStateManager.xmlGraph = globalStatesGraph;
            Instantiate(globalStateManager);
        }

        protected override IStateManager GetStateManager() {
            return GlobalStateManager.Instance;
        }

        protected override IDataManager InstantiateDataManager() {
            //IGameDataManager.xmlGameData = gameData;
            IGlobalDataManager.xmlGameData = gameData;
            return Instantiate(globalDataManager);
        }

    }

}
