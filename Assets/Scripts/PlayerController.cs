using Unity.VisualScripting;
using UnityEngine;
using TMPro;

/// <summary>
/// プレイヤーの挙動を管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの挙動に関するステータス")]
    [SerializeField] private float _moveSpeed = 0;          // 移動速度
    [SerializeField] private float _jumpForce = 0;          // ジャンプ力
    [SerializeField] private float _maxPushForce = 0;       // 箱を押し出す最大の力

    [SerializeField] private LayerMask _boxLayer = default; //レイが衝突するレイヤー

    [SerializeField] private TMP_Text _showPower = default; // 押す力を表示するテキストUI

    private const int INVERTED_ORIENTATION = -1;            // 向きを反転する用の定数

    private float _moveDirection = 0;                       // 移動方向

    private float _zLocalScale = 0;                         // プレイヤーのZ軸のローカルスケール
    private float _invertedZLocalScale = 0;                 // 反転時のプレイヤーのZ軸のローカルスケール

    private float _interval = 1f;                           // 押された状態の解除までの時間間隔
    private float _timer = 0;                               // 経過時間を記録するタイマー

    private float _pushForce = 0;                           // 現在の押す力
    private bool _isPushed = false;                         // プレイヤーが箱を押している状態かどうか

    private PlayerMove _playerMove = default;               // プレイヤーの移動に関するクラス
    private PlayerJump _playerJump = default;               // プレイヤーのジャンプに関するクラス
    private PushBox _pushBox = default;                     // プレイヤーの箱を押し出す挙動に関するクラス

    /// <summary>
    /// /初期化を行う
    /// </summary>
    private void Awake()
    {
        _playerMove = new PlayerMove(this.GetComponent<Rigidbody>(), _moveSpeed);   // 移動クラスを初期化
        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);   // ジャンプクラスを初期化
        _pushBox = new PushBox(_maxPushForce, _boxLayer);                           // 箱を押し出すクラスを初期化

        _zLocalScale = transform.localScale.z;                                      // プレイヤーのZ軸のローカルスケールを取得
        _invertedZLocalScale = transform.localScale.z * INVERTED_ORIENTATION;       // プレイヤーのZ軸のローカルスケールを取得し反転処理を行う
    }

    /// <summary>
    /// プレイヤーのアクションの処理を毎フレーム行う
    /// </summary>
    private void Update()
    {
        _isPushed = _pushBox.IsPushed; // 押し状態を取得

        if (_isPushed)
        {
            //時間を計測
            _timer += Time.deltaTime;

            //一定時間経過したら
            if (_timer >= _interval)
            {
                _isPushed = false;

                _pushBox.IsPushed = _isPushed;

                //タイマーリセット
                _timer = 0;
            }
        }

        _playerJump.PlayerJumping(this.transform.position);                                     // ジャンプの処理を行う
        _pushForce = _pushBox.PlayerPushing(this.transform.position, transform.localScale.z);   // 箱を押し出す処理を行う
        _moveDirection = _playerMove.PlayerMovement(_isPushed);                                 // 移動の処理を行う

        ChangeDirection(); // 移動方向に応じた向き変更
        ChangeText();      // 押し出し力の表示更新
    }

    /// <summary>
    /// 移動の向きに応じてプレイヤーのローカルスケールを変更する
    /// </summary>
    private void ChangeDirection()
    {
        Vector3 playerLocalScale = this.transform.localScale; // 現在のスケールを一時保存

        if (_moveDirection > 0)
        {
            this.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y, _zLocalScale);
        }
        else if (_moveDirection < 0)
        {
            this.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y, _invertedZLocalScale);
        }
    }

    private void ChangeText()
    {
        if (_pushForce > 0)
        {
            _showPower.text = _pushForce.ToString(); // 押し出し力を文字列で表示
        }
        else if (_pushForce <= 0)
        {
            _showPower.text = ""; // 力が0以下の場合は非表示
        }
    }
}
