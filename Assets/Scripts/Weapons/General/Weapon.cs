using UnityEngine;
using Weapons.WeaponActions;

namespace Weapons.General
{
  public class Weapon : MonoBehaviour
  {
    public WeaponAction primaryAction;
    public WeaponAction secondaryAction;
    public WeaponId weaponId;
    public PickableWeaponItem pickableWeapon;

    public void PerformPrimaryAction()
    {
      if (primaryAction != null) 
        primaryAction.Perform();
    }

    public void PerformSecondaryAction()
    {
      if(secondaryAction != null)
        secondaryAction.Perform();
    }

    public void CancelPrimaryAction()
    {
      if (primaryAction != null) 
        primaryAction.Cancel();
    }

    public void CancelSecondaryAction()
    {
      if(secondaryAction != null)
        secondaryAction.Cancel();
    }

    private void Reset() => 
      weaponId = GetComponent<WeaponId>();
  }
}