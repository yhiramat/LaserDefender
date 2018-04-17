﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;

	public float speed;
	public float padding;
	public float projectileSpeed;
	public float firingRate;
	//public Rigidbody2D player; // thought I need this...

	float xmin;
	float xmax;
	//Vector3 topmost;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;

		//Vector3 topmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, distance));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime; // more readable and simpler than the way above

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime; // more readable and simpler than the way above

		}

		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("being pushed");
			InvokeRepeating ("Fire", 0.00001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void Fire () {
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed, 0);
		Debug.Log ("fire!");
	}
}