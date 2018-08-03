using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
    //开始先对视觉范围的控制
    public bool enemySight = false;//视觉标志位
    public Vector3 alertPosition = Vector3.zero;//记录主角位置的变量
    private float filedView = 110;//定义机器人的视野范围
    //开始对机器人进行听觉控制
    public bool enemyListen = false;//机器人听觉标志位
    private Animator playerAnim;//获得主角的状态机，因为要借助他，判断主角是否发出移动的声音
    private SphereCollider _collider;//获取该机器人身上的spherecollider碰撞器，因为后面要用碰撞器的半径，来与声音传播路径的长度做比较
    private NavMeshAgent navAgent;//获取机器人身上的路径网格组件

    private Vector3 preLastPosition = Vector3.zero;
	// Use this for initialization
	void Start () {
        playerAnim = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Animator>();
        _collider = GetComponent<SphereCollider>();
        navAgent = GetComponent<NavMeshAgent>();
        preLastPosition = GameController._intance.lastPlayerPosition;
	}
	void Update()
    {
        if (preLastPosition != GameController._intance.lastPlayerPosition)
        {//如果两个值是不相同的，那么就说明警报位置发生改变了
            alertPosition = GameController._intance.lastPlayerPosition;
            preLastPosition = GameController._intance.lastPlayerPosition;
        }
    }
	void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            //机器人的视觉处理
            Vector3 forward = transform.forward;//记录当前机器人的正前方
            Vector3 playerDir = other.transform.position - transform.position;//记录机器人到主角的角度
            float temp = Vector3.Angle(forward, playerDir);//计算两者之间的夹角
            //当主角藏在房间里面，机器人在外面，也认为机器人是看不到主角的
            RaycastHit hit;
            bool result = Physics.Raycast(transform.position + Vector3.up, other.transform.position - transform.position, out hit);
            if (temp <= 0.5f * filedView&&(result==false||hit.collider.tag==Tags.player))
            {//即此时意味着机器人可以看到主角
                if (PlayerHealth.HP > 0)
                {
                    enemySight = true;
                    alertPosition = other.transform.position;
                    GameController._intance.CheckPlayer(other.transform);//调用控制器中的拉响警报方法，当主角时听到声音时，只是去追踪，不拉响警报，也不通知其他机器人
                }else
                {
                    enemySight = false;
                    GameController.AlermOn = false;
                }                
            }
            else
            {//否则即机器人看不到主角，需将标志位置false
                enemySight = false;
            }

            //机器人的听觉处理
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("LocalMotion"))
            {//如果为真 说明主角正在可测范围内移动发声，此时便可以计算声音传到机器人的路径长度了
                //想计算声音的路径长度，就得先获得路径信息
                NavMeshPath path = new NavMeshPath();
                if (navAgent.CalculatePath(other.transform.position, path))
                {//为真说明 从机器人到主角的路径可以获取到，且所有的路径信息都存放在path中
                    //为了后面计算路径长度，此时将所有的路径点都存放到一个位置数组中
                    Vector3[] wayPoint = new Vector3[path.corners.Length];//设置数组的大小
                    //wayPoint[0] = transform.position;//将机器人的位置放到第0位置
                    //wayPoint[wayPoint.Length - 1] = other.transform.position;//将主角的位置放到数组的最后一位
                    for(int i = 0; i < path.corners.Length; i++)
                    {
                        wayPoint[i] = path.corners[i];//将用路径检测到的中间结点 也放入到数组中
                    }
                    //计算音乐路径长度
                    float length = 0;//存储路径长度值
                    for(int i = 0; i < wayPoint.Length-1; i++)
                    {
                        length += (wayPoint[i + 1] - wayPoint[i]).magnitude; //计算两点之间的值依次加到length中
                    }
                    //判断音乐路径与可测区域的半径的大小
                    if (length < _collider.radius)
                    {//如果为真，即可听到声音
                        enemyListen = true;
                        alertPosition = other.transform.position;
                    }
                    else
                    {//否则则标志位为false
                        enemyListen = false;
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.player)
        {
            enemySight = false;
            enemyListen = false;
        }
    }
}
