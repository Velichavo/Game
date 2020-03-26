using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public Vector2 direction;
	public float speed;
	Ray2D ray;
	Vector2 prevPosition;

	// Use this for initialization
	void Start () {
		prevPosition = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, speed * Time.deltaTime);

		if (hit.Length > 0) 
		{
			foreach (RaycastHit2D h in hit) 
			{
				if (h.collider.gameObject.tag == "Ground") 
				{
				Destroy (gameObject);
				}
			}
		}


		prevPosition = transform.position;
		Vector2 vec = direction.normalized * speed * Time.deltaTime;
		transform.position += new Vector3 (vec.x, vec.y); 
	
	}
	

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Vector2 v = direction.normalized * speed;
		//Gizmos.DrawLine(transform.position, transform.position + new Vector3(v.x, v.y));
		Gizmos.DrawLine(prevPosition, transform.position);
	}

}
