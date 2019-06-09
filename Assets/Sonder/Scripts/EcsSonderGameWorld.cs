using Sonder.Scripts.AssetHandlers;

namespace Sonder.Scripts {
    using LeopotamGroup.Ecs;

    public class EcsSonderGameWorld : EcsWorld {
        public readonly SonderStartupData StartupData;

        public EcsSonderGameWorld(SonderStartupData data) {
            StartupData = data;
        }
    }
}