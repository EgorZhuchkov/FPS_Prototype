using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsController
{
  [RequireComponent(typeof(GroundCheck))]
  public class Jump : MonoBehaviour
  {
    public InputAction jumpAction;
    public float jumpStrength = 2;
    public GroundCheck groundCheck;

    private Rigidbody _rigidbody;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
      jumpAction.performed += OnJump;
    }

    private void OnEnable() => 
      jumpAction.Enable();

    private void OnDisable() => 
      jumpAction.Disable();

    private void OnDestroy() => 
      jumpAction.performed -= OnJump;

    private void OnJump(InputAction.CallbackContext ctx)
    {
      if (groundCheck.isGrounded)
        _rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
    }

    private void Reset() =>
      groundCheck = GetComponentInChildren<GroundCheck>();
  }
}