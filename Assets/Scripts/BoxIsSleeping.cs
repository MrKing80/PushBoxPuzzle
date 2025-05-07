using UnityEngine;

/// <summary>
/// �����Î~���Ă��邩�̔�����s���X�N���v�g
/// </summary>
public class BoxIsSleeping : MonoBehaviour
{
    // ����Rigidbody���i�[���邽�߂̕ϐ�
    private Rigidbody _boxRigidbody = default;

    /// <summary>
    /// ����������������
    /// </summary>
    private void Awake()
    {
        // ���ɃA�^�b�`����Ă���Rigidbody���擾
        _boxRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ��莞�Ԃ��Ƃɔ�����s���iFixedUpdate�͕������Z�̃^�C�~���O�ŌĂ΂��j
    /// </summary>
    private void FixedUpdate()
    {
        // ���������I�Ɂu�Î~���Ă���v���ǂ������`�F�b�N
        if (_boxRigidbody.IsSleeping())
        {
            // �Î~���Ă���ꍇ�A�������Z�𖳌������čœK����}��
            _boxRigidbody.isKinematic = true;
        }
    }
}
