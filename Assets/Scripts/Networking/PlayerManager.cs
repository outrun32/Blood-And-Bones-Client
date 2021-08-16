using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private int _id;
    private string _username;
    [SerializeField] private bool _usernameIsvizible;
    [SerializeField] private ClientPlayerController _clientController;
    [SerializeField] private Text _usernameText;

    public void SetID(int value)
    {
        _id = value;
    }
    
    public void SetUsername(string value)
    {
        if (_usernameIsvizible) _usernameText.text = value;
        else _usernameText.text = "";
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
