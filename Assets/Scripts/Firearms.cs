using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearms : MonoBehaviour
{
    protected int magCapacityFull;
    protected int remainingAmmo;

    protected bool isSingleFire = true;

    
    public int MagCapacityFull {
        get {
            return magCapacityFull;
        }
    }

    public int RemainingAmmo {
        get {
            return remainingAmmo;
        }
    }

    public int ConsumingAmmo {
        set {
            remainingAmmo -= value;
            if(remainingAmmo < 0) {
                remainingAmmo = 0;
            }
        }
    }

    public int ReloadingAmmo {
        set {
            if(remainingAmmo == 0) {
                remainingAmmo = value;
            }else {
                remainingAmmo = value + 1;
            }
        }
    }

    public virtual void Fire() { }

    public virtual void Reload() { }

    public virtual void SwitchingFireMode() { }


}
