using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsController
{
  public class FirstPersonLook : MonoBehaviour
  {
    public InputAction lookAction;
    public float sensitivity = 1;
    public float smoothing = 2;
    public Transform character;

    private Vector2 _currentMouseLook;
    private Vector2 _appliedMouseDelta;

    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    private void Update()
    {
      Vector2 mouseDelta = lookAction.ReadValue<Vector2>() * (sensitivity * smoothing);
      _appliedMouseDelta = Vector2.Lerp(_appliedMouseDelta, mouseDelta, 1 / smoothing);
      _currentMouseLook += _appliedMouseDelta;
      _currentMouseLook.y = Mathf.Clamp(_currentMouseLook.y, -90, 90);
      
      transform.localRotation = Quaternion.AngleAxis(-_currentMouseLook.y, Vector3.right);
      character.localRotation = Quaternion.AngleAxis(_currentMouseLook.x, Vector3.up);
    }

    private void OnEnable() => 
      lookAction.Enable();

    private void OnDisable() => 
      lookAction.Disable();

    private void Reset() =>
      character = GetComponentInParent<FirstPersonMovement>().transform;
  }
}