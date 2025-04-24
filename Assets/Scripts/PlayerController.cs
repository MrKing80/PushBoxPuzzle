using Unity.VisualScripting;
using UnityEngine;
using TMPro;

/// <summary>
/// �v���C���[�̋������Ǘ�����N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̋����Ɋւ���X�e�[�^�X")]
    [SerializeField] private float _moveSpeed = 0;          // �ړ����x
    [SerializeField] private float _jumpForce = 0;          // �W�����v��
    [SerializeField] private float _maxPushForce = 0;       // ���������o���ő�̗�

    [SerializeField] private LayerMask _boxLayer = default; //���C���Փ˂��郌�C���[

    [SerializeField] private TMP_Text _showPower = default;

    private const int INVERTED_ORIENTATION = -1;            // �����𔽓]����p�̒萔

    private float _moveDirection = 0;                       // �ړ�����

    private float _zLocalScale = 0;                         // �v���C���[��Z���̃��[�J���X�P�[��
    private float _invertedZLocalScale = 0;                 // ���]���̃v���C���[��Z���̃��[�J���X�P�[��

    private float _interval = 1f;
    private float _timer = 0;

    private float _pushForce = 0;
    private bool _isPushed = false;

    private PlayerMove _playerMove = default;               // �v���C���[�̈ړ��Ɋւ���N���X
    private PlayerJump _playerJump = default;               // �v���C���[�̃W�����v�Ɋւ���N���X
    private PushBox _pushBox = default;                     // �v���C���[�̔��������o�������Ɋւ���N���X

    /// <summary>
    /// /���������s��
    /// </summary>
    private void Awake()
    {
        _playerMove = new PlayerMove(this.GetComponent<Rigidbody>(), _moveSpeed);   // �ړ��N���X��������
        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);   // �W�����v�N���X��������
        _pushBox = new PushBox(_maxPushForce, _boxLayer);                           // ���������o���N���X��������

        _zLocalScale = transform.localScale.z;                                      // �v���C���[��Z���̃��[�J���X�P�[�����擾
        _invertedZLocalScale = transform.localScale.z * INVERTED_ORIENTATION;       // �v���C���[��Z���̃��[�J���X�P�[�����擾�����]�������s��
    }

    /// <summary>
    /// �v���C���[�̃A�N�V�����̏����𖈃t���[���s��
    /// </summary>
    private void Update()
    {
        _isPushed = _pushBox.IsPushed;

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

        _playerJump.PlayerJumping(this.transform.position);                                     // �W�����v�̏������s��
        _pushForce = _pushBox.PlayerPushing(this.transform.position, transform.localScale.z);                // ���������o���������s��
        _moveDirection = _playerMove.PlayerMovement(_isPushed);                                 // �ړ��̏������s��

        Debug.Log(_pushForce);

        ChangeDirection();
        ChangeText();
    }

    /// <summary>
    /// �ړ��̌����ɉ����ăv���C���[�̃��[�J���X�P�[����ύX����
    /// </summary>
    private void ChangeDirection()
    {
        Vector3 playerLocalScale = this.transform.localScale;

        if (_moveDirection > 0)
        {
            this.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y, _zLocalScale);
        }
        else if (_moveDirection < 0)
        {
            this.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y, _invertedZLocalScale);
        }
    }

    private void ChangeText()
    {
        if(_pushForce > 0)
        {
            _showPower.text = _pushForce.ToString();
        }
        else if(_pushForce <= 0)
        {
            _showPower.text = "";
        }
    }
}
