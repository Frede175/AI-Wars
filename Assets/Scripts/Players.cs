using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Players : MonoBehaviour {

	public string playerName;
	public int teamNumber;
	public bool human;
	public Color playerColor;
	public List<GameObject> units = new List<GameObject>();


	public Players(string _playerName, int _teamNumber, bool _human, Color _playerColor)
	{
		playerName = _playerName;
		teamNumber = _teamNumber;
		human = _human;
		playerColor = _playerColor;

	}

	public void AddUnit(GameObject unit)
	{
		units.Add (unit);
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
