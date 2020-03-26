using UnityEngine;
using System.Collections;

public class scriptTestJoint : MonoBehaviour {

	// Use this for initialization
	void Start () {

        /*if (GetComponent<HingeJoint2D>().connectedBody)
        {
            Vector3 pos = transform.localPosition;
            pos.x *= transform.localScale.x;
            pos.y *= transform.localScale.y;
            transform.localPosition = pos;
        }*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.localPosition + transform.position, .03f);
    }
}
