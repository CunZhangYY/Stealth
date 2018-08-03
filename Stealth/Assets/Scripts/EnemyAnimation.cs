using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {
    //在EnemyMoveAI中控制了机器人的导航，那么此处就利用NavMeshAgent组件导航，来控制机器人的的运动
    private Animator anim;
    private NavMeshAgent navAgent;
    public float speedDamp = 0.3f;//移动速度阻尼
    public float angleSpeedDamp = 0.3f;//旋转角度阻尼
    private EnemySight sight;
    public static float temp = 0;
    public float tempAngle = 0;
    public static float dampSpeed = 3;
    public static float targetSpeed=0;
    private EnemyMoveAI move;
	// Use this for initialization
	void Start () {
        anim=GetComponent<Animator>();
        navAgent = this.GetComponent<NavMeshAgent>();
        sight = GetComponent<EnemySight>();
        move = GetComponent<EnemyMoveAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (move.length <=0.3f)//如果速度值为zero，那么说明当前navAgent.destination没有位置更新
        {
            EnemyAnimation.temp = Mathf.Lerp(EnemyAnimation.temp, 0, Time.deltaTime * EnemyAnimation.dampSpeed);
            anim.SetFloat("Speed", temp);
            if (temp < 0.1)
            {
                temp = 0;
            }
            tempAngle = Mathf.Lerp(tempAngle, 0, Time.deltaTime * EnemyAnimation.dampSpeed);
            anim.SetFloat("AngleSpeed", tempAngle);
            if (tempAngle < 0.1)
            {
                tempAngle = 0;
            }
        }
        else
        {//否则说明，有目标位置，此时就需要旋转，移动，
            //只要有目标位置旋转是一定要执行的
            //确定旋转角度值
            float angle = Vector3.Angle(transform.forward, move.way[1] - move.way[0]);//测得速度方向，与主角正方向的夹角
            float angleRad = 0;
            angleRad = angle * Mathf.Deg2Rad;//将角度值转换成弧度制标示
            //确定旋转方向
            Vector3 crossRes = Vector3.Cross(transform.forward, move.way[1] - move.way[0]);//确定旋转法线
            if (crossRes.y < 0)
            {//即法线方向为负，说明速度方向在机器人正方向的的左边，那么需要向逆时针旋转，
                angleRad = -angleRad;
            }
            tempAngle= Mathf.Lerp(tempAngle, angleRad, Time.deltaTime * EnemyAnimation.dampSpeed);
            anim.SetFloat("AngleSpeed", tempAngle);//将旋转参数传过去 
                                                  //接下来进行速度的控制
                                                  //我要求当角度大于90度时，进行无速度旋转，当小于90度时，在正方向上分配一个期望速度navAgent.desiredVelocity的分量速度值，让他进行边旋转便运动
            if (angle > 3f)
            {
                //anim.SetFloat("Speed", 0);
            }
            else
            {//否则即使小于90 度
             //Vector3 temp = navAgent.desiredVelocity;
             //Vector3 prejection = Vector3.Project(temp, transform.forward);//求得在机器人正方向上的速度分量,当旋转到与期望速度一个方向时，速度值也就与期望速度一样了，实现了平滑
             
                    EnemyAnimation.temp = Mathf.Lerp(EnemyAnimation.temp, targetSpeed, Time.deltaTime * EnemyAnimation.dampSpeed);
                    anim.SetFloat("Speed", temp);                               
            }
        }
        anim.SetBool("PlayerInSight", sight.enemySight);
    }
}
