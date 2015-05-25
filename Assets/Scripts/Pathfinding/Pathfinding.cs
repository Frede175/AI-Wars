using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	public Transform seeker, target;


	Node sNode;
	Node tNode;

	Map map;

	void Awake()
	{
		map = GetComponent<Map>();

	}

	void Update()
	{
			FindPath(new Vector2(seeker.position.x, seeker.position.y), new Vector2(target.position.x, target.position.y));
	}


	public void FindPath(Vector2 startPos, Vector2 targetPos)
	{
		Node startNode =  map.WorldPointToGridPoint(startPos);
		Node targetNode = map.WorldPointToGridPoint(targetPos);
		if (sNode == startNode && tNode == targetNode)
			return;
		else
		{
			sNode = startNode;
			tNode = targetNode;
		}
		

		Heap<Node> openSet = new Heap<Node>(map.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);
		while (openSet.Count > 0)
		{
			Node currentNode = openSet.RemoveFirst();

			closedSet.Add(currentNode);

			if (currentNode == targetNode)
			{
				GetPath(startNode, targetNode);

				return;
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

	}

	void GetPath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node trackNode = endNode;
		
		while (trackNode != startNode)
		{
			path.Add(trackNode);
			trackNode = trackNode.parent;
		}
		path.Add(trackNode);
		path.Reverse();
		map.path = path;

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
