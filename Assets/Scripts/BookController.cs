using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using TLGFPowerBooks;

public class BookController : MonoBehaviour {

	public int font_size = 40;
	public Font font;

	private PBook pBook;
	private int page_cur = 0;
	private int page_cnt = 0;
	private float time_since_last_turn;

	void Start () {
		pBook = GetComponent<PBook>();
	}
	
	void Update () {
		time_since_last_turn += Time.deltaTime;

		if ((OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x > 0) && page_cur <= page_cnt && time_since_last_turn > 1f) {
			time_since_last_turn = 0f;
			if (page_cur == 0) {
				pBook.OpenBook();
			} else {
				pBook.NextPage();
			}
			page_cur += 2;
		}
		if ((OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x < 0) && time_since_last_turn > 1f) {
			time_since_last_turn = 0f;
			page_cur -= 2;
			if (page_cur <= 0) {
				pBook.CloseBook();
			} else {
				pBook.PrevPage();
			}
		}
	}

	private string DownloadBookContent (string link) {
		string content = "";
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
		request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
		using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
			using (Stream stream = response.GetResponseStream()) {
				using (StreamReader reader = new StreamReader(stream)) {
					content = reader.ReadToEnd();
				}
			}
		}
		return content;
	}

	public void PopulateBook (BookInfo book_info) {
		string title = book_info.title;
		string content = DownloadBookContent(book_info.link);

		if (title.Length > 100) {
			title = title.Substring(0, 100) + "...";
		}
		Text title_text = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
		title_text.text = title;
		title_text.font = font;
		title_text.verticalOverflow = VerticalWrapMode.Overflow;

		title_text = transform.GetChild(1).GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>();
		title_text.text = title;
		title_text.font = font;
		title_text.verticalOverflow = VerticalWrapMode.Overflow;

		List<string> pages = BookParser.GetPages(content);
		page_cnt = pages.Count;
		for (int i = 0; i < Mathf.Min(page_cnt, 100); i++) {
			Text page_text = pBook.contentContainer.GetChild(i).GetChild(0).GetComponent<Text>();
			page_text.text = pages[i];
			page_text.font = font;
			page_text.fontSize = font_size;
			page_text.alignment = TextAnchor.MiddleLeft;
			Text pageno_text = pBook.contentContainer.GetChild(i).GetChild(1).GetComponent<Text>();
			pageno_text.text = "" + (int)(i+1);
			pageno_text.font = font;
		}
	}

}
