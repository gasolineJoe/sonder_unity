using UnityEngine;

namespace Sonder.Scripts.Systems {
    public abstract class ObjectSpawnExtensions {

        public static GameObject GetWorld() {
            return GameObject.FindWithTag("GameWorld");
        }

        public static GameObject GetUi() {
            return GameObject.FindWithTag("GameUi");
        }
        
        public static GameObject SpawnToParent(GameObject gameObject, GameObject parent) {
            var obj = Object.Instantiate(gameObject, parent.transform);
            return obj;
        }
        
        public static GameObject SpawnWorld(GameObject gameObject) {
            return SpawnToParent(gameObject, GetWorld());
        }
        
        public static GameObject SpawnUi(GameObject gameObject) {
            return SpawnToParent(gameObject, GetUi());
        }

        public static void Despawn(GameObject gameObject) {
            Object.Destroy(gameObject);
        }
    }
}