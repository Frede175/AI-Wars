using UnityEngine;
using System.Collections;
using RTS;

public class WorldObjects : MonoBehaviour {

	public string objectName;
	public int cost, sellValue, maxHealth, toughness;
	public bool canMove = false;

	protected string[] actions = {};
	protected Player player;
	protected bool isSelected;
	protected Bounds selectionBounds;
	protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);



	protected virtual void Awake()
	{
		selectionBounds = ResourceManager.InvalidBounds;
		CalculateBounds();
	}

	protected virtual void Start()
	{
		player = transform.root.GetComponent<Player>();
		SetColor();
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

	private void DrawSelection()
	{
		GUI.skin = ResourceManager.GuiSelectBox;
		Rect selectBox = WorkManager.CalcSelectionBox(selectionBounds, playingArea);
		//GUI.BeginGroup(playingArea);
		DrawSelectionBox(selectBox);
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
			WorldObjects worldObject = hitObject.transform.root.GetComponent< WorldObjects >();
			if(worldObject)
			{
				ChangeSelection(worldObject, controller);
			}
		}
		else if (!controller.SelectedObject.canMove)
		{
			controller.SelectedObject.SetSelection(false, controller.hud.GetPlayingArea());
			controller.SelectedObject = null;
		}
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
	
}
