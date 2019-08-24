using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMarkBehaviour : MonoBehaviour {
	public Transform target;
    private float smoothTime = 10f;
    private float yOffset = 0f;
    private float xOffset = 0f;
    private Transform thisTransform;
	// Use this for initialization
	void Start () {
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.position = new Vector3(Mathf.Lerp(thisTransform.position.x, target.position.x + xOffset, Time.deltaTime * smoothTime), Mathf.Lerp(thisTransform.position.y, target.position.y + yOffset, Time.deltaTime * smoothTime), Mathf.Lerp(thisTransform.position.z, target.position.z + xOffset, Time.deltaTime * smoothTime));
	}
}
