using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    
    //���ǂ̎��
    public GameObject fullWall;
    public GameObject halfWall;
    public GameObject sidedWall;

    //GameObject determinedWall;

    //�����_���T�C�R��
    int dice;

    //�ǂ������W�B
    Vector3 wallPositionXplus;
    Vector3 wallPositionXminus;
    Vector3 wallPositionZplus;
    Vector3 wallPositionZminus;

    //�ǂ����݂��邩�ǂ���(�������ɂ̂�true�ɂȂ�)
    bool isWallXplusExist = false;
    bool isWallXminusExist = false;
    bool isWallZplusExist = false;
    bool isWallZminusExist = false;


    //���ǂɊւ���v���p�e�B
    //�ׂ̕������ǂ����ׂ����ǂ�����₢���킹��B
    public bool IsWallXplusExist {
        get {
            return isWallXplusExist;
        }
    }

    public bool IsWallXminusExist {
        get {
            return isWallXminusExist;
        }
    }

    public bool IsWallZplusExist {
        get {
            return isWallZplusExist;
        }
    }

    public bool IsWallZminusExist {
        get {
            return isWallZminusExist;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    void RollADice() {
        dice = Random.Range(0, 10);
    }

    void InitializeWallsPosition() {

        wallPositionXplus =
            new Vector3(transform.position.x + 5,
                        transform.position.y + 2,
                        transform.position.z);
        wallPositionXminus =
            new Vector3(transform.position.x - 5,
                        transform.position.y + 2,
                        transform.position.z);
        wallPositionZplus =
            new Vector3(transform.position.x,
                        transform.position.y + 2,
                        transform.position.z + 5);
        wallPositionZminus =
            new Vector3(transform.position.x,
                        transform.position.y + 2,
                        transform.position.z - 5);
    }

    public void CreateRoom(bool needWallXplus, bool needWallXminus,
                           bool needWallZplus, bool needWallZminus) {

        InitializeWallsPosition();

        if(needWallXplus == false && needWallXminus == false &&
           needWallZplus == false && needWallZminus == false) {

            int r = Random.Range(0, 4);

            switch (r) {
                case 0:
                needWallXplus = true;
                break;

                case 1:
                needWallXminus = true;
                break;

                case 2:
                needWallZplus = true;
                break;

                case 3:
                needWallZminus = true;
                break;

                default:
                break;
            }
        }

        //���K�v������΍��B
        if (needWallXplus == true) {
            GenerateWallXplus();
        }
        if (needWallXminus == true) {
            GenerateWallXminus();
        }
        if (needWallZplus == true) {
            GenerateWallZplus();
        }
        if (needWallZminus == true) {
            GenerateWallZminus();
        }
    }

    void GenerateWallXplus() {
        RollADice();

        switch (dice) {
            case 0:
            Instantiate(sidedWall, wallPositionXplus, Quaternion.identity);
            isWallXplusExist = true;
            break;

            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            Instantiate(fullWall, wallPositionXplus, Quaternion.identity);
            isWallXplusExist = true;//�ǂ�������̂�true��
            break;



            case 6:
            Instantiate(sidedWall, wallPositionXplus, Quaternion.identity);
            isWallXplusExist = true;
            break;


            case 7:
            case 8:
            Instantiate(halfWall, wallPositionXplus, Quaternion.identity);

            isWallXplusExist = true;//�ǂ�������̂�true��
            break;

            case 9:
            break;

            default:
            break;
        }//switch
    }//GenerateWallXplus

    void GenerateWallXminus() {
        RollADice();

        switch (dice) {
            case 0:
            case 1:
            Instantiate(sidedWall, wallPositionXminus, Quaternion.identity);
            isWallXminusExist = true;
            break;

            case 2:
            case 3:
            Instantiate(fullWall, wallPositionXminus, Quaternion.identity);
            isWallXminusExist = true;
            break;

            case 4:
            case 5:
            Instantiate(sidedWall, wallPositionXminus, Quaternion.identity);
            isWallXminusExist = true;
            break;

            case 6:
            case 7:
            case 8:
            Instantiate(halfWall, wallPositionXminus, Quaternion.identity);
            isWallXminusExist = true;
            break;

            case 9:
            case 10:
            break;

            default:
            break;
        }//switch
    }//GenerateWallXminus

    void GenerateWallZplus() {
        RollADice();

        switch (dice) {
            case 0:
            case 1:
            Instantiate(sidedWall, wallPositionZplus, Quaternion.Euler(0, 90, 0));
            isWallZplusExist = true;
            break;

            case 2:
            case 3:
            Instantiate(fullWall, wallPositionZplus, Quaternion.Euler(0, 90, 0));
            isWallZplusExist = true;
            break;

            case 4:
            case 5:
            Instantiate(sidedWall, wallPositionZplus, Quaternion.Euler(0, -90, 0));
            isWallZplusExist = true;
            break;

            case 6:
            case 7:
            case 8:
            Instantiate(halfWall, wallPositionZplus, Quaternion.Euler(0, 90, 0));
            isWallZplusExist = true;
            break;

            case 9:
            case 10:
            break;

            default:
            break;
        }//switch
    }//GenerateWallZplus

    void GenerateWallZminus() {
        RollADice();

        switch (dice) {
            case 0:
            case 1:
            Instantiate(sidedWall, wallPositionZminus, Quaternion.Euler(0, 90, 0));
            isWallZminusExist = true;
            break;

            case 2:
            case 3:
            Instantiate(fullWall, wallPositionZminus, Quaternion.Euler(0, 90, 0));
            isWallZminusExist = true;
            break;

            case 4:
            case 5:
            Instantiate(sidedWall, wallPositionZminus, Quaternion.Euler(0, -90, 0));
            isWallZminusExist = true;
            break;

            case 6:
            case 7:
            case 8:
            Instantiate(halfWall, wallPositionZminus, Quaternion.Euler(0, 90, 0));
            isWallZminusExist = true;
            break;

            case 9:
            case 10:
            break;

            default:
            break;
        }//switch
    }//GenerateWallZminus

}
