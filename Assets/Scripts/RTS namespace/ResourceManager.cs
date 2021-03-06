﻿using UnityEngine;
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

		//Resourece for game:
		public static int maxStartMoney { get { return 1500; } } 
		public static int minStartMoney { get { return 1000; } }
		public static int GetAverageStartMoney()
		{
			return (int)(maxStartMoney + minStartMoney)/2;
		}

		public static int moneyPerDeposit { get { return 400; } }
		public static int moneyTransfereSpeed { get { return 3; } }
		public static int moneyDelay { get { return 10; } } 

		//GUI
		private static GUISkin guiSelectBox;
		private static Texture2D  healthActive, healthDeActive, processBar;
		public static GUISkin GuiSelectBox { get { return guiSelectBox; } }
		public static Texture2D HealthActive { get { return healthActive; } }
		public static Texture2D HealthDeActive { get { return healthDeActive; } }
		public static Texture2D ProcessBar { get { return processBar; } }
		public static void storeHUDVars (GUISkin guiSkin, Texture2D _healthActive, Texture2D _healthDeActive, Texture2D _processBar)
		{
			guiSelectBox = guiSkin;
			healthActive = _healthActive;
			healthDeActive = _healthDeActive;
			processBar = _processBar;
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

		public static Texture2D GetBuildTexture(string name)
		{
			return objectList.GetBuildTexture(name);
		}


		//Deposits:
		public static List<GameObject> availableDeposits = new List<GameObject>();
		public static List<GameObject> noneAvailableDeposits = new List<GameObject>();

		public static void MakeListsForDeposits()
		{
			availableDeposits = objectList.GetDeposits();
		}

		public static void AddToNoneAvailable(GameObject deposit)
		{
			if (availableDeposits.Contains(deposit)) 
			{
				noneAvailableDeposits.Add(deposit);
				availableDeposits.Remove(deposit);
			}
		}

		public static void AddToAvailbable(GameObject deposit)
		{
			if (noneAvailableDeposits.Contains(deposit)) 
			{
				availableDeposits.Add(deposit);
				noneAvailableDeposits.Remove(deposit);
			}
		}

		public static bool DepositIsAvailable(GameObject deposit)
		{
			return (availableDeposits.Contains(deposit));
		}
	}
}

