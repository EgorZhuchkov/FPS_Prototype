using FpsController;
using UnityEngine;
using Weapons.General;

namespace Weapons.WeaponActions
{
  public class ScopeAction : WeaponAction
  {
    public WeaponAnimator weaponAnimator;
    public FirstPersonLook fpsLook;
    public float scopedSensitivity = 0.05f;

    private float _defaultSensitivity;

    private void Awake() => 
      _defaultSensitivity = fpsLook.sensitivity;

    public override void Perform() => Aim();

    public override void Cancel() => StopAim();

    private void Reset()
    {
      weaponAnimator = GetComponent<WeaponAnimator>();
      if (!(Camera.main is null))
        fpsLook = Camera.main.GetComponent<FirstPersonLook>();
    }

    private void Aim()
    {
      weaponAnimator.Aim();
      fpsLook.sensitivity = scopedSensitivity;
    }

    private void StopAim()
    {
      weaponAnimator.StopAim();
      fpsLook.sensitivity = _defaultSensitivity;
    }
  }
}