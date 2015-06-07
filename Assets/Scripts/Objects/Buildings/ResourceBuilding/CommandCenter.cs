using UnityEngine;
using System.Collections;
using RTS;

public class CommandCenter : Building {

	public bool isBuying;


	protected override void Start()
	{
		base.Start();
		isBuying = false;
		actions = new string[] {"Buy desposit"};
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
			Deposit deposit = hitObject.GetComponent<Deposit>();
			deposit.Buy(player);
		}

	}

	public void MarkDeposits(){

	}

	/*---------UI----------*/
}
