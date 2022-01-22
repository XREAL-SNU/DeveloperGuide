/*
Path: /Assets/Scripts/UI/Rooms
*/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class CreateRoomMenu : MonoBehaviourPunCallBacks {
    [SerializeField]
    private Text  _roomName;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases) {
        _roomsCanvases = canvases;
    }

    public void OnClick_CreateRoom() {
        if(!PhotonNetwork.IsConnected()) {
            return;
        }

        RoomOptions options = new RoomOptions();
        options.maxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom() {
		MasterManager.DebugConsole.AddText("Created Room Successfully", this);
        _roomsCanvases.CurrentRoomCanvas.Show();
    }

	public override void OnCreatedRoomFailed(short returnCode, string message) {
		MasterManager.DebugConsole.AddText("Room Creation Failed: " + message, this);	
	}
}