using UnityEngine;
using System.Collections;
using RTS;

public class Builder : Unit {

	public string buildingTobuild;
	public Vector3 spawnPosForBuilding;
	public Quaternion rotationForBuilding;
	public bool isBuilt;
	public GameObject building;


	protected override void Start()
	{
		base.Start ();
		if (!isBuilt) building = ResourceManager.GetBuilding (buildingTobuild);
		MoveUnit (spawnPosForBuilding);
	}

	protected override void Update()
	{
		base.Update ();
		if (transform.position == spawnPosForBuilding) {
			StopMove();
			BuildBuilding();
		}
	}

	public void BuildBuilding()
	{
		if (isBuilt) {
			player.AddResource(building);

		} else {
			player.AddBuilding(building, spawnPosForBuilding, rotationForBuilding);
		}
		Destroy (gameObject);
	}
}
