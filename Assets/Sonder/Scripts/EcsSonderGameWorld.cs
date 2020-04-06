using LeopotamGroup.Ecs;
using Sonder.Scripts.AssetHandlers;

namespace Sonder.Scripts {
    public class EcsSonderGameWorld : EcsWorld {
        public readonly SonderStartupData StartupData;
        public bool IsFrozen = true;

        public EcsSonderGameWorld(SonderStartupData data) {
            StartupData = data;
        }
    }
}