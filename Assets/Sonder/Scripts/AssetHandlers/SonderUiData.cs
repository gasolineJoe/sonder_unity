using UnityEngine;

namespace Sonder.Scripts.AssetHandlers {
    [CreateAssetMenu(fileName = "RoomScriptable.asset", menuName = "Create New SonderUiData")]
    public class SonderUiData : ScriptableObject {
        public GameObject UiText;
    }
}