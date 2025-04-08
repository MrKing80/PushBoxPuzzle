using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動に関するクラス
/// </summary>
public class PlayerMove
{
    private float _moveSpeed = 0;                       // 移動速度
    private Rigidbody _playerRigidbody = default;       // プレイヤーのRigidbody

    /// <summary>
    /// PlayerMoveのコンストラクタ
    /// 各変数を初期化する
    /// </summary>
    /// <param name="playerRigidbody">PlayerControllerから受け取ったRigidbody</param>
    /// <param name="moveSpeed">PlayerControllerから受け取った移動速度</param>
    public PlayerMove(Rigidbody playerRigidbody, float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _playerRigidbody = playerRigidbody;
    }

    /// <summary>
    /// プレイヤーの移動の処理を行う
    /// </summary>
    public float PlayerMovement()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");       // キー入力を検知する

        // 入力によって移動方向を変える
        if (moveDirection == 0)
        {
            _playerRigidbody.velocity = new Vector3(0, _playerRigidbody.velocity.y, 0);
        }
        else
        {
            _playerRigidbody.velocity = new Vector3(moveDirection * _moveSpeed, _playerRigidbody.velocity.y, 0);
        }

        return moveDirection;   //移動方向の情報を返す
    }
}
