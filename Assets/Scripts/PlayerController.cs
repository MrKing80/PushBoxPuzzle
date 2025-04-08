using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̋������Ǘ�����N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̋����Ɋւ���X�e�[�^�X")]
    [SerializeField] private float _moveSpeed = 0;      // �ړ����x
    [SerializeField] private float _jumpForce = 0;      // �W�����v��
    [SerializeField] private float _maxPushForce = 0;   // ���������o���ő�̗�

    private float _moveDirection = 0;                   // �ړ�����

    private const int INVERTED_ORIENTATION = -1;        // �����𔽓]����p�̒萔
    
    private float _xLocalScale = 0;                     // �v���C���[��X���̃��[�J���X�P�[��
    private float _invertedXLocalScale = 0;             // ���]���̃v���C���[��X���̃��[�J���X�P�[��
    
    private PlayerMove _playerMove = default;           // �v���C���[�̈ړ��Ɋւ���N���X
    private PlayerJump _playerJump = default;           // �v���C���[�̃W�����v�Ɋւ���N���X
    private PushBox _pushBox = default;                 // �v���C���[�̔��������o�������Ɋւ���N���X

    /// <summary>
    /// /���������s��
    /// </summary>
    private void Awake()
    {
        _playerMove = new PlayerMove(this.GetComponent<Rigidbody>(), _moveSpeed);   // �ړ��N���X��������
        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);   // �W�����v�N���X��������
        _pushBox = new PushBox(_maxPushForce);                                      // ���������o���N���X��������

        _xLocalScale = transform.localScale.x;                                      // �v���C���[��X���̃��[�J���X�P�[�����擾
        _invertedXLocalScale = transform.localScale.x * INVERTED_ORIENTATION;       // �v���C���[��X���̃��[�J���X�P�[�����擾�����]�������s��
    }

    /// <summary>
    /// �v���C���[�̃A�N�V�����̏����𖈃t���[���s��
    /// </summary>
    private void Update()
    {
        _moveDirection = _playerMove.PlayerMovement();                              // �ړ��̏������s��
        _playerJump.PlayerJumping(this.transform.position);                         // �W�����v�̏������s��
        _pushBox.PlayerPushing(this.transform.position, transform.localScale.x);    // ���������o���������s��

        ChangeDirection();
    }

    /// <summary>
    /// �ړ��̌����ɉ����ăv���C���[�̃��[�J���X�P�[����ύX����
    /// </summary>
    private void ChangeDirection()
    {
        Vector3 playerLocalScale = this.transform.localScale;

        if (_moveDirection > 0)
        {
            this.transform.localScale = new Vector3(_invertedXLocalScale, playerLocalScale.y, playerLocalScale.z);
        }
        else if (_moveDirection < 0)
        {
            this.transform.localScale = new Vector3(_xLocalScale, playerLocalScale.y, playerLocalScale.z);
        }
    }
}
