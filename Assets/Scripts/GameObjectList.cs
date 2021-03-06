﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class GameObjectList : MonoBehaviour {

	public GameObject[] units;
	public GameObject[] buildings;
	public GameObject[] worldObjects;
	public List<GameObject> deposits;
	public GameObject player;
	private static bool created = false;

	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this);
			ResourceManager.SetObjectList(this);
			created = true;
		}
		else
		{
			Destroy(this);
		}

	}

	void Start()
	{
		deposits = new List<GameObject>();
		FindDeposits();
		ResourceManager.MakeListsForDeposits();
	}

	void FindDeposits()
	{
		List<GameObject> childs = new List<GameObject>();
		Transform depositsList = GameObject.Find("DepositsList").transform;
		foreach (Transform child in depositsList)
		{
			if (child != depositsList)
			{
				if (child != null && child.gameObject != null) childs.Add(child.gameObject);
			}
		}
		deposits = childs;
	}

	public GameObject GetUnit(string name)
	{
		for (int i = 0; i < units.Length; i++)
		{
			Unit unit = units[i].GetComponent<Unit>();
			if (unit.objectName == name && unit) return units[i];
		}
		
		return null;
	}
	
	public GameObject GetBuilding(string name)
	{
		for (int i = 0; i < buildings.Length; i++)
		{
			Building building = buildings[i].GetComponent<Building>();
			if (building && building.objectName == name) return buildings[i];
			
		}

		return null;
	}

	public GameObject GetWorldObject(string name)
	{
		for (int i = 0; i < worldObjects.Length; i++)
		{
			WorldObjects worldObject = worldObjects[i].GetComponent<WorldObjects>();
			if (worldObject && worldObject.objectName == name) return worldObjects[i];
			
		}
		
		return null;
	}

	public GameObject GetPlayer()
	{
		return player;
	}

	public Texture2D GetBuildTexture(string name)
	{
		for (int i = 0; i < units.Length; i++)
		{
			Unit unit = units[i].GetComponent<Unit>();
			if (unit && unit.objectName == name) return unit.BuildTexture;
		}

		for (int i = 0; i < buildings.Length; i++)
		{
			Building building = buildings[i].GetComponent<Building>();
			if (building && building.objectName == name) return building.BuildTexture;
			
		}

		return null;
	}

	public List<GameObject> GetDeposits()
	{
		return deposits;
	}

}
