using UnityEngine;
using System.Collections;

public class addJoint : MonoBehaviour {

    public Rigidbody2D rb;
    public float loverAngle = 0;
    public float upperAngle = 360;
    Vector3 anchorConnected;

	// Use this for initialization
	void Start () {
        if (rb)
        {
            HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
            anchorConnected = transform.localPosition - rb.transform.localPosition;
            joint.connectedAnchor = anchorConnected;
            joint.connectedBody = rb;
            //Debug.Log(anchorConnected.ToString() + "  " + transform.l);
        }
        else
        {
            HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
            joint.connectedAnchor = transform.position;
            joint.connectedBody = null;
        }

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(anchorConnected + transform.position, .03f);
    }
}
