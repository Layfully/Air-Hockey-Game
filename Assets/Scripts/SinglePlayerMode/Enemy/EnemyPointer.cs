using UnityEngine;

public class EnemyPointer : PointerCore {

    public Transform aimTarget;

    public void Aim()
    {
        transform.LookAt(aimTarget);
    }
}
