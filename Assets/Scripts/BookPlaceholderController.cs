using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPlaceholderController : MonoBehaviour {

	private BookInfo book_info;

	public void Set (BookInfo book_info) {
		this.book_info = book_info;
		GetComponentInChildren<Text>().text = book_info.title;
	}

}
