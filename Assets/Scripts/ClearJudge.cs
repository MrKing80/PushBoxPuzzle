using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearJudge : MonoBehaviour
{
    [SerializeField] private LayerMask _boxLayer = default;
    private Rigidbody _hitRigidbody = default;
    private bool _isClear = false;
    private float _stopThreshold = 0.001f; // ���x�����̒l�ȉ��Ȃ�~�܂��Ă���Ƃ݂Ȃ�

    public bool IsClear
    {
        get { return _isClear; }
        private set { _isClear = value; }
    }

    private void FixedUpdate()
    {
        ThrowARay();
    }

    private void ThrowARay()
    {
        Vector3 offset = Vector3.down * 0.5f;
        float maxRayDistans = 0.5f;         //���C�̎ˏo����
        RaycastHit _hitInfo = default;      // ���C���q�b�g��������I�u�W�F�N�g�̏��
        Ray ray = new Ray(transform.position + offset, transform.up);

        Debug.DrawRay(transform.position + offset, transform.up * maxRayDistans, Color.black);      // ���C��`�悷��

        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {

            _hitRigidbody = _hitInfo.rigidbody;

            if (_hitRigidbody.velocity.magnitude < _stopThreshold && !_isClear)
            {
                _isClear = true;
                Debug.Log("�N���A");
            }
        }
        else if(!Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {
            _isClear = false;
        }


    }
}
