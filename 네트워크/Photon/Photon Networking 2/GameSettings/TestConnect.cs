using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallBakcs {

	public void start() {
		print("Connecting to Server");
		PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
		PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
		PhotonNetwork.ConnectUsingSettings();
	}
	
	public override void onConnectedToMaster() {
		print(”Connected to Server”);
		print(PhotonNetwork.LocalPlayer.NickName);
	}

	public override void OnDisconnected(DisconnectCause cause) {
		print("Disconnected from server: " + cause.ToString());
	}
	
}