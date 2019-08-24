public class EnemyShootPuck : ShootPuckCore 
{
     void Start()
    {
        Pointer = FindObjectOfType<EnemyPointer>().gameObject;
    }
}
