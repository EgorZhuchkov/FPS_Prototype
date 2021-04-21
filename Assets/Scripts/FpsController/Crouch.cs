using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace FpsController
{
  [RequireComponent(typeof(GroundCheck))]
  public class Crouch : MonoBehaviour
  {
    public InputAction crouchAction;
    public float crouchYLocalPosition = 1;
    public Transform headTransform;
    public CapsuleCollider bodyCollider;
    public GroundCheck groundCheck;

    private float _defaultHeadYLocalPosition;
    private float _defaultCapsuleColliderHeight;
    private bool _isCrouched;

    private void Awake() => 
      crouchAction.performed += OnCrouch;

    private void Start()
    {
      _defaultHeadYLocalPosition = headTransform.localPosition.y;
      if (bodyCollider)
        _defaultCapsuleColliderHeight = bodyCollider.height;
    }
    
    private void OnEnable() => 
      crouchAction.Enable();

    private void OnDisable() => 
      crouchAction.Disable();

    private void OnDestroy() => 
      crouchAction.performed -= OnCrouch;


    private void OnCrouch(InputAction.CallbackContext obj)
    {
      if (!groundCheck.isGrounded)
        return;
      
      if (!_isCrouched)
      {
        var localPosition = headTransform.localPosition;
        localPosition = new Vector3(localPosition.x, crouchYLocalPosition, localPosition.z);
        headTransform.localPosition = localPosition;
        if (bodyCollider)
        {
          bodyCollider.height = _defaultCapsuleColliderHeight - (_defaultHeadYLocalPosition - crouchYLocalPosition);
          bodyCollider.center = Vector3.up * bodyCollider.height * .5f;
        }
      }
      else
      {
        var localPosition = headTransform.localPosition;
        localPosition = new Vector3(localPosition.x, _defaultHeadYLocalPosition, localPosition.z);
        headTransform.localPosition = localPosition;
        if (bodyCollider)
        {
          bodyCollider.height = _defaultCapsuleColliderHeight;
          bodyCollider.center = Vector3.up * bodyCollider.height * .5f;
        }
      }

      _isCrouched = !_isCrouched;
    }

    private void Reset()
    {
      headTransform = GetComponentInChildren<Camera>().transform;
      bodyCollider = GetComponentInChildren<CapsuleCollider>();
      groundCheck = GetComponentInChildren<GroundCheck>();
    }
  }
}