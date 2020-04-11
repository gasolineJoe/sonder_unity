using System;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Entities;
using TMPro;
using UnityEngine;

namespace Sonder.Scripts.Ui {
    public class InventoryItemUi : BaseEntity {
        public Body Body;
        public Disabable Disabable;
        public TextMeshPro text;
        
        public static InventoryItemUi New(EcsSonderGameWorld world, GameObject uiObject) {
            var ui = CreateThis<InventoryItemUi>(world);
            
            ui.Disabable = ui.AddComponent<Disabable>(world);
            ui.Body = ui.AddComponent<Body>(world);
            ui.text = uiObject.GetComponentInChildren<TextMeshPro>();
            
            ui.Body.init(uiObject);
            ui.Disabable.init(uiObject);

            return ui;
        }

        public void SetText(String s) {
            text.text = s;
        }
    }
}