using UnityEngine;
using System.Collections;

public class TankFactory : Building {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		actions = new string[] {"Cube Tank"};
	}

	protected override void OnGUI()
	{
		base.OnGUI();
		if (isSelected) DrawActions();
	}
}
