using UnityEngine;

public class PlayerZone : ZoneCore {

    private ShootPuck shootPuck;

    public  override void Start()
    {
        base.Start();
        shootPuck = GameObject.FindGameObjectWithTag("PucksP2").GetComponent<ShootPuck>();
        ChangePuck.ActivePlayerPuck = Random.Range(0, ChangePuck.PlayerPuckList.Count);
    }
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.SetParent(Pucks.transform);
        if (!ChangePuck.PlayerPuckList.Contains(other.gameObject.transform.parent.gameObject))
        {
            ChangePuck.PlayerPuckList.Add(other.gameObject.transform.parent.gameObject);
        }
        ChangePuck.EnemyPuckList.Remove(other.gameObject.transform.parent.gameObject);
        ChangePuck.ActiveEnemyPuck = Random.Range(0, ChangePuck.EnemyPuckList.Count);
        if (ChangePuck.PlayerPuckList.Count != 0)
        {
            MarkBehaviour.target = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].transform;
            shootPuck.Puck = ChangePuck.EnemyPuckList[ChangePuck.ActiveEnemyPuck].GetComponent<Rigidbody>();
            shootPuck.Pointer.transform.position = shootPuck.Puck.gameObject.transform.position;
        }
    }
}
