using UnityEngine;
using System.Collections;

public class WorldObjects : MonoBehaviour {

	public string objectName;
	public int teamNumber, cost, sellValue, maxHealth, toughness;
	public GameObject prefab;

	protected string[] actions = {};
	protected Player player;
	protected bool isSelected;


	protected virtual void Awake()
	{

	}

	protected virtual void Start()
	{
		player = transform.root.GetComponent<Player>();
	}

	protected virtual void Update()
	{

	}

	protected virtual void OnGUI()
	{

	}

	protected virtual void ActionToFunction(string action)
	{

	}
	
}
