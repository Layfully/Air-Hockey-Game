using UnityEngine;

public class RotatePointerWithMouse : PointerCore { 

    public override void Update () {
        base.Update();
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
    }
}
