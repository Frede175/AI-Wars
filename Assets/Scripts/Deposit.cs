using UnityEngine;
using System.Collections;
using RTS;

public class Deposit : Building {

	public int moneyLeft;
	public int transfereSpeed;
	public int delay;
	public Transform[] blocks;
	public Vector3 commandCenterPos;

	private bool isEmpty = false;


	private int numberOfBlocksDisplayed;
	private Vector3 test;
	
	private int numberOfUnitsToSend = 3;

	protected override void Start () {
		base.Start();
		moneyLeft = ResourceManager.moneyPerDeposit;
		transfereSpeed = ResourceManager.moneyTransfereSpeed;
		transfereSpeed = 75;
		delay = 10;
		blocks = new Transform[4];
		actions = new string[] {"Delivery"};
		for (int i = 0; i < 4; i++)
		{
			blocks[i] = transform.GetChild(i);
		}
		numberOfBlocksDisplayed = blocks.Length;
		FindPosition();
	}

	protected override void Update()
	{
		base.Update();
		if (isEmpty)
			Destroy(gameObject);
			
	}

	private int TakeMoney()
	{

		if (moneyLeft < transfereSpeed)
		{
			int money = moneyLeft;
			moneyLeft = 0;
			isEmpty = true;
			return money;
		}
		moneyLeft -= transfereSpeed;
		CalculateDeposit();
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

	public override void TakeOver(Player controller)
	{
		base.TakeOver (controller);
		commandCenterPos = player.commandCenterObj.transform.position;
		StartCoroutine("SendMoney");
	}

	IEnumerator SendMoney()
	{
		while (true)
		{
			for (int i = 0; i < numberOfUnitsToSend; i++) {
				GameObject unit = GetObjectByName(actions[0], "Unit");
				Delivery delivery = unit.GetComponent<Delivery>();
				delivery.amout = TakeMoney();
				delivery.goingTo = commandCenterPos;
				player.AddUnit(unit, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(2);
			}
			yield return new WaitForSeconds (delay);
		}
	}





}
