using UnityEngine;

public class ClearJudge : MonoBehaviour
{
    [SerializeField] private LayerMask _boxLayer = default;     // ���C���q�b�g���郌�C���[
    private Rigidbody _hitRigidbody = default;                  // �q�b�g��������̃��W�b�h�{�f�B
    private bool _isPressed = false;                            // �X�C�b�`�������ꂽ��
    private float _stopThreshold = 0.001f;                      // ���x�����̒l�ȉ��Ȃ�~�܂��Ă���Ƃ݂Ȃ�

    /// <summary>
    /// �X�C�b�`�̏�Ԃ�Ԃ��v���p�e�B
    /// </summary>
    public bool GetIsPressed
    {
        get { return _isPressed; }
    }

    /// <summary>
    /// ��莞�Ԃ��ƂɃ��C���΂��A������Ƃ�
    /// </summary>
    private void FixedUpdate()
    {
        ThrowARay();
    }

    /// <summary>
    /// ���C���ˏo���A�X�C�b�`�������ꂽ��������s�����\�b�h
    /// </summary>
    private void ThrowARay()
    {
        Vector3 offset = Vector3.down * 0.5f;                               // �ˏo�ʒu�𒲐�����
        float maxRayDistans = 0.5f;                                         // ���C�̎ˏo����
        RaycastHit _hitInfo = default;                                      // ���C���q�b�g��������I�u�W�F�N�g�̏��
        Ray ray = new Ray(transform.position + offset, transform.up);       // ���C���΂�

        Debug.DrawRay(transform.position + offset, transform.up * maxRayDistans, Color.black);      // ���C��`�悷��

        // ���C���q�b�g���Ă��邩
        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {
            //�q�b�g��������̃��W�b�h�{�f�B���擾
            _hitRigidbody = _hitInfo.rigidbody;

            //�q�b�g�������肪�~�܂��Ă���A���X�C�b�`��������Ă��Ȃ��ꍇ
            if (_hitRigidbody.velocity.magnitude < _stopThreshold && !_isPressed)
            {
                //�X�C�b�`��������Ă����Ԃɂ���
                _isPressed = true;
            }
        }
        //���C���q�b�g���Ă��Ȃ���΃X�C�b�`��������Ă��Ȃ���Ԃɂ���
        else if(!Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {
            _isPressed = false;
        }
    }
}
