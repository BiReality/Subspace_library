using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SceneController : Photon.PunBehaviour {

	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	public override void OnConnectedToMaster () {
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.IsVisible = true;
		roomOptions.MaxPlayers = 0;  // no limit
		PhotonNetwork.CreateRoom("Subspace", roomOptions, TypedLobby.Default);
	}

	public override void OnJoinedRoom () {
		PhotonNetwork.Instantiate("room", Vector3.zero, Quaternion.identity, 0);
		PhotonNetwork.Instantiate("bookshelfarray", Vector3.zero, Quaternion.identity, 0);

		GameObject.Find("bookshelfarray(Clone)").GetComponent<BookshelfController>().Init();
	}

}
