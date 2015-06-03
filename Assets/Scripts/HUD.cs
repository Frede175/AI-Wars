using UnityEngine;
using System.Collections;
using RTS;

public class HUD : MonoBehaviour {


	private Player player;
	private int hudWidth;
	private int hudheight;
	public GUISkin selectBox;
	
	public Texture2D healthActive;
	public Texture2D healthDeActive;
	public Texture2D processBar;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent< Player >();
		ResourceManager.storeHUDVars(selectBox, healthActive, healthDeActive, processBar);
	}
	
	// Update is called once per frame
	void OnGUI () {
	}

	public Rect GetPlayingArea()
	{
		return new Rect (0,Screen.height, Screen.width, Screen.height);
	}

	public bool BoundsOfHUD()
	{
		Vector3 mousePos = Input.mousePosition;
		bool inBounds = (mousePos.x >= 0 && mousePos.x <= Screen.width) && (mousePos.y >= 0 && mousePos.y <= Screen.height);
		return inBounds;
	}


}
