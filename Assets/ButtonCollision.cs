using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCollision : MonoBehaviour {

	public Image bg_image;
	public ButtonsHandler buttons_handler;

	void OnTriggerEnter (Collider other) {
		if (other.tag.Contains("Toucher")) {
			buttons_handler.clicked(this, other);
		}
	}

}
