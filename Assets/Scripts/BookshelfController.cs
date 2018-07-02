using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfController : MonoBehaviour {

	private List<BookInfo> books = new List<BookInfo>();


	void Start () {
		ImportBookInfo();
		GetComponent<BookController>().PopulateBook(books[0]);
	}
	
	void Update () {
		
	}

	private void ImportBookInfo () {
		string s = System.IO.File.ReadAllText("Assets/Scripts/booklist.txt");
		string[] lines = s.Split('\n');
		for (int i = 0; i < lines.Length; i += 6) {
			BookInfo book_info = new BookInfo();
			book_info.title = lines[i+0];
			book_info.author = lines[i+1];
			book_info.link = lines[i+2];
			book_info.lang = lines[i+3];
			books.Add(book_info);
		}
	}

}
