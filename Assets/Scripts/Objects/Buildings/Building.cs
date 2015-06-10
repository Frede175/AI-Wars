using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Building : WorldObjects {

	public int tier ;
	public float buildRate;
	protected Queue<string> buildQueue;
	protected float currentBuildProcess = 0;
	private float buildTimeForUnit;
	private float maxBuildTime;
	protected Vector3 spawnPosition;
	protected Quaternion spawnRotation;


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
		if (currentBuildProcess > 0)
		{
			isBuilding = true;
			healthBarLevel = 1;
		}
		else
		{
			isBuilding = false;
			healthBarLevel = 0;
		}
		base.OnGUI();
		if (isSelected)
		{
			DrawActions ();
			if (buildQueue.Count > 0)
				DrawQueue();
		}
			


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
				player.AddUnit(GetObjectByName(buildQueue.Dequeue(), "Unit"), spawnPosition, spawnRotation);
			}
		}
	}

	private float GetBuildTime(string unit)
	{
		return GetObjectByName(unit, "Unit").GetComponent<Unit>().buildTime;
	}

	private Vector3 CalculateSpawnPosition()
	{
		Vector3 pos = transform.position;
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

	protected override void DrawBars(Rect selectBox)
	{
		base.DrawBars (selectBox);
		float height = selectBox.height / 8;
		GUI.BeginGroup(new Rect(selectBox.min.x, selectBox.min.y - (height * 1.5f), selectBox.width, height));
		GUI.DrawTexture (new Rect (0, 0, selectBox.width * currentBuildProcess / buildTimeForUnit, height), ResourceManager.ProcessBar);
		GUI.EndGroup();
	}

	protected virtual void DrawActions()
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

	void DrawQueue()
	{
		string[] buildQueueArray = buildQueue.ToArray();
		for (int i = 0; i < buildQueueArray.Length; i++)
		{
			GUI.BeginGroup(new Rect (Screen.width-60, 0+32*i, 32, 32));
			GUI.Box(new Rect(0,0,32,32), GetTextureByName(buildQueueArray[i]));
			GUI.EndGroup();
		}
	}

	public override void MouseClick(Player controller, GameObject hitObject, Vector3 hitPoint)
	{
		base.MouseClick(controller, hitObject, hitPoint);
	}

	public virtual void TakeOver(Player controller)
	{
		if (isBought)
			return;

		ResourceManager.AddToNoneAvailable(gameObject);
		player = transform.root.GetComponent<Player>();
		SetColor();
		isBought = true;
	}

}
