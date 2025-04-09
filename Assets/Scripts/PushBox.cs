using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox
{
    private float _ratio = 10;                  // �����͂̍ő�l�ɑ΂��čŏ��l�����߂�̂Ɏg�p����l
    private float _maxPushForce = 0;            // �����͂̍ő�l
    private float _minPushForce = 0;            // �����͂̍ŏ��l
    private float _currentPushForce = 0;        // ���݂̉�����
    private float _interval = 0.5f;             // �͂𑝉�������ۂ̃C���^�[�o��
    private float _timer = 0;                   // ���Ԃ��v������ϐ�
    private bool _isPushable = false;           // �����������Ƃ��ł��邩�ǂ���
    private RaycastHit _hitInfo = default;      // ���C���q�b�g��������I�u�W�F�N�g�̏��


    /// <summary>
    /// PushBox�̃R���X�g���N�^
    /// ���������͂̍ő�l���ŏ��l�����߂�
    /// </summary>
    /// <param name="maxPushForce">PlayerController����󂯎���������͂̍ő�l</param>
    public PushBox(float maxPushForce)
    {
        _maxPushForce = maxPushForce;
        _minPushForce = _maxPushForce / _ratio;
    }


    /// <summary>
    /// �v���C���[�����������o������
    /// </summary>
    /// <param name="playerPos">�v���C���[�̃|�W�V����</param>
    /// <param name="zLocalScal">�v���C���[��X���̃��[�J���X�P�[��</param>
    public void PlayerPushing(Vector3 playerPos, float zLocalScal)
    {
        //���������o�����Ƃ��\�ȏ�Ԃ��A�X�y�[�X��������Ă����珈�����s��
        if (PushableChecker(playerPos,zLocalScal) && Input.GetKey(KeyCode.Space))
        {
            //���Ԃ��v��
            _timer += Time.deltaTime;

            //��莞�Ԍo�߂�����
            if(_timer >= _interval)
            {
                //�����͂��ő�l�𒴂�����ŏ��l�Ƀ��Z�b�g
                if (_currentPushForce >= _maxPushForce)
                {
                    _currentPushForce = _minPushForce;
                }

                //�����͂𑝉�
                _currentPushForce += _minPushForce;

                //�^�C�}�[���Z�b�g
                _timer = 0;

                Debug.Log(_currentPushForce);

            }

        }

        //�������Ƃ��\�ȏ�Ԃ��A�X�y�[�X�L�[����w�����ꂽ�珈�����s��
        if (PushableChecker(playerPos, zLocalScal) && Input.GetKeyUp(KeyCode.Space))
        {
            //���C���q�b�g�����I�u�W�F�N�g��Rigidbody���擾
            Rigidbody boxRig = _hitInfo.rigidbody;

            //�������Z�̉e�����󂯂�悤�ɂ���
            boxRig.isKinematic = false;

            //���ɗ͂������ē�����
            boxRig.AddForce(new Vector3(_currentPushForce * zLocalScal, 0, 0), ForceMode.Impulse);

            //�����͂����Z�b�g
            _currentPushForce = 0;

        }

    }

    /// <summary>
    /// �����������Ƃ��ł��邩�𔻒肷�鏈��
    /// �����C�͈͓̔��ɔ�������Ή�����Ɣ��f����
    /// </summary>
    /// <param name="playerPos">�v���C���[�̃|�W�V����</param>
    /// <param name="zLocalScal">�v���C���[��X���̃��[�J���X�P�[��</param>
    /// <returns>true�Ȃ�Δ����������Ƃ��\�Afalse�Ȃ�Δ����������Ƃ͂ł��Ȃ�</returns>
    private bool PushableChecker(Vector3 playerPos, float zLocalScal)
    {        
        float maxRayDistans = 0.5f;         //���C�̎ˏo����
        Ray ray = default;

        //�v���C���[�̌����ɉ����ă��C�̎ˏo������ς���
        if (zLocalScal < 0)
        {
            ray = new Ray(playerPos, Vector3.left);     //������
        }
        else if (zLocalScal > 0)
        {
            ray = new Ray(playerPos, Vector3.right);    //�E����
        }

        //���C��`�悷��
        Debug.DrawRay(playerPos, Vector3.right * maxRayDistans * zLocalScal, Color.red);

        //���C���q�b�g���Ă����true��Ԃ������łȂ����false��Ԃ�
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
