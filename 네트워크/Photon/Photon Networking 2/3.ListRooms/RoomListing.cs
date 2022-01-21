using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviourPunCallbacks {
	[SerializeField]	
	private Text _text;

	public RoomInfo RoomInfo {
		get;
		private set;
	}

	public void setTRoomInfo(RoomInfo roomInfo) {
		RoomInfo = roomInfo;
		_text.text = roomInfo.MaxPlayers + ", "	+ roomInfo.Name;
	}
}