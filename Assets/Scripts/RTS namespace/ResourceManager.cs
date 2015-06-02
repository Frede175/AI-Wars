using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace RTS
{
	public class ResourceManager : MonoBehaviour {
		public static float cameraSpeed { get { return 10f; } }
		public static float minHeight { get { return 5f; } }
		public static float maxHeight { get { return 10f; } }
		public static float zoomSpeed { get { return 2f; } }
		public static float movementBorder { get { return 50f; }}
		private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
		public static Vector3 InvalidPosition { get { return invalidPosition; } }

		//GUI
		private static GUISkin guiSelectBox;
		private static Texture2D  healthActive, healthDeActive;
		public static GUISkin GuiSelectBox { get { return guiSelectBox; } }
		public static Texture2D HealthActive { get { return healthActive; } }
		public static Texture2D HealthDeActive { get { return healthDeActive; } }
		public static void storeHUDVars (GUISkin guiSkin, Texture2D _healthActive, Texture2D _healthDeActive)
		{
			guiSelectBox = guiSkin;
			healthActive = _healthActive;
			healthDeActive = _healthDeActive;
		}


		private static Bounds invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));
		public static Bounds InvalidBounds { get { return invalidBounds; } }


		private static GameObjectList objectList;
		public static void SetObjectList(GameObjectList _objectList)
		{
			objectList = _objectList;
		}

		public static GameObject GetUnit(string name)
		{
			return objectList.GetUnit(name);
		}

		public static GameObject GetBuilding(string name)
		{
			return objectList.GetBuilding(name);
		}

		public static GameObject GetWorldObject(string name)
		{
			return objectList.GetWorldObject(name);
		}

		public static GameObject GetPlayer()
		{
			return objectList.GetPlayer();
		}
	}
}

