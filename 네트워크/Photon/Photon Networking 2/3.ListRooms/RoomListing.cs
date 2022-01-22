/*
Path: /Assets/Scripts/UI/Rooms
*/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {
	[SerializeField]	
	private Text _text;

	public RoomInfo RoomInfo {
		get;
		private set;
	}

	public void setRoomInfo(RoomInfo roomInfo) {
		RoomInfo = roomInfo;
		_text.text = roomInfo.MaxPlayers + ", "	+ roomInfo.Name;
	}

	public void OnClick_Button() {
		// if (PhotonNetwork.InLobby) {
		// 	PhotonNetwork.LeaveLobby();
		// }
		PhotonNetwork.JoinRoom(RoomInfo.Name);
	}
}