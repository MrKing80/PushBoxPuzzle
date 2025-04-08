using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox
{
    private bool _isPushable = false;
    private RaycastHit _hitInfo = default;

    public PushBox(){ }

    public void PlayerPushing(Vector3 playerPos, float xLocalScal)
    {
        if (PushableChecker(playerPos,xLocalScal) && Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody boxRig = _hitInfo.rigidbody;
            boxRig.isKinematic = false;
            boxRig.AddForce(new Vector3(10,0,0),ForceMode.Impulse);
        }
    }

    private bool PushableChecker(Vector3 playerPos, float xLocalScal)
    {
        Vector3 origin = playerPos;
        float maxRayDistans = 0.5f;
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

        return _isPushable;

    }
}
