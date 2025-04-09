using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox
{
    private float _ratio = 10;
    private float _maxPushForce = 0;
    private float _minPushForce = 0;
    private float _currentPushForce = 0;
    private float _interval = 0.5f;
    private float _timer = 0;
    private bool _isPushable = false;
    private RaycastHit _hitInfo = default;

    public PushBox(float maxPushForce)
    {
        _maxPushForce = maxPushForce;
        _minPushForce = _maxPushForce / _ratio;
    }

    public void PlayerPushing(Vector3 playerPos, float xLocalScal)
    {
        if (PushableChecker(playerPos,xLocalScal) && Input.GetKey(KeyCode.Space))
        {

            _timer += Time.deltaTime;

            if(_timer >= _interval)
            {
                if (_currentPushForce >= _maxPushForce)
                {
                    _currentPushForce = _minPushForce;
                }

                _currentPushForce += _minPushForce;

                _timer = 0;

                Debug.Log(_currentPushForce);

            }

        }

        if (PushableChecker(playerPos, xLocalScal) && Input.GetKeyUp(KeyCode.Space))
        {
            Rigidbody boxRig = _hitInfo.rigidbody;
            boxRig.isKinematic = false;
            boxRig.AddForce(new Vector3(_currentPushForce * -xLocalScal, 0, 0), ForceMode.Impulse);

            _currentPushForce = 0;

        }

    }

    private bool PushableChecker(Vector3 playerPos, float xLocalScal)
    {
        Vector3 origin = playerPos;
        
        float maxRayDistans = 0.7f;

        Ray ray = default;

        if (xLocalScal > 0)
        {
            ray = new Ray(origin, Vector3.left);
        }
        else if (xLocalScal < 0)
        {
            ray = new Ray(origin, Vector3.right);
        }

        Debug.DrawRay(origin, Vector3.left * maxRayDistans * xLocalScal, Color.red);

        if (Physics.Raycast(ray, out _hitInfo, maxRayDistans))
        {
            _isPushable = true;
        }
        else
        {
            _isPushable = false;
        }

        //Debug.Log(_isPushable);

        return _isPushable;

    }
}
