using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScaler : MonoBehaviour {

	public Transform book_transform;
	private GameObject books;
	private GetBook book;

	void Start () {
		var books = GameObject.FindGameObjectsWithTag("DynamicBook");
		if (books.Length) {
			book = books[0].GetComponent<GetBook>();
			book.transform.localScale = book_transform.localScale;
			book.transform.position = book_transform.position;
			book.transform.rotation = book_transform.rotation;
		}
	}

}
