using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Entities;

namespace Sonder.Scripts.Ui {
    public class InventoryUiData {
        public Human User;
        public Storage Storage;

        public InventoryUiData(Human human, Storage storage) {
            User = human;
            Storage = storage;
        }
    }
}