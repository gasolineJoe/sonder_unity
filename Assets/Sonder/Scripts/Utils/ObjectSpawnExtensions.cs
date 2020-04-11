using UnityEngine;

namespace Sonder.Scripts.Systems {
    public abstract class ObjectSpawnExtensions {

        public static GameObject GetWorld() {
            return GameObject.FindWithTag("GameWorld");
        }
        
        public static GameObject Spawn(GameObject gameObject) {
            return Object.Instantiate(gameObject, GetWorld().transform);
        }
        
        public static GameObject Spawn(GameObject gameObject, GameObject parent) {
            var obj = Object.Instantiate(gameObject, GetWorld().transform);
            obj.transform.parent = parent.transform;
            return obj;
        }

        public static void Despawn(GameObject gameObject) {
            Object.Destroy(gameObject);
        }
    }
}