using UnityEngine;

/// <summary>
/// �����Î~���Ă��邩�̔�����s���X�N���v�g
/// </summary>
public class BoxIsSleeping : MonoBehaviour
{
    private Rigidbody _boxRigidbody = default;  // ����Rigidbody

    /// <summary>
    /// ����������������
    /// </summary>
    private void Awake()
    {
        // ���ɃA�^�b�`����Ă���Rigidbody���擾
        _boxRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ��莞�Ԃ��Ƃɔ�����s��
    /// </summary>
    private void FixedUpdate()
    {
        // �����Î~���Ă���̂��𔻒�
        if (_boxRigidbody.IsSleeping())
        {
            // �Î~���Ă���Ε������Z�̉e�����󂯂Ȃ��悤�ɂ���
            _boxRigidbody.isKinematic = true;
        }
    }
}
