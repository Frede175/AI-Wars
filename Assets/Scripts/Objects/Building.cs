using UnityEngine;
using System.Collections;

public class Building : WorldObjects {

	public enum buildingType { Factory, MoneyCollector }

	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Start () {
		base.Start();
	}
	
	protected override void Update () {
		base.Update();
	}
	
	protected override void OnGUI() {
		base.OnGUI();
	}
	
}
