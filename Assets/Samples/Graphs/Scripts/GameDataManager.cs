/* Empty methods as no game data in this sample. */
using StateEngine;

namespace SampleGraphs {

    public class GameDataManager : IGameDataManager {

        protected override void LoadSpecifics() { }

        protected override void CommitChangesSpecifics() { }

        protected override void ResetLifeData() { }

        protected override void ResetContinueData() { }

    }

}
