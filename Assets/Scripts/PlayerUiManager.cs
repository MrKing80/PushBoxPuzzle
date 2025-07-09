using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] private PushBox _pushBox = default;        //PushBoxを格納する変数
    [SerializeField] private TMP_Text _showPower = default;     //押す力を表示するテキストUI
    private GameObject _pauseCanvas = default;                  //ポーズメニューのオブジェクトを格納する変数

    private float _pushForce = 0f;  //箱を押す力

    private void Awake()
    {
        _pauseCanvas = GameObject.FindGameObjectWithTag("Pause");

        if (_pauseCanvas != null)
        {
            //オブジェクトを非表示にする
            _pauseCanvas.SetActive(false);
        }
    }


    private void Update()
    {
        //キーが入力されたらオブジェクトの表示状態に応じて表示と非表示を切り替える
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseCanvas.activeSelf)
        {
            _pauseCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseCanvas.activeSelf)
        {
            _pauseCanvas.SetActive(false);
        }


    }
    private void FixedUpdate()
    {
        _pushForce = _pushBox.GetPushForce; //押し出す力を取得

        ChangeText();      // 押し出し力の表示更新
    }

    /// <summary>
    /// 現在の箱を押し出す力を表示させるメソッド
    /// </summary>
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
