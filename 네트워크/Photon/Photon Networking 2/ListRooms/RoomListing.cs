using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviourPunCallbacks {
	[SerializeField]	
	private Text _text;

	public void setTRoomInfo(RoomInfo roomInfo) {
		_text.text = roomInfo.MaxPlayers + ", "	+ roomInfo.Name;
	}
}