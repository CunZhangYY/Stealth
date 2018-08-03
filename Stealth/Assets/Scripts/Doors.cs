using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {
    private Animator _anim;
    public AudioSource _audio;
    private int count = 0;//记录有多少游戏对象进入触发区域
    
    
	// Use this for initialization
	void Start () {
        _anim =GetComponent<Animator>();  
	}
	   
	// Update is called once per frame
	void Update () {
        if (_anim.IsInTransition(0))
        {
            if (_audio.isPlaying == false)
            {
                _audio.Play();
            }
        }         
        if (this.transform.tag == Tags.needKey)
        {//判断本身的标签是否是NeedKey，即为需要钥匙的最终结束门，如果是则判断player是否拥有钥匙
            if (GameObject.FindGameObjectWithTag(Tags.player).GetComponent< Player>().hasKey == true)
            {//如果拥有钥匙则打开门
                _anim.SetBool("Close", count <= 0);//当有游戏对象进入触发区域时count值便大于0，count<=0结果为false，使Close为false，播放开门动画，
               //当停留在触发区域的所有游戏对象都离开后，即count为0时，则count<=0为true，则关门。
            }
            //如果没有钥匙，就不开门
        }
        else
        {//即标签是不需要钥匙就可以开门的普通门，就直接执行开门操作
            _anim.SetBool("Close", count <= 0);//当有游戏对象进入触发区域时count值便大于0，count<=0结果为false，使Close为false，播放开门动画，
            //当停留在触发区域的所有游戏对象都离开后，即count为0时，则count<=0为true，则关门。
        }


    }
    void OnTriggerEnter(Collider other)
    {
        //当该门是电梯门时 那么不允许机器人进入 只有当不是电梯门时才可以让他们进入
        if (this.transform.tag != Tags.needKey)
        {
            if (other.tag == Tags.enemy && (other is CapsuleCollider))
            {
                count++;
            }
        }
            if (other.tag == Tags.player)
            {
                count++;
            }
        
            
    }
    void OnTriggerExit(Collider other)
    {
        if (this.transform.tag != Tags.needKey)
        {
            if (other.tag == Tags.enemy && (other is CapsuleCollider))
            {
                count--;
            }
        }
            if (other.tag == Tags.player)
            {
                count--;
            }
        
    }
}
