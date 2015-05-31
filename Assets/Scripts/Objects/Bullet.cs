using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float damage;
	public Vector3 velocity;
	public Color color;
	TrailRenderer trail;
	SpriteRenderer renderer;

	private bool alive = false;


	void Awake()
	{
		renderer = gameObject.GetComponent<SpriteRenderer>();
		trail = gameObject.GetComponent<TrailRenderer>();
	}

	void OnEnable()
	{
		SetColor();
		alive = true;
		Invoke("Destory", 1f);
	}

	void Destory()
	{

		transform.position = new Vector3 (0, 0, 100);
		trail.time = -1;
		Invoke("ResetTrail", 0.02f);

	}

	void ResetTrail()
	{
		trail.time = 0.05f;
		gameObject.SetActive(false);
	}
	
	void FixedUpdate()
	{
		if (alive)
		{
			CastRay();
			transform.position +=  velocity * Time.fixedDeltaTime;
		}
	}


	void OnDisable()
	{
		CancelInvoke();
	}

	void CastRay()
	{
		Ray ray = new Ray (transform.position, transform.up * velocity.magnitude * Time.fixedDeltaTime);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, velocity.magnitude * Time.fixedDeltaTime, 9))
		{
			if (hit.collider.gameObject.tag == "Unit" || hit.collider.gameObject.tag == "Building" && hit.collider.gameObject.layer != gameObject.layer)
			{
				alive = false;
				Destory();
			}
		}
	}

	void SetColor()
	{
		trail.material.SetColor("_TintColor", color);
		renderer.material.SetColor("_TintColor", color);
	}
}
