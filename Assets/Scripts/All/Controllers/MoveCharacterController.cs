using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterController: IFixedUpdate
{
    private CharacterController _characterController;
    private Transform _transform;
    private bool _groundedPlayer;
    private bool _isJumped;
    private float _gravityAcceleration = -9.8f;
    private float _speed;
    private float _jumpHeight;
    
    private Vector3 playerVelocity;
    private Vector3 _direction;
    public MoveCharacterController(CharacterController characterController,Transform transform)
    {
        _characterController = characterController;
        _transform = transform;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void SetRotation(Quaternion rotation)
    {
        _transform.rotation = rotation;
    }
    
    public void Move(Vector2 direction)
    {
        _direction = _transform.forward * direction.y + _transform.right * direction.x;
    }

    public void Jump()
    {
        _isJumped = true;
    }
    public void FixedUpdate()
    {
        _groundedPlayer = _characterController.isGrounded;
        
        playerVelocity.y += _gravityAcceleration * Time.deltaTime;
        _characterController.Move(_direction * Time.deltaTime);
        if (_groundedPlayer)
        {
            playerVelocity.y = 0f;
        }
        if (_isJumped && _groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityAcceleration);
        }
        _characterController.Move(playerVelocity * Time.deltaTime);
    }
}
