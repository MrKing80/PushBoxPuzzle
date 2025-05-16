using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] private PushBox _pushBox = default;
    [SerializeField] private Canvas _pauseCanvas = default;
    [SerializeField] private TMP_Text _showPower = default; // �����͂�\������e�L�X�gUI

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

        ChangeText();      // �����o���͂̕\���X�V
    }

    private void ChangeText()
    {
        if (_pushForce > 0)
        {
            _showPower.text = _pushForce.ToString(); // �����o���͂𕶎���ŕ\��
        }
        else if (_pushForce <= 0)
        {
            _showPower.text = ""; // �͂�0�ȉ��̏ꍇ�͔�\��
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
