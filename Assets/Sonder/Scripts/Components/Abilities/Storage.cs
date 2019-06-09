using Sonder.Scripts.Components.Items;

namespace Sonder.Scripts.Components.Abilities {
    using System.Collections.Generic;

    public class Storage {
        public readonly List<Item> Inventory = new List<Item>();

        public void Add(Item item) {
            Inventory.Add(item);
        }

        public override string ToString() {
            return Inventory.Count.ToString();
        }
    }
}