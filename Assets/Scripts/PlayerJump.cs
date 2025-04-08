using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    private Rigidbody _playerRigidbody = default;
    private float _jumpForce = 0;
    private bool _isGround = true;
    private RaycastHit _hitInfo = default;

    public PlayerJump(Rigidbody playerRigidbody, float jumpForce)
    {
        _jumpForce = jumpForce;
        _playerRigidbody = playerRigidbody;
    }

    public void PlayerJumping(Vector3 playerPos)
    {
        Vector3 origin = playerPos;
        float maxRayDistans = 1f;

        Ray ray = new Ray(origin,Vector3.down);

        Debug.DrawRay(origin, Vector3.down * maxRayDistans, Color.red);

        if(Physics.Raycast(ray, out _hitInfo, maxRayDistans))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && _isGround)
        {
            _playerRigidbody.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
        }
    }
}
