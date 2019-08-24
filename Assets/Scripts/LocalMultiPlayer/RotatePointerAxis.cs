using UnityEngine;

public class RotatePointerAxis : PointerCore {

    #region Variables
    public float smoothDelta = 2.0f;
    
    public string inputName;
    #endregion

    #region Properties

    public float SmoothDelta
    {
        get { return smoothDelta; }

        set { smoothDelta = value; }
    }
    public string InputName
    {
        get { return inputName; }

        set { inputName = value; }
    }

    #endregion

    #region Functions
    public override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0, Input.GetAxis(InputName) * (Time.deltaTime / SmoothDelta) * speed)); // Player2 or Horizontal
    }
    #endregion

}
