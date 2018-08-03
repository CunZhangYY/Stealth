using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    private static int hp = 100;
    private Animator health;
    public static int HP
    {
        get
        {
            return hp;
        }
    }
    void Start()
    {
        health = GetComponent<Animator>();
    }
    public void Damage(int hard)
    {
        hp -= hard;
        if (hp <= 0)
        {
            health.SetBool("Dead", true);//当血量为0时，让主角播放死亡动画
            AlermLight.Intance.alermOn = false;
        }
    }
	
	
	
}
