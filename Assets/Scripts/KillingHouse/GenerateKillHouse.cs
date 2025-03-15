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

    //壁を作る必要があるか
    //(デフォルトでtrue、隣の部屋の壁の存在によって部分的にfalseとなる)
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
        //→二次元配列を使えていない。
        /*
            配列にInstantiateで作ったクローンを格納して扱ったほうが   
            壁の存在など部屋ごとの情報を保持したまま扱えて便利かと思う。
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
        //特に指定しないと、プレハブのtransformをそのままクローンに適用する
        //プレハブのtransformを初期化して0,0,0にしている場合は注意する。
        //ここでは、プレハブの初期座標ではなく生成座標を別途入力

        generateRoom = floor.GetComponent<GenerateRoom>();
        generateRoom.CreateRoom(needWallXplus, needWallXminus,
                                needWallZplus, needWallZminus);
    }

    bool ConvertWallExistanceToNeeds(bool exist) {
        if(exist == true) {//存在していれば
            return false;//不必要を返す
        } else {            //存在していなければ
            return true;//必要を返す。
        }
    }


}
