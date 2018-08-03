using UnityEngine;
using System.Collections;

public class CctvCam : MonoBehaviour {
    void OnTriggerStay(Collider other)
    {//需要产生的触发事件包括警报的响起，还有要记录主角的当前位置，因为后面敌人要朝这个方向赶过来
        if (other.tag == Tags.player)//通过标签来查找游戏主角对象
        {//如果进入触发区的是主角时 再进行以下操作
            GameController._intance.CheckPlayer(other.transform);
            //GameController._intance.alermOn = true;//警报响起，可能还要计时，响多久
            //GameController._intance. lastPlayerPosition = other.transform.position;
        }
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
