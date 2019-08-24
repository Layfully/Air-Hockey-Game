using UnityEngine;

public class PointerCore : MonoBehaviour {

    public Transform target;
    public int speed = 600;
    public MarkBehaviour mark;
    public float smoothTime = 10f;
    public float yOffset = 0f;
    public float xOffset = 0f;
    private Transform thisTransform;

    public  virtual void Start()
    {
        thisTransform = transform;
        target = mark.target;
    }

    public  virtual void Update()
    {
        target = mark.target;
        thisTransform.position = new Vector3(Mathf.Lerp(thisTransform.position.x, target.position.x + xOffset, Time.deltaTime * smoothTime), Mathf.Lerp(thisTransform.position.y, target.position.y + yOffset, Time.deltaTime * smoothTime), Mathf.Lerp(thisTransform.position.z, target.position.z + xOffset, Time.deltaTime * smoothTime));
    }
}
