using System.Collections.Generic;
using UnityEngine;

namespace Weapons.General
{
  public class WeaponHolder : MonoBehaviour
  {
    public Weapon currentWeapon;
    public AudioSource equipAudio;
    
    private readonly Dictionary<string, Weapon> _characterWeapons = new Dictionary<string, Weapon>();

    private void Awake()
    {
      var weapons = GetComponentsInChildren<Weapon>(true);
      foreach (Weapon weapon in weapons)
      {
        _characterWeapons.Add(weapon.weaponId.id, weapon);
        weapon.gameObject.SetActive(false);
      }
    }

    public void PickUpWeapon(string weaponId)
    {
      if(currentWeapon)
        Drop(currentWeapon);
      EquipNewWeapon(weaponId);
    }

    public void PerformPrimaryAction()
    { 
      if(currentWeapon)
        currentWeapon.PerformPrimaryAction();
    }

    public void PerformSecondaryAction()
    { 
      if(currentWeapon)
        currentWeapon.PerformSecondaryAction();
    }

    public void CancelPrimaryAction()
    {
      if(currentWeapon)
        currentWeapon.CancelPrimaryAction();
    }

    public void CancelSecondaryAction()
    {
      if(currentWeapon)
        currentWeapon.CancelSecondaryAction();
    }

    private void Drop(Weapon weapon)
    {
      weapon.pickableWeapon.Drop(transform.position, transform.forward);
      weapon.gameObject.SetActive(false);
    }

    private void EquipNewWeapon(string weaponId)
    {
      _characterWeapons[weaponId].gameObject.SetActive(true);
      currentWeapon = _characterWeapons[weaponId];
      equipAudio.Play();
    }
  }
}
