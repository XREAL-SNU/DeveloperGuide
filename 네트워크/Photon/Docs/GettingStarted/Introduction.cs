
/*
Connect And Callbacks
*/
// gets you online in no time: It grabs all important settings from the PhotonServerSettings asset and off you go.
PhotonNetwork.ConnectUsingSettings();

public class YourClass : MonoBehaviourPunCallbacks
{
    // ...
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinRandomRoom();
    }
    // ...
}

/*
Matchmaking
*/
// Join room "someRoom"
PhotonNetwork.JoinRoom("someRoom");
//Fails if "someRoom" is not existing, closed or full. Error callback: IMatchmakingCallbacks.OnJoinRoomFailed

// Tries to join any random game:
PhotonNetwork.JoinRandomRoom();
//Fails if there are no open games. Error callback: IMatchmakingCallbacks.OnJoinRandomFailed

// Create this room.
PhotonNetwork.CreateRoom("MyMatch");
// Fails if "MyMatch" room already exists and calls: IMatchmakingCallbacks.OnCreateRoomFailed

RoomOptions roomOptions = new RoomOptions();
roomOptions.IsVisible = false;
roomOptions.MaxPlayers = 4;
PhotonNetwork.JoinOrCreateRoom(nameEveryFriendKnows, roomOptions, TypedLobby.Default);

/*
Game Logic
*/
// used as Observed component in a PhotonView, this only reads/writes the position
public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
{
    if (stream.IsWriting)
    {
        Vector3 pos = transform.localPosition;
        stream.Serialize(ref pos);
    }
    else
    {
        Vector3 pos = Vector3.zero;
        stream.Serialize(ref pos);  // pos gets filled-in. must be used somewhere
    }
}

// defining a method that can be called by other clients:
[PunRPC]
public void OnAwakeRPC(byte myParameter)
{
    //Debug.Log(string.Format("RPC: 'OnAwakeRPC' Parameter: {0} PhotonView: {1}", myParameter, this.photonView));
}

// calling the RPC somewhere else
photonView.RPC("OnAwakeRPC", RpcTarget.All, (byte)1);

PhotonNetwork.RaiseEvent(eventCode, eventContent, raiseEventOptions, SendOptions.SendReliable);
