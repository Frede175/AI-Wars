using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tank : Unit {


	public float damage;
	public float rangeRadius;
	public float turnSpeed;
	public float firingRate;
	public float spawnDistance = 1;


	private float lastShot = 0f;
	
	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Start () {
		base.Start();
	}
	
	protected override void Update () {
		base.Update();
		CheckForEnemies();
	}
	
	protected override void OnGUI() {
		base.OnGUI();
	}

	void Shoot(GameObject target)	
	{
		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.up);
		if (Physics.Raycast(ray, out hit, rangeRadius,9))
		{
			if (hit.collider.gameObject == target && Time.time > lastShot + firingRate)
			{
				GameObject bullet = ObjectPooling.objectPooling.GetObjectFromList();
				bullet.transform.position = transform.position + (transform.up*spawnDistance);
				bullet.transform.rotation = transform.rotation;
				bullet.SetActive(true);
				lastShot = Time.time;
			}

		}
		RotateToTarget(target.transform.position);
	}

	void RotateToTarget(Vector3 position)
	{
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
		GameObject target = null;
		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (hitColliders[i].gameObject == gameObject)
				continue;

			if (hitColliders[i].gameObject.tag == "Unit" /*&& owner != hitColliders[i].gameObject.transform.GetComponent<WorldObjects>().owner*/)
			{
				float distance = Vector3.Distance(transform.position, hitColliders[i].gameObject.transform.position);
				if (distance < targetDistance)
				{
					targetDistance = distance;
					target = hitColliders[i].gameObject;

				}
			}
		}

		if (target != null)
			Shoot(target);
	}
}
