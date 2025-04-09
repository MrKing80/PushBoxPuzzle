using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの挙動を管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの挙動に関するステータス")]
    [SerializeField] private float _moveSpeed = 0;      // 移動速度
    [SerializeField] private float _jumpForce = 0;      // ジャンプ力
    [SerializeField] private float _maxPushForce = 0;   // 箱を押し出す最大の力

    private float _moveDirection = 0;                   // 移動方向

    private const int INVERTED_ORIENTATION = -1;        // 向きを反転する用の定数
    
    private float _zLocalScale = 0;                     // プレイヤーのX軸のローカルスケール
    private float _invertedZLocalScale = 0;             // 反転時のプレイヤーのX軸のローカルスケール
    
    private PlayerMove _playerMove = default;           // プレイヤーの移動に関するクラス
    private PlayerJump _playerJump = default;           // プレイヤーのジャンプに関するクラス
    private PushBox _pushBox = default;                 // プレイヤーの箱を押し出す挙動に関するクラス

    /// <summary>
    /// /初期化を行う
    /// </summary>
    private void Awake()
    {
        _playerMove = new PlayerMove(this.GetComponent<Rigidbody>(), _moveSpeed);   // 移動クラスを初期化
        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);   // ジャンプクラスを初期化
        _pushBox = new PushBox(_maxPushForce);                                      // 箱を押し出すクラスを初期化

        _zLocalScale = transform.localScale.z;                                      // プレイヤーのX軸のローカルスケールを取得
        _invertedZLocalScale = transform.localScale.z * INVERTED_ORIENTATION;       // プレイヤーのX軸のローカルスケールを取得し反転処理を行う
    }

    /// <summary>
    /// プレイヤーのアクションの処理を毎フレーム行う
    /// </summary>
    private void Update()
    {
        _moveDirection = _playerMove.PlayerMovement();                              // 移動の処理を行う
        _playerJump.PlayerJumping(this.transform.position);                         // ジャンプの処理を行う
        _pushBox.PlayerPushing(this.transform.position, transform.localScale.z);    // 箱を押し出す処理を行う

        ChangeDirection();
    }

    /// <summary>
    /// 移動の向きに応じてプレイヤーのローカルスケールを変更する
    /// </summary>
    private void ChangeDirection()
    {
        Vector3 playerLocalScale = this.transform.localScale;

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
