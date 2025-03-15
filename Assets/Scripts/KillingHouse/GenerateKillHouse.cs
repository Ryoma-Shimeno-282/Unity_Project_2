using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateKillHouse : MonoBehaviour
{
    public GameObject floor;

    public int scaleZ = 4;
    public int scaleX = 1;
    public Vector3 entrancePos;

    GenerateRoom generateRoom;

    //�ǂ����K�v�����邩
    //(�f�t�H���g��true�A�ׂ̕����̕ǂ̑��݂ɂ���ĕ����I��false�ƂȂ�)
    bool needWallXplus = true;
    bool needWallXminus = true;
    bool needWallZplus = true;
    bool needWallZminus = true;

    
    //bool isWallXplusExist = false;
    //bool isWallXminusExist = false;
    //bool isWallZplusExist = false;
    //bool isWallZminusExist = false;

    int[,] scaleOfKillHouse;

    // Start is called before the first frame update
    void Start()
    {
        scaleOfKillHouse = new int[scaleZ, scaleX];
        //���񎟌��z����g���Ă��Ȃ��B
        /*
            �z���Instantiate�ō�����N���[�����i�[���Ĉ������ق���   
            �ǂ̑��݂ȂǕ������Ƃ̏���ێ������܂܈����ĕ֗����Ǝv���B
         */
        BuildRoomManager();
    }

    void BuildRoomManager() {
        for(int i = 0; i < scaleX; i++) {
            for(int j = 0; j < scaleZ; j++) {
                
                BuildKillHouseRoom(needWallXplus, needWallXminus, 
                                   needWallZplus, needWallZminus);
                entrancePos.z += 10f;
                needWallZminus = false;
            }
            needWallZminus = true;
            needWallXminus = false;
            entrancePos.z -= 10f * scaleZ;
            entrancePos.x += 10f;
        }
    }

    void BuildKillHouseRoom(bool needWallXplus, bool needWallXminus,
                            bool needWallZplus, bool needWallZminus) {
        Instantiate(floor, entrancePos, Quaternion.identity);

        floor.transform.position = entrancePos;
        //���Ɏw�肵�Ȃ��ƁA�v���n�u��transform�����̂܂܃N���[���ɓK�p����
        //�v���n�u��transform������������0,0,0�ɂ��Ă���ꍇ�͒��ӂ���B
        //�����ł́A�v���n�u�̏������W�ł͂Ȃ��������W��ʓr����

        generateRoom = floor.GetComponent<GenerateRoom>();
        generateRoom.CreateRoom(needWallXplus, needWallXminus,
                                needWallZplus, needWallZminus);
    }

    bool ConvertWallExistanceToNeeds(bool exist) {
        if(exist == true) {//���݂��Ă����
            return false;//�s�K�v��Ԃ�
        } else {            //���݂��Ă��Ȃ����
            return true;//�K�v��Ԃ��B
        }
    }


}
