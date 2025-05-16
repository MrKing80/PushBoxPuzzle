using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] private PushBox _pushBox = default;
    [SerializeField] private Canvas _pauseCanvas = default;
    [SerializeField] private TMP_Text _showPower = default; // 押す力を表示するテキストUI

    private float _pushForce = 0f;

    private void Awake()
    {
        _pauseCanvas.enabled = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseCanvas.enabled)
        {
            _pauseCanvas.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseCanvas.enabled)
        {
            _pauseCanvas.enabled = false;
        }


    }
    private void FixedUpdate()
    {
        _pushForce = _pushBox.GetPushForce;

        ChangeText();      // 押し出し力の表示更新
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

    public void CloseButton()
    {
        _pauseCanvas.enabled = false;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
