using System;
using TMPro;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities {
    public class Sign : BaseEntity {
        public TextMeshPro text;

        public static Sign New(EcsSonderGameWorld world, GameObject gameObject) {
            var sign = CreateThis<Sign>(world);

            sign.text = gameObject.GetComponentInChildren<TextMeshPro>();
            return sign;
        }

        public void SetText(String s) {
            text.text = s;
        }
    }
}