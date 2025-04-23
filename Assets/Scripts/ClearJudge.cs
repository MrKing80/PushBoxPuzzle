using UnityEngine;

public class ClearJudge : MonoBehaviour
{
    [SerializeField] private LayerMask _boxLayer = default;     // レイがヒットするレイヤー
    private Rigidbody _hitRigidbody = default;                  // ヒットした相手のリジッドボディ
    private bool _isPressed = false;                            // スイッチが押されたか
    private float _stopThreshold = 0.001f;                      // 速度がこの値以下なら止まっているとみなす

    /// <summary>
    /// スイッチの状態を返すプロパティ
    /// </summary>
    public bool GetIsPressed
    {
        get { return _isPressed; }
    }

    /// <summary>
    /// 一定時間ごとにレイを飛ばし、判定をとる
    /// </summary>
    private void FixedUpdate()
    {
        ThrowARay();
    }

    /// <summary>
    /// レイを射出し、スイッチが押されたか判定を行うメソッド
    /// </summary>
    private void ThrowARay()
    {
        Vector3 offset = Vector3.down * 0.5f;                               // 射出位置を調整する
        float maxRayDistans = 0.5f;                                         // レイの射出距離
        RaycastHit _hitInfo = default;                                      // レイがヒットした相手オブジェクトの情報
        Ray ray = new Ray(transform.position + offset, transform.up);       // レイを飛ばす

        Debug.DrawRay(transform.position + offset, transform.up * maxRayDistans, Color.black);      // レイを描画する

        // レイがヒットしているか
        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {
            //ヒットした相手のリジッドボディを取得
            _hitRigidbody = _hitInfo.rigidbody;

            //ヒットした相手が止まっている、かつスイッチが押されていない場合
            if (_hitRigidbody.linearVelocity.magnitude < _stopThreshold && !_isPressed)
            {
                //スイッチが押されている状態にする
                _isPressed = true;
            }
        }
        //レイがヒットしていなければスイッチが押されていない状態にする
        else if(!Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {
            _isPressed = false;
        }
    }
}
