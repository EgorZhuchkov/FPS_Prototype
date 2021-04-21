using UnityEngine;

namespace Weapons.WeaponActions
{
    public abstract class WeaponAction : MonoBehaviour
    {
        public abstract void Perform();
        public abstract void Cancel();
    }
}