using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    private NavMeshAgent navAgent;
    public Transform [] wayPoint;
    private int index = 0;
    public float time = 3;
    private float timer = 0;
    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = wayPoint[index].position;
        navAgent.updatePosition = false;
        navAgent.updateRotation = false;

    }
	
	// Update is called once per frame
	void Update () {
        Patrolling();
	}

    private void Patrolling()
    {
        print(navAgent.desiredVelocity.magnitude);

        if (navAgent.remainingDistance < 0.5)
        {
            //navAgent.Stop();

            timer += Time.deltaTime;
            if (timer > time)
            {
                index++;
                index %= 4;
                navAgent.SetDestination( wayPoint[index].position);
                timer = 0;
            }
        }
    }
}
