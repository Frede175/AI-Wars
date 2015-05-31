using UnityEngine;
using System.Collections;
using RTS;

public class UserInput : MonoBehaviour {

	private Player player;
	public Transform mapArea;
	Vector3 WorldBottomOfMap;

	float mapWidth, mapHeight;


	// Use this for initialization
	void Start() {
		player = transform.root.GetComponent<Player>();
		mapWidth =  mapArea.lossyScale.x;
		mapHeight = mapArea.lossyScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.human)
		{	
			MoveCamera();
			CameraZoom();
			MouseActivity();
		}

	}

	private Vector3 FindHitPoint()
	{
		Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(mousePos, out hit))
		{
			return hit.point;
		}
		return ResourceManager.InvalidPosition;
	}

	private GameObject FindHitObject()
	{
		Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(mousePos, out hit))
		{
			return hit.collider.gameObject;
		}
		return null;
	}

	void MouseActivity() 
	{
		if (Input.GetMouseButtonDown(0)) LeftMouse();
		else if (Input.GetMouseButtonDown(1)) RightMouse();
	}

	void LeftMouse()
	{

		if (player.hud.BoundsOfHUD())
		{	
			GameObject worldObject = FindHitObject();
			Vector3 worldPoint = FindHitPoint();

			if (worldObject && worldPoint != ResourceManager.InvalidPosition)
			{
				if (player.SelectedObject) player.SelectedObject.MouseClick(player, worldObject, worldPoint);
				else if (worldObject.name != "Board")
				{
					WorldObjects worldObjects = worldObject.GetComponent<WorldObjects>();
					if (worldObjects)
					{
						player.SelectedObject = worldObjects;
						worldObjects.SetSelection(true, player.hud.GetPlayingArea());
					}
				}
			}
		}
	}

	void DeselectObject()
	{
		if (player.SelectedObject) 
		{
			player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
			player.SelectedObject = null;
		}
	}
	
	void RightMouse()
	{	
		if (player.SelectedObject && !player.SelectedObject.canMove)
		{	
			Debug.Log("Deselect");
			DeselectObject();
		}
		else if (player.SelectedObject && player.SelectedObject.canMove)
		{
			Debug.Log("try to move");
			GameObject worldObject = FindHitObject();
			Vector3 worldPoint = FindHitPoint();
			
			if (worldObject.name == "Board" && worldPoint != ResourceManager.InvalidPosition)
			{
				Debug.Log("Mouse Click");
				player.SelectedObject.MouseClick(player, worldObject, worldPoint);
			}
		}

	}
	
	void MoveCamera()
	{
		float posX = Input.mousePosition.x;
		float posY = Input.mousePosition.y;
		Vector3 movement = new Vector3(0,0,0);
		bool mouseMove = false;

		if (posX <= 0 + ResourceManager.movementBorder && posX >= 0)
		{
			mouseMove = true;
			movement.x -= ResourceManager.cameraSpeed;
		}
		else if (posX <= Screen.width && posX >= Screen.width - ResourceManager.movementBorder)
		{
			mouseMove = true;
			movement.x += ResourceManager.cameraSpeed;
		}

		if (posY <= 0 + ResourceManager.movementBorder && posY >= 0)
		{
			mouseMove = true;
			movement.z -= ResourceManager.cameraSpeed;
		}
		else if (posY <= Screen.height && posY >= Screen.height - ResourceManager.movementBorder)
		{
			mouseMove = true;
			movement.z += ResourceManager.cameraSpeed;
		}

		if (!mouseMove)
		{
			float xAxis = Input.GetAxisRaw("Horizontal");
			float yAxis = Input.GetAxisRaw("Vertical");

			movement.x += xAxis;
			movement.z += yAxis;
		}

		//Calc the movement of the camera
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin;
		destination.x += movement.x;
		destination.y += movement.z;
		float height = Camera.main.orthographicSize;
		float width = height * Screen.width / Screen.height;
		float minX = width - mapWidth/2;
		float maxX = mapWidth/2 - width;
		float minY = height - mapHeight/2;
		float maxY = mapHeight/2 - height;
		destination.x = Mathf.Clamp(destination.x, minX, maxX);
		destination.y = Mathf.Clamp(destination.y, minY, maxY);


		if (destination != origin)
		{
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, ResourceManager.cameraSpeed * Time.deltaTime);
		}

	}

	void CameraZoom()
	{
		float zoom = -Input.GetAxis("Mouse ScrollWheel") * ResourceManager.zoomSpeed;
		float cameraZoom = Camera.main.orthographicSize;
		Camera.main.orthographicSize = Mathf.Clamp(cameraZoom + zoom, ResourceManager.minHeight, ResourceManager.maxHeight);
	}
}
