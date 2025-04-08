using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIsSleeping : MonoBehaviour
{
    private Rigidbody _boxRigidbody = default;

    private void Start()
    {
        _boxRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_boxRigidbody.IsSleeping())
        {
            _boxRigidbody.isKinematic = true;
        }
    }
}
