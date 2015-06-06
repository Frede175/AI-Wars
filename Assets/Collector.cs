using UnityEngine;
using System.Collections;

public class Collector : Unit {
	
	public float rangeForCollectingMoney = 2f;
	public WorldObjects deposit;
	public Vector3 positionDeposit;
	public Vector3 positionHome;

	protected override void Start () {
		base.Start ();
	}
	

	protected override void Update () {
		base.Update();

	}

	public void StartCollecting()
	{

	}

	public void StopCollecting()
	{

	}




}
