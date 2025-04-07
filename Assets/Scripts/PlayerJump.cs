using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    private float _jumpForce = 0;
    private Rigidbody _playerRigidbody = default;

    public PlayerJump(Rigidbody playerRigidbody, float jumpForce)
    {
        _jumpForce = jumpForce;
        _playerRigidbody = playerRigidbody;
    }

    public void PlayerJumping()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            //‚±‚±‚ÉƒWƒƒƒ“ƒv‚Ìˆ—addforce‚ğg‚¤—\’è
        }
    }
}
