using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FiveOnFive.Controllers;
using Networking;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public float Radius;
    public LayerMask LayerMask;
    public PlayerController PlayerController;
    public GameObject Enemy;
    [SerializeField] private List<GameObject> _listTargets, _allTargets;
    [SerializeField]private int ind = 0;
    [ContextMenu("Aim")]
    public void Aim()
    {
        FindAllTargets();
        CheckAllPlayer();
        if (_listTargets.Count > 0) Enemy = _listTargets[0].gameObject;
        if (Enemy)
        {
            PlayerController.Aim(Enemy.transform);
        }
    }

    public void FindAllTargets()
    {
        _allTargets = GameManager.Players.Select(t => t.Value.gameObject).ToList().Where(n => n.tag == "Enemy")
            .OrderBy(n => Vector3.Distance(transform.position, n.gameObject.transform.position)).ToList();
        ind = 0;

    }
    [ContextMenu("NotAim")]
    public void NotAim()
    {
        PlayerController.NotAim();
    }
    public void Next()
    {
        ind++;
        CheckAllPlayer();
        if (_listTargets.Count <= ind) ind = 0;

        if (ind < _listTargets.Count && ind >= 0)
        {
            Enemy = _listTargets[ind].gameObject;
            PlayerController.Aim(Enemy.transform);
        }
        else NotAim();
        Debug.Log("Next");
    }
    public void Last()
    {
        ind--;
        CheckAllPlayer();
        if (ind < 0) ind = _listTargets.Count-1;

        if (ind >= 0 && ind < _listTargets.Count)
        {
            Enemy = _listTargets[ind].gameObject;
            PlayerController.Aim(Enemy.transform);
        }
        else NotAim();
        Debug.Log("Last");
    }

    public void CheckAllPlayer()
    {

        _listTargets = new List<GameObject>();
        foreach (GameObject target in _allTargets)
        {
            if (CheckPlayer(target))
            {
                _listTargets.Add(target);
            }
        }
    }

    public bool CheckPlayer(GameObject gameObject)
    {
        RaycastHit hit;   
        if (Physics.Raycast( transform.position,gameObject.transform.position, out hit, Mathf.Infinity, LayerMask))
        {
            return false;
        }
        return true;
    }

    
    
}
