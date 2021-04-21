using UnityEngine;
using System.Collections;

namespace SciFiArsenal
{
  public class SciFiProjectileScript : MonoBehaviour
  {
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;
    [HideInInspector] public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool _hasCollided = false;

    void Start()
    {
      projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
      projectileParticle.transform.parent = transform;
      if (muzzleParticle)
      {
        muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation);
        muzzleParticle.transform.rotation = transform.rotation * Quaternion.Euler(180, 0, 0);
        Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
      }
    }

    void OnCollisionEnter(Collision hit)
    {
      if (_hasCollided) 
        return;
      
      _hasCollided = true;
      
      if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHithit))
      {
        impactParticle = Instantiate(impactParticle, transform.position,
          Quaternion.FromToRotation(Vector3.up, raycastHithit.normal));
      }

      foreach (GameObject trail in trailParticles)
      {
        GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
        curTrail.transform.parent = null;
        Destroy(curTrail, 3f);
      }

      Destroy(projectileParticle, 3f);
      Destroy(impactParticle, 5f);
      Destroy(gameObject);

      ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
      for (int i = 1; i < trails.Length; i++)
      {
        ParticleSystem trail = trails[i];

        if (trail.gameObject.name.Contains("Trail"))
        {
          trail.transform.SetParent(null);
          Destroy(trail.gameObject, 2f);
        }
      }
    }
  }
}