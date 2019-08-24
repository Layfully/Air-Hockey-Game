using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Disconnect : MonoBehaviour {

	public void DropConnection(){
		MatchInfo matchinfo = NetworkManager.singleton.matchInfo;
		NetworkManager.singleton.matchMaker.DropConnection(matchinfo.networkId,matchinfo.nodeId,0,NetworkManager.singleton.OnDropConnection);
		NetworkManager.singleton.StopHost();
	}
}
