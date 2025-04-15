using UnityEngine;

/// <summary>
/// プレイヤーのジャンプに関するクラス
/// </summary>
public class PlayerJump
{
    private float _jumpForce = 0;                       // ジャンプ力
    private bool _isGround = true;                      // 接地しているか
    private Rigidbody _playerRigidbody = default;       // プレイヤーのRigidbody
    private RaycastHit _hitInfo = default;              // レイのヒットしたオブジェクトの情報を格納する変数

    /// <summary>
    /// PlayerJumpのコンストラクタ
    /// 各変数を初期化する
    /// </summary>
    /// <param name="playerRigidbody">PlayerControllerから受け取ったプレイヤーのRigidbody</param>
    /// <param name="jumpForce">PlayerControllerから受け取ったジャンプ力</param>
    public PlayerJump(Rigidbody playerRigidbody, float jumpForce)
    {
        _jumpForce = jumpForce;
        _playerRigidbody = playerRigidbody;
    }

    /// <summary>
    /// プレイヤーのジャンプに関する処理
    /// </summary>
    /// <param name="playerPos">プレイヤーの位置</param>
    public void PlayerJumping(Vector3 playerPos)
    {
        float maxRayDistans = 0.1f;                             // レイの射出距離
        Ray ray = new Ray(playerPos, Vector3.down);             //レイを飛ばす

        Debug.DrawRay(playerPos, Vector3.down * maxRayDistans, Color.red);      // レイを描画する

        // 飛ばしたレイが何かにヒットしているか
        if(Physics.Raycast(ray, out _hitInfo, maxRayDistans))
        {
            _isGround = true;   //接地している
        }
        else
        {
            _isGround = false;  //接地していない
        }

        //Wキーが押されていて、なおかつ接地している時ジャンプをする
        if (Input.GetKeyDown(KeyCode.W) && _isGround)
        {
            _playerRigidbody.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
        }
    }
}
