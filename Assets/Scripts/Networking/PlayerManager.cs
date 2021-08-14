using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;

    public ClientPlayerController clientController;


    public void SetPosition(Vector3 position)
    {
        clientController.SetPosition(position);
    }

    public void SetRotation(Quaternion rotation)
    {
        clientController.SetRotation(rotation);
    }
}
