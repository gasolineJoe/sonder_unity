using LeopotamGroup.Ecs;
using Sonder.Scripts.AssetHandlers;
using Sonder.Scripts.Ui;

namespace Sonder.Scripts {
    public class EcsSonderGameWorld : EcsWorld {
        public readonly SonderStartupData StartupData;
        public bool IsFrozen = true;
        public UiMode UiMode = UiMode.NONE;
        public SystemUiMode SystemUiMode = SystemUiMode.NONE;

        public EcsSonderGameWorld(SonderStartupData data) {
            StartupData = data;
        }
    }
}