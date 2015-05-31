using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour {

	public static ObjectPooling objectPooling;
	public GameObject objectToMake;
	private List<GameObject> objects;
	public int ObjectsToMakeAtStart;

	void Start () 
	{
		objectPooling = this;
		objects = new List<GameObject>();

		for (int i = 0; i < ObjectsToMakeAtStart; i++)
		{
			GameObject obj = (GameObject)Instantiate(objectToMake);
			obj.SetActive(false);
			objects.Add(obj);
		}
	}

	public GameObject GetObjectFromList()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			if (!objects[i].activeInHierarchy)
			{
				return objects[i];
			}
		}

		GameObject obj = (GameObject)Instantiate(objectToMake);
		objects.Add(obj);
		return obj;
	}

}
