using UnityEngine;
using System.Collections;

public class Delivery : Unit {

	public Vector3 goingTo;
	public int amout;
	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
		Moving ();
	}

	private void Moving()
	{
		if (!moving)
		{
			if (transform.position == goingTo)
			{
				player.commandCenter.GetMoney(amout);
				Destroy(gameObject);
			}
			else
			{
				MoveUnit(goingTo);
			}
		}
	}
}
