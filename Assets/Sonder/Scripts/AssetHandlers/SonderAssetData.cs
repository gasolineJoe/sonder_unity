using UnityEngine;

namespace Sonder.Scripts.AssetHandlers {
    [CreateAssetMenu(fileName = "SonderAssetData.asset", menuName = "Create New SonderAssetData")]
    public class SonderAssetData : ScriptableObject {
        public GameObject hero;
        public GameObject startRoom;
        [Space] public GameObject[] rooms;
    }
}