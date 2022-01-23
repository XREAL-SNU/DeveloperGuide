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

	public override void OnMasterClientSwitched(Player newMasterClient) {
		// Make All of the Players in the Room Leave When the Master Client Changes (Leaves)
		// Comment this Code Out to Let Users Play Even When the Master Client Changes (Leaves)
		_roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
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
		// Edit this to Change Who Can Start the Game
		if(PhotonNetwork.IsMasterClient) {
			// Edit this to Allow the Game to be Started without Everyone Selecting Ready
			for(int i=0; i < _listings.Count; i++) {
				if(_listings[i].Player != PhotonNetwork.LocalPlayer) {
					if(!_listings[i].Ready) return;
				}
			}

			PhotonNetwork.CurrentRoom.IsOpen = false;
			PhotonNetwork.CurrentRoom.IsVisible = false;
			PhotonNetwork.LoadLevel(1);
		}
	}

	public void OnClick_ReadyUp() {
		if(!PhotonNetwork.IsMasterClient) {
			SetReadyUp(!_ready);
			base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);
		}
	}

	[PunRPC]
	private void RPC_ChangeReadyState(Player player, bool ready) {
		int index = _listings.FindIndex(x => x.Player == player);
		if(index != -1) {
			_listings[index].Ready = ready;
		}
	}
}