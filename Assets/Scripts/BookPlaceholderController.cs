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

	[PunRPC]
	void InteractUse (int playerId) {
		GameObject bookinhand = null;
		GameObject[] books = GameObject.FindGameObjectsWithTag("bookinhand");
		foreach (GameObject book in books) {
			if (book.GetComponent<PhotonView>().ownerId == playerId) {
				bookinhand = book;
			}
		}
		if (bookinhand == null) return;
		bookinhand.GetComponent<BookController>().LoadBook(book_info);
	}
	
}
