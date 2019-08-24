using UnityEngine;
public class EnemyZone : ZoneCore {

    #region Variables

    #endregion

    #region Properties
    public ShootPuck ShootPuck { get; set; }
    #endregion

    #region Functions
    public override void Start()
    {
        base.Start();
        ShootPuck = FindObjectOfType<ShootPuck>();
        ChangePuck.ActiveEnemyPuck = Random.Range(0, ChangePuck.EnemyPuckList.Count);

        //  ShootPuck.puck = MarkBehaviour.target.GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.SetParent(Pucks.transform);
        ChangePuck.PlayerPuckList.Remove(other.gameObject.transform.parent.gameObject);
        if (!ChangePuck.EnemyPuckList.Contains(other.gameObject.transform.parent.gameObject))
        {
            ChangePuck.EnemyPuckList.Add(other.gameObject.transform.parent.gameObject);
        }
        ChangePuck.ActivePlayerPuck = Random.Range(0, ChangePuck.PlayerPuckList.Count);

        if (ChangePuck.PlayerPuckList.Count != 0)
        {
            MarkBehaviour.target = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].transform;
            ShootPuck.Puck = ChangePuck.PlayerPuckList[ChangePuck.ActivePlayerPuck].GetComponent<Rigidbody>();
            ShootPuck.Pointer.transform.position = ShootPuck.Puck.gameObject.transform.position;
        }
        else
        {
            OnEvent();
        }
    }
    #endregion
}
