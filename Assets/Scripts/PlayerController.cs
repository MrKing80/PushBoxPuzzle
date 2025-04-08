using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの移動に関するステータス")]
    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _jumpForce = 0;

    private float _moveDirection = 0;

    private const int NORMAL_ORIENTATION = 1;
    private const int INVERTED_ORIENTATION = -1;
    private float _xLocalScale = 0;
    private float _invertedXLocalScale = 0; 
    private PlayerMove _playerMove = default;
    private PlayerJump _playerJump = default;
    private PushBox _pushBox = default;

    private void Start()
    {
        _playerMove = new PlayerMove(this.GetComponent<Rigidbody>(), _moveSpeed);
        _playerJump = new PlayerJump(this.GetComponent<Rigidbody>(), _jumpForce);
        _pushBox = new PushBox();

        _xLocalScale = transform.localScale.x * NORMAL_ORIENTATION;
        _invertedXLocalScale = transform.localScale.x * INVERTED_ORIENTATION;
    }

    private void Update()
    {
        Vector3 playerLocalScale = this.transform.localScale;

        _moveDirection = _playerMove.PlayerMovement();
        _playerJump.PlayerJumping(this.transform.position);
        _pushBox.PlayerPushing(this.transform.position, transform.localScale.x) ;

        if(_moveDirection > 0)
        {
            this.transform.localScale = new Vector3(_invertedXLocalScale, playerLocalScale.y, playerLocalScale.z);
        }
        else if(_moveDirection < 0)
        {
            this.transform.localScale = new Vector3(_xLocalScale, playerLocalScale.y, playerLocalScale.z);
        }
    }
}
