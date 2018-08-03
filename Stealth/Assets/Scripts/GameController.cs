using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    [HideInInspector] public Vector3 lastPlayerPosition = Vector3.zero;//这个位置放在这里也方便后续访问

    private static bool alermOn = false;
    private GameObject[] sirens;
    public static GameController _intance;
    public AudioSource normal;
    public AudioSource panic;
    public static bool AlermOn
    {
        get
        {
            return alermOn;
        }
        set
        {
            alermOn = value;
        }
    }
    private GameController() { }
	// Use this for initialization
    void Awake()
    {
        alermOn = false;
        _intance = this;

    }
    void Start () {
        sirens = GameObject.FindGameObjectsWithTag(Tags.siren);//获取对警报喇叭对象的引用
	}
	
	// Update is called once per frame
	void Update () {
        if (alermOn == false)
        {//当警报标志位为false时，需将panic音乐慢慢关掉，再把normal音乐慢慢打开
            if (panic.isPlaying == true)
            {//如果此时panic音乐开着，那么就将他慢慢关掉
                panic.volume = Mathf.Lerp(panic.volume, 0, Time.deltaTime);
                if (Mathf.Abs(panic.volume-0) < 0.2)
                {
                    panic.Stop();
                }
            }
            if (normal.isPlaying == false)
            {
                normal.Play();
            }
            normal.volume = Mathf.Lerp(normal.volume, 0.3f, Time.deltaTime);

        }
        else
        {
            if (alermOn == true)
            {//当警报标志位为true时，将normal音乐慢慢关掉，将panic音乐渐渐打开
                if (panic.isPlaying==false)
                {
                    //将panic音乐渐渐打开
                    panic.Play();
                }
                panic.volume = Mathf.Lerp(panic.volume, 0.3f, Time.deltaTime);

                if (normal.isPlaying == true)
                {
                    //将normal音乐慢慢关掉
                    normal.volume = Mathf.Lerp(normal.volume, 0, Time.deltaTime);
                    if (Mathf.Abs(normal.volume - 0) < 0.2)
                    {
                        normal.Stop();
                    }
                }
            }
        }


        if (alermOn == true)
        {
            //此时警报灯应该亮
            AlermLight._intance.alermOn = true;
            PlayStart();
        }
        else
        {
            if (alermOn == false)
            {
                AlermLight._intance.alermOn = false;
                PlayStop();
            }
        }
	
	}

    private void PlayStart()
    {//控制警报喇叭的开始响
        foreach(GameObject go in sirens)
        {
            if (go.GetComponent<AudioSource>().isPlaying == false)
            {//若是警报没有播放时
                go.GetComponent<AudioSource>().Play();
            }
        }
    }
    private void PlayStop()
    {//控制警报喇叭的关闭
        foreach(GameObject go in sirens)
        {
            go.GetComponent<AudioSource>().Stop();//即使没有音乐在播放再执行一次关闭操作也不会出现错误
        }
    }
    public void CheckPlayer(Transform player)
    {//将外部脚本对此脚本经常调用的两个属性进行整合处理，当触发函数触发时，调用这个函数即可
        alermOn = true;
        lastPlayerPosition = player.position;
    }

}
