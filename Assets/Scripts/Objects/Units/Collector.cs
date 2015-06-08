using UnityEngine;
using System.Collections;

public class Collector : Unit {
	
	public float rangeForCollectingMoney = 2f;
	public GameObject gameDeposit;
	public GameObject gameCommanedCenter;
	public float maxMoneyLoad;
	public Vector3 positionDeposit;
	public Vector3 positionHome;
	public float money = 0;
	public bool GoingToBase;

	protected override void Start () {
		base.Start ();
	}
	

	protected override void Update () {
		base.Update();
		StartCollecting ();

	}

	public void StartCollecting()
	{
		Deposit deposit = gameDeposit.GetComponent<Deposit>();
		if (money == 0 && transform.position != positionDeposit && !GoingToBase) {
			MoveUnit(positionDeposit);
		}
		if (transform.position == positionDeposit && money < maxMoneyLoad && !GoingToBase) {
			if (deposit.IsOwnedBy(player))
			{
				if (!deposit.IsEmpty())
				{
					money += deposit.TakeMoney();
					if (money >= maxMoneyLoad)
					{
						money = maxMoneyLoad;
						GoingToBase = true;
					}

					if(deposit.IsEmpty()) GoingToBase = true;
				}
			}
		}

		if (GoingToBase && transform.position != positionHome ) {
			MoveUnit(positionHome);
		}

		if (transform.position == positionHome && GoingToBase)
		{
			CommandCenter commandCenter = gameCommanedCenter.GetComponent<CommandCenter>();
			if (commandCenter.IsOwnedBy(player))
			{
				if (money > 0)
				{
					commandCenter.GetMoney(money);
					money = 0;
					GoingToBase = false;
				}
			}
		}

		if (deposit.IsEmpty () && money == 0) {
			Destroy(this);
		}


	}

	public void StopCollecting()
	{

	}




}
