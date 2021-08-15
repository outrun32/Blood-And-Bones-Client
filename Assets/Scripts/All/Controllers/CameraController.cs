using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform camera;
    [SerializeField]private float rotationSpeed = 2f;
    [SerializeField]private float minCameraAngle = -50f;
    [SerializeField]private float maxCameraAngle = 50f;

    private Vector3 rotation;

    public void MoveCamera(Vector2 axis)
    {
        rotation.x = Mathf.Clamp(rotation.x + axis.y * rotationSpeed, minCameraAngle, maxCameraAngle);
        rotation.y += axis.x * rotationSpeed;
        camera.transform.rotation = Quaternion.Euler(rotation);
    }

    public Vector3 GetCameraTransformForward()
    {
        return new Vector3(camera.forward.x, 0, camera.forward.z);
    }
}
