using UnityEngine;
using System.Collections;
using RTS;

public class Unit : WorldObjects {

	public float speed;
	public bool moving;
	private Vector2[] path;
	private int targetIndex;


	protected virtual void Awake()
	{
		base.Awake();
	}

	// Use this for initialization
	protected virtual void Start () {
		base.Start();
		canMove = true;
		moving = false;
		//MoveUnit(new Vector3(25, 17, -0.5f));

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		base.Update();

	}

	protected virtual void OnGUI()
	{
		base.OnGUI();
	}

	public override void MouseClick(Player controller, GameObject hitObject, Vector3 hitPoint)
	{
		base.MouseClick(controller, hitObject, hitPoint);
		
		if (controller.human && isSelected)
		{
			if (hitObject.name == "Board" && hitPoint != ResourceManager.InvalidPosition)
			{
				float x = hitPoint.x;
				float y = hitPoint.y;
				float z = -0.5f;
				
				MoveUnit(new Vector3(x,y,z));
			}
		}
	}

	public void MoveUnit(Vector3 destination)
	{
		PathManager.RequestPath(transform.position, destination, OnFinished);
	}

	void OnFinished(Vector2[] _path, bool success)
	{
		if (success)
		{
			path = null;
			path = _path;
			targetIndex = 0;
			moving = true;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}


	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = new Vector3(path[0].x, path[0].y, -0.5f);
		
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					moving = false;
					yield break;
				}
				currentWaypoint = new Vector3(path[targetIndex].x, path[targetIndex].y, -0.5f);
			}
			
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			CalculateBounds();
			yield return null;
		}
	}
}
