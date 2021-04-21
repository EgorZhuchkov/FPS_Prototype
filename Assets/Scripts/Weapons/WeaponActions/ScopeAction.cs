using System;
using System.Collections;
using UnityEngine;

namespace Weapons.WeaponActions
{
  public class ScopeAction : WeaponAction
  {
    public WeaponAnimator weaponAnimator;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public GameObject scopedOverlay;
    public GameObject crosshair;
    public float scopedFov = 15.0f;
    
    private float _defaultFov;

    private void Awake() => 
      _defaultFov = mainCamera.fieldOfView;

    public override void Perform()
    {
      StopAllCoroutines();
      StartCoroutine(ChangeScopeState(true));
    }

    public override void Cancel()
    {
      StopAllCoroutines();
      StartCoroutine(ChangeScopeState(false));
    }

    private IEnumerator ChangeScopeState(bool scoped)
    {
      crosshair.SetActive(!scoped);

      if (scoped)
      {
        weaponAnimator.Aim();
        yield return new WaitForSeconds(.15f);
      }
      else
      {
        weaponAnimator.StopAim();
      }

      weaponCamera.SetActive(!scoped);
      scopedOverlay.SetActive(scoped);
      mainCamera.fieldOfView = scoped ? scopedFov : _defaultFov;
    }

    private void Reset()
    {
      weaponAnimator = GetComponent<WeaponAnimator>();
    }
  }
}