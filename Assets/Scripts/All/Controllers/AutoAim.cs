using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public float Radius;
    public PlayerController PlayerController;
    public GameObject Enemy;
    private List<GameObject> _listTargets;
    private int ind = 0;
    [ContextMenu("Aim")]
    public void Aim()
    {
        FindAllTargets();
        if (Enemy)
        {
            PlayerController.Aim(Enemy.transform);
        }
    }

    public void FindAllTargets()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position,Radius);
        List<GameObject> list = GameManager.Players.Select(t => t.Value.gameObject).ToList().Where(n => n.tag == "Enemy")
            .OrderBy(n => Vector3.Distance(transform.position, n.gameObject.transform.position)).ToList();
        if (list.Count > 0) Enemy = _listTargets[0].gameObject;
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
        if (_listTargets.Count == ind - 1) ind = 0;
        Enemy = _listTargets[ind].gameObject;
        Debug.Log("Next");
    }

    public void Last()
    {
        ind--;
        if (_listTargets.Count == 0) ind = _listTargets.Count-1;
        Enemy = _listTargets[ind].gameObject;
        Debug.Log("Last");
    }
    
}
