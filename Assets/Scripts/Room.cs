using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{
    bool isRoomExist = true;
    

    bool isTileFilled = false;
    //�L�������邩�ǂ����A���������邩�ǂ���
    //isTileFilled��true�Ȃ�L��������B�܂蕔���͍��Ȃ��B
    //isTileFilled��false�Ȃ�L�����Ȃ��A����������B�܂蕔�������B

    //���������Ƃ������Ƃ́A�ׂ̕������₢���킹�Ă����Ƃ�
    //�ǂ����Ȃ��ŁA�Ƃ������b�Z�[�W�𑗂�K�v������B

    //�܂�AisRoomExist�A���������݂��邩�A��
    //true�Ȃ畔�����u���݂���v�̂ŁA�ǂ����Ȃ��ŁA�Ƃ������ƁB

    //false�Ȃ�A���������݂��Ȃ��A�܂�A�L��������Ƃ������ƂȂ̂ŁA
    //�ǂ�����āA�Ƃ������b�Z�[�W���K�v�B

    //isRoomExist == true
    //���Ǎ��Ȃ���GenerateWalls���Ă΂Ȃ��B

    //isRoomExsit == false
    //���ǂ���遨GenerateWalls���ĂԁB

    public bool IsTileFilled {
        get {
            return isTileFilled;
        }
        set {
            isTileFilled = value;
            if (isTileFilled == true) {
                //�^�C�������܂��Ă���
                //���L��������
                //�����������Ȃ��B
                //�����݂��Ȃ�
                //�������ɂ͕ǂ��K�v�B
                isRoomExist = false;
                
            } else {
                isRoomExist = true;
                //�^�C�������܂��Ă��Ȃ�
                //���L�����Ȃ�
                //������������
                //�����݂���
                //�������ɂ͕ǂ�����Ȃ��B
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
