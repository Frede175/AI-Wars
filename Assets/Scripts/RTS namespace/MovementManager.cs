using UnityEngine;
using System.Collections;


namespace RTS
{
	public class MovementManager : MonoBehaviour {
		public static float cameraSpeed { get { return 20f; } }
		public static float minHeight { get { return 10f; } }
		public static float maxHeight { get { return 40f; } }
		public static float movementBorder { get { return 50f; }}
	}
}

