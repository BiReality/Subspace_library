using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfController : MonoBehaviour {

	private List<BookInfo> books = new List<BookInfo>();

	public void Init () {
		ImportBookInfo();
		for (int shelf = 0; shelf < 1; shelf++) {
			for (int row = 0; row < 1; row++) {
				for (int col = 0; col < 12; col++) {
					int index = shelf * 60 + row * 12 + col;
					transform.GetChild(shelf).GetChild(row).GetChild(col).GetComponent<BookPlaceholderController>().Set(new int[]{0, 0}, books[index]);
				}
			}
		}
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
