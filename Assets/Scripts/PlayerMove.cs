using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// プレイヤーの移動に関するクラス
/// </summary>
public class PlayerMove
{
    private float _doNothing = 0;
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
    public float PlayerMovement(bool isPushed)
    {
        // キー入力を検知する
        float moveDirection = Input.GetAxisRaw("Horizontal");

        if (isPushed)
        {
            return _doNothing;
        }
        else
        {
            // プレイヤーを移動させる
            if (moveDirection == 0)
            {
                //キー入力がなければ動かさない
                _playerRigidbody.linearVelocity = new Vector3(0, _playerRigidbody.linearVelocity.y, 0);
            }
            else
            {
                //キーの入力に応じてプレイヤーを移動させる
                _playerRigidbody.linearVelocity = new Vector3(moveDirection * _moveSpeed, _playerRigidbody.linearVelocity.y, 0);
            }

            return moveDirection;   //移動方向の情報を返す

        }
    }
}
