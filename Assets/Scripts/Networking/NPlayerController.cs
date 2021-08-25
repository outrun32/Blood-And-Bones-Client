using System.Collections;
using System.Collections.Generic;
using FiveOnFive.Controllers;
using UnityEngine;

public class NPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    private Vector2 axis;

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        ClientSend.PlayerInput(_controller.InputModel);
    }
}
