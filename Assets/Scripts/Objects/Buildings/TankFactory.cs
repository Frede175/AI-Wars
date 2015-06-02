using UnityEngine;
using System.Collections;

public class TankFactory : Building {

	// Use this for initialization
	void Start () {
		base.Start();
		actions = new string[] {"Cube Tank", "Tank2"};
	}

	protected override void OnGUI()
	{
		base.OnGUI();
		if (isSelected) DrawActions();
	}
}
