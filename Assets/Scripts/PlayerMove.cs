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
        // �L�[���͂����m����
        float moveDirection = Input.GetAxisRaw("Horizontal"); 
        
        // �v���C���[���ړ�������
        if (moveDirection == 0)
        {
            //�L�[���͂��Ȃ���Γ������Ȃ�
            _playerRigidbody.velocity = new Vector3(0, _playerRigidbody.velocity.y, 0);
        }
        else
        {
            //�L�[�̓��͂ɉ����ăv���C���[���ړ�������
            _playerRigidbody.velocity = new Vector3(moveDirection * _moveSpeed, _playerRigidbody.velocity.y, 0);
        }

        return moveDirection;   //�ړ������̏���Ԃ�
    }
}
