using System;
using System.Collections;
using UnityEngine;

namespace Weapons.WeaponActions
{
  public class ScopeAction : WeaponAction
  {
    private static readonly int Scoped = Animator.StringToHash("Scoped");
    
    public Animator weaponAnimator;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public GameObject scopedOverlay;
    public GameObject crosshair;
    public float scopedFov = 15.0f;

    private bool _scoped;
    private float _defaultFov;

    private void Awake() => 
      _defaultFov = mainCamera.fieldOfView;

    public override void Perform()
    {
      _scoped = !_scoped;

      StopAllCoroutines();
      StartCoroutine(ChangeScopeState(_scoped));
    }
  
    private IEnumerator ChangeScopeState(bool scoped)
    {
      weaponAnimator.SetBool(Scoped, scoped);
      crosshair.SetActive(!scoped);

      if (scoped)
        yield return new WaitForSeconds(.15f);

      weaponCamera.SetActive(!scoped);
      scopedOverlay.SetActive(scoped);
      mainCamera.fieldOfView = scoped ? scopedFov : _defaultFov;
    }

    private void Reset()
    {
      weaponAnimator = GetComponent<Animator>();
    }
  }
}