using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Tank : Unit {
	
	public float damage = 2f;
	public float rangeRadius;
	public float firingRate;
	public float spawnDistance = 1.5f; //For shooting



	//Bullet settings
	public float bulletSpeed = 100f;
	public float accuracy = 20f;


	private float lastShot = 0f;
	
	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Start () {
		base.Start();
	}
	
	protected override void Update () {
		base.Update();
		if (player != null)
			CheckForEnemies();
	}
	
	protected override void OnGUI() {
		base.OnGUI();
	}

	void Shoot(GameObject target)	
	{
		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.up);
		if (Physics.Raycast(ray, out hit, rangeRadius))
		{
			if (hit.collider.gameObject == target && Time.time > lastShot + firingRate && hit.collider.gameObject.layer != 9)
			{
				GameObject bullet = ObjectPooling.objectPooling.GetObjectFromList();
				bullet.transform.position = transform.position + (transform.up*spawnDistance);
				bullet.transform.rotation = transform.rotation;
				Bullet bul = bullet.GetComponent<Bullet>();
				bul.velocity = transform.rotation * new Vector3 ( Random.Range(-accuracy, accuracy ), bulletSpeed * Random.Range(0.9f, 1.1f));
				bul.damage = damage;
				bul.color = player.color;
				bul.parent = transform.position;
				bul.range = rangeRadius;
				bullet.SetActive(true);
				lastShot = Time.time;
			}
		}
		RotateToTarget(target.transform.position);
	}

	void RotateToTarget(Vector3 position)
	{
		CalculateBounds();
		Vector3 vT = position - transform.position;
		vT.z = transform.position.z;
		Quaternion qTarget;
		qTarget = Quaternion.LookRotation(Vector3.forward, vT);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, qTarget, turnSpeed * Time.deltaTime);
	
	
	}



	void CheckForEnemies()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeRadius);
		float targetDistance = 99999;
		GameObject Unittarget = null;
		GameObject	Buildingtarget = null;
		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (hitColliders[i].gameObject == gameObject)
				continue;

			if (hitColliders[i].gameObject.tag == "Unit" || hitColliders[i].gameObject.tag == "Building")
			{
				WorldObjects worldObject = hitColliders[i].gameObject.transform.GetComponent<WorldObjects>();
				if (!worldObject.IsPlayerSet())
					return;

				if (!worldObject.IsOwnedBy(player))
				{
					float distance = Vector3.Distance(transform.position, hitColliders[i].gameObject.transform.position);
					if (distance < targetDistance)
					{
						targetDistance = distance;
						if (hitColliders[i].gameObject.tag == "Unit")
							Unittarget = hitColliders[i].gameObject;
						if (hitColliders[i].gameObject.tag == "Building")
							Buildingtarget = hitColliders[i].gameObject;
					}
				}
			}

		}

		if (Unittarget != null)
			Shoot(Unittarget);
		else if (Buildingtarget != null)
			Shoot(Buildingtarget);
	}
}
