/*
Path: Assets/Scripts/SingleUse
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OwnershipTransfer : MonoBehaviourPun, IPunOwnershipCallbacks {
    // Could use OnEnable and OnDisable instead
    private void Awake() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    
    private void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer) {
        if(targetView != base.photonView) return;

        base.photonView.TransferOwnership(requestingPlayer);
    }

    private void OnOwnershipTransferred(PhotonView targetView, Player newOwner) {
        if(targetView != base.photonView) return;
    }
    
    private void OnMouseDown() {
        base.photonView.RequestOwnership();
    }
}
