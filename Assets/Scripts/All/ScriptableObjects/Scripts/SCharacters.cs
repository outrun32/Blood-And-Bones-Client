using System.Collections.Generic;
using UnityEngine;

namespace All.ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "Characters", menuName = "ScriptableObjects/Characters", order = 1)]
    public class SCharacters : ScriptableObject
    {
        public List<SCharacter> Characters;
    }
}
