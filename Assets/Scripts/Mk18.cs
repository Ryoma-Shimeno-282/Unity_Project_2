using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mk18 : Firearms
{
    public GameObject muzzle;
    public GameObject hitMarker;

    


    Ray bulletPath;
    RaycastHit bulletHit;



    // Start is called before the first frame update
    void Start()
    {
        magCapacityFull = 10;
        remainingAmmo = magCapacityFull;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire() {
        if (isSingleFire == true) {
            if (RemainingAmmo > 0) {
                bulletPath = new Ray(muzzle.transform.position, muzzle.transform.right);

                if (Physics.Raycast(bulletPath, out bulletHit)) {
                    Instantiate(hitMarker, bulletHit.point, Quaternion.identity);
                }

                ConsumingAmmo = 1;

            }
        } else {

        }

    }//Fire

    public override void Reload() {
        ReloadingAmmo = magCapacityFull;
    }//Reload

    public override void SwitchingFireMode() {
        if(isSingleFire == true) {
            isSingleFire = false;
        } else {
            isSingleFire = true;
        }
    }

}
