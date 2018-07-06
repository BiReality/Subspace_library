using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPlaceholderController : MonoBehaviour {

	private int[] child_index;
	private BookInfo book_info;

	public void Set (int[] child_index, BookInfo book_info) {
		this.child_index = child_index;
		this.book_info = book_info;
		GetComponentInChildren<UnityEngine.UI.Text>().text = book_info.title;
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.OthersBuffered, child_index, book_info.title);
	}


	/*
	// Client
	public override void StartUsing (VRTK_InteractUse usingObject) {
		base.StartUsing(usingObject);
		GameObject.Find("bookinhand").GetComponent<BookController>().LoadBook(book_info);
	}
	*/
}
