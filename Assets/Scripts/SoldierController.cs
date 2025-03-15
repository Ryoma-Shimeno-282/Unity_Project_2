using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierController : MonoBehaviour
{
    public GameObject player;
    public GameObject mainWeapon;
    

    public float moveSpeedAdjuster;
    public Camera soldierFace;
    public float xSensivity;
    public float ySensivity;

    public float maxAngle;
    public float minAngle;



    CharacterController controller;
    Vector2 inputVector;
    Vector3 moveVector;
    Vector2 lookVector;
    float cameraPitch;
    float cameraYaw;

    Firearms firearms;

    //Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        firearms = mainWeapon.GetComponent<Firearms>();
        //animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        MoveSoldier(moveVector);
    }

    void LateUpdate() {
        RotateSoldier(lookVector);
    }

    public void OnMove(InputValue value) {
        inputVector = value.Get<Vector2>();
        moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
    }

    public void OnLook(InputValue value) {
        lookVector = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value) {
        if (value.isPressed) {
            firearms.Fire();
            if (firearms.RemainingAmmo > 0) {
                //animator.Set/Trigger("Fire1");
            }
        }
    }

    public void OnReload(InputValue value) {
        if (value.isPressed) {
            firearms.Reload();
        }
    }


    public void MoveSoldier(Vector3 moveVector) {
        Vector3 globalized = transform.TransformDirection(moveVector);
        controller.Move(globalized * Time.deltaTime * moveSpeedAdjuster);    
    }

    public void RotateSoldier(Vector2 lookVector) {

        //cñU‚è
        cameraPitch -= lookVector.y * ySensivity * Time.deltaTime;
        //‚È‚ñ‚Å‚©‚í‚©‚ç‚È‚¢‚¯‚ÇA-=‚É‚µ‚½‚ç‚¤‚Ü‚­‚¢‚Á‚½B
        //“ü—Í—Ê‚ª1`-1‚Å‚ ‚é‚±‚Æ‚âA“ü—Í‚ª—LŒø‚È‚Ì‚ªˆêu‚¾‚¯‚Å‚ ‚é‚±‚Æ‚ª
        //‰e‹¿‚µ‚Ä‚¢‚»‚¤B
        cameraPitch = ClampRotationValue(cameraPitch, minAngle, maxAngle);
        //— •Ô‚ç‚È‚¢‚æ‚¤‚Éclamp

        //‰¡ñU‚è
        cameraYaw = lookVector.x * xSensivity * Time.deltaTime;
        //‹t‚É‰¡‚Å+=‚Æ‚·‚é‚Æ‰ñ“]‚ª~‚Ü‚ç‚È‚­‚È‚Á‚½B
        //‚â‚Í‚è“ü—Í‚ªˆêu‚µ‚©—LŒø‚Å‚È‚¢‰]X‚ª‰e‹¿‚µ‚Ä‚¢‚é‚æ‚¤‚Å‚ ‚éB
        //Rotate()‚ÆQuaternion.Euler(‚à‚µ‚­‚Ítransform.localRotation)‚Ìˆá‚¢
        //‚È‚Ì‚©‚à‚µ‚ê‚È‚¢B

        //¶‰E‚Í‘Ì‚ğU‚Á‚Ä
        player.transform.Rotate(0f, cameraYaw, 0f);

        //ã‰º‚ÍƒJƒƒ‰‚ğU‚Á‚Ä
        soldierFace.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    float ClampRotationValue(float angle, float minAngle, float maxAngle) {
        if(angle < -360f) {
            angle += 360f;
        }
        if(angle > 360f) {
            angle -= 360f;
        }

        return Mathf.Clamp(angle, minAngle, maxAngle);
    }



}
