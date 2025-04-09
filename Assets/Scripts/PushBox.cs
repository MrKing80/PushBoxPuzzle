using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox
{
    private float _ratio = 10;                  // 押す力の最大値に対して最小値を決めるのに使用する値
    private float _maxPushForce = 0;            // 押す力の最大値
    private float _minPushForce = 0;            // 押す力の最小値
    private float _currentPushForce = 0;        // 現在の押す力
    private float _interval = 0.5f;             // 力を増加させる際のインターバル
    private float _timer = 0;                   // 時間を計測する変数
    private bool _isPushable = false;           // 箱を押すことができるかどうか
    private RaycastHit _hitInfo = default;      // レイがヒットした相手オブジェクトの情報


    /// <summary>
    /// PushBoxのコンストラクタ
    /// 箱を押す力の最大値を最小値を決める
    /// </summary>
    /// <param name="maxPushForce">PlayerControllerから受け取った押す力の最大値</param>
    public PushBox(float maxPushForce)
    {
        _maxPushForce = maxPushForce;
        _minPushForce = _maxPushForce / _ratio;
    }


    /// <summary>
    /// プレイヤーが箱を押し出す処理
    /// </summary>
    /// <param name="playerPos">プレイヤーのポジション</param>
    /// <param name="zLocalScal">プレイヤーのX軸のローカルスケール</param>
    public void PlayerPushing(Vector3 playerPos, float zLocalScal)
    {
        //箱を押し出すことが可能な状態かつ、スペースが押されていたら処理を行う
        if (PushableChecker(playerPos,zLocalScal) && Input.GetKey(KeyCode.Space))
        {
            //時間を計測
            _timer += Time.deltaTime;

            //一定時間経過したら
            if(_timer >= _interval)
            {
                //押す力が最大値を超えたら最小値にリセット
                if (_currentPushForce >= _maxPushForce)
                {
                    _currentPushForce = _minPushForce;
                }

                //押す力を増加
                _currentPushForce += _minPushForce;

                //タイマーリセット
                _timer = 0;

                Debug.Log(_currentPushForce);

            }

        }

        //押すことが可能な状態かつ、スペースキーから指が離れたら処理を行う
        if (PushableChecker(playerPos, zLocalScal) && Input.GetKeyUp(KeyCode.Space))
        {
            //レイがヒットしたオブジェクトのRigidbodyを取得
            Rigidbody boxRig = _hitInfo.rigidbody;

            //物理演算の影響を受けるようにする
            boxRig.isKinematic = false;

            //箱に力を加えて動かす
            boxRig.AddForce(new Vector3(_currentPushForce * zLocalScal, 0, 0), ForceMode.Impulse);

            //押す力をリセット
            _currentPushForce = 0;

        }

    }

    /// <summary>
    /// 箱を押すことができるかを判定する処理
    /// ※レイの範囲内に箱があれば押せると判断する
    /// </summary>
    /// <param name="playerPos">プレイヤーのポジション</param>
    /// <param name="zLocalScal">プレイヤーのX軸のローカルスケール</param>
    /// <returns>trueならば箱を押すことが可能、falseならば箱を押すことはできない</returns>
    private bool PushableChecker(Vector3 playerPos, float zLocalScal)
    {        
        float maxRayDistans = 0.5f;         //レイの射出距離
        Ray ray = default;

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

        //レイがヒットしていればtrueを返すそうでなければfalseを返す
        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans))
        {
            _isPushable = true;
        }
        else
        {
            _isPushable = false;
        }

        //Debug.Log(_isPushable);

        return _isPushable;

    }
}
