using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    private Vector3 offset;
    private Transform player;
    public float movSpeed = 3;//位置变化阻尼
    public float angleSpeed = 3;//角度旋转阻尼
    public LayerMask layer;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offset = transform.position - player.position;
        offset = new Vector3(0, offset.y, offset.z);//保证在x轴方向上主角跟摄像机没有偏移
	}
	
	// Update is called once per frame
	void Update () {
        //如果这样先将其位置移动到这里，这个过程没有才用线性插值，会显得很突兀，并且下面有用到这个操作，这个就显得多余transform.position = player.position + offset;//先把摄像机保持跟主角指定的偏移
        //接下来，微调调整摄像机位置，以及角度，使人在墙角时摄像机仍能看得到
        //选取5个待测的点
        Vector3 beginPos = player.position + offset;//起始点
        Vector3 endPos = Vector3.up * offset.magnitude + player.position;//终点
        Vector3 pos1 = Vector3.Lerp(beginPos, endPos, 0.25f);
        Vector3 pos2 = Vector3.Lerp(beginPos, endPos, 0.5f);
        Vector3 pos3 = Vector3.Lerp(beginPos, endPos, 0.75f);
        Vector3 [] posArray = new Vector3[]{ beginPos,pos1,pos2,pos3,endPos};//将选取的五个点放在数组里
        Vector3 targetPos = beginPos;//将目标点设为开始点，防止当有障碍物遮在主角头顶时，选取的5个点，所发出的射线都不能检测到主角时，不出现错误，
        for(int i = 0; i < 5; i++)
        {
            RaycastHit hit;
            if(Physics.Raycast(posArray[i],new Vector3(player.position.x, player.position .y+1, player.position.z )- posArray[i],out hit,layer))
            {
                if (hit.collider.tag == Tags.player)
                {//如果检测到的对象时主角
                    targetPos = posArray[i];
                    break;
                }
                if (hit.collider.tag == Tags.switchUnit || hit.collider.tag == Tags.door||hit.collider.tag==Tags.laser||hit.collider.tag==Tags.wire||hit.collider.tag==Tags.cctvCamCollision||hit.collider.tag==Tags.needKey)
                {//因为检测到这些物体时，主角的头部如果高于其碰撞器，会导致只有在最上面才能看到主角的错误信息，所以如果检测到的物体是这些的话，就不让他进行，移位判断，避免跳动
                    break;
                }
            }
            //else
            //{//当没有检测到任何物体时，也把这个第一个位置给目标位置，因为前面有Vector3 targetPos = transform.position;这句话，所以这个else其实可以省略
            //    targetPos = posArray[i];
            //}
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * movSpeed);//线性插值移到目的位置
        Quaternion nowRotation = transform.rotation;//当前旋转角度
        transform.LookAt(new Vector3(player.position.x, player.position.y + 1, player.position.z));//摄像机关注的点也应该是主角的身体中部
        Quaternion targetRotation = transform.rotation;//目标旋转角度
        transform.rotation = Quaternion.Lerp(nowRotation, targetRotation, Time.deltaTime * angleSpeed);//线性插值旋转视野到目标视野
	}
    
}
