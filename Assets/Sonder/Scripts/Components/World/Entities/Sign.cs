using System;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities {
    public class Sign {
        public TextMesh text;

        public static Sign New(EcsSonderGameWorld world, GameObject gameObject) {
            var entity = world.CreateEntity();
            var sign = world.AddComponent<Sign>(entity);
            sign.text = gameObject.GetComponentInChildren<TextMesh>();
            return sign;
        }

        public void setText(String text) {
            this.text.text = text;
        }
    }
}