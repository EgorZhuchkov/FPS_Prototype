using UnityEngine;
using Weapons.WeaponActions;

namespace Weapons
{
  public class Weapon : MonoBehaviour
  {
    public WeaponAction primaryAction;
    public WeaponAction secondaryAction;

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
  }
}