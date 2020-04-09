using UnityEngine;

namespace Sonder.Scripts.AssetHandlers {
    [CreateAssetMenu(fileName = "SonderUiData.asset", menuName = "Create New SonderUiData")]
    public class SonderUiData : ScriptableObject {
        public GameObject uiText;
    }
}