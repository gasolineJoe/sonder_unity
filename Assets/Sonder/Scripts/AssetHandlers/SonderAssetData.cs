using UnityEngine;

namespace Sonder.Scripts.AssetHandlers {
    [CreateAssetMenu(fileName = "RoomScriptablee.asset", menuName = "Create New SonderAssetData")]
    public class SonderAssetData : ScriptableObject {
        public GameObject Hero;
        public GameObject startRoom;
        [Space] public GameObject[] rooms;
    }
}