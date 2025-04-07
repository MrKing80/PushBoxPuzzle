using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの移動に関するステータス")]
    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _jumpForce = 0;

    private PlayerMove _playerMove = default;
    private PlayerJump _playerJump = default;

    private void Start()
    {
        _playerMove = new PlayerMove(this.GetComponent<Rigidbody>(), _moveSpeed);
        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);
    }

    private void Update()
    {
        _playerMove.PlayerMovement();
        _playerJump.PlayerJumping();
    }
}
