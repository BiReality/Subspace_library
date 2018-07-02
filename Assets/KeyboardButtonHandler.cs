using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButtonHandler : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.tag.Contains("Toucher")) {
			GetComponent<Button>().onClick.Invoke();
		}
	}

}
