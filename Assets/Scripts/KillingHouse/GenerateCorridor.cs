using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCorridor : MonoBehaviour
{
    public GameObject corridor55;

    public GameObject floor55;
    public GameObject wall5;
    public GameObject centerFedDoor5;
    public GameObject cornerFedDoor5_1;
    public GameObject cornerFedDoor5_2;

    public GameObject roomsArrayTest;

    public int numberOfTileZ;
    public int numberOfTileX;




    Vector3 corridorPos;
    Vector3 floorPos;
    float posX = 0f, posZ = 0f;
    int dice;//ランダム用

    Room[,] rooms;//座標軸は[x, z]、[横、縦]、for文では[i,j]


    // Start is called before the first frame update
    void Start()
    {
        InitiateRoomArray();
        InitiateCorridor();
        RandomGenerateCorridor();
        BuildRoomsOnArrays();
    }

    void InitiateRoomArray() {
        rooms = new Room[numberOfTileX, numberOfTileZ];
        for(int i = 0; i < numberOfTileX; i++) {
            for (int j = 0; j < numberOfTileZ; j++) {
                rooms[i, j] = new Room();
            }
        }
    }

    void FillTileOfArray(float posX, float posZ) {
        int z;
        int x;

        if(posX == 0) {
            x = 0;
        }
        if(posZ == 0) {
            z = 0;
        }

        x = (int)posX / 5;
        z = (int)posZ / 5;


        rooms[x, z].IsTileFilled = true;

        //確認用
        //Vector3 testPos = new Vector3(posX, 2, posZ);
        //Instantiate(roomsArrayTest, testPos, Quaternion.identity);
    }

    void FillRoomOfArray(float posX, float posZ) {
        int x = (int)posX / 5;
        int z = (int)posZ / 5;

        rooms[x, z].IsRoomExist = true;
    }

    void InitiateCorridor() {
        //左下(原点)から時計回りに外周を生成

        for (int i = 1; i <= numberOfTileZ; i++) {//左
            corridorPos = new Vector3(posX, 0, posZ);//(0,0,0)
            Instantiate(corridor55, corridorPos, Quaternion.identity);
            FillTileOfArray(posX, posZ);
            posZ += 5;//最終的にposZは満タン+5に(0,0,max+5)
        }

        posX += 5;//一個隣に
        posZ -= 5;//最後のループ一回で増えすぎた分を調整

        for(int l = 1; l <= numberOfTileX - 1; l++) {//奥
            corridorPos = new Vector3(posX, 0, posZ);//(5,0,max)
            Instantiate(corridor55, corridorPos, Quaternion.identity);
            FillTileOfArray(posX, posZ);
            posX += 5;//posXも満タン+5に(max+5,0,max)
        }

        posX -= 5;//増えすぎた分を調整
        posZ -= 5;//一個手前に

        for (int k = 1; k <= numberOfTileZ - 1; k++) {//右
            corridorPos = new Vector3(posX, 0, posZ);
            Instantiate(corridor55, corridorPos, Quaternion.identity);
            FillTileOfArray(posX, posZ);
            posZ -= 5;
        }

        posZ += 5;
        posX -= 5;

        for (int j = 1; j <= numberOfTileX - 2; j++) {//手前
            corridorPos = new Vector3(posX, 0, posZ);
            Instantiate(corridor55, corridorPos, Quaternion.identity);
            FillTileOfArray(posX, posZ);
            posX -= 5;
        }
    }

    void RandomGenerateCorridor() {
        //建物内にランダムに廊下を生成

        //四方の外廊下の内どこから廊下を伸ばすか

        //まず左
        dice = RollaDice(0, 1);//0,1,2,のダイス
        if(dice == 0) {// 1/3の確率で左から廊下を伸ばす   
            int r = RollaDice(1, numberOfTileZ - 2 + 1);
            //1からの方が分かりやすそうだから1ずらす(第二引数はexclusive)
            //端っこは考慮しない

            posX = 5;
            posZ = 5 * r;

            Debug.Log("left" + "posX" + posX + "posZ" + posZ);

            ExtendCorrider(0, posX, posZ);//左は0

        } else {// 確率が外れると終わり
            return;
        }

        //次に奥
        dice = RollaDice(0, 1);
        if (dice == 0) {
            int r = RollaDice(1, numberOfTileX - 2 + 1);

            posX = 5 * r;
            posZ = (numberOfTileZ - 2) * 5;

            Debug.Log("far" + "posX" + posX + "posZ" + posZ);

            ExtendCorrider(1, posX, posZ);//奥は1

        } else {// 確率が外れると終わり
            return;
        }

        //次に右
        dice = RollaDice(0, 1);
        if (dice == 0) {
            int r = RollaDice(1, numberOfTileZ - 2 + 1);

            posX = (numberOfTileX - 2) * 5;
            posZ = 5 * r;

            Debug.Log("right" + "posX" + posX + "posZ" + posZ);

            ExtendCorrider(2, posX, posZ);//左は2

        } else {// 確率が外れると終わり
            return;
        }

        //最後に手前
        dice = RollaDice(0, 1);
        if (dice == 0) {
            int r = RollaDice(1, numberOfTileX - 2 + 1);

            posX = 5 * r;
            posZ = 5;

            Debug.Log("near" + "posX" + posX + "posZ" + posZ);

            ExtendCorrider(3, posX, posZ);//手前は3

        } else {// 確率が外れると終わり
            return;
        }

    }

    void ExtendCorrider(int direction ,float posX, float posZ) {
        //指定位置に廊下を伸ばす。タイルの数はランダム
        //directionは左,奥,右,手前,の区別用。0～4の数字


        float x = posX;
        float z = posZ;

        if (direction == 0) {//左から伸ばしていくならdirectionは0

            
            dice = RollaDice(3, numberOfTileX);
            Debug.Log("left_dice" + dice);

            for (int i = 0; i < dice; i++) {
                corridorPos = new Vector3(x, 0, z);//X方向にずれていく
                Instantiate(corridor55, corridorPos, Quaternion.identity);
                FillTileOfArray(x, z);
                x += 5;
            }


        }//if direction == 0

        if (direction == 1) {//奥の場合は1

            dice = RollaDice(3, numberOfTileZ);
            Debug.Log("far_dice" + dice);
            for (int i = 0; i < dice; i++) {
                corridorPos = new Vector3(x, 0, z);//X方向にずれていく
                Instantiate(corridor55, corridorPos, Quaternion.identity);
                FillTileOfArray(x, z);
                z -= 5;
            }

        }//if direction == 1

        if (direction == 2) {//右の場合は2

            dice = RollaDice(3, numberOfTileZ);
            Debug.Log("right_dice" + dice);
            for (int i = 0; i < dice; i++) {
                corridorPos = new Vector3(x, 0, z);//X方向にずれていく
                Instantiate(corridor55, corridorPos, Quaternion.identity);
                FillTileOfArray(x, z);
                x -= 5;
            }

        }//if direction == 2

        if (direction == 3) {//手前の場合は3

            dice = RollaDice(3, numberOfTileZ);
            Debug.Log("near_dice" + dice);
            for (int i = 0; i < dice; i++) {
                corridorPos = new Vector3(x, 0, z);//X方向にずれていく
                Instantiate(corridor55, corridorPos, Quaternion.identity);
                FillTileOfArray(x, z);
                z += 5;
            }
        }//if direction == 1


    }

    int RollaDice(int inclusivemMin, int exclusiveMax) {
        int r = Random.Range(inclusivemMin, exclusiveMax);
        return r;
;    }

    void BuildRoomsOnArrays() {
        Room room;
        float posX, posZ;
        
        for(int i=0; i < numberOfTileX; i++) {
            for(int j = 0; j < numberOfTileZ; j++) {
                room = rooms[i, j];
                if (!room.IsTileFilled) {//部屋が建てられるか、＝廊下が無いか
                    posX = i * 5;
                    posZ = j * 5;
                    GenerateRooms(posX, posZ, i, j);
                }
            }
        }
    }

    void GenerateRooms(float posX, float posZ, int i, int j) {
        //床座標
        floorPos = new Vector3(posX, 0, posZ);
        Vector3 left = new Vector3(posX, 0, posZ + 5);
        Vector3 far = new Vector3(posX + 5, 0, posZ + 5);
        Vector3 right = new Vector3(posX + 5, 0, posZ);

        //床作る
        Instantiate(floor55, floorPos, Quaternion.identity);


        Room roomLeft = rooms[i - 1, j];
        bool isLeftRoomExist = roomLeft.IsRoomExist;
        //trueなら隣に部屋がある
        //部屋があるから、壁を作らない。
        //壁を作らないということは、GenerateWallsを呼ばない
        //trueならGenerateWallsを呼ばない
        //falseならGenerateWallsを呼ぶ

        Room roomRight = rooms[i + 1, j];
        bool isRightRoomExist = roomRight.IsRoomExist;

        Room roomFar = rooms[i, j + 1];
        bool isFarRoomExist = roomFar.IsRoomExist;

        Room roomNear = rooms[i, j - 1];
        bool isNearRoomExist = roomNear.IsRoomExist;



        //四隅の壁作る
        //隣の部屋が存在したら、壁を作らない。
        //つまり、roomExistたちがtrueなら、部屋があるということなので
        //壁を作るメソッドを呼ばないようにする。
        if (isLeftRoomExist == false) {
            GenerateWalls(0, left);
        } else {//もし壁を作らない場合でも、一定確率でやっぱり作る。
            //このelse文の中は、建物内部に作る壁の処理。
            dice = RollaDice(0,3);
            if (dice == 0) {
                GenerateBuildingWalls(0, left);
            }
        }

        if (isRightRoomExist == false) {
            GenerateWalls(2, right);
        } else {
            dice = RollaDice(0, 3);
            if (dice == 0) {
                GenerateBuildingWalls(2, right);
            }
        }

        if (isFarRoomExist == false) {
            GenerateWalls(1, far);
        } else {
            dice = RollaDice(0, 3);
            if (dice == 0) {
                GenerateBuildingWalls(1, far);
            }
        }

        if (isNearRoomExist == false) {
            GenerateWalls(3, floorPos);
        } else {
            dice = RollaDice(0, 3);
            if (dice == 0) {
                GenerateBuildingWalls(3, floorPos);
            }
        }


    }

    void GenerateBuildingWalls(int direction, Vector3 pos) {
        if (direction == 0) {//left

            //Instantiate(wall5, pos, Quaternion.Euler(0, 90, 0));

            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 4) {//0,1,2,3,
                Instantiate(wall5, pos, Quaternion.Euler(0, 90, 0));
            } else if (dice >= 4 && dice < 6) {//45
                Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 90, 0));
            } else if (dice >= 6 && dice < 8) {//67
                Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 90, 0));
            } else if (dice >= 8 && dice < 10) {//89
                Instantiate(cornerFedDoor5_2, pos, Quaternion.Euler(0, 90, 0));
            }
        }//if left

        if (direction == 1) {//far

            //Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 180, 0));


            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 4) {//0,1,2,3
                Instantiate(wall5, pos, Quaternion.Euler(0, 180, 0));
            } else if (dice >= 4 && dice < 6) {//4,5
                Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 180, 0));
            } else if (dice >= 6 && dice < 8) {//6,7
                Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 180, 0));
            } else if (dice >= 8 && dice < 10) {//8,9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.Euler(0, 180, 0));
            }
        }//if far

        if (direction == 2) {//right


            //Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 270, 0));

            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 4) {//0,1,2,3
                Instantiate(wall5, pos, Quaternion.Euler(0, 270, 0));
            } else if (dice >= 4 && dice < 6) {//4,5
                Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 270, 0));
            } else if (dice >= 6 && dice < 8) {//6,7
                Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 270, 0));
            } else if (dice >= 8 && dice < 10) {//8,9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.Euler(0, 270, 0));
            }
        }//if right

        if (direction == 3) {//near

            //Instantiate(cornerFedDoor5_2, pos, Quaternion.identity);

            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 4) {//0,1,2,3
                Instantiate(wall5, pos, Quaternion.identity);
            } else if (dice >= 4 && dice < 6) {//4,5
                Instantiate(centerFedDoor5, pos, Quaternion.identity);
            } else if (dice >= 6 && dice < 8) {//6,7
                Instantiate(cornerFedDoor5_1, pos, Quaternion.identity);
            } else if (dice >= 8 && dice < 10) {//8,9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.identity);
            }
        }//if near
    }//GenerateWalls


    void GenerateWalls(int direction, Vector3 pos) {
        if(direction == 0) {//left

            //Instantiate(wall5, pos, Quaternion.Euler(0, 90, 0));

            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 7) {//0,1,2,3,4,5,6,
                Instantiate(wall5, pos, Quaternion.Euler(0, 90, 0));
            } else if (dice >= 7 && dice < 8) {//7
                Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 90, 0));
            } else if (dice >= 8 && dice < 9) {//8
                Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 90, 0));
            } else if (dice >= 9 && dice < 10) {//9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.Euler(0, 90, 0));
            }
        }//if left

        if (direction == 1) {//far

            //Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 180, 0));


            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 7) {//0,1,2,3
                Instantiate(wall5, pos, Quaternion.Euler(0, 180, 0));
            } else if (dice >= 7 && dice < 8) {//4,5
                Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 180, 0));
            } else if (dice >= 8 && dice < 9) {//6,7
                Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 180, 0));
            } else if (dice >= 9 && dice < 10) {//8,9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.Euler(0, 180, 0));
            }
        }//if far

        if (direction == 2) {//right


            //Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 270, 0));

            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 7) {//0,1,2,3
                Instantiate(wall5, pos, Quaternion.Euler(0, 270, 0));
            } else if (dice >= 7 && dice < 8) {//4,5
                Instantiate(centerFedDoor5, pos, Quaternion.Euler(0, 270, 0));
            } else if (dice >= 8 && dice < 9) {//6,7
                Instantiate(cornerFedDoor5_1, pos, Quaternion.Euler(0, 270, 0));
            } else if (dice >= 9 && dice < 10) {//8,9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.Euler(0, 270, 0));
            }
        }//if right

        if (direction == 3) {//near

            //Instantiate(cornerFedDoor5_2, pos, Quaternion.identity);

            dice = RollaDice(0, 10);
            if (dice >= 0 && dice < 7) {//0,1,2,3
                Instantiate(wall5, pos, Quaternion.identity);
            } else if (dice >= 7 && dice < 8) {//4,5
                Instantiate(centerFedDoor5, pos, Quaternion.identity);
            } else if (dice >= 8 && dice < 9) {//6,7
                Instantiate(cornerFedDoor5_1, pos, Quaternion.identity);
            } else if (dice >= 9 && dice < 10) {//8,9
                Instantiate(cornerFedDoor5_2, pos, Quaternion.identity);
            }
        }//if near
    }//GenerateWalls

}
