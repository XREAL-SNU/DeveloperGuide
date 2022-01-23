/*
Path: Assets/Scirpts/UI/Rooms
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour {
    private ExitGames.Client.Photon.Hashtable _properties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField]
    private Text _text;

    private void SetCustomNumber() {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 100);
        _text.text = result.ToString();

        _properties["RandomNumber"] = result;
        // _properties.Remove("RandomNumber");
        PhotonNetwork.SetPlayerCustomProperties(_properties);
        // PhotonNetwork.LocalPlayer.CustomProperties = _properties;
    }

    public void OnClick_Button() {
        SetCustomNumber();
    }
}
