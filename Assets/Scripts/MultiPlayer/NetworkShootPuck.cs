using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkShootPuck : NetworkBehaviour {

#region Variables
    public List<GameObject> playerPuckList = new List<GameObject>(4);
	public int playerActivePuck;

    [SerializeField]
    private GameObject pointer;
    [SerializeField]
    private Rigidbody puck;
    [SerializeField]
    private int speed = 60;
    [SerializeField]
    private bool fired;
    [SerializeField]
    private string inputName;
    [SerializeField]
    private string pointerTag;
    [SerializeField]
    private float timer;
	public float holdTime = 0.7f;


	   #region Properties
    public int Speed
    {
        get { return speed; }

        set { speed = value; }
    }
    public string InputName
    {
        get { return inputName; }

        set { inputName = value; }
    }
    public bool Fired
    {
        get { return fired; }

        set { fired = value; }
    }
    public GameObject Pointer
    {
        get { return pointer; }

        set { pointer = value; }
    }
    public Rigidbody Puck
    {
        get { return puck; }

        set { puck = value; }
    }
    public string PointerTag
    {
        get { return pointerTag; }

        set { pointerTag = value; }
    }
    #endregion
#endregion

    void Start(){
        playerActivePuck = 0;
        for(int i = 0; i <= 3; i++){
            playerPuckList.Add(transform.GetChild(i).gameObject);
        }
    }
	
	void Update () {

		timer += Time.deltaTime;

		if(!isLocalPlayer)
			return;

        if (Input.GetButton(InputName)) 
        {
            holdTime += Time.deltaTime / 5;
            holdTime = Mathf.Clamp(holdTime, 0, 2);
        }

        if (Input.GetButtonUp(InputName))
        {
            Fired = true;
            Shoot();
            holdTime = 0.7f;
        }
	}
	public void Shoot()
    {
        if(timer > 1)
        {
            Puck.velocity = transform.TransformDirection(Pointer.transform.forward * Speed * holdTime);
            timer = 0;
        }
    }
}
