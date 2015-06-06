using UnityEngine;
using System.Collections;

public class CommandCenter : Building {


	protected override void Start()
	{
		actions = new string[] {"Buy desposit"};
	}

	public void GetMoney(float amount)
	{
		player.money += (int)amount;
	}




}
