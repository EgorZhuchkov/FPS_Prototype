using UnityEngine;

namespace VFX.Scripts
{
  public class SelfDestruct : MonoBehaviour
  {
    public float lifetime = 1.0f;

    private void Awake() => 
      Destroy(gameObject, lifetime);
  }
}
