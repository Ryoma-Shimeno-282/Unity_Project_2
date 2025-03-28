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

        //縦首振り
        cameraPitch -= lookVector.y * ySensivity * Time.deltaTime;
        //なんでかわからないけど、-=にしたらうまくいった。
        //入力量が1〜-1であることや、入力が有効なのが一瞬だけであることが
        //影響していそう。
        cameraPitch = ClampRotationValue(cameraPitch, minAngle, maxAngle);
        //裏返らないようにclamp

        //横首振り
        cameraYaw = lookVector.x * xSensivity * Time.deltaTime;
        //逆に横で+=とすると回転が止まらなくなった。
        //やはり入力が一瞬しか有効でない云々が影響しているようである。
        //Rotate()とQuaternion.Euler(もしくはtransform.localRotation)の違い
        //なのかもしれない。

        //左右は体を振って
        player.transform.Rotate(0f, cameraYaw, 0f);

        //上下はカメラを振って
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
