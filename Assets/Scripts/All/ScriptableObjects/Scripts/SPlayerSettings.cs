using UnityEngine;

namespace All.ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    public class SPlayerSettings : ScriptableObject
    {
        public SCharacter Character;

        public string PlayFabID;
    }
}
