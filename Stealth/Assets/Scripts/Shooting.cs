using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
    private Animator anim;
    private bool shotHight = false;//即初态为峰值未到来
    public int hurt = 30;
    private PlayerHealth health;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        health =GameObject.FindGameObjectWithTag(Tags.player). GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetFloat("Shot") > 0.5f&&shotHight==false)//因为射击动画的播放需要很长时间，
            //而这个检测方法是每帧更新的，所以要加判断，使在一次射击动画播放过程中，只记录一次伤害
        {//大于0.5说明射击动画的峰值来临，即动画在播放中，次吃应记录一次动画的播放
            health.Damage(hurt);//传递伤害
            shotHight = true;
        }
        else
        {
            shotHight = false;
        }
	}
}
