using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierArm : MonoBehaviour
{
    public GameObject mainWeapon;

    Firearms firearms;
    Animator animator;

    bool isRestPosition = true;


    // Start is called before the first frame update
    void Start()
    {
        firearms = mainWeapon.GetComponent<Firearms>();
        animator = this.GetComponent<Animator>();
    }

    public void OnShoot(InputValue value) {
        if (value.isPressed) {
            if (firearms.RemainingAmmo > 0) {
                if (isRestPosition == true) {
                    animator.SetTrigger("Fire1");
                } else {
                    animator.SetTrigger("Fire2");
                }
            }
        }
    }

    public void OnReload(InputValue value) {
        if (value.isPressed) {
            animator.SetTrigger("Reload");
        }
        if(isRestPosition == true) {
            animator.SetBool("IsRest", true);
        } else {
            animator.SetBool("IsRest", false);
        }
    }

    public void OnPositionTransition(InputValue value) {
        if(isRestPosition == true) {//デフォルト状態であれば
            if (value.isPressed) {//入力を検知して
                animator.SetTrigger("ChestPositionTransition");
                //遷移アニメ挿入(chestへの遷移アニメ)
                isRestPosition = false;
                //状態を更新(今はchest)
            }
        } else {//デフォルト状態でなければ
            if (value.isPressed) {//入力を検知して
                animator.SetTrigger("RestPositionTransition");
                //アニメ再生(defaultへの遷移アニメ)
                isRestPosition = true;
                //状態を更新(今はdefault)
            }
        }
    }

    



}
