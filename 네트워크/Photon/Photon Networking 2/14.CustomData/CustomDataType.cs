/*
Path: Assets/Scripts/SingleUse/CustomDataTypeExample
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Ling;
using System.Text;

public class CustomDataTypes : MonoBehaviourPunCallbacks {
    [SerializeField]
    private MyCustomSerialization _customSerialization = new MyCustomSerialization();
    [SerializeField]
    private bool _sendAsTyped = true;

    private void Start() {
        // First: Type, Second: 'M' is 255 ; choose from 0 ~ 255
        // Register Custom Data Type
        PhotonPeer.RegisterType(typeof(MyCustomSerialization), (byte)'M', MyCustomSerialization.Serialize, MyCustomSerialization.Deserialize);
    }

    private void Update() {
        if(_customSerialization.MyNumber != -1) {
            SendCustomSerialization(_customSerialization, _sendAsTyped);
            _customSerialization.MyNumber = -1;
            _customSerialization.MyString = string.Empty;
        }
    }

    private void SendCustomSerialization(MyCustomSerialization data, bool typed) {
        if(typed) base.photonView.RPC("RPC_TypedReceiveMyCustomSerialization", RpcTarget.AllViaServer, _customSerialization);
        else base.photonView.RPC("RPC_ReceiveMyCustomSerialization", RpcTarget.AllViaServer, MyCustomSerialization.Serialize(_customSerialization));
    }

    [PunRPC]
    private void RPC_TypedReceiveMyCustomSerialization(MyCustomSerialization datas) {
        Print("Receive Type: " + datas.MyNumber + ", " + datas.MyString);
    }

    [PunRPC]
    private void RPC_ReceiveMyCustomSerialization(byte[] datas) {
        MyCustomSerialization data = MyCustomSerialization.Deserialize(datas);
        Print("Receive Type: " + result.MyNumber + ", " + result.MyString);
    }
}
