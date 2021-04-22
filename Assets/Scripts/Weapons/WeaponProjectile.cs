using System.Collections;
using UnityEngine;

namespace Weapons
{
  [RequireComponent(typeof(ProjectileEffect), typeof(Rigidbody))]
  public class WeaponProjectile : MonoBehaviour
  {
    public ProjectileEffect effect;

    private float _damage;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Rigidbody _rb;

    private void Awake() => 
      _rb = GetComponent<Rigidbody>();

    public void ResetProjectile()
    {
      
      _rb.velocity = Vector3.zero;
      _rb.angularVelocity = Vector3.zero;
    }

    public void Shoot(Vector3 from, Vector3 to, float damage, float speed)
    {
      _damage = damage;
      _startPosition = from;
      _targetPosition = to;

      LaunchProjectile(speed);
      StartCoroutine(DisableWhenReachedDestination());
    }

    public void OnCollisionEnter(Collision other)
    {
      DealDamage(other.gameObject);
      gameObject.SetActive(false);
    }

    private IEnumerator DisableWhenReachedDestination()
    {
      while (!ReachedDestination())
        yield return new WaitForFixedUpdate();
      gameObject.SetActive(false);

      bool ReachedDestination()
      {
        return Vector3.Distance(transform.position, _targetPosition) <=
               Vector3.Distance(_startPosition, transform.position);
      }
    }

    private void DealDamage(GameObject target)
    {
      //TODO
      Debug.Log($"{target.name} hit");
    }

    private void Reset() =>
      effect = GetComponent<ProjectileEffect>();

    private void LaunchProjectile(float speed)
    {
      transform.position = _startPosition;
      transform.LookAt(_targetPosition);
      _rb.AddForce(transform.forward * speed);
      effect.Activate();
    }
  }
}