using UnityEngine;
using System.Collections;


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
		public static GUISkin GuiSelectBox { get { return guiSelectBox; } }
		public static void storeGuiSelectBox (GUISkin guiSkin)
		{
			guiSelectBox = guiSkin;
		}


		private static Bounds invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));
		public static Bounds InvalidBounds { get { return invalidBounds; } }

	}
}

