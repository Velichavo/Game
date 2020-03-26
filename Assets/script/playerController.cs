using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	Vector3 position;
	Vector2 direction;
	float angle;
	float coefBody;
	float coefArm;
	Transform body;
	Transform armLeft;
	Transform armRight;
	Transform armRight2;
	Transform head;
	GameObject text;
	public bool showVar = true;
	public float moveForce = 50f;		
	public float maxSpeed = 0.1f;			
	public float jumpForce = 1000f;
	Animator anim;
	Transform cam;
	float h;
	Rigidbody2D rigidBody2D;
	public Transform onGround;
	bool jump = false;
	bool grounded = false;
	Transform currenWeapon;
	Vector2 currentWeaponDirection;
	public Transform weapon;
	Vector3 posMouse;
	weapon wep;
    public float hitPoint = 100;
    public Transform[] bodyPart;
    public Transform[] bodyJoint;
    public Vector2[] partConstraint;
    public Transform wepCollision;
    float coefBend = 40;
    float weaponAngle = 0;

    #region
    enum namePart : int { nBody, nHead = 1, nForearmLeft = 2, nForearmRight = 5, nForearmRight2 = 6 }; 
    #endregion


    // Use this for initialization
	void Start () {
		body = bodyPart[(int)namePart.nBody];
        armLeft = bodyPart[(int)namePart.nForearmLeft];
        armRight = bodyPart[(int)namePart.nForearmRight];
        armRight2 = bodyPart[(int)namePart.nForearmRight2];
        head = bodyPart[(int)namePart.nHead];
		anim = GetComponent<Animator>();
		rigidBody2D = GetComponent<Rigidbody2D>();
		//weapon = GetComponent<Transform>().Find("weapon");
        cam = GameObject.Find("Main Camera").transform;
	}
	
	// Update is called once per frame
	void Update () {

        CameraPosition();

        if (GetComponent<Rigidbody2D>())
        {
            position = Camera.main.WorldToScreenPoint(armLeft.position);
            direction = Input.mousePosition - position;
            Vector3 posMain = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dirMain = Input.mousePosition - posMain;
            float dir;

            if (angle > 0)
            {
                coefBody = 8;
                coefArm = 1f;
            }
            else
            {
                coefBody = 1.5f;
                coefArm = 1.3f;
            }

            if (dirMain.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                dir = 0;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                angle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
                dir = 180;
            }

            //Debug.Log(dirMain.x.ToString());

            if (dirMain.magnitude > coefBend)
            {
                body.transform.rotation = Quaternion.Euler(0, dir, angle / coefBody);
                head.transform.rotation = Quaternion.Euler(0, dir, angle / 2);
                armLeft.transform.rotation = Quaternion.Euler(0, dir, angle - 10);
                armRight.transform.rotation = Quaternion.Euler(0, dir, angle * 1.3f - 40);
                armRight2.transform.rotation = Quaternion.Euler(0, dir, angle * coefArm + 20);
            }

            if (currenWeapon != null)
            {
                currenWeapon.transform.position = weapon.transform.position;
                currentWeaponDirection = posMouse - head.position;

                if (dirMain.magnitude > coefBend)
                {
                    
                    if (currentWeaponDirection.x > 0)
                    {
                        weaponAngle = Mathf.Atan2(currentWeaponDirection.y, currentWeaponDirection.x) * Mathf.Rad2Deg;
                    }
                    else
                    {
                        weaponAngle = Mathf.Atan2(currentWeaponDirection.y, -currentWeaponDirection.x) * Mathf.Rad2Deg;
                    }
                }


                Vector3 vec = new Vector3(0, dir, weaponAngle);
                currenWeapon.transform.rotation = Quaternion.Euler(vec);

                if (Input.GetMouseButtonDown(0))
                {
                    wep = currenWeapon.GetComponent<weapon>();
                    wep.shoot = true;
                }

            }

            if (Input.GetKeyDown("m"))
            {
                Die();
            }
        }

	}

	void OnTriggerEnter2D(Collider2D t)
	{
		if (t.gameObject.layer == LayerMask.NameToLayer("Weapon") && currenWeapon != t.transform)
		{
			currenWeapon = t.transform;
		}

        if (t.gameObject.GetComponent<levelEnd>())
        {
            levelEnd levelEnd = t.gameObject.GetComponent<levelEnd>();
            Debug.Log(levelEnd.nextLevel.ToString());
            DontDestroyOnLoad(GameObject.Find("player"));
            DontDestroyOnLoad(GameObject.Find("target"));
            DontDestroyOnLoad(GameObject.Find("Main Camera"));
            DontDestroyOnLoad(currenWeapon.gameObject);
            Application.LoadLevel(levelEnd.nextLevel);
        }
	}

	void FixedUpdate()
	{
        if (GetComponent<Rigidbody2D>())
        {

            h = Input.GetAxis("Horizontal");

            if (h != 0)
            {
                anim.SetBool("mv", true);
            }
            else
            {
                anim.SetBool("mv", false);
            }

            anim.SetFloat("speed", h);
            anim.SetFloat("direction", Mathf.Sign(direction.x));

            if (Mathf.Abs(rigidBody2D.velocity.x) < maxSpeed)
            {
                if (grounded)
                    rigidBody2D.AddForce(Vector2.right * h * moveForce);
                else
                    rigidBody2D.AddForce(Vector2.right * h * moveForce / 10);
            }
            grounded = Physics2D.OverlapCircle(onGround.position, 0.2f, 1 << LayerMask.NameToLayer("Ground"));

            if (grounded && Input.GetKeyDown(KeyCode.Space) && !jump)
            {
                jump = true;
            }

            if (grounded)
            {
                anim.SetBool("jump", false);
            }
            else
                anim.SetBool("jump", true);

            if (jump)
            {
                rigidBody2D.AddForce(new Vector2(0f, jumpForce));
                jump = false;
                anim.SetBool("jump", true);
            }

        }

        if (hitPoint < 0)
            Die();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		for (int i = 0; i < 15; i++)
        {
            Gizmos.DrawSphere(transform.position + new Vector3(bodyPart[i].localPosition.x * transform.localScale.x, 
                bodyPart[i].localPosition.y,
                bodyPart[i].localPosition.z), 0.02f);
        }

        //Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), armLeft.position);
	}

	void CameraPosition()
	{
		posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float posx = posMouse.x - transform.position.x;
		float posy = posMouse.y - transform.position.y;
		Vector3 positionCamera =Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x + posx / 2, transform.position.y + posy / 2, -10), 0.1f); 
		cam.transform.position = positionCamera;
	}

	public void OnGUI()
	{
		// если следует отобразить имя
		if (showVar)
		{
			// считаем позицию
			Rect rect = new Rect(transform.position.x, transform.position.y, 120f, 30f);
			
			// создаем стиль с выравниванием по центру
			GUIStyle label = new GUIStyle(GUI.skin.label);
			label.alignment = TextAnchor.MiddleCenter;
			
			// выводим имя объекта с созданным стилем, чтобы имя было выведено по центру
			GUI.Label(rect, hitPoint.ToString(), label);
		}
	}

    public void Die()
    {
        Vector2[] anchorPoints = new Vector2[bodyPart.Length];

        var prevRotation = transform.rotation;

        transform.rotation = Quaternion.Euler(0, 0, 0);

        for (int i = 0; i < bodyPart.Length; i++)
        {
            var bone = bodyPart[i];

            bone.gameObject.AddComponent<Rigidbody2D>();
            bone.GetComponent<Collider2D>().enabled = true;
            bone.GetComponent<Rigidbody2D>().velocity = rigidBody2D.velocity;

            anchorPoints[i] = bone.localPosition;
        }

        transform.rotation = prevRotation;

        for (int i = 0; i < bodyPart.Length; i++)
        {
            var bone = bodyPart[i];
            var parent = bone.parent;

            if (!parent) continue;

            var joint = bone.gameObject.AddComponent<HingeJoint2D>();

            joint.connectedAnchor = anchorPoints[i];
            joint.connectedBody = parent.GetComponent<Rigidbody2D>();
        }

        var anchorPart = 1;

        HingeJoint2D shg = bodyPart[anchorPart].gameObject.AddComponent<HingeJoint2D>();
        shg.anchor = new Vector2(0, 0.1f);
        shg.connectedAnchor = bodyPart[anchorPart].transform.position;
        shg.connectedBody = null;

        GameObject.Destroy(gameObject.GetComponent<Rigidbody2D>());
        Collider2D[] col = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D c in col)
        {
            c.enabled = false;
        }

        GameObject.Destroy(wepCollision.gameObject);
        GameObject.Destroy(GetComponent<Animator>());
    }
}
