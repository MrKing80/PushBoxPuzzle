using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] private PushBox _pushBox = default;        //PushBox���i�[����ϐ�
    [SerializeField] private TMP_Text _showPower = default;     //�����͂�\������e�L�X�gUI
    private GameObject _pauseCanvas = default;                  //�|�[�Y���j���[�̃I�u�W�F�N�g���i�[����ϐ�

    private float _pushForce = 0f;  //����������

    private void Awake()
    {
        _pauseCanvas = GameObject.FindGameObjectWithTag("Pause");

        if (_pauseCanvas != null)
        {
            //�I�u�W�F�N�g���\���ɂ���
            _pauseCanvas.SetActive(false);
        }
    }


    private void Update()
    {
        //�L�[�����͂��ꂽ��I�u�W�F�N�g�̕\����Ԃɉ����ĕ\���Ɣ�\����؂�ւ���
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
        _pushForce = _pushBox.GetPushForce; //�����o���͂��擾

        ChangeText();      // �����o���͂̕\���X�V
    }

    /// <summary>
    /// ���݂̔��������o���͂�\�������郁�\�b�h
    /// </summary>
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
}
