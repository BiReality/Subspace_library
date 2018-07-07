using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;

public class BookController : MonoBehaviour {

	private List<string> pages;
	private int page_cnt = 0;
	private int page_idx_l = 0, page_idx_r = 1;

	[PunRPC]
	void InteractUse (int playerId) {
		FlipNext();
	}

	private void FlipNext () {
		int ms = page_idx_r;
		int ml = Mathf.Min(page_idx_l + 2, page_cnt - 2);
		page_idx_l = Mathf.Min(page_idx_l + 2, page_cnt - 2);
		page_idx_r = Mathf.Min(page_idx_r + 2, page_cnt - 1);
		StartCoroutine(AnimateFlipNext(ms, ml));
	}

	IEnumerator AnimateFlipNext (int ms, int ml) {
		transform.GetChild(2).GetChild(0).GetComponent<Text>().text = pages[page_idx_r];
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{2, 0}, pages[page_idx_r]);
		transform.GetChild(2).GetChild(1).GetComponent<Text>().text = page_idx_r.ToString();
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{2, 1}, page_idx_r.ToString());

		/*
		Transform page_m = PhotonNetwork.Instantiate("bookflippage", Vector3.zero, Quaternion.identity, 0).transform;
		page_m.parent = transform;
		page_m.GetComponent<PhotonView>().RPC("SetParent", PhotonTargets.Others, GetComponent<PhotonView>().viewID);
		page_m.GetChild(1).GetChild(0).GetComponent<Text>().text = pages[ms];
		page_m.GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{1, 0}, pages[ms]);
		page_m.GetChild(2).GetChild(0).GetComponent<Text>().text = pages[ml];
		page_m.GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{2, 0}, pages[ml]);

		for (float theta = 0f; theta < Mathf.PI; theta += Mathf.PI / 32f) {
			page_m.localPosition = new Vector3(0.125f * Mathf.Cos(theta), 0.125f * Mathf.Sin(theta), 0f);
			page_m.localRotation = Quaternion.Euler(0f, 0f, theta / Mathf.PI * 180f);
			yield return new WaitForSeconds(Time.deltaTime);
		}

		PhotonNetwork.Destroy(page_m.GetComponent<PhotonView>());
		*/
		yield return new WaitForSeconds(Time.deltaTime);

		transform.GetChild(1).GetChild(0).GetComponent<Text>().text = pages[page_idx_l];
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{1, 0}, pages[page_idx_l]);
		transform.GetChild(1).GetChild(1).GetComponent<Text>().text = page_idx_l.ToString();
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{1, 1}, page_idx_l.ToString());

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

	public void LoadBook (BookInfo book_info) {
		string title = book_info.title;
		string content = DownloadBookContent(book_info.link);
		pages = BookParser.GetPages(content);
		page_cnt = pages.Count;
		page_idx_l = 0; page_idx_r = 1;

		transform.GetChild(1).GetChild(0).GetComponent<Text>().text = pages[page_idx_l];
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{1, 0}, pages[page_idx_l]);
		transform.GetChild(1).GetChild(1).GetComponent<Text>().text = page_idx_l.ToString();
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{1, 1}, page_idx_l.ToString());

		transform.GetChild(2).GetChild(0).GetComponent<Text>().text = pages[page_idx_r];
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{2, 0}, pages[page_idx_r]);
		transform.GetChild(2).GetChild(1).GetComponent<Text>().text = page_idx_r.ToString();
		GetComponent<PhotonView>().RPC("SetText", PhotonTargets.Others, new int[]{2, 1}, page_idx_r.ToString());
	}

}
