using UnityEngine;
using System.Collections;

public class playerStart : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var player = GameObject.Find("player");
        player.transform.position = transform.position;
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
