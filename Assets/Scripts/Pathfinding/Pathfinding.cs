using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour {

	public bool simple = true;
	Map map;
	PathManager pathManager;

	void Awake()
	{
		map = GetComponent<Map>();
		pathManager = GetComponent<PathManager>();
	}

	public void StartFindingPath(Vector3 start, Vector3 end)
	{
		start = new Vector2 (start.x, start.y);
		end = new Vector2 (end.x, end.y);
		StartCoroutine(FindPath(start, end));
	}


	IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
	{
		Node startNode =  map.WorldPointToGridPoint(startPos);
		Node targetNode = map.WorldPointToGridPoint(targetPos);

		Vector2[] waypoints = new Vector2[0];
		bool success = false;

		if (!startNode.isWalkable && !targetNode.isWalkable)
			yield break;

		Heap<Node> openSet = new Heap<Node>(map.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);
		while (openSet.Count > 0)
		{
			Node currentNode = openSet.RemoveFirst();

			closedSet.Add(currentNode);

			if (currentNode == targetNode)
			{
				success = true;
				break;
			}

			foreach (Node neighbour in map.GetNeighbours(currentNode))
			{
				if (!neighbour.isWalkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int MovemntCostToneighbour = currentNode.gCost + DistanceBetween(currentNode, neighbour);
				if (MovemntCostToneighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = MovemntCostToneighbour;
					neighbour.hCost = DistanceBetween(neighbour, targetNode);
					neighbour.parent = currentNode;
					if (!openSet.Contains(neighbour))
					{
						openSet.Add(neighbour);
					}
					else
					{
						openSet.UpdateItem(neighbour);
					}

				}
			}
		}
		yield return null;
		if (success)
		{
			waypoints = GetPath(startNode, targetNode, targetPos);
		}
		pathManager.FinishedProcessingPath(waypoints, success);

	}

	Vector2[] GetPath(Node startNode, Node endNode, Vector2 targetPos)
	{
		List<Node> path = new List<Node>();
		Node trackNode = endNode;
		
		while (trackNode != startNode)
		{
			path.Add(trackNode);
			trackNode = trackNode.parent;
		}
		Vector2[] newPath;
		if (simple)
		{
			newPath = simplePath(path, targetPos);
		}
		else
		{
			newPath = convertToVector2(path, targetPos);
		}
		Array.Reverse(newPath);
		return newPath;

	}

	Vector2[] simplePath(List<Node> path, Vector2 targetPos)
	{
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 dirOld = Vector2.zero;
		waypoints.Add(path[0].worldPos);
		for (int a = 1; a < path.Count; a++)
		{
			Vector2 dirNew = new Vector2 (path[a].nodeX - path[a-1].nodeX, path[a].nodeY - path[a-1].nodeY);
			if (dirNew != dirOld)
			{
				waypoints.Add(path[a].worldPos);
			}
			dirOld = dirNew;
		}
		waypoints[0] = targetPos;
		return waypoints.ToArray();
	}

	Vector2[] convertToVector2(List<Node> path, Vector2 targetPos)
	{
		List<Vector2> waypoints = new List<Vector2>();
		foreach (Node n in path)
		{
			waypoints.Add(n.worldPos);
		}
		return waypoints.ToArray();
	}

	int DistanceBetween(Node nodeA, Node nodeB)
	{
		int distanceX = Mathf.Abs(nodeA.nodeX - nodeB.nodeX);
		int distanceY = Mathf.Abs(nodeA.nodeY - nodeB.nodeY);

		if (distanceX > distanceY)
			return 14 * distanceY + 10 * (distanceX - distanceY);
		return 14 * distanceX + 10 * (distanceY - distanceX);

	}


}
