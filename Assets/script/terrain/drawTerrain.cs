using UnityEngine;
using System.Collections;

public class drawTerrain : MonoBehaviour {

    float depthTerrain = 0.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        /*EdgeCollider2D edges = transform.GetComponent<EdgeCollider2D>();

        for (int i = 0; i < edges.pointCount - 1; i++)
        {
            Vector3 begin = new Vector3(edges.points[i].x, edges.points[i].y, 0) + transform.position;
            Vector3 destination = new Vector3(edges.points[i + 1].x, edges.points[i + 1].y, 0) + transform.position;
            Debug.DrawLine(begin, destination);
        }*/
	}

	void OnDrawGizmos()
	{
		EdgeCollider2D edges = transform.GetComponent<EdgeCollider2D>();

		Gizmos.color = Color.black;

		for( int i = 0; i < edges.pointCount - 1; i++)
		{
			Vector3 begin = new Vector3(edges.points[i].x, edges.points[i].y, 0) + transform.position;
            Vector3 destination = new Vector3(edges.points[i + 1].x, edges.points[i + 1].y, 0) + transform.position;
			Gizmos.DrawLine(begin, destination);
            Vector3 vec1 = new Vector3(-(edges.points[i + 1].y - edges.points[i].y), edges.points[i + 1].x - edges.points[i].x , 0);
            Vector3 vec2 = new Vector3(-(edges.points[i].y - edges.points[i + 1].y), edges.points[i].x - edges.points[i + 1].x, 0);

			Gizmos.DrawLine(begin, begin - vec1.normalized * depthTerrain);
            Gizmos.DrawLine(destination, destination + vec2.normalized * depthTerrain);
            Gizmos.DrawLine(begin - vec1.normalized * depthTerrain, destination + vec2.normalized * depthTerrain);
            Gizmos.DrawSphere(begin, 0.05f);

			float lenght = Mathf.Sqrt(Mathf.Pow(destination.x - begin.x, 2) + Mathf.Pow(destination.y - begin.y, 2));

			Vector3 vecB = (destination - begin) / lenght;
		
			for(float j = 0; j < lenght; j += 0.1f)
			{
				Gizmos.DrawLine(begin + vecB * j, begin + vecB * j +new Vector3(0, 0.1f, 0));
			}

		}

		//Debug.Log(str + "zfz");
	}
}
