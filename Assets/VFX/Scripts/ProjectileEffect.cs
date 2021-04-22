using UnityEngine;
using UnityEngine.Serialization;

namespace VFX.Scripts
{
  public class ProjectileEffect : MonoBehaviour
  {
    [FormerlySerializedAs("impactParticle")]
    public GameObject impactParticlePrefab;

    [FormerlySerializedAs("projectileParticle")]
    public GameObject projectileParticlePrefab;

    [FormerlySerializedAs("muzzleParticle")]
    public GameObject muzzleParticlePrefab;

    private GameObject _projectileParticle;

    private bool _hasCollided;

    public void Instantiate()
    {
      _hasCollided = false;
      _projectileParticle = Instantiate(projectileParticlePrefab, transform);

      Instantiate(muzzleParticlePrefab, transform.position, transform.rotation * Quaternion.Euler(180, 0, 0),
        transform.parent);
    }

    public void OnCollision(Vector3 impactNormal)
    {
      if (_hasCollided)
        return;

      _hasCollided = true;

      Instantiate(impactParticlePrefab, transform.position,
        Quaternion.FromToRotation(Vector3.up, impactNormal), transform.parent);

      Destroy(_projectileParticle);
    }
  }
}