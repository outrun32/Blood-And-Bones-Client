using UnityEngine;

namespace All.ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "NetworkSettings", menuName = "ScriptableObjects/NetworkSettings", order = 1)]
    public class SNetworkSettings : ScriptableObject
    {
        public string IP;
        public int Port;
    }
}
