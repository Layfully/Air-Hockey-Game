using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class NetworkCollisionDetect : MonoBehaviour {
	public GameObject particlePrefab;
    public AudioSource collsionSound;
	public static NetworkMarkBehaviour player1MarkBehaviour;
    public static NetworkMarkBehaviour player2MarkBehaviour;
	public static NetworkShootPuck player1ShootPuck;
    public static NetworkShootPuck player2ShootPuck;
    public static NetworkRotatePuck player1RotatePointer;
    public static NetworkRotatePuck player2RotatePointer;
	public static NetworkPlayerZone player1Zone;
	public static NetworkPlayerZone player2Zone;
	public NetworkInstanceId id;
	public Text text;

	void Start(){
		text = FindObjectOfType<Text>();
		Setup();
	}
	public static void Setup(){
		Debug.Log("Remove it later");
		NewGame.mode = NewGame.GameMode.Multiplayer;

		if(NewGame.mode == NewGame.GameMode.Multiplayer){
			foreach(GameObject item in GameObject.FindGameObjectsWithTag("PlayerMark")){
				if(player1MarkBehaviour == null){
					player1MarkBehaviour = item.GetComponent<NetworkMarkBehaviour>();
				}
				else if(item.GetComponent<NetworkMarkBehaviour>() != player1MarkBehaviour){
					player2MarkBehaviour = item.GetComponent<NetworkMarkBehaviour>();
				}
			}
			foreach(GameObject item in GameObject.FindGameObjectsWithTag("PucksP1")){
				if(player1ShootPuck == null){
					player1RotatePointer = item.GetComponent<NetworkRotatePuck>();
					player1ShootPuck = item.GetComponent<NetworkShootPuck>();
				}
				else if(item.GetComponent<NetworkShootPuck>() != player1ShootPuck){
					player2RotatePointer = item.GetComponent<NetworkRotatePuck>();
					player2ShootPuck = item.GetComponent<NetworkShootPuck>();
				}
			}

			foreach(GameObject item in GameObject.FindGameObjectsWithTag("MultiplayerZone")){
				if(player1Zone == null){
					player1Zone = item.GetComponent<NetworkPlayerZone>();
					player1Zone.isPlayer1 = true;
				}
				else if(item.GetComponent<NetworkPlayerZone>() != player1Zone){
					player2Zone = item.GetComponent<NetworkPlayerZone>();
					player2Zone.isPlayer1 = false;
				}
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Puck")){
			if(player1ShootPuck.Fired || player2ShootPuck.Fired){
				Debug.Log("simulate particles here");
			}

			 if(player1ShootPuck.playerPuckList[player1ShootPuck.playerActivePuck] == gameObject && player1ShootPuck.Fired){
				player1ShootPuck.playerActivePuck++;

				if(player1ShootPuck.playerActivePuck >= player1ShootPuck.playerPuckList.Count){
					player1ShootPuck.playerActivePuck = 0;
				}
				
				player1MarkBehaviour.target = player1ShootPuck.playerPuckList[player1ShootPuck.playerActivePuck].transform;
				player1RotatePointer.target = player1MarkBehaviour.target;
				player1ShootPuck.Puck = player1ShootPuck.playerPuckList[player1ShootPuck.playerActivePuck].GetComponent<Rigidbody>();
	            player1ShootPuck.Pointer.transform.position = player1ShootPuck.Puck.gameObject.transform.position;
				player1ShootPuck.Fired = false;

			 }
			 else if(player2ShootPuck.playerPuckList[player2ShootPuck.playerActivePuck] == gameObject && player2ShootPuck.Fired){

				player2ShootPuck.playerActivePuck++;

				if(player2ShootPuck.playerActivePuck >= player2ShootPuck.playerPuckList.Count){
					player2ShootPuck.playerActivePuck = 0;
				}
				player2MarkBehaviour.target = player2ShootPuck.playerPuckList[player2ShootPuck.playerActivePuck].transform;
				player2RotatePointer.target = player2MarkBehaviour.target;
				player2ShootPuck.Puck = player2ShootPuck.playerPuckList[player2ShootPuck.playerActivePuck].GetComponent<Rigidbody>();
	            player2ShootPuck.Pointer.transform.position = player2ShootPuck.Puck.gameObject.transform.position;
				player2ShootPuck.Fired = false;
			 }
		}
	}
}
