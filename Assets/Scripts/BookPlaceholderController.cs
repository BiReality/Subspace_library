using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class BookPlaceholderController : VRTK_InteractableObject {

	private BookInfo book_info;

	public void Set (BookInfo book_info) {
		this.book_info = book_info;
		GetComponentInChildren<Text>().text = book_info.title;
	}

	protected void Start () {
		this.isUsable = true;
	}

	public override void StartUsing (VRTK_InteractUse usingObject) {
		base.StartUsing(usingObject);
		GameObject.Find("BookInHand").GetComponent<BookController>().PopulateBook(book_info);
	}

}
