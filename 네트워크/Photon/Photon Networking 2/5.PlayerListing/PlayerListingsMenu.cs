/*
Path: /Assets/Scripts/UI/Rooms
*/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks {
	[SerializeField]	
	private Transform _content;
	[SerializeField]
	private PlayerListing _playerListing;
	[SerializeField]
	private Text _readyUpText;

	private List<PlayerListing> _listings = new List<PlayerListing>();
	private RoomsCanvases _roomsCanvases;
	private bool _ready = false;

	// private void Awake() {
	// 	GetCurrentRoomPlayers();
	// }

	// private void Start() {
	// 	foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players) {
	// 		AddPlayerListing(playerInfo.Value);
	// 	}
	// }

	public override void OnEnable() {
		base.OnEnable();
		SetReadyUp(false);
		GetCurrentRoomPlayers();
	}

	public override void OnDisable() {
		base.OnDisable();
		RemovePlayerListings();
	}

	public void FirstInitialize(RoomsCanvases canvases) {
		_roomsCanvases = canvases;
	}

	private void SetReadyUp(bool state) {
		_ready = state;
		if(_ready) _readyUpText.text = "Ready";
		else _readyUpText.text = "Not Ready";
	}

	// private void GetCurrentRoomPlayers() {
	// 	_listings.Clear();
	// 	foreach (Transform child in _content) {
	// 		Destroy(child.gameObject);
	// 	}

	// 	if (PhotonNetwork.CurrentRoom == null) {
	// 		return;
	// 	}

	// 	foreach (Player player in PhotonNetwork.PlayerList) {
	// 		PlayerListing listing = Instantiate(_playerListing, _content);
	// 		if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber) {
	// 			listing.transform.GetChild(0).GetComponent<Text>().text = "You";
	// 		}
	// 		listing.setPlayerInfo(player);
	// 		_listings.Add(listing);
	// 	}
	// }

	private void GetCurrentRoomPlayers() {
		if(!PhotonNetwork.IsConnected) return;
		if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;

		foreach(KeyValuePair<int, Player>playerInfo in PhotonNetwork.CurrentRoom.Players) {
			AddPlayerListing(playerInfo.Value);
		}
	}

	private void AddPlayerListing(Player player) {
		int index = _listings.FindIndex(x => x.Player == player);
		if(index != -1) {
			_listings[index].setPlayerInfo(player);
			return;
		}

		PlayerListing listing = Instantiate(_playerListing, _content);
		if(listing != null) {
			listing.setPlayerInfo(newPlayer);
			_listings.Add(listing);
		}
	}

	private void RemovePlayerListings() {
		foreach(PlayerListing listing in _listings) {
			Destroy(listing.gameObject);
		}
		_listings.Clear();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer) {
		AddPlayerListing(newPlayer);
	}

	public override void OnPlayerLeftRoom(Player otherPlayer) {
		int index = _listings.FindIndex( x => x.Player == otherPlayer);
		if(index != -1) {
			Destroy(_listings[index].gameObject);
			_listings.RemoveAt(index);
		}
	}

	public void OnClick_StartGame() {
		if(PhotonNetwork.IsMasterClient) {
			
			PhotonNetwork.LoadLevel(1);
		}
	}
}