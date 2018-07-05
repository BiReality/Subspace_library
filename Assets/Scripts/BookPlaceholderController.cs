using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class BookPlaceholderController : VRTK_InteractableObject {

	private BookInfo book_info;

	// Server
	public void Set (BookInfo book_info) {
		this.book_info = book_info;
		GetComponentInChildren<Text>().text = book_info.title;
		GetComponent<PhotonView>().RPC("SetClient", PhotonTargets.AllBuffered, book_info.title, book_info.link);
	}

	// Client
	[PunRPC]
	public void SetClient (string title, string link) {
		this.book_info.title = title;
		this.book_info.link = link;
		GetComponentInChildren<Text>().text = book_info.title;
	}

	// Client
	public override void StartUsing (VRTK_InteractUse usingObject) {
		base.StartUsing(usingObject);
		GameObject.Find("bookinhand").GetComponent<BookController>().LoadBook(book_info);
	}

}
