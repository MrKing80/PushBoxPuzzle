using UnityEngine;

/// <summary>
/// �v���C���[�̔��������o���A�N�V�������Ǘ�����N���X
/// </summary>
public class PushBox : MonoBehaviour 
{
    private float _ratio = 5;                   // �����͂̍ő�l�ɑ΂��čŏ��l�����߂�̂Ɏg�p����l
    private float _maxPushForce = 0;            // �����͂̍ő�l
    private float _minPushForce = 0;            // �����͂̍ŏ��l
    private float _currentPushForce = 0;        // ���݂̉�����
    private float _tmpPushForce = 0;            // �\���p�ȂǂɈꎞ�ێ�����l

    private float _interval = 0.5f;             // �͂𑝉�������ۂ̃C���^�[�o��
    private float _timer = 0;                   // ���Ԃ��v������ϐ�

    private bool _isPushable = false;           // �����������Ƃ��ł��邩�ǂ���
    private bool _isPushed = false;             // ���������ꂽ���ǂ���

    private RaycastHit _hitInfo = default;      // ���C���q�b�g��������I�u�W�F�N�g�̏��

    /// <summary>
    /// �O�����牟���ꂽ��Ԃ��擾�E�ݒ肷��v���p�e�B
    /// </summary>
    public bool IsPushed
    {
        get { return _isPushed; }
        set { _isPushed = value; }
    }

    /// <summary>
    /// ���������͂̍ő�l���󂯎��v���p�e�B
    /// </summary>
    public float SetMaxPushForce
    {
        set { _maxPushForce = value; }
    }

    /// <summary>
    /// ���������͂�Ԃ��v���p�e�B
    /// </summary>
    public float GetPushForce
    {
        get { return _tmpPushForce; }
    }


    private void Start()
    {
        _minPushForce = _maxPushForce / _ratio;     //�󂯎�����͂���ɍŏ��l������
        _currentPushForce = _minPushForce;          //�ŏ��l���i�[������������
    }

    /// <summary>
    /// �v���C���[�����������o������
    /// </summary>
    /// <param name="boxLayer">���C���Փ˂��郌�C���[</param>
    public void PlayerPushing(LayerMask boxLayer)
    {
        Vector3 playerPos = this.transform.position;        //�v���C���[�̃|�W�V�������擾
        float zLocalScal = transform.localScale.z;          //�v���C���[��z���̃��[�J���X�P�[�����擾

        // �����Ȃ��󋵂ŃX�y�[�X�L�[��������Ă���A�܂��͉��������Ă��Ȃ��Ƃ�
        if (!PushableChecker(playerPos, zLocalScal, boxLayer))
        {
            // �^�C�}�[�����Z�b�g
            _timer = 0;

            // �ŏ��̗͂�ϐ��Ɋi�[
            _currentPushForce = _minPushForce;

            // �ϐ��̒��g��0�ɂ���
            _tmpPushForce = 0;

            return;
        }

        //���������o�����Ƃ��\�ȏ�Ԃ��A�X�y�[�X��������Ă����珈�����s��
        if (PushableChecker(playerPos, zLocalScal, boxLayer) && Input.GetKey(KeyCode.Space))
        {
            //���Ԃ��v��
            _timer += Time.deltaTime;

            //��莞�Ԍo�߂�����
            if (_timer >= _interval)
            {
                //�����͂��ő�l�𒴂�����ŏ��l�Ƀ��Z�b�g
                if (_currentPushForce >= _maxPushForce)
                {
                    _currentPushForce = _minPushForce;
                }

                //�����͂𑝉�
                _currentPushForce += _minPushForce;

                // �e�L�X�g�ɕ\������p�Ɍ��݂̗͂̒l���i�[
                _tmpPushForce = _currentPushForce;

                //�^�C�}�[���Z�b�g
                _timer = 0;

                return ;
            }

        }

        //�������Ƃ��\�ȏ�Ԃ��A�X�y�[�X�L�[����w�����ꂽ�珈�����s��
        if (PushableChecker(playerPos, zLocalScal, boxLayer) && Input.GetKeyUp(KeyCode.Space))
        {
            //���C���q�b�g�����I�u�W�F�N�g��Rigidbody���擾
            Rigidbody boxRig = _hitInfo.rigidbody;

            //�������Z�̉e�����󂯂�悤�ɂ���
            boxRig.isKinematic = false;

            //���ɗ͂������ē�����
            boxRig.AddForce(new Vector3(_currentPushForce * zLocalScal, 0, 0), ForceMode.Impulse);

            //�����͂����Z�b�g
            _currentPushForce = _minPushForce;

            _isPushed = true;
        }
    }

    /// <summary>
    /// �����������Ƃ��ł��邩�𔻒肷�鏈��
    /// �����C�͈͓̔��ɔ�������Ή�����Ɣ��f����
    /// </summary>
    /// <param name="playerPos">�v���C���[�̃|�W�V����</param>
    /// <param name="zLocalScal">�v���C���[��X���̃��[�J���X�P�[��</param>
    /// <param name="boxlayer">���C���Փ˂��郌�C���[</param>
    /// <returns>true�Ȃ�Δ����������Ƃ��\�Afalse�Ȃ�Δ����������Ƃ͂ł��Ȃ�</returns>
    private bool PushableChecker(Vector3 playerPos, float zLocalScal, LayerMask boxlayer)
    {
        float maxRayDistans = 0.5f;         //���C�̎ˏo����
        Ray ray = default;                  // ���C�̕ϐ���������

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

        //���C���q�b�g���Ă����true��Ԃ��A�����łȂ����false��Ԃ�
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
