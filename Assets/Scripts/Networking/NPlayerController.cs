using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    private Vector2 axis;

    private void FixedUpdate()
    {
        axis = _controller.JoyAxis;
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        float[] _inputs = new float[]
        {
            axis.x,
            axis.y
        };
        bool _isJumping = _controller.IsJumped;
        ClientSend.PlayerMovement(_inputs, _isJumping);
    }
}
