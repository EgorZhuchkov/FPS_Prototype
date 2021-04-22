using Animations;
using UnityEngine;

namespace Weapons.General
{
  public class WeaponAnimator : MonoBehaviour, IAnimationStateReader
  {
    private static readonly int Scoped = Animator.StringToHash("Scoped");
    private static readonly int Charge = Animator.StringToHash("Charge");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    
    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _chargeStateHash = Animator.StringToHash("Charging");
    private readonly int _shootingStateHash = Animator.StringToHash("Shot");
    private readonly int _scopeStateHash = Animator.StringToHash("Scope");
    
    private Animator _animator;

    public AnimatorState State { get; private set; }

    public void BeginCharge() => 
      _animator.SetBool(Charge, true);

    public void CancelCharge() =>
      _animator.SetBool(Charge, false);

    public void PlayShoot() => 
      _animator.SetTrigger(Shoot);

    public void Aim() =>
      _animator.SetBool(Scoped, true);

    public void StopAim() =>
      _animator.SetBool(Scoped, false);

    private void Awake() => 
      _animator = GetComponent<Animator>();

    public void EnteredState(int stateHash) => 
      State = StateFor(stateHash);

    public void ExitedState(int stateHash)
    {
    }

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
        state = AnimatorState.Idle;
      else if (stateHash == _chargeStateHash)
        state = AnimatorState.Charge;
      else if (stateHash == _shootingStateHash)
        state = AnimatorState.Shooting;
      else if (stateHash == _scopeStateHash)
        state = AnimatorState.Scope;
      else
        state = AnimatorState.Unknown;
      
      return state;
    }
  }
}