using System.Collections.Generic;
using System.Globalization;
using All.Modes;
using UnityEngine;

namespace UI.Controllers
{
    public class ScrollViewController : MonoBehaviour
    {
        [SerializeField] private ScrollImage _prefab;
        [SerializeField] private Transform _conteiner;

        public void SetScrollViewPlayerData(Dictionary<string,PlayerDataModel> team, int spawnCount)
        {
            foreach (KeyValuePair<string,PlayerDataModel> player in team)
            {
                ScrollImage scrollImage = Instantiate(_prefab, _conteiner);
                scrollImage.Username.text = player.Key;
                scrollImage.Score.text = player.Value.Score.ToString(CultureInfo.InvariantCulture);
                scrollImage.Kills.text = player.Value.KillCount.ToString(CultureInfo.InvariantCulture);
                scrollImage.Death.text = player.Value.DeathCount.ToString(CultureInfo.InvariantCulture);
            }
            for (int i = team.Count; i < spawnCount; i++)
            {
                ScrollImage scrollImage = Instantiate(_prefab, _conteiner);
                scrollImage.Username.text = "";
                scrollImage.Score.text = "";
                scrollImage.Kills.text = "";
                scrollImage.Death.text = "";
            }
        }
    }
}
