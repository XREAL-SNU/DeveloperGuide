/*
Path: /Assets/Scripts/UI/Rooms
*/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour {
	[SerializeField]	
	private Text _text;

	public Player Player {
		get;
		private set;
	}

	public void setPlayerInfo(Player player) {
		Player = player;
		_text.text = player.NickName;
	}
}