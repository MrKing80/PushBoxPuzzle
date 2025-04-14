using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearJudge : MonoBehaviour
{
    [SerializeField] private LayerMask _boxLayer = default;
    private Rigidbody _hitRigidbody = default;
    private bool _isClear = false;

    private void FixedUpdate()
    {
        ThrowARay();
    }

    private void ThrowARay()
    {
        Vector3 offset = Vector3.down * 0.1f;
        float maxRayDistans = 0.5f;         //���C�̎ˏo����
        RaycastHit _hitInfo = default;      // ���C���q�b�g��������I�u�W�F�N�g�̏��
        Ray ray = new Ray(transform.position + offset,transform.up);

        Debug.DrawRay(transform.position + offset, transform.up * maxRayDistans, Color.black);      // ���C��`�悷��

        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans, _boxLayer))
        {
            Debug.Log("aaa");

            _hitRigidbody = _hitInfo.rigidbody;

            if(_hitRigidbody.IsSleeping())
            {
                _isClear = true;
                Debug.Log("�N���A");
            }
        }
        

    }
}
