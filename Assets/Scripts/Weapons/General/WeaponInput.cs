using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.General
{
  [RequireComponent(typeof(WeaponHolder))]
  public class WeaponInput : MonoBehaviour
  {
    public InputAction primaryWeaponAction;
    public InputAction secondaryWeaponAction;

    private WeaponHolder _weaponHolder;

    private void Awake()
    {
      primaryWeaponAction.performed += OnPrimaryWeaponAction;
      secondaryWeaponAction.performed += OnSecondaryWeaponAction;
      primaryWeaponAction.canceled += OnPrimaryWeaponActionCanceled;
      secondaryWeaponAction.canceled += OnSecondaryWeaponActionCanceled;
      _weaponHolder = GetComponent<WeaponHolder>();
    }

    private void OnEnable()
    {
      primaryWeaponAction.Enable();
      secondaryWeaponAction.Enable();
    }

    private void OnDisable()
    {
      primaryWeaponAction.Disable();
      secondaryWeaponAction.Disable();
    }

    private void OnDestroy()
    {
      primaryWeaponAction.performed -= OnPrimaryWeaponAction;
      secondaryWeaponAction.performed -= OnSecondaryWeaponAction;
    }

    private void OnPrimaryWeaponAction(InputAction.CallbackContext obj) => 
      _weaponHolder.PerformPrimaryAction();

    private void OnSecondaryWeaponAction(InputAction.CallbackContext obj) => 
      _weaponHolder.PerformSecondaryAction();

    private void OnPrimaryWeaponActionCanceled(InputAction.CallbackContext obj) => 
      _weaponHolder.CancelPrimaryAction();

    private void OnSecondaryWeaponActionCanceled(InputAction.CallbackContext obj) => 
      _weaponHolder.CancelSecondaryAction();
  }
}