using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsMenu : MonoBehaviourPunCallbacks {
	[SerializeField]	
	private Transform _content;
	[SerializeField]
	private RoomListing _roomListing;

	public override void OnRoomListUpdate(List<RoomInfo> roomList) {
		foreach(RoomInfo info in roomList) {
			RoomListing listing = Instantiate(_roomListing, _content);
			if(listing != null) {
				listing.setRoomInfo(info);
			}
		}
	}
}