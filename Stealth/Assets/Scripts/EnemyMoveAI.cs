using UnityEngine;
using System.Collections;

public class EnemyMoveAI : MonoBehaviour {
    public Transform[] wayPoints;
    private NavMeshAgent navAgent;
    private int index = 0;//要移动到的位置索引
    public float time = 3;
    public float timer = 0;


    private EnemySight sight;
    public float checkTime = 3;
    public float checkTimer = 0;

    private PlayerHealth health;

    private Vector3 targetPos;
    public  float length;
    public  Vector3[] way;
    int l = 0;
    // Use this for initialization
    void Start () {
        navAgent = this.GetComponent<NavMeshAgent>();
        sight = this.GetComponent<EnemySight>();
        health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();
        targetPos = wayPoints[index].position;

    }

    // Update is called once per frame
    void Update () {
        //此处开始控制机器人的三种状态
        //机器人的三种状态分别是自主巡逻，追捕主角，开枪射击
        if (sight.enemySight == true&&PlayerHealth.HP>0)//当主角的血量大于0时才可以射击，否则不可以射击
        {//如果机器人看到，执行开枪
            //shoot
            Shooting();
        }
        else
        {
            if (sight.alertPosition != Vector3.zero&&PlayerHealth.HP>0)//也是只有当主角的血量大于0的情况下，才允许去追踪
            {//机器人没有看到，但是，这个追踪坐标有变化，不是zero，说明可能是听到了声音，也或者是别的监视器什么的发现了主角，此时追踪
                //追踪
                Chasing();
            }
            else
            {//再否则，就说明现在什么警报都没有，也可能是主角已死亡，应该进行巡逻
                Patrolling();
            }
        }
        
	}
    void Shooting() {
        //navAgent.Stop();//此事要执行射击动画所以要关闭导航
    }
    void Chasing()
    {
        EnemyAnimation.targetSpeed = 4;
        
        targetPos = sight.alertPosition;//将要追踪的位置付给导航网格的目的位置
        NavAgentPath();
        if (length < 2.0f)
        {//当执行了这句话说明，机器人没有看到,所以此时应该回去原地进行巡逻，
         //到这个目的地了，没看见主角，应该再停留一段之间，查看，所以设置一个计时器
            targetPos = transform.position;
            timer += Time.deltaTime;
            if (timer >= time)
            {
                sight.alertPosition = Vector3.zero;
                GameController.AlermOn = false;
                timer = 0;
            }
            
        }
    }

    private void Patrolling()
    {
        targetPos = wayPoints[index].position;
        EnemyAnimation.targetSpeed = 2.0f;
        NavAgentPath();
        if (length< 0.3f)
        {//如果导航移动到距离目标位置还有小于0.5的距离时，认为就是移动到了目标位置
            print("休息");
            targetPos = transform.position;
            timer += Time.deltaTime;
            if (timer >= time)
            {
                index++;
                index %= wayPoints.Length;
                targetPos = wayPoints[index].position;                
                timer = 0;
            }
        }
    }

    private void NavAgentPath()
    {//导航路径 整合
        NavMeshPath path = new NavMeshPath();
        navAgent.CalculatePath(targetPos, path);
        way = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            way[i] = path.corners[i];//将用导航网格获得的路径关键点，添加到这个数组中
        }//那么此时way这个数组中包含的就是从这个机器人当前位置 到目的位置 所要经过的所有关键点
         //那么接下来 机器人导航按照上面所说的路径来
         //先计算这个路径的总长度。用来判断是否到达目的地的关键要素
        length = 0;
        for (int i = 0; i < way.Length - 1; i++)
        {
            length += (way[i + 1] - way[i]).magnitude;//计算两点之间的长度
        }
        //for(int i = 0; i < way.Length; i++)
        //{
        //    print(way[i]);
        //}
        //l++;
        //print("way数组的长度" + way.Length);
        //print("遍历完"+l);
    }
}
