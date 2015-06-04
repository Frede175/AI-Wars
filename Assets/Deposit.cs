using UnityEngine;
using System.Collections;
using RTS;

public class Deposit : Building {

	public int moneyLeft;
	public int transfereSpeed;
	public Transform[] blocks;


	private int numberOfBlocksDisplayed;


	protected override void Start () {
		base.Start();
		actions = new string[] {"Buy"};
		moneyLeft = ResourceManager.moneyPerMoneyBase;
		transfereSpeed = ResourceManager.moneyTransfereSpeed;
		blocks = new Transform[4];
		for (int i = 0; i < 4; i++)
		{
			blocks[i] = transform.GetChild(i);
		}
		numberOfBlocksDisplayed = blocks.Length;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		CalculateDeposit();
		int money = TakeMoney();
	}

	public int TakeMoney()
	{
		moneyLeft -= transfereSpeed;
		CalculateDeposit();
		if (moneyLeft <= 0) moneyLeft = 0;
		return transfereSpeed;
	}

	void CalculateDeposit()
	{
		float procent = (float)moneyLeft / ResourceManager.moneyPerMoneyBase;
		if (procent > 0.75f)
			DisplayBlocks(4);
		else if (procent > 0.50f)
			DisplayBlocks(3);
		else if (procent > 0.25f)
			DisplayBlocks(2);	
		else if (procent > 0)
			DisplayBlocks(1);
		else
			DisplayBlocks(0);
	}

	void DisplayBlocks(int number)
	{
		Debug.Log(number);
		for (int i = 0; i < 4; i++)
		{
			blocks[i].gameObject.SetActive(true);
		}

		for (int i = 0; i < 4-number; i++)
		{
			blocks[i].gameObject.SetActive(false);
		}

		numberOfBlocksDisplayed = number;



	}



}
