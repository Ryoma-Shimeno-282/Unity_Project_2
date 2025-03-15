using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{
    bool isRoomExist = true;
    

    bool isTileFilled = false;
    //廊下があるかどうか、部屋が作れるかどうか
    //isTileFilledがtrueなら廊下がある。つまり部屋は作れない。
    //isTileFilledがfalseなら廊下がなく、部屋が作れる。つまり部屋を作る。

    //部屋を作るということは、隣の部屋が問い合わせてきたとき
    //壁を作らないで、というメッセージを送る必要がある。

    //つまり、isRoomExist、部屋が存在するか、が
    //trueなら部屋が「存在する」ので、壁を作らないで、ということ。

    //falseなら、部屋が存在しない、つまり、廊下があるということなので、
    //壁を作って、というメッセージが必要。

    //isRoomExist == true
    //→壁作らない→GenerateWallsを呼ばない。

    //isRoomExsit == false
    //→壁を作る→GenerateWallsを呼ぶ。

    public bool IsTileFilled {
        get {
            return isTileFilled;
        }
        set {
            isTileFilled = value;
            if (isTileFilled == true) {
                //タイルが埋まっている
                //→廊下がある
                //→部屋が作れない。
                //→存在しない
                //→そこには壁が必要。
                isRoomExist = false;
                
            } else {
                isRoomExist = true;
                //タイルが埋まっていない
                //→廊下がない
                //→部屋が作れる
                //→存在する
                //→そこには壁がいらない。
            }
        }
    }
    
    public bool IsRoomExist {
        get {
            return isRoomExist;
        }
        set {
            isRoomExist = value;
        }
    }

    

}
