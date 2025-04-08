using UnityEngine;

/// <summary>
/// 箱が静止しているかの判定を行うスクリプト
/// </summary>
public class BoxIsSleeping : MonoBehaviour
{
    private Rigidbody _boxRigidbody = default;  // 箱のRigidbody

    /// <summary>
    /// 初期化処理をする
    /// </summary>
    private void Awake()
    {
        // 箱にアタッチされているRigidbodyを取得
        _boxRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 一定時間ごとに判定を行う
    /// </summary>
    private void FixedUpdate()
    {
        // 箱が静止しているのかを判定
        if (_boxRigidbody.IsSleeping())
        {
            // 静止していれば物理演算の影響を受けないようにする
            _boxRigidbody.isKinematic = true;
        }
    }
}
