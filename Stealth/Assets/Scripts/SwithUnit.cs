using UnityEngine;
using System.Collections;

public class SwithUnit : MonoBehaviour {
    public GameObject laser;//要控制的激光栅栏
    public AudioSource _audio;//当点击Z键关闭激光时，发出的声音
    public Material unit;//取得这个控制器上面的开锁纹理材质，进行替换
    public GameObject screen;//获取开锁图片游戏对象

    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (_audio.isPlaying == false)
                {
                    _audio.Play();
                }
                laser.SetActive(false);
                screen.GetComponent<MeshRenderer>().material = unit;//不能用materials[0]来赋值
            }
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
