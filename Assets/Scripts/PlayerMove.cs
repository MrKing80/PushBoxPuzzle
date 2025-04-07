using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    private float _moveSpeed = 0;
    private Rigidbody _playerRigidbody = default;

    public PlayerMove(Rigidbody playerRigidbody, float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _playerRigidbody = playerRigidbody;
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    /// <param name="playerRidigbody">プレイヤーのRigidbody</param>
    public void PlayerMovement()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");

        Debug.Log(moveDirection);

        if(moveDirection == 0)
        {
            _playerRigidbody.velocity = new Vector3(0, _playerRigidbody.velocity.y, 0);
        }
        else
        {
            _playerRigidbody.velocity = new Vector3(moveDirection * _moveSpeed, _playerRigidbody.velocity.y,0);
        }
    }
}
