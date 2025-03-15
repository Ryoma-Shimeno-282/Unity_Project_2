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
        if(isRestPosition == true) {//�f�t�H���g��Ԃł����
            if (value.isPressed) {//���͂����m����
                animator.SetTrigger("ChestPositionTransition");
                //�J�ڃA�j���}��(chest�ւ̑J�ڃA�j��)
                isRestPosition = false;
                //��Ԃ��X�V(����chest)
            }
        } else {//�f�t�H���g��ԂłȂ����
            if (value.isPressed) {//���͂����m����
                animator.SetTrigger("RestPositionTransition");
                //�A�j���Đ�(default�ւ̑J�ڃA�j��)
                isRestPosition = true;
                //��Ԃ��X�V(����default)
            }
        }
    }

    



}
