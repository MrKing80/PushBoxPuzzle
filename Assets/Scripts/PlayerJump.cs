using UnityEngine;

/// <summary>
/// �v���C���[�̃W�����v�Ɋւ���N���X
/// </summary>
public class PlayerJump
{
    private float _jumpForce = 0;                       // �W�����v��
    private bool _isGround = true;                      // �ڒn���Ă��邩
    private Rigidbody _playerRigidbody = default;       // �v���C���[��Rigidbody
    private RaycastHit _hitInfo = default;              // ���C�̃q�b�g�����I�u�W�F�N�g�̏����i�[����ϐ�

    /// <summary>
    /// PlayerJump�̃R���X�g���N�^
    /// �e�ϐ�������������
    /// </summary>
    /// <param name="playerRigidbody">PlayerController����󂯎�����v���C���[��Rigidbody</param>
    /// <param name="jumpForce">PlayerController����󂯎�����W�����v��</param>
    public PlayerJump(Rigidbody playerRigidbody, float jumpForce)
    {
        _jumpForce = jumpForce;
        _playerRigidbody = playerRigidbody;
    }

    /// <summary>
    /// �v���C���[�̃W�����v�Ɋւ��鏈��
    /// </summary>
    /// <param name="playerPos">�v���C���[�̈ʒu</param>
    public void PlayerJumping(Vector3 playerPos)
    {
        float maxRayDistans = 0.1f;                             // ���C�̎ˏo����
        Ray ray = new Ray(playerPos, Vector3.down);             //���C���΂�

        Debug.DrawRay(playerPos, Vector3.down * maxRayDistans, Color.red);      // ���C��`�悷��

        // ��΂������C�������Ƀq�b�g���Ă��邩
        if(Physics.Raycast(ray, out _hitInfo, maxRayDistans))
        {
            _isGround = true;   //�ڒn���Ă���
        }
        else
        {
            _isGround = false;  //�ڒn���Ă��Ȃ�
        }

        //W�L�[��������Ă��āA�Ȃ����ڒn���Ă��鎞�W�����v������
        if (Input.GetKeyDown(KeyCode.W) && _isGround)
        {
            _playerRigidbody.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
        }
    }
}
