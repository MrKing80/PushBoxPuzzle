using UnityEngine;

/// <summary>
/// 箱が静止しているかの判定を行うスクリプト
/// </summary>
public class BoxIsSleeping : MonoBehaviour
{
    // 箱のRigidbodyを格納するための変数
    private Rigidbody _boxRigidbody = default;

    /// <summary>
    /// 初期化処理をする
    /// </summary>
    private void Awake()
    {
        // 箱にアタッチされているRigidbodyを取得
        _boxRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 一定時間ごとに判定を行う（FixedUpdateは物理演算のタイミングで呼ばれる）
    /// </summary>
    private void FixedUpdate()
    {
        // 箱が物理的に「静止している」かどうかをチェック
        if (_boxRigidbody.IsSleeping())
        {
            // 静止している場合、物理演算を無効化して最適化を図る
            _boxRigidbody.isKinematic = true;
        }
    }
}
