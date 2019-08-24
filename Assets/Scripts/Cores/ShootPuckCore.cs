using UnityEngine;
public class ShootPuckCore : MonoBehaviour
{
    #region Variables
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
    #endregion

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
    public float holdTime = 0.7f;
    #endregion

    public virtual void Update()
    {
        timer += Time.deltaTime;
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
