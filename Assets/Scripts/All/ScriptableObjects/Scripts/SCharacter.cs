using UnityEngine;

namespace All.ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]

    public class SCharacter : ScriptableObject
    {
        public int SpawnIndex;
        public float MaxHealth;
        public float StartHealth;
        public float MaxMana;
        public float StartMana;
    }
}
