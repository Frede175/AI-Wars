using UnityEngine;
using System.Collections;
using RTS;

public class UserInput : MonoBehaviour {

	private Players player;
	public Transform mapArea;
	public bool Human;
	Vector3 WorldBottomOfMap;

	float mapWidth, mapHeight;

	// Use this for initialization
	void Awake () {
		player = transform.root.GetComponent< Players >();
		mapWidth =  mapArea.lossyScale.x;
		mapHeight = mapArea.lossyScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera();
	}

	void MoveCamera()
	{
		float posX = Input.mousePosition.x;
		float posY = Input.mousePosition.y;
		Vector3 movement = new Vector3(0,0,0);

		if (posX <= 0 + MovementManager.movementBorder && posX >= 0)
		{
			movement.x -= MovementManager.cameraSpeed;
		}
		else if (posX <= Screen.width && posX >= Screen.width - MovementManager.movementBorder)
		{
			movement.x += MovementManager.cameraSpeed;
		}

		if (posY <= 0 + MovementManager.movementBorder && posY >= 0)
		{
			movement.z -= MovementManager.cameraSpeed;
		}
		else if (posY <= Screen.height && posY >= Screen.height - MovementManager.movementBorder)
		{
			movement.z += MovementManager.cameraSpeed;
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
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, MovementManager.cameraSpeed * Time.deltaTime);
		}



	}
}
