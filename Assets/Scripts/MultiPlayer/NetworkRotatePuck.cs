using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkRotatePuck : NetworkBehaviour {

    public GameObject pointer;
	public Transform target;
    public int speed = 600;
    public NetworkMarkBehaviour mark;
    public float smoothTime = 10f;
    public float yOffset = 0f;
    public float xOffset = 0f;
    private Transform thisTransform;
	void Start () {
		thisTransform = pointer.transform;
        target = mark.target;
	}
	
	void Update () {
        target = mark.target;
        thisTransform.position = new Vector3(Mathf.Lerp(thisTransform.position.x, target.position.x + xOffset, Time.deltaTime * smoothTime), Mathf.Lerp(thisTransform.position.y, target.position.y + yOffset, Time.deltaTime * smoothTime), Mathf.Lerp(thisTransform.position.z, target.position.z + xOffset, Time.deltaTime * smoothTime));
        if(!isLocalPlayer){
            return;
        }
		pointer.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
	}
}
