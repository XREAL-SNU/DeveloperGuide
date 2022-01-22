/*
Path: /Assets/Scripts/UI/Rooms
*/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsMenu : MonoBehaviourPunCallbacks {
	[SerializeField]	
	private Transform _content;
	[SerializeField]
	private RoomListing _roomListing;

	private List<RoomListing> _listings = new List<RoomListing>();
	private RoomsCanvases _roomsCanvases;

	public void FirstInitialize(RoomsCanvases canvases) {
		_roomsCanvases = canvases;
	}

	public override void OnJoinedRoom() {
		_roomsCanvases.CurrentRoomCanvas.Show();
		_content.DestroyChildren();
		_listings.Clear();
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList) {
		foreach(RoomInfo info in roomList) {
			if(info.RemovedFromList) {
				// Removed From RoomList
				int index = _listings.FindIndex( x => x.RoomInfo.Name == info.Name);
				if(index != -1) {
					Destroy(_listings[index].gameObject);
					_listings.RemoveAt(index);
				}
			} else {
				// Added to RoomList
				int index = _listings.FindIndex( x => x.RoomInfo.Name == info.Name);
				if(index == -1) {
					RoomListing listing = Instantiate(_roomListing, _content);
					if(listing != null) {
						listing.setRoomInfo(info);
						_listings.Add(listing);
					}
				} else {
					_listings[index].setRoomInfo(info);
				}
			}
		}
	}
}