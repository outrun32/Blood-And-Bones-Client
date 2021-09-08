using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    private Transform _target;
    void Update()
    {
        if (_target) transform.LookAt(_target);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
}
