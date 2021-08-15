using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _id;
    private string _username;
    [SerializeField] private ClientPlayerController _clientController;

    public void SetID(int value)
    {
        _id = value;
    }
    
    public void SetUsername(string value)
    {
        _username = value;
    }
    
    public void SetPosition(Vector3 position)
    {
        _clientController.SetPosition(position);
    }

    public void SetRotation(Quaternion rotation)
    {
        _clientController.SetRotation(rotation);
    }
}
