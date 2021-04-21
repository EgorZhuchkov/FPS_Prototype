using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Weapons
{
  [RequireComponent(typeof(WeaponHolder))]
  public class WeaponInput : MonoBehaviour
  {
    [FormerlySerializedAs("fireAction")] public InputAction primaryWeaponAction;
    [FormerlySerializedAs("aimAction")] public InputAction secondaryWeaponAction;
    public InputAction changeWeaponAction;

    private WeaponHolder _weaponHolder;

    private void Awake()
    {
      primaryWeaponAction.performed += OnPrimaryWeaponAction;
      secondaryWeaponAction.performed += OnSecondaryWeaponAction;
      primaryWeaponAction.canceled += OnPrimaryWeaponActionCanceled;
      secondaryWeaponAction.canceled += OnSecondaryWeaponActionCanceled;
      _weaponHolder = GetComponent<WeaponHolder>();
    }

    private void OnPrimaryWeaponActionCanceled(InputAction.CallbackContext obj)
    {
      _weaponHolder.CurrentWeapon.CancelPrimaryAction();
    }

    private void OnSecondaryWeaponActionCanceled(InputAction.CallbackContext obj)
    {
      _weaponHolder.CurrentWeapon.CancelSecondaryAction();
    }

    private void Update()
    {
      ChangeWeapon(changeWeaponAction.ReadValue<float>());
    }

    private void OnEnable()
    {
      primaryWeaponAction.Enable();
      secondaryWeaponAction.Enable();
      changeWeaponAction.Enable();
    }

    private void OnDisable()
    {
      primaryWeaponAction.Disable();
      secondaryWeaponAction.Disable();
      changeWeaponAction.Disable();
    }

    private void OnDestroy()
    {
      primaryWeaponAction.performed -= OnPrimaryWeaponAction;
      secondaryWeaponAction.performed -= OnSecondaryWeaponAction;
      primaryWeaponAction.canceled -= OnPrimaryWeaponActionCanceled;
      secondaryWeaponAction.canceled -= OnSecondaryWeaponActionCanceled;
    }

    private void OnPrimaryWeaponAction(InputAction.CallbackContext obj)
    {
      _weaponHolder.CurrentWeapon.PerformPrimaryAction();
    }

    private void OnSecondaryWeaponAction(InputAction.CallbackContext obj)
    {
      _weaponHolder.CurrentWeapon.PerformSecondaryAction();
    }

    private void ChangeWeapon(float scrollDelta)
    {
      if (scrollDelta > 0)
        _weaponHolder.SwitchToNextWeapon();
      else if (scrollDelta < 0)
        _weaponHolder.SwitchToPreviousWeapon();
    }
  }
}