using UnityEngine;
using System.Collections;

public class InnerDoor : MonoBehaviour {
    public Transform innerLeftDoor;
    public Transform innerRightDoor;
    public Transform outLeftDoor;
    public Transform outRightDoor;
    public float moveSpeed = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        innerLeftDoor.position = new Vector3(Mathf.Lerp(innerLeftDoor.position.x,outLeftDoor.position.x,Time.deltaTime*moveSpeed), innerLeftDoor.position.y, innerLeftDoor.position.z);
        innerRightDoor.position = new Vector3(Mathf.Lerp(innerRightDoor.position.x,outRightDoor.position.x,Time.deltaTime*moveSpeed), innerRightDoor.position.y, innerRightDoor.position.z);
	}
}
