using UnityEngine;
using System.Collections;
using RTS;

public class Unit : WorldObjects {

	public float speed;
	public float turnSpeed;
	public bool moving;
	public bool rotation;
	[Tooltip("In seconds")]
	public float buildTime = 5f;
	private Vector2[] path;
	private int targetIndex;

	private float angel = 10f;

	protected override void Awake()
	{
		base.Awake();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
		canMove = true;
		moving = false;
		//MoveUnit(new Vector3(25, 17, -0.5f));

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

	}

	protected override void OnGUI()
	{
		base.OnGUI();
	}

	public override void MouseClick(Player controller, GameObject hitObject, Vector3 hitPoint)
	{
		base.MouseClick(controller, hitObject, hitPoint);
		
		if (controller.human && isSelected && IsOwnedBy(controller))
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

	public void StopMove()
	{
		StopCoroutine ("FollowPath");
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
		CalculateBounds();
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
			//TODO Work on getting rotation slower.
			//transform.rotation = Quaternion.Euler (0,0, Mathf.Atan2(currentWaypoint.y-transform.position.y, currentWaypoint.x-transform.position.x)*180 / Mathf.PI - 90f);

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			CalculateBounds();
			yield return null;
		}
	}

	void RotateToMovement()
	{

	}
}
