using UnityEngine;

/// <summary>
/// プレイヤーの箱を押し出すアクションを管理するクラス
/// </summary>
public class PushBox : MonoBehaviour 
{
    private float _ratio = 5;                   // 押す力の最大値に対して最小値を決めるのに使用する値
    private float _maxPushForce = 0;            // 押す力の最大値
    private float _minPushForce = 0;            // 押す力の最小値
    private float _currentPushForce = 0;        // 現在の押す力
    private float _tmpPushForce = 0;            // 表示用などに一時保持する値

    private float _interval = 0.5f;             // 力を増加させる際のインターバル
    private float _timer = 0;                   // 時間を計測する変数

    private bool _isPushable = false;           // 箱を押すことができるかどうか
    private bool _isPushed = false;             // 箱が押されたかどうか

    private RaycastHit _hitInfo = default;      // レイがヒットした相手オブジェクトの情報

    /// <summary>
    /// 外部から押された状態を取得・設定するプロパティ
    /// </summary>
    public bool IsPushed
    {
        get { return _isPushed; }
        set { _isPushed = value; }
    }

    /// <summary>
    /// 箱を押す力の最大値を受け取るプロパティ
    /// </summary>
    public float SetMaxPushForce
    {
        set { _maxPushForce = value; }
    }

    /// <summary>
    /// 箱を押す力を返すプロパティ
    /// </summary>
    public float GetPushForce
    {
        get { return _tmpPushForce; }
    }


    private void Start()
    {
        _minPushForce = _maxPushForce / _ratio;     //受け取った力を基に最小値を決定
        _currentPushForce = _minPushForce;          //最小値を格納し初期化する
    }

    /// <summary>
    /// プレイヤーが箱を押し出す処理
    /// </summary>
    /// <param name="boxLayer">レイが衝突するレイヤー</param>
    public void PlayerPushing(LayerMask boxLayer)
    {
        Vector3 playerPos = this.transform.position;        //プレイヤーのポジションを取得
        float zLocalScal = transform.localScale.z;          //プレイヤーのz軸のローカルスケールを取得

        // 押せない状況でスペースキーが押されている、または何も押していないとき
        if (!PushableChecker(playerPos, zLocalScal, boxLayer))
        {
            // タイマーをリセット
            _timer = 0;

            // 最小の力を変数に格納
            _currentPushForce = _minPushForce;

            // 変数の中身を0にする
            _tmpPushForce = 0;

            return;
        }

        //箱を押し出すことが可能な状態かつ、スペースが押されていたら処理を行う
        if (PushableChecker(playerPos, zLocalScal, boxLayer) && Input.GetKey(KeyCode.Space))
        {
            //時間を計測
            _timer += Time.deltaTime;

            //一定時間経過したら
            if (_timer >= _interval)
            {
                //押す力が最大値を超えたら最小値にリセット
                if (_currentPushForce >= _maxPushForce)
                {
                    _currentPushForce = _minPushForce;
                }

                //押す力を増加
                _currentPushForce += _minPushForce;

                // テキストに表示する用に現在の力の値を格納
                _tmpPushForce = _currentPushForce;

                //タイマーリセット
                _timer = 0;

                return ;
            }

        }

        //押すことが可能な状態かつ、スペースキーから指が離れたら処理を行う
        if (PushableChecker(playerPos, zLocalScal, boxLayer) && Input.GetKeyUp(KeyCode.Space))
        {
            //レイがヒットしたオブジェクトのRigidbodyを取得
            Rigidbody boxRig = _hitInfo.rigidbody;

            //物理演算の影響を受けるようにする
            boxRig.isKinematic = false;

            //箱に力を加えて動かす
            boxRig.AddForce(new Vector3(_currentPushForce * zLocalScal, 0, 0), ForceMode.Impulse);

            //押す力をリセット
            _currentPushForce = _minPushForce;

            _isPushed = true;
        }
    }

    /// <summary>
    /// 箱を押すことができるかを判定する処理
    /// ※レイの範囲内に箱があれば押せると判断する
    /// </summary>
    /// <param name="playerPos">プレイヤーのポジション</param>
    /// <param name="zLocalScal">プレイヤーのX軸のローカルスケール</param>
    /// <param name="boxlayer">レイが衝突するレイヤー</param>
    /// <returns>trueならば箱を押すことが可能、falseならば箱を押すことはできない</returns>
    private bool PushableChecker(Vector3 playerPos, float zLocalScal, LayerMask boxlayer)
    {
        float maxRayDistans = 0.5f;         //レイの射出距離
        Ray ray = default;                  // レイの変数を初期化

        //プレイヤーの向きに応じてレイの射出方向を変える
        if (zLocalScal < 0)
        {
            ray = new Ray(playerPos, Vector3.left);     //左方向
        }
        else if (zLocalScal > 0)
        {
            ray = new Ray(playerPos, Vector3.right);    //右方向
        }

        //レイを描画する
        Debug.DrawRay(playerPos, Vector3.right * maxRayDistans * zLocalScal, Color.red);

        //レイがヒットしていればtrueを返す、そうでなければfalseを返す
        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans, boxlayer))
        {
            _isPushable = true;
        }
        else
        {
            _isPushable = false;
        }

        return _isPushable;
    }
}
