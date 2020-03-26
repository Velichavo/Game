using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour {

	public bool shoot = false;
	public float shootingTime;
	public float reloadingTime;
	public int amountBullet;
	public int amountHolder;
	float counterShooting  = -1f;
	float counterReloading = -1f;
	public float bulletSpeed;
	public Transform bltStart;

	public Object Bullet;

	bullet scrBullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		counterShooting -= 1f;

		if (shoot && counterShooting < 0 && counterReloading < 0) 
		{
			Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float posx = posMouse.x - transform.position.x;
			float posy = posMouse.y - transform.position.y;

			GameObject  blt = Instantiate(Bullet, bltStart.position, transform.rotation) as GameObject;
			Debug.Log(blt.ToString());
			scrBullet = blt.transform.GetComponent<bullet>();
			scrBullet.direction = new Vector2(posx, posy);
			scrBullet.speed = bulletSpeed;

			counterShooting = shootingTime;
			shoot = false;

			Debug.Log(transform.rotation.ToString());
		}
	}
}
