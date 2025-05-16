using Unity.VisualScripting;
using UnityEngine;
using TMPro;

/// <summary>
/// �v���C���[�̋������Ǘ�����N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�֌W�̃X�N���v�g")]
    [SerializeField] private PlayerMove _playerMove = default;               // �v���C���[�̈ړ��Ɋւ���N���X
    [SerializeField] private PlayerJump _playerJump = default;               // �v���C���[�̃W�����v�Ɋւ���N���X
    [SerializeField] private PushBox _pushBox = default;                     // �v���C���[�̔��������o�������Ɋւ���N���X


    [Header("�v���C���[�̋����Ɋւ���X�e�[�^�X")]
    [SerializeField] private float _moveSpeed = 0;          // �ړ����x
    [SerializeField] private float _jumpForce = 0;          // �W�����v��
    [SerializeField] private float _maxPushForce = 0;       // ���������o���ő�̗�

    [SerializeField] private LayerMask _boxLayer = default; //���C���Փ˂��郌�C���[

    private Rigidbody _playerRigidoby = default;

    private float _interval = 1f;                           // �����ꂽ��Ԃ̉����܂ł̎��ԊԊu
    private float _timer = 0;                               // �o�ߎ��Ԃ��L�^����^�C�}�[

    private bool _isPushed = false;                         // �v���C���[�����������Ă����Ԃ��ǂ���

    /// <summary>
    /// /���������s��
    /// </summary>
    private void Awake()
    {
        _playerRigidoby = this.GetComponent<Rigidbody>();

        _playerMove.Rigidbody = _playerRigidoby;    //playerMove�Ƀ��W�b�h�{�f�B�̏���n��
        _pushBox.SetMaxPushForce = _maxPushForce;   //pushBox�ɔ��������o���ő�̗͂�n��

        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);   // �W�����v�N���X��������

    }

    /// <summary>
    /// �v���C���[�̃A�N�V�����̏����𖈃t���[���s��
    /// </summary>
    private void Update()
    {
        _isPushed = _pushBox.IsPushed; // ������Ԃ��擾

        if (_isPushed)
        {
            //���Ԃ��v��
            _timer += Time.deltaTime;

            //��莞�Ԍo�߂�����
            if (_timer >= _interval)
            {
                _isPushed = false;

                _pushBox.IsPushed = _isPushed;

                //�^�C�}�[���Z�b�g
                _timer = 0;
            }
        }

        _playerMove.PlayerMovement(_moveSpeed, _isPushed);      // �ړ��̏������s��
        _playerJump.PlayerJumping(this.transform.position);     // �W�����v�̏������s��
        _pushBox.PlayerPushing(_boxLayer);                      // ���������o���������s��

    }
}
