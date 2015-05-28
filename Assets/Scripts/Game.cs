using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game instance;
	public int numberOfPlayers;
	public List<GameObject> units;
	List<Players> players = new List<Players>();
	List<Color> colors = new List<Color>();
	int minNumberOfTeams = 2;
	int maxNumberOfTeams = 5;

	void Start () {
		instance = this;
		//numberOfPlayers = Mathf.Clamp(numberOfPlayers, minNumberOfTeams, maxNumberOfTeams);
		MakeTeams ();

	}
	

	void Update () {
	
	}

	void MakeTeams()
	{
		//Colors for teams:
		colors.Add (Color.blue);
		colors.Add (Color.red);
		colors.Add (Color.green);
		colors.Add (Color.cyan);
		colors.Add (Color.yellow);


		//Assigning teams:
		Players player = new Players("Real player", 1, true, colors[0]);
		players.Add (player);
		
		for (int i = 1; i < numberOfPlayers; i++)
		{
			Players AI = new Players("AI " + i, i+1, false, colors[i]);
			players.Add(AI);
		}
	}

	public void SpawnUnit(Players player, GameObject unit, Vector3 position)
	{
		GameObject newUnit = Instantiate(unit, position, Quaternion.identity) as GameObject;
		newUnit.GetComponent<SpriteRenderer> ().color = player.playerColor;
		player.AddUnit (newUnit);

	}
}
