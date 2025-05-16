using Unity.VisualScripting;
using UnityEngine;
using TMPro;

/// <summary>
/// プレイヤーの挙動を管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("プレイヤー関係のスクリプト")]
    [SerializeField] private PlayerMove _playerMove = default;               // プレイヤーの移動に関するクラス
    [SerializeField] private PlayerJump _playerJump = default;               // プレイヤーのジャンプに関するクラス
    [SerializeField] private PushBox _pushBox = default;                     // プレイヤーの箱を押し出す挙動に関するクラス


    [Header("プレイヤーの挙動に関するステータス")]
    [SerializeField] private float _moveSpeed = 0;          // 移動速度
    [SerializeField] private float _jumpForce = 0;          // ジャンプ力
    [SerializeField] private float _maxPushForce = 0;       // 箱を押し出す最大の力

    [SerializeField] private LayerMask _boxLayer = default; //レイが衝突するレイヤー

    private Rigidbody _playerRigidoby = default;

    private float _interval = 1f;                           // 押された状態の解除までの時間間隔
    private float _timer = 0;                               // 経過時間を記録するタイマー

    private bool _isPushed = false;                         // プレイヤーが箱を押している状態かどうか

    /// <summary>
    /// /初期化を行う
    /// </summary>
    private void Awake()
    {
        _playerRigidoby = this.GetComponent<Rigidbody>();

        _playerMove.Rigidbody = _playerRigidoby;    //playerMoveにリジッドボディの情報を渡す
        _pushBox.SetMaxPushForce = _maxPushForce;   //pushBoxに箱を押し出す最大の力を渡す

        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);   // ジャンプクラスを初期化

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

        _playerMove.PlayerMovement(_moveSpeed, _isPushed);      // 移動の処理を行う
        _playerJump.PlayerJumping(this.transform.position);     // ジャンプの処理を行う
        _pushBox.PlayerPushing(_boxLayer);                      // 箱を押し出す処理を行う

    }
}
