    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPlayerController : MonoBehaviour
{
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
