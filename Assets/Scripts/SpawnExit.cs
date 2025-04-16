using System.Collections;
using UnityEngine;

public class SpawnExit : MonoBehaviour
{
    private GameObject[] _switches = default;      //ステージ上に配置されたスイッチを格納する配列
    private GameObject _exitDoor = default;        //出口オブジェクトを格納する変数
    private bool[] _isPresseds = default;                           //それぞれのスイッチの状態を格納する配列
    private bool _isPassedTheCheck = false;                         //すべてのスイッチが押されたかを判定する変数
    private float _waitSec = 0.5f;                                  //一定時間待機する時間を格納した変数

    /// <summary>
    /// ゲーム開始時、初期化の設定を行う
    /// </summary>
    private void Awake()
    {
        _switches = GameObject.FindGameObjectsWithTag("ClearJudgeObject");
        _exitDoor = GameObject.FindGameObjectWithTag("Finish");

        //出口を非表示にする
        _exitDoor.SetActive(false);

        //要素数をステージ上に配置されたスイッチと同じ数にする
        _isPresseds = new bool[_switches.Length];

        //配列を初期化する
        for (int i = 0; i < _switches.Length; i++)
        {
            _isPresseds[i] = false;
        }

        StartCoroutine(SwitchChecker());
    }

    /// <summary>
    /// 毎フレーム、クリアの判定を行う
    /// </summary>
    private void Update()
    {
        ClearChecker();

        //すべてのボタンが押されている状態であれば、出口を出現させる
        if (_isPassedTheCheck)
        {
            _exitDoor.SetActive(true);
        }
    }

    /// <summary>
    /// すべてのスイッチが押されているかチェックを行う
    /// </summary>
    /// <returns>falseは押されていないスイッチがある、trueはすべてのスイッチが押されている</returns>
    private bool ClearChecker()
    {
        //スイッチの状態が格納されている配列を探索
        for (int i = 0; i < _isPresseds.Length; i++)
        {
            //一つでもfalseがあれば返り値としてfalseを返す
            if (!_isPresseds[i])
            {
                return _isPassedTheCheck = false;
            }
        }

        //すべて押されていればtrueを返す
        return _isPassedTheCheck = true;
    }

    /// <summary>
    /// スイッチが押されているかのチェックを行う
    /// </summary>
    private IEnumerator SwitchChecker()
    {
        //すべてのスイッチが押されている状態になるまでループを回す
        while (!_isPassedTheCheck)
        {
            int i = 0;

            //配列の要素を一つずつ見ていく
            while (i < _isPresseds.Length)
            {
                //現在のスイッチの状態を格納
                _isPresseds[i] = _switches[i].GetComponent<ClearJudge>().GetIsPressed;

                //一定時間待つ
                yield return new WaitForSeconds(_waitSec);

                i++;
            }
        }
    }
}
