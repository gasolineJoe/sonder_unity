using System;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Entities;
using TMPro;
using UnityEngine;

namespace Sonder.Scripts.Ui {
    public class UiComponent : BaseEntity {

        public Body Body;
        public Disabable Disabable;
        public TextMeshPro text;
        
        public static UiComponent New(EcsSonderGameWorld world, GameObject uiObject) {
            var ui = CreateThis<UiComponent>(world);
            
            ui.Disabable = ui.AddComponent<Disabable>(world);
            ui.Body = ui.AddComponent<Body>(world);
            ui.text = uiObject.GetComponentInChildren<TextMeshPro>();
            
            ui.Body.init(uiObject);
            ui.Disabable.init(uiObject);
            ui.Disabable.SetActive(false);

            return ui;
        }

        public void SetText(String s) {
            text.text = s;
        }
    }
}