using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerController : MonoBehaviour
{
    public Vector3 _offsetCenter;
    [SerializeField] private FirstPersonController _player;
    [SerializeField] private Animator _playrAnimator;
    [SerializeField] private Vector3 _playerMovemend;
    [SerializeField] private bool _isJump;
    [SerializeField] private bool _isGround;

    public void Update()
    {
        if (_player.enabled == true)
        {
            _playerMovemend = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _isJump = _player.enableJump;
            _isGround = _player.IsGrounded;

            _playrAnimator.SetFloat("X", _playerMovemend.x);
            _playrAnimator.SetFloat("Y", _playerMovemend.z);
            _playrAnimator.SetBool("IsGround", _isGround);
            if (Input.GetKey(KeyCode.Space))
            {
                _isJump = true;
            }
            else
                _isJump = false;
            _playrAnimator.SetBool("IsJump", _isJump);
        }
    }
}
