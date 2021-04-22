using Interactions;
using UnityEngine;

namespace Weapons.General
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(Collider))]
  public class PickableWeaponItem : MonoBehaviour, IInteractable
  {
    public WeaponId weaponId;
    public WeaponHolder holder;
    public float dropForce = 2.0f;
    private Rigidbody _rb;

    private void Awake() => 
      _rb = GetComponent<Rigidbody>();

    private void Reset() => 
      holder = FindObjectOfType<WeaponHolder>();

    public void Interact() => 
      PickUp();

    private void PickUp()
    {
      gameObject.SetActive(false);
      holder.PickUpWeapon(weaponId.id);
    }

    public void Drop(Vector3 from, Vector3 direction)
    {
      gameObject.SetActive(true);
      transform.position = from + direction * 0.3f;
      _rb.AddForce(direction * dropForce);
    }
  }
}