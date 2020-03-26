using UnityEngine;
using System.Collections;

public class target : MonoBehaviour {

	Vector3 mousePos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		transform.position = mousePos;
	}
}
