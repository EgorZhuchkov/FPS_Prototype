using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsController
{
  public class FirstPersonMovement : MonoBehaviour
  {
    public InputAction movementAction;
    public float speed = 5;
    private Vector2 _velocity;

    private void FixedUpdate()
    {
      _velocity = movementAction.ReadValue<Vector2>() * (speed * Time.fixedDeltaTime);
      transform.Translate(_velocity.x, 0, _velocity.y);
    }
    
    private void OnEnable() => 
      movementAction.Enable();

    private void OnDisable() => 
      movementAction.Disable();
  }
}