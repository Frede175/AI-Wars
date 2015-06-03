using UnityEngine;
using System.Collections;
using RTS;

public class WorldObjects : MonoBehaviour {

	public string objectName;
	public int cost, sellValue, toughness;
	public float health, maxHealth;
	public bool canMove = false;


	protected string[] actions = {};
	protected Player player;
	protected bool isSelected;
	protected Bounds selectionBounds;
	protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);

	protected int healthBarLevel = 0;
	protected bool isBuilding = false;




	protected virtual void Awake()
	{
		selectionBounds = ResourceManager.InvalidBounds;
		CalculateBounds();

	}

	protected virtual void Start()
	{
		player = transform.root.GetComponent<Player>();
		SetColor();
		CheckAlive ();
		maxHealth = health;
	}

	protected virtual void Update()
	{

	}

	protected virtual void OnGUI()
	{
		if (isSelected) DrawSelection();
	}

	protected virtual void ActionToFunction(string action)
	{

	}
	protected virtual void DrawSelectionBox(Rect selectBox) {
		GUI.Box(selectBox, "");
	}

	protected virtual void DrawBars(Rect selectBox)
	{


		GUI.skin = null;
		float height = selectBox.height/8;

		//Health Bar
		GUI.BeginGroup(new Rect(selectBox.min.x, selectBox.min.y - 15f - ((height-height/2)*healthBarLevel) , selectBox.width, height));
		GUI.DrawTexture(new Rect(0,0,selectBox.width, height), ResourceManager.HealthDeActive);
		GUI.BeginGroup(new Rect(0,0, selectBox.width * health/maxHealth, height));
		GUI.DrawTexture(new Rect(0,0, selectBox.width, height), ResourceManager.HealthActive);
		GUI.EndGroup();
		GUI.EndGroup();
	}

	private void DrawSelection()
	{
		GUI.skin = ResourceManager.GuiSelectBox;
		Rect selectBox = WorkManager.CalcSelectionBox(selectionBounds, playingArea);
		//GUI.BeginGroup(playingArea);
		DrawSelectionBox(selectBox);
		DrawBars(selectBox);
		//GUI.EndGroup();
	}

	public void CalculateBounds()
	{
		selectionBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>())
		{
			selectionBounds.Encapsulate(r.bounds);
		}
	}

	public void SetSelection(bool selected, Rect playingArea) 
	{
		isSelected = selected;
		if (selected) this.playingArea = playingArea;
	}
	
	public virtual void MouseClick(Player controller, GameObject hitObject, Vector3 hitPoint)
	{

		if (isSelected && hitObject && hitObject.name != "Board")
		{
			WorldObjects worldObject = hitObject.transform.GetComponent< WorldObjects >();
			if(worldObject)
			{
				ChangeSelection(worldObject, controller);
			}
		}
		/*else if (!controller.SelectedObject.canMove)
		{
			controller.SelectedObject.SetSelection(false, controller.hud.GetPlayingArea());
			controller.SelectedObject = null;
		}*/
	}

	private void ChangeSelection(WorldObjects worldObject, Player controller)
	{
		SetSelection(false, playingArea);
		if (controller.SelectedObject) controller.SelectedObject.SetSelection(false, playingArea);
		controller.SelectedObject = worldObject;
		worldObject.SetSelection(true, controller.hud.GetPlayingArea());
	}

	public void SetColor()
	{
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.color = player.color;


	}

	public bool IsOwnedBy(Player controller)
	{
		if (player.Equals(controller))
			return true;
		else
			return false;
	}

	public void TakeDamage(float damage)
	{
		health -= damage-toughness/100;
		CheckAlive ();
	}

	public void CheckAlive()
	{
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	public GameObject GetObjectByName(string name, string caller)
	{
		switch(caller)
		{
		case "Building":
			return ResourceManager.GetUnit(name);
		default:
			return null;
		}
	}

	public bool IsPlayerSet()
	{
		if (player != null)
			return true;
		else
			return false;
	}



	
}
