using System.Collections;
using UnityEngine;

namespace Weapons
{
  public class ProjectileEffect : MonoBehaviour
  {
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;

    private bool _hasCollided;
    private const float MuzzleLifetime = 1.5f;
    private const float ImpactLifetime = 5.0f;
    private const float ParticleLifetime = 3.0f;

    private void Awake()
    {
      InstantiateParticles();
      DisableParticles();
    }

    public void Activate()
    {
      ResetProjectile();

      projectileParticle.SetActive(true);
      muzzleParticle.SetActive(true);
      muzzleParticle.transform.position = transform.position;
      muzzleParticle.transform.rotation = transform.rotation * Quaternion.Euler(180, 0, 0);
      StartCoroutine(DisableAfterDelay(muzzleParticle, MuzzleLifetime));
    }

    private void OnCollisionEnter(Collision other)
    {
      if (_hasCollided)
        return;

      _hasCollided = true;

      impactParticle.transform.position = transform.position;
      impactParticle.transform.rotation = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);
      impactParticle.SetActive(true);

      StartCoroutine(DisableAfterDelay(projectileParticle, ParticleLifetime));
      StartCoroutine(DisableAfterDelay(impactParticle, ImpactLifetime));
    }

    private void InstantiateParticles()
    {
      projectileParticle = Instantiate(projectileParticle, transform);
      muzzleParticle = Instantiate(muzzleParticle, transform.parent);
      impactParticle = Instantiate(impactParticle, transform.parent);
    }

    private void DisableParticles()
    {
      projectileParticle.SetActive(false);
      muzzleParticle.SetActive(false);
      impactParticle.SetActive(false);
    }

    private void ResetProjectile()
    {
      StopAllCoroutines();
      DisableParticles();
    }

    private IEnumerator DisableAfterDelay(GameObject target, float delay)
    {
      yield return new WaitForSeconds(delay);
      target.SetActive(false);
    }
  }
}