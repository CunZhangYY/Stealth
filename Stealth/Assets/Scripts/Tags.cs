using UnityEngine;
using System.Collections;

public class Tags : MonoBehaviour {
    //此脚本主要记录那些标签名，将标签名用字符串变量表示，这样在程序中就不会因为使用字符串而导致检查不出来的错误了
    public const string player = "Player";//主角的标签
    public const string siren = "Siren";//警报喇叭的标签
    public const string enemy = "Enemy";//敌人的标签
    public const string door = "Door";//自动门的标签
    public const string needKey = "NeedKey";//需要钥匙的结束门标签
    public const string switchUnit = "SwitchUnit";//激光控制器的开关标签
    public const string laser = "Laser";//激光的标签
    public const string wire = "Wire";//电线杆上的电线
    public const string cctvCam = "CctvCam";//监视器的标签
    public const string cctvCamCollision = "CctvCamCollision";//监视器上的碰撞区域标签
}
