using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    //此脚本用来设计主角的行走、动画播放等
    private Animator anim;
    private float maxBlend = 5.6f;//最大混合度
    public float moveSpeed = 6;//平滑起步速率
    public float angleSpeed = 5;//旋转阻尼
    private AudioSource _audio;
    public  bool hasKey=false;//是否拥有钥匙，默认状态是没有拥有钥匙的
    //private float temp = 0;//向量插值运算的中间值
    Vector3 target;
    Vector3 now;
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        target = transform.forward;//因为游戏刚开始时，没有按方向键，所以target没法赋值
        now = transform.forward;//所以导致 在走else中求angle的语句时就会出错，所以在此给他赋上初值，使两者相等，
        //在else中计算时，计算出的angle值也只能是0，这样就符合游戏开始时主角不旋转的要求了

    }

    // Update is called once per frame
    void Update() {
        if (PlayerHealth.HP > 0)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("Sneak", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("Sneak", false);
            }
            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
            {
                //速度的混合要慢慢地从小变化到大，这样才会呈现出慢慢起跑的感觉，不至于显得突兀
                float newSpeed = Mathf.Lerp(anim.GetFloat("Speed"), maxBlend, Time.deltaTime * moveSpeed);
                anim.SetFloat("Speed", newSpeed);
                //接下来控制动画的旋转
                //print("h:" + h);
                //print("v:" + v);
                target = new Vector3(h, 0, v);

            }
            else
            {//为什么要在这里设置旋转，因为在上面按键产生到结束的那段时间可能不够去旋转到指定方向，所以当按键结束时要在这里继续旋转

                anim.SetFloat("Speed", 0);
            }

            now = transform.forward;
            float angle = Vector3.Angle(now, target);
            Vector3 nomal = Vector3.Cross(now, target);
            if (nomal.y < 0)
            {//小于0 说明目标向量在主角正方向的左边，角度值应该为负值
                angle = -angle;
            }

            transform.Rotate(Vector3.up * angle * angleSpeed * Time.deltaTime);




            if (anim.GetCurrentAnimatorStateInfo(0).IsName("LocalMotion"))
            {
                PlayFootAudio();
            }
            else
            {
                StopFootAudio();
            }
        }else
        {
            StopFootAudio();//保证主角没血时脚步声停止
        }
           
    }
    
    private void PlayFootAudio()
    {
        //控制脚步声的开始播放
        if (_audio.isPlaying == false)
        {
            _audio.Play();

        }
    }
    private void StopFootAudio()
    {
        //控制脚步声的暂停
        if (_audio.isPlaying==true)
        {
            _audio.Stop();
        }
        
    }
}
