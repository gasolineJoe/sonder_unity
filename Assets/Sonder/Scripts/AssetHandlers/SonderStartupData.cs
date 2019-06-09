namespace Sonder.Scripts.AssetHandlers {
    using UnityEngine;

    [CreateAssetMenu(fileName = "RoomScriptable.asset", menuName = "Create New Startup Data")]
    public class SonderStartupData : ScriptableObject {
        public GameObject Hero;
        public GameObject startRoom;
        [Space] public GameObject[] rooms;
    }
}