using UnityEngine;
using System.Collections.Generic;



namespace RTS
{
	public class WorkManager {
		public static Rect CalcSelectionBox(Bounds selectionBoxBounds, Rect playingArea)
		{
			float cx = selectionBoxBounds.center.x;
			float cy = selectionBoxBounds.center.y;
			float cz = selectionBoxBounds.center.z;
			//shorthand for the coordinates of the extents of the selection bounds
			float ex = selectionBoxBounds.extents.x;
			float ey = selectionBoxBounds.extents.y;
			float ez = selectionBoxBounds.extents.z;
			
			//Determine the screen coordinates for the corners of the selection bounds
			List< Vector3 > corners = new List< Vector3 >();
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy+ey, cz+ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy+ey, cz-ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy-ey, cz+ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy+ey, cz+ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy-ey, cz-ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy-ey, cz+ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy+ey, cz-ez)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy-ey, cz-ez)));

			Bounds screenBounds = new Bounds(corners[0], Vector3.zero);
			for(int i = 1; i < corners.Count; i++) {
				screenBounds.Encapsulate(corners[i]);
			}

			float selectBoxTop = playingArea.height - (screenBounds.center.y + screenBounds.extents.y);
			float selectBoxLeft = screenBounds.center.x - screenBounds.extents.x;
			float selectBoxWidth = 2 * screenBounds.extents.x;
			float selectBoxHeight = 2 * screenBounds.extents.y;

			return new Rect(selectBoxLeft, selectBoxTop, selectBoxWidth, selectBoxHeight);
		}
		
	}

}
	