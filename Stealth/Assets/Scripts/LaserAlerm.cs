using UnityEngine;
using System.Collections;

public class LaserAlerm : MonoBehaviour {
    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            GameController._intance.CheckPlayer(other.transform);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
