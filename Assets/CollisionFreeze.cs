using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFreeze : MonoBehaviour {

	public Vector3 safe_pos;
	public Quaternion safe_rot;
	public Vector3 prev_pos;
	public Quaternion prev_rot;

	void Start () {
		safe_pos = transform.position;
		safe_rot = transform.rotation;
	}
	
	void Update () {
		prev_pos = safe_pos;
		prev_rot = safe_rot;
		safe_pos = transform.position;
		safe_rot = transform.rotation;
	}

	void OnCollisionStay (Collision collision) {
		transform.position = safe_pos;
		transform.rotation = safe_rot;
		safe_pos = prev_pos;
		safe_rot = prev_rot;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

}
