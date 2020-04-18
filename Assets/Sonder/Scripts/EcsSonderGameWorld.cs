using LeopotamGroup.Ecs;
using Sonder.Scripts.AssetHandlers;
using Sonder.Scripts.Ui;

namespace Sonder.Scripts {
    public class EcsSonderGameWorld : EcsWorld {
        public readonly SonderAssetData AssetData;
        public readonly SonderUiData UiData;
        public readonly GameState GameState;

        public EcsSonderGameWorld(SonderAssetData data, SonderUiData ui) {
            AssetData = data;
            UiData = ui;
            GameState = new GameState();
        }
    }
}