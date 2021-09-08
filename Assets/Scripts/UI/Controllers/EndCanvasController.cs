using System.Collections.Generic;
using System.Linq;
using All.Modes;
using UnityEngine;

namespace UI.Controllers
{
    public class EndCanvasController : MonoBehaviour
    {
        [SerializeField] private ScrollViewController _blueTeam, _redTeam;
        [SerializeField] private GameObject _panel;
        public void SetData(Dictionary<string,PlayerDataModel> redTeam, Dictionary<string,PlayerDataModel> blueTeam)
        {
            var redSortedTeam = redTeam.OrderBy(t => t.Value.Score).ToDictionary(p => p.Key
                , p => p.Value);
            var blueSortedTeam = blueTeam.OrderBy(t => t.Value.Score).ToDictionary(p => p.Key
                , p => p.Value);
            _blueTeam.SetScrollViewPlayerData(blueSortedTeam, 5);
            _redTeam.SetScrollViewPlayerData(redSortedTeam, 5);
        }

        public void SetActive()
        {
            _panel.SetActive(true);
        }
        
    }
}
