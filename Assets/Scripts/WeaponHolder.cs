using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
  public Weapon[] weapons;
  public Weapon CurrentWeapon => weapons[_currentWeaponIndex];
  
  private int _currentWeaponIndex;

  public void SwitchToNextWeapon()
  {
    weapons[_currentWeaponIndex].gameObject.SetActive(false);
    
    _currentWeaponIndex++;
    if (_currentWeaponIndex > weapons.Length - 1)
      _currentWeaponIndex = 0;
    
    weapons[_currentWeaponIndex].gameObject.SetActive(true);
  }

  public void SwitchToPreviousWeapon()
  {
    weapons[_currentWeaponIndex].gameObject.SetActive(false);
    
    _currentWeaponIndex--;
    if (_currentWeaponIndex < 0)
      _currentWeaponIndex = weapons.Length - 1;
    
    weapons[_currentWeaponIndex].gameObject.SetActive(true);
  }
}
