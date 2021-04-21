using UnityEngine;

namespace FpsController
{
  public class GroundCheck : MonoBehaviour
  {
    public float maxGroundDistance = .3f;
    public bool isGrounded;

    private void LateUpdate() => 
      isGrounded = Physics.Raycast(transform.position, Vector3.down, maxGroundDistance);

    private void OnDrawGizmosSelected()
    {
      if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, maxGroundDistance))
        Debug.DrawLine(transform.position, hit.point, Color.white);
      else
        Debug.DrawLine(transform.position, transform.position + Vector3.down * maxGroundDistance, Color.red);
    }
  }
}