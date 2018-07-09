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
		PhotonNetwork.CreateRoom("liujch1998" + "Library", roomOptions, TypedLobby.Default);
	}

	public override void OnJoinedRoom () {
		PhotonNetwork.Instantiate("room", Vector3.zero, Quaternion.identity, 0);
		PhotonNetwork.Instantiate("bookshelfarray", new Vector3(0f, 0f, 4f), Quaternion.Euler(0f, 180f, 0f), 0);

		GameObject.Find("bookshelfarray(Clone)").GetComponent<BookshelfController>().Init();
	}

	public override void OnPhotonPlayerConnected (PhotonPlayer player) {
		GameObject bookinhand = PhotonNetwork.Instantiate("bookinhand", new Vector3(0f, 1f, 0f), Quaternion.identity, 0);
		bookinhand.GetComponent<PhotonView>().TransferOwnership(player);
	}

}
