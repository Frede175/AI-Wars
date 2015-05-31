using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Player : MonoBehaviour{

	public string playerName;
	public bool human;
	public HUD hud;
	public Color color;
	public WorldObjects SelectedObject { get; set; }



	// Use this for initialization
	void Start () {
		hud = GetComponentInChildren< HUD >();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
