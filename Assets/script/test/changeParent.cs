using UnityEngine;
using System.Collections;

public class changeParent : MonoBehaviour {

    public Transform parent;

	// Use this for initialization
    void Start()
    {
        gameObject.transform.parent = parent;
    }
	// Update {s called once per frame
	void Update () {
	
	}
}
