using UnityEngine;
public class PlayerZone1 : ZoneCore {

    public EnemyShootPuck ShootPuck { get; set; }

    #region Functions
    public override void Start()
    {
        base.Start();
        ShootPuck = FindObjectOfType<EnemyShootPuck>();
        ChangePuck.ActivePlayerPuck = Random.Range(0, ChangePuck.PlayerPuckList.Count);
        ChangePuck = FindObjectOfType<ChangePuck>();
    }
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.SetParent(Pucks.transform);
        ChangePuck.EnemyPuckList.Remove(other.gameObject.transform.parent.gameObject);
        if (!ChangePuck.PlayerPuckList.Contains(other.gameObject.transform.parent.gameObject))
        {
            ChangePuck.PlayerPuckList.Add(other.gameObject.transform.parent.gameObject);
        }
        ChangePuck.ActiveEnemyPuck = Random.Range(0, ChangePuck.EnemyPuckList.Count);

        if (ChangePuck.EnemyPuckList.Count != 0)
        {
            MarkBehaviour.target = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].transform;
            ShootPuck.Puck = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].GetComponent<Rigidbody>();
            ShootPuck.Pointer.transform.position = ShootPuck.Puck.gameObject.transform.position;
        }
    }
    #endregion
}
