using System;
using System.Collections.Generic;
using Sonder.Scripts.Components.World.Items;

namespace Sonder.Scripts.Components.Parts {
    public class Storage {
        public readonly List<Item> Inventory = new List<Item>();
        public String name;

        public void Add(Item item) {
            Inventory.Add(item);
        }

        public override string ToString() {
            return name + " with " + Inventory.Count + " items";
        }
    }
}