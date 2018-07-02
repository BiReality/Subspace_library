using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfBook : MonoBehaviour {

	public Text text;
	public Book book;

	void Start () {
		text.text = "";
	}
	
	public void Set (Book book) {
		this.book = book;
		text.text = book.title;
	}

}
