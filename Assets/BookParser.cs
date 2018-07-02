using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookParser {

	public static int GetNextWordLen (string content, int ptr) {
		int length = 0;
		while (content[ptr] != ' ' && content[ptr] != '\n') {
			ptr++;
			if (ptr >= content.Length) break;
			length++;
		}
		return length;
	}

	public static List<string> GetPages (string content, int PAGE_WIDTH, int PAGE_HEIGHT) {
		int WORD_CUT_LENGTH = PAGE_WIDTH / 4;
		const string PAGE_BREAK_PLACEHOLDER = "\x0b";
		const string TRIPLE_NEWLINE_PLACEHOLDER = "\x0c";
		const string DOUBLE_NEWLINE_PLACEHOLDER = "\a";
		const string NEWLINE_SPACE_PLACEHOLDER = "\x10";

		content = content.Replace("\r\n", "\n").Replace("\r", "\n");
		for (int i = 8; i >= 4; i--) {
			content = content.Replace("\n" * i, PAGE_BREAK_PLACEHOLDER);
		}
		content = content.Replace("\n" * 3, TRIPLE_NEWLINE_PLACEHOLDER);
		content = content.Replace("\n" * 2, DOUBLE_NEWLINE_PLACEHOLDER);
		content = content.Replace("\n ", NEWLINE_SPACE_PLACEHOLDER);
		content = content.Replace("\n", " ");
		content = content.Replace(NEWLINE_SPACE_PLACEHOLDER, "\n ");
		content = content.Replace(DOUBLE_NEWLINE_PLACEHOLDER, "\n\n");
		content = content.Replace(TRIPLE_NEWLINE_PLACEHOLDER, "\n\n\n");
		content = content.Replace("\x08", "");

		List<string> pages = new List<>();
		int x = 0, y = 0;
		string page = "";
		for (int i = 0; i < content.Length; i++) {
			string cur_char = "" + content[i];
			x++;
			int next_word_len = GetNextWordLen(content, i);

			if (cur_char == PAGE_BREAK_PLACEHOLDER) {  // signaled page break
				if (page.Trim() != "") {
					pages.Add(page);
				}
				page = ""; x = y = 0;
			} else if (y > PAGE_HEIGHT - 2) {  // natural page break
				if (page.Trim() != "") {
					pages.Add(page);
				}
				page = ""; x = y = 0;
				i--;
			} else if (cur_char == "\n") {  // signaled new line
				page += "\n";
				x = 0; y++;
			} else if (x == PAGE_WIDTH || x + next_word_len > PAGE_WIDTH) {  /// natural new line
				if (next_word_len > WORD_CUT_LENGTH) {
					int allowed = PAGE_WIDTH - x - 1;
					for (int j = 0; j < allowed; j++) {
						page += content[i+j];
					}
					if (allowed && content[i+allowed-1] != ' ') {
						page += "-";
					}
					i += allowed;
				}
				page += "\n"; x = 0; y++;
				i--;
			} else {
				if (i < content.Length - 1 && x == 1 && cur_char == " " && content[i+1] != ' ') {
					x--;
				} else {
					page += cur_char;
				}
			}
		}
		pages.Add(page);

		return pages;
	}

}
