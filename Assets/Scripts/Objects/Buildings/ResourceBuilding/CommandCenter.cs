using UnityEngine;
using System.Collections;
using RTS;

public class CommandCenter : Building {

	public bool isBuying;
	private string[] unitsNames = new string[] {"Builder"};


	protected override void Start()
	{
		base.Start();
		isBuying = false;
		actions = new string[] {"Buy desposit"};
		spawnPosition = transform.position + transform.up * (selectionBounds.max.y - selectionBounds.min.y)/1.5f;
	}

	public void GetMoney(float amount)
	{
		player.money += (int)amount;
	}

	protected override void OnGUI()
	{
		base.OnGUI();
		if (isSelected) DrawActions();
		if (!isSelected) isBuying = false;
	}

	protected override void DrawActions()
	{
		GUI.BeginGroup(new Rect (64, 64, 100, 20));
		if (GUI.Button(new Rect (0,0,100,20), "Buy Deposit"))
		{
			isBuying = true;
		}
		GUI.EndGroup();
	}

	//This need to go ind the UI section, when it's being made
	/*---------UI----------*/

	public override void MouseClick(Player controller, GameObject hitObject, Vector3 hitPoint)
	{
		if (!isBuying) base.MouseClick(controller, hitObject, hitPoint);

		if (isBuying && hitObject.tag == "Deposit" && ResourceManager.DepositIsAvailable(hitObject))
		{
			GameObject unit = ResourceManager.GetUnit(unitsNames[0]);
			Builder builder = unit.GetComponent<Builder>();
			Deposit deposit = hitObject.GetComponent<Deposit>();
			deposit.commandCenterPos = spawnPosition;
			builder.spawnPosForBuilding = deposit.spawnPosition;
			builder.isBuilt = true;
			builder.buildingTobuild = "Deposit";
			builder.building = hitObject;
			player.AddUnit(unit, spawnPosition, transform.rotation);
		}

	}

	public void MarkDeposits(){

	}

	/*---------UI----------*/
}
