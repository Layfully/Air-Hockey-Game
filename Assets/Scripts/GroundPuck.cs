using UnityEngine;
public class GroundPuck : MonoBehaviour {
    #region Variables

    #endregion

    #region Properties
    public Rigidbody Rigidbody { get; set; }

    public RigidbodyConstraints RigidbodyConstraints { get; set; }

    public bool IsGrounded { get; set; }

    public bool Check { get; set; }

    #endregion

    #region Functions
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        RigidbodyConstraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    void Update()
    {
        if (IsGrounded)
        {
            Rigidbody.constraints = RigidbodyConstraints;
        }

        if (Check)
        {
            IsGrounded = true;
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Check = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        
        Check = false;
        IsGrounded = false;
    }
    #endregion

}
