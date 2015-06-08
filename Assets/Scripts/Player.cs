using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Player : MonoBehaviour{

	public string playerName;
	public bool human;
	public HUD hud;
	public Color color;
	public WorldObjects SelectedObject { get; set; }

	//Resources
	public int money;



	// Use this for initialization
	void Start () {
		hud = GetComponentInChildren< HUD >();
		money = ResourceManager.GetAverageStartMoney();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddUnit(GameObject unit, Vector3 spawnPos, Quaternion rotation)
	{
		Units units = GetComponentInChildren< Units >();
		GameObject newUnit = Instantiate(unit, spawnPos, rotation) as GameObject;
		newUnit.transform.parent = units.transform;
		
	}

	public void AddBuilding(GameObject Building, Vector3 spawnPos, Quaternion rotation)
	{
		Buildings buildings = GetComponentInChildren<Buildings> ();
		GameObject newBuilding = Instantiate (Building, spawnPos, rotation) as GameObject;
		newBuilding.transform.parent = buildings.transform;
	}

	public void AddResource(GameObject building)
	{
		Buildings buildings = GetComponentInChildren<Buildings>();
		building.transform.parent = buildings.transform;
		Building b = building.GetComponent<Building> ();
		b.Buy (this);

	}


}
