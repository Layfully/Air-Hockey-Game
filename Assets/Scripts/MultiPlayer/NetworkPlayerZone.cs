using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerZone : MonoBehaviour {
	public GameObject puckParent;
	public bool isPlayer1 = false;
	void OnTriggerEnter(Collider other){
		Debug.Log("triggered");

		other.gameObject.transform.parent.SetParent(puckParent.transform);

		if(NetworkCollisionDetect.player1ShootPuck.playerPuckList.Contains(other.gameObject.transform.parent.gameObject) && !isPlayer1){
			NetworkCollisionDetect.player1ShootPuck.playerPuckList.Remove(other.gameObject.transform.parent.gameObject);
			if(!NetworkCollisionDetect.player2ShootPuck.playerPuckList.Contains(other.gameObject.transform.parent.gameObject)){
				NetworkCollisionDetect.player2ShootPuck.playerPuckList.Add(other.gameObject.transform.parent.gameObject);
			}
			if(NetworkCollisionDetect.player1ShootPuck.playerPuckList.Count != 0){
				NetworkCollisionDetect.player1MarkBehaviour.target = NetworkCollisionDetect.player1ShootPuck.playerPuckList[NetworkCollisionDetect.player1ShootPuck.playerActivePuck].transform;
				NetworkCollisionDetect.player1ShootPuck.Puck = NetworkCollisionDetect.player1ShootPuck.playerPuckList[NetworkCollisionDetect.player1ShootPuck.playerActivePuck].GetComponent<Rigidbody>();
				NetworkCollisionDetect.player1ShootPuck.Pointer.transform.position = NetworkCollisionDetect.player1ShootPuck.Puck.gameObject.transform.position;
			}
		}
		else if(NetworkCollisionDetect.player2ShootPuck.playerPuckList.Contains(other.gameObject.transform.parent.gameObject) && isPlayer1){
			NetworkCollisionDetect.player2ShootPuck.playerPuckList.Remove(other.gameObject.transform.parent.gameObject);
			if(!NetworkCollisionDetect.player1ShootPuck.playerPuckList.Contains(other.gameObject.transform.parent.gameObject)){
				NetworkCollisionDetect.player1ShootPuck.playerPuckList.Add(other.gameObject.transform.parent.gameObject);
			}
			if(NetworkCollisionDetect.player2ShootPuck.playerPuckList.Count != 0){
				NetworkCollisionDetect.player2MarkBehaviour.target = NetworkCollisionDetect.player2ShootPuck.playerPuckList[NetworkCollisionDetect.player2ShootPuck.playerActivePuck].transform;
				NetworkCollisionDetect.player2ShootPuck.Puck = NetworkCollisionDetect.player2ShootPuck.playerPuckList[NetworkCollisionDetect.player2ShootPuck.playerActivePuck].GetComponent<Rigidbody>();
				NetworkCollisionDetect.player2ShootPuck.Pointer.transform.position = NetworkCollisionDetect.player2ShootPuck.Puck.gameObject.transform.position;
			}
		}

/*        ChangePuck.EnemyPuckList.Remove(other.gameObject.transform.parent.gameObject);
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
        }*/
	}
}
