using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {


	public float speed = 100f;
	public float damage = 2f;


	void OnEnable()
	{
		Invoke("Destory", 1f);
	}

	void Destory()
	{
		gameObject.SetActive(false);
	}
	
	void Update()
	{
		transform.position +=  transform.up * speed * Time.deltaTime;
	}


	void OnDisable()
	{
		CancelInvoke();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag != "Laser")
			gameObject.SetActive(false);

	}
}
