using Sonder.Scripts.Ui;

namespace Sonder.Scripts {
    public class GameState {
        public bool IsFrozen = false;
        public SystemUiMode SystemUiMode = SystemUiMode.NONE;
        
        public UiMode UiMode = UiMode.NONE;
        public InventoryUiData InventoryUiData = null;
    }
}