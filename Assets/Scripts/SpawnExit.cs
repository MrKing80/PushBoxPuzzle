using System.Collections;
using UnityEngine;

public class SpawnExit : MonoBehaviour
{
    private GameObject[] _switches = default;      //�X�e�[�W��ɔz�u���ꂽ�X�C�b�`���i�[����z��
    private GameObject _exitDoor = default;        //�o���I�u�W�F�N�g���i�[����ϐ�
    private bool[] _isPresseds = default;                           //���ꂼ��̃X�C�b�`�̏�Ԃ��i�[����z��
    private bool _isPassedTheCheck = false;                         //���ׂẴX�C�b�`�������ꂽ���𔻒肷��ϐ�
    private float _waitSec = 0.5f;                                  //��莞�ԑҋ@���鎞�Ԃ��i�[�����ϐ�

    /// <summary>
    /// �Q�[���J�n���A�������̐ݒ���s��
    /// </summary>
    private void Awake()
    {
        _switches = GameObject.FindGameObjectsWithTag("ClearJudgeObject");
        _exitDoor = GameObject.FindGameObjectWithTag("Finish");

        //�o�����\���ɂ���
        _exitDoor.SetActive(false);

        //�v�f�����X�e�[�W��ɔz�u���ꂽ�X�C�b�`�Ɠ������ɂ���
        _isPresseds = new bool[_switches.Length];

        //�z�������������
        for (int i = 0; i < _switches.Length; i++)
        {
            _isPresseds[i] = false;
        }

        StartCoroutine(SwitchChecker());
    }

    /// <summary>
    /// ���t���[���A�N���A�̔�����s��
    /// </summary>
    private void Update()
    {
        ClearChecker();

        //���ׂẴ{�^����������Ă����Ԃł���΁A�o�����o��������
        if (_isPassedTheCheck)
        {
            _exitDoor.SetActive(true);
        }
    }

    /// <summary>
    /// ���ׂẴX�C�b�`��������Ă��邩�`�F�b�N���s��
    /// </summary>
    /// <returns>false�͉�����Ă��Ȃ��X�C�b�`������Atrue�͂��ׂẴX�C�b�`��������Ă���</returns>
    private bool ClearChecker()
    {
        //�X�C�b�`�̏�Ԃ��i�[����Ă���z���T��
        for (int i = 0; i < _isPresseds.Length; i++)
        {
            //��ł�false������ΕԂ�l�Ƃ���false��Ԃ�
            if (!_isPresseds[i])
            {
                return _isPassedTheCheck = false;
            }
        }

        //���ׂĉ�����Ă����true��Ԃ�
        return _isPassedTheCheck = true;
    }

    /// <summary>
    /// �X�C�b�`��������Ă��邩�̃`�F�b�N���s��
    /// </summary>
    private IEnumerator SwitchChecker()
    {
        //���ׂẴX�C�b�`��������Ă����ԂɂȂ�܂Ń��[�v����
        while (!_isPassedTheCheck)
        {
            int i = 0;

            //�z��̗v�f��������Ă���
            while (i < _isPresseds.Length)
            {
                //���݂̃X�C�b�`�̏�Ԃ��i�[
                _isPresseds[i] = _switches[i].GetComponent<ClearJudge>().GetIsPressed;

                //��莞�ԑ҂�
                yield return new WaitForSeconds(_waitSec);

                i++;
            }
        }
    }
}
