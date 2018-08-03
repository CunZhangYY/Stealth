using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    //控制该游戏对象的闪烁

    public bool isFlicker = false;
    public float onTime = 3;//打开激光的时间
    public float offTime = 3;//关闭激光的时间
    private float timer = 0;//计时器
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isFlicker == true)
        {
            timer += Time.deltaTime;
            if (GetComponent<MeshRenderer>().enabled == true)
            {//当这个物体正在被渲染时，执行以下操作
                if (timer >= onTime)
                {//激光打开的时间段用完了，该关闭激光了
                 //this.enabled = false;//不能这样用，因为这样的话，整个脚本就失活了，包括脚本的运行在内，都不执行了
                    GetComponent<MeshRenderer>().enabled = false;
                    GetComponent<BoxCollider>().enabled = false;
                    timer = 0;
                }
            }
            if (GetComponent<MeshRenderer>().enabled == false)
            {//当这个物体没有被渲染时，执行以下操作
                if (timer >= offTime)
                {
                    GetComponent<MeshRenderer>().enabled = true;
                    GetComponent<BoxCollider>().enabled = true;
                    timer = 0;
                }
            }
            
            
        }
	}
}
