using UnityEngine;
using System.Collections;
using RTS;

public class Deposit : Building {

	public int moneyLeft;
	public int transfereSpeed;
	public Transform[] blocks;

	private bool isEmpty;


	private int numberOfBlocksDisplayed;
	private Vector3 test;


	protected override void Start () {
		base.Start();
		moneyLeft = ResourceManager.moneyPerDeposit;
		isEmpty = moneyLeft >= 0;
		transfereSpeed = ResourceManager.moneyTransfereSpeed;
		blocks = new Transform[4];
		for (int i = 0; i < 4; i++)
		{
			blocks[i] = transform.GetChild(i);
		}
		numberOfBlocksDisplayed = blocks.Length;
		FindPosition();
	}

	public int TakeMoney()
	{
		moneyLeft -= transfereSpeed;
		CalculateDeposit();
		if (moneyLeft <= 0)
		{
			moneyLeft = 0;
			isEmpty = true;
		}
		return transfereSpeed;
	}

	void CalculateDeposit()
	{
		float procent = (float)moneyLeft / ResourceManager.moneyPerDeposit;
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

	//For testing
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawCube(test, new Vector3(0.1f,0.1f,0.1f));
	}

	public bool IsEmpty()
	{
		return isEmpty;
	}

	public void FindPosition()
	{
		Vector3 position = transform.position + transform.up * (selectionBounds.max.y - selectionBounds.min.y)/2;
		test = position; //Testing

	}

	public void Buy(Player controller)
	{
		ResourceManager.AddToNoneAvailable(gameObject);
		controller.AddResource(gameObject);
		player = transform.root.GetComponent<Player>();
		SetColor();
		isBought = true;
	}



}
