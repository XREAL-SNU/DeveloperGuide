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

	private List<PlayerListing> _listings = new List<PlayerListing>();

	// private void Awake() {
	// 	GetCurrentRoomPlayers();
	// }

	private void Start() {
		foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players) {
			AddPlayerListing(playerInfo.Value);
		}
	}

	private void GetCurrentRoomPlayers() {
		_listings.Clear();
		foreach (Transform child in _content) {
			Destroy(child.gameObject);
		}

		if (PhotonNetwork.CurrentRoom == null) {
			return;
		}

		foreach (Player player in PhotonNetwork.PlayerList) {
			PlayerListing listing = Instantiate(_playerListing, _content);
			if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber) {
				listing.transform.GetChild(0).GetComponent<Text>().text = "You";
			}
			listing.setPlayerInfo(player);
			_listings.Add(listing);
		}
	}

	private void AddPlayerListing(Player player) {
		PlayerListing listing = Instantiate(_playerListing, _content);
		if(listing != null) {
			listing.setPlayerInfo(newPlayer);
			_listings.Add(listing);
		}
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
}