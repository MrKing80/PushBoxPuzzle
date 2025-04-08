using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̈ړ��Ɋւ���N���X
/// </summary>
public class PlayerMove
{
    private float _moveSpeed = 0;                       // �ړ����x
    private Rigidbody _playerRigidbody = default;       // �v���C���[��Rigidbody

    /// <summary>
    /// PlayerMove�̃R���X�g���N�^
    /// �e�ϐ�������������
    /// </summary>
    /// <param name="playerRigidbody">PlayerController����󂯎����Rigidbody</param>
    /// <param name="moveSpeed">PlayerController����󂯎�����ړ����x</param>
    public PlayerMove(Rigidbody playerRigidbody, float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _playerRigidbody = playerRigidbody;
    }

    /// <summary>
    /// �v���C���[�̈ړ��̏������s��
    /// </summary>
    public float PlayerMovement()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");       // �L�[���͂����m����

        // ���͂ɂ���Ĉړ�������ς���
        if (moveDirection == 0)
        {
            _playerRigidbody.velocity = new Vector3(0, _playerRigidbody.velocity.y, 0);
        }
        else
        {
            _playerRigidbody.velocity = new Vector3(moveDirection * _moveSpeed, _playerRigidbody.velocity.y, 0);
        }

        return moveDirection;   //�ړ������̏���Ԃ�
    }
}
