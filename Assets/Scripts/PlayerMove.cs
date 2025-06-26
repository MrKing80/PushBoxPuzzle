using UnityEngine;

/// <summary>
/// プレイヤーの移動に関するクラス
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private Rigidbody _playerRigidbody = default;       // プレイヤーのRigidbody
    private float _moveDirection = 0f;                  // プレイヤーの移動する方向
    private float _zLocalScale = 0f;                    // プレイヤーのZ軸のローカルスケール
    private float _invertedZLocalScale = 0f;            // 反転時のプレイヤーのZ軸のローカルスケール
    private const int INVERTED_ORIENTATION = -1;        // 向きを反転する用の定数

    /// <summary>
    /// Rigidbodyの情報を受け取るプロパティ
    /// </summary>
    public Rigidbody Rigidbody
    {
        get { return _playerRigidbody; }
        set { _playerRigidbody = value;}
    }

    private void Awake()
    {
        _zLocalScale = transform.localScale.z;                                      // プレイヤーのZ軸のローカルスケールを取得
        _invertedZLocalScale = transform.localScale.z * INVERTED_ORIENTATION;       // プレイヤーのZ軸のローカルスケールを取得し反転処理を行う
    }

    /// <summary>
    /// プレイヤーの移動の処理を行う
    /// </summary>
    public void PlayerMovement(float moveSpeed, bool isPushed)
    {
        //箱が押されている状態であれば処理を行わない
        if (isPushed)
        {
            return;
        }

        // キー入力を検知する
        _moveDirection = Input.GetAxisRaw("Horizontal");

        ChangeDirection();  //プレイヤーの向きを変える

        // プレイヤーを移動させる
        if (_moveDirection == 0)
        {
            //キー入力がなければ動かさない
            _playerRigidbody.linearVelocity = new Vector3(0, _playerRigidbody.linearVelocity.y, 0);
        }
        else
        {
            //キーの入力に応じてプレイヤーを移動させる
            _playerRigidbody.linearVelocity = new Vector3(_moveDirection * moveSpeed, _playerRigidbody.linearVelocity.y, 0);
        }

    }


    /// <summary>
    /// 移動の向きに応じてプレイヤーのローカルスケールを変更する
    /// </summary>
    private void ChangeDirection()
    {
        Vector3 playerLocalScale = this.transform.localScale; // 現在のスケールを一時保存

        //移動方向がプラスであれば右方向、マイナスであれば左方向を向く
        if (_moveDirection > 0)
        {
            this.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y, _zLocalScale);
        }
        else if (_moveDirection < 0)
        {
            this.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y, _invertedZLocalScale);
        }
    }

}
