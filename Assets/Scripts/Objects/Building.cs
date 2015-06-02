using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : WorldObjects {

	public int tier ;
	public float buildRate;
	protected Queue<string> buildQueue;
	protected float currentBuildProcess = 0;
	private float buildTimeForUnit;
	private float maxBuildTime;
	private Vector3 spawnPosition;
	private Quaternion spawnRotation;


	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Start () {
		base.Start();
		buildQueue = new Queue<string>();
		spawnPosition = CalculateSpawnPosition();
		spawnRotation = CalculateRotation();

	}
	
	protected override void Update () {
		base.Update();
		ProcessQueue();

	}
	
	protected override void OnGUI() {
		base.OnGUI();
	}

	private void ProcessQueue()
	{
		if (buildQueue.Count > 0)
		{
			if (currentBuildProcess == 0)
				buildTimeForUnit = GetBuildTime(buildQueue.Peek());

			currentBuildProcess += buildRate * Time.fixedDeltaTime;
			if (currentBuildProcess >= buildTimeForUnit)
			{
				currentBuildProcess = 0.0f;
				player.AddUnit(GetObjectByName(buildQueue.Dequeue(), "Building"), spawnPosition, spawnRotation);
			}
		}
	}

	private float GetBuildTime(string unit)
	{
		return GetObjectByName(unit, "Building").GetComponent<Unit>().buildTime;
	}

	private Vector3 CalculateSpawnPosition()
	{
		Vector3 pos = transform.position; //This may not work haha :D
		pos += transform.up * 2;
		pos.z = -0.5f;
		return pos;
	}

	private Quaternion CalculateRotation()
	{
		return transform.rotation;
	}

	void AddToQueue(string unitName)
	{
		buildQueue.Enqueue(unitName);
	}

	public void DrawActions()
	{
		for (int i = 0; i < actions.Length;  i++)
		{
			GUI.BeginGroup(new Rect(64, 32*i+64, 100, 20));
			if (GUI.Button(new Rect(0,0,100,20), actions[i]))
			{
				AddToQueue(actions[i]);
			}
			GUI.EndGroup();
		}
	}
}
