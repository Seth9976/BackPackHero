using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class PathFinding : MonoBehaviour
{
	// Token: 0x0600005A RID: 90 RVA: 0x00003D48 File Offset: 0x00001F48
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Vector2 SearchDirection(Vector2 startingSpace, string myDirection)
	{
		Vector2 vector;
		if (PathFinding.directionLookup.TryGetValue(myDirection, out vector))
		{
			return startingSpace + vector;
		}
		return startingSpace;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003D70 File Offset: 0x00001F70
	public static GameObject[] GetObjectsAtVector(Vector2 position)
	{
		RaycastHit2D[] array = Physics2D.RaycastAll(position, Vector3.forward);
		GameObject[] array2 = new GameObject[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].collider.gameObject;
		}
		return array2;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00003DBC File Offset: 0x00001FBC
	public static bool OpenGridSpace(PathFinding.Location loc)
	{
		if (loc.position.y > 5f || loc.position.y < 0.5f || loc.position.x < -5f || loc.position.x > 5f)
		{
			return false;
		}
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(loc.position);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			if (objectsAtVector[i].CompareTag("GridSquare"))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003E40 File Offset: 0x00002040
	public static bool GridSpaceHere(PathFinding.Location loc, bool meaningless)
	{
		if (loc.position.y > 5f || loc.position.y < 0f || loc.position.x < -5f || loc.position.x > 5f)
		{
			return false;
		}
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(loc.position);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			if (objectsAtVector[i].CompareTag("GridSquare"))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003EC3 File Offset: 0x000020C3
	public static bool FindPath(Vector2 startingVector, Vector2 endingVector, Func<PathFinding.Location, bool, bool> AcceptableSpace, out List<Vector2> pathList, string[] directions = null)
	{
		return PathFinding.FindPath(new List<Vector2> { startingVector }, new List<Vector2> { endingVector }, AcceptableSpace, out pathList, directions);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00003EE8 File Offset: 0x000020E8
	public static bool FindPath(List<Vector2Int> startingVectors, List<Vector2Int> endingVectors, Func<PathFinding.Location, bool, bool> AcceptableSpace, out List<Vector2> pathList, string[] directions = null)
	{
		return PathFinding.FindPath(startingVectors.Select((Vector2Int v) => v).ToList<Vector2>(), endingVectors.Select((Vector2Int v) => v).ToList<Vector2>(), AcceptableSpace, out pathList, directions);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003F54 File Offset: 0x00002154
	public static void FindConnectedStructures(List<Vector2Int> startingVectors, Func<PathFinding.Location, bool, bool> AcceptableSpace, out HashSet<Overworld_Structure> structures, out List<Vector2> vecsTried)
	{
		string[] array = new string[] { "e", "w", "n", "s" };
		Queue<PathFinding.Location> queue = new Queue<PathFinding.Location>();
		structures = new HashSet<Overworld_Structure>();
		vecsTried = new List<Vector2>();
		HashSet<Vector2> hashSet = new HashSet<Vector2>();
		foreach (Vector2Int vector2Int in startingVectors)
		{
			Vector2 vector = vector2Int;
			queue.Enqueue(new PathFinding.Location(vector, new List<Vector2>(), 0));
			vecsTried.Add(vector);
			hashSet.Add(vector);
		}
		int num = 0;
		while (queue.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return;
			}
			PathFinding.Location location = queue.Dequeue();
			for (int i = 0; i < array.Length; i++)
			{
				Vector2 vector2 = PathFinding.SearchDirection(location.position, array[i]);
				if (!hashSet.Contains(vector2))
				{
					vecsTried.Add(vector2);
					hashSet.Add(vector2);
					PathFinding.Location location2 = new PathFinding.Location(vector2, new List<Vector2>(), location.strikeNumber);
					foreach (GridObject gridObject in GridObject.GetItemsAtPosition(location2.position))
					{
						if (gridObject.type == GridObject.Type.item)
						{
							Overworld_Structure componentInParent = gridObject.GetComponentInParent<Overworld_Structure>();
							if (componentInParent && !structures.Contains(componentInParent))
							{
								structures.Add(componentInParent);
							}
						}
					}
					if (AcceptableSpace(location2, false))
					{
						queue.Enqueue(location2);
					}
				}
			}
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004120 File Offset: 0x00002320
	private static bool CloseEnoughToEnd(Vector2 position, List<Vector2> endingVectors)
	{
		foreach (Vector2 vector in endingVectors)
		{
			if (Vector2.Distance(position, vector) < 0.1f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000417C File Offset: 0x0000237C
	public static bool FindPath(List<Vector2> startingVectors, List<Vector2> endingVectors, Func<PathFinding.Location, bool, bool> AcceptableSpace, out List<Vector2> pathList, string[] directions = null)
	{
		if (directions == null)
		{
			directions = new string[] { "e", "w", "n", "s" };
		}
		pathList = new List<Vector2>();
		if (startingVectors.Intersect(endingVectors).Any<Vector2>())
		{
			return true;
		}
		List<PathFinding.Location> list = new List<PathFinding.Location>();
		List<PathFinding.Location> list2 = new List<PathFinding.Location>();
		foreach (Vector2 vector in startingVectors)
		{
			list.Add(new PathFinding.Location(vector, new List<Vector2>(), 0));
		}
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return false;
			}
			PathFinding.Location location = list[0];
			list.RemoveAt(0);
			for (int i = 0; i < directions.Length; i++)
			{
				Vector2 vector2 = PathFinding.SearchDirection(location.position, directions[i]);
				PathFinding.Location location2 = new PathFinding.Location(vector2, new List<Vector2>(location.path) { location.position }, location.strikeNumber);
				if (PathFinding.enableDebug)
				{
					string text = "newLocation: ";
					Vector2 vector3 = location2.position;
					Debug.Log(text + vector3.ToString());
				}
				if (PathFinding.CloseEnoughToEnd(location2.position, endingVectors) && AcceptableSpace(location2, true))
				{
					pathList = location2.path;
					PathFinding.storedLocationsTried = list2;
					PathFinding.endingPosition = vector2;
					return true;
				}
				if (AcceptableSpace(location2, false))
				{
					bool flag = false;
					foreach (PathFinding.Location location3 in list2)
					{
						if (location3.position == location2.position && location3.strikeNumber <= location2.strikeNumber)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (PathFinding.enableDebug)
						{
							string text2 = "Adding new location: ";
							Vector2 vector3 = location2.position;
							Debug.Log(text2 + vector3.ToString());
						}
						list2.Add(location2);
						list.Add(location2);
					}
				}
			}
		}
		PathFinding.storedLocationsTried = list2;
		return false;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000043D4 File Offset: 0x000025D4
	public static void FindAcceptableConnectedSpaces(List<Vector2Int> startingVectors, Func<PathFinding.Location, bool> WinningSpace, Func<PathFinding.Location, bool, bool> AcceptableSpace, out List<Vector2> winningSpaces, string[] directions = null)
	{
		if (directions == null)
		{
			directions = new string[] { "e", "w", "n", "s" };
		}
		new List<Vector2>();
		winningSpaces = new List<Vector2>();
		List<PathFinding.Location> list = new List<PathFinding.Location>();
		List<PathFinding.Location> list2 = new List<PathFinding.Location>();
		foreach (Vector2Int vector2Int in startingVectors)
		{
			Vector2 vector = vector2Int;
			list.Add(new PathFinding.Location(vector, new List<Vector2>(), 0));
		}
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return;
			}
			PathFinding.Location location = list[0];
			list.RemoveAt(0);
			for (int i = 0; i < directions.Length; i++)
			{
				PathFinding.Location location2 = new PathFinding.Location(PathFinding.SearchDirection(location.position, directions[i]), new List<Vector2>(location.path) { location.position }, location.strikeNumber);
				if (AcceptableSpace(location2, false))
				{
					bool flag = false;
					foreach (PathFinding.Location location3 in list2)
					{
						if (location3.position == location2.position && location3.strikeNumber <= location2.strikeNumber)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						list2.Add(location2);
						list.Add(location2);
					}
				}
				if (WinningSpace(location2))
				{
					List<Vector2> path = location2.path;
					winningSpaces.Add(location2.position);
					return;
				}
			}
		}
		PathFinding.storedLocationsTried = list2;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000045B4 File Offset: 0x000027B4
	public static bool FindPathRanked(Vector2 startingVector, Vector2 endingVector, Func<PathFinding.Location, bool, int> AcceptableSpace, out List<Vector2> pathList, string[] directions = null)
	{
		if (directions == null)
		{
			directions = new string[] { "e", "w", "n", "s" };
		}
		pathList = new List<Vector2>();
		if (endingVector == startingVector)
		{
			return true;
		}
		PathFinding.placesTried = new List<Vector2>();
		List<PathFinding.Location> list = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return false;
			}
			list = PathFinding.SortByRank(list);
			PathFinding.Location location = list[0];
			list.RemoveAt(0);
			int rankValue = location.rankValue;
			for (int i = 0; i < directions.Length; i++)
			{
				Vector2 vector = PathFinding.SearchDirection(location.position, directions[i]);
				PathFinding.Location location2 = new PathFinding.Location(vector, new List<Vector2>(location.path) { location.position }, 0);
				if (vector == endingVector && AcceptableSpace(location2, true) != -1)
				{
					pathList = location2.path;
					return true;
				}
				int num2 = AcceptableSpace(location2, false);
				if (num2 != -1 && !PathFinding.placesTried.Contains(vector))
				{
					PathFinding.placesTried.Add(vector);
					if (directions[i] == "ne" || directions[i] == "se" || directions[i] == "nw" || directions[i] == "sw")
					{
						num2 += 6;
					}
					location2.rankValue = rankValue + num2;
					list.Add(location2);
				}
			}
		}
		return false;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004763 File Offset: 0x00002963
	private static List<PathFinding.Location> SortByRank(List<PathFinding.Location> objects)
	{
		return objects.OrderBy((PathFinding.Location x) => x.rankValue).ToList<PathFinding.Location>();
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004790 File Offset: 0x00002990
	public static void FindAvailableSpaces(Vector2 startingVector, int distance, Func<PathFinding.Location, bool, bool> AcceptableSpace, out List<PathFinding.Location> availableSpacesList, string[] directions = null)
	{
		startingVector = Vector2Int.RoundToInt(startingVector);
		availableSpacesList = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		PathFinding.placesTried = new List<Vector2> { startingVector };
		List<PathFinding.Location> list = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		if (directions == null)
		{
			directions = new string[] { "e", "w", "n", "s" };
		}
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return;
			}
			PathFinding.Location location = list[0];
			list.RemoveAt(0);
			for (int i = 0; i < directions.Length; i++)
			{
				Vector2 vector = PathFinding.SearchDirection(location.position, directions[i]);
				if (!PathFinding.placesTried.Contains(vector))
				{
					PathFinding.placesTried.Add(vector);
					List<Vector2> list2 = new List<Vector2>(location.path);
					list2.Add(location.position);
					PathFinding.Location location2 = new PathFinding.Location(vector, list2, 0);
					if (AcceptableSpace(location2, false))
					{
						availableSpacesList.Add(location2);
						if (location.path.Count < distance)
						{
							list.Add(new PathFinding.Location(vector, list2, 0));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000048EC File Offset: 0x00002AEC
	public static void FindRandomConnectedSpaces(Vector2 startingVector, int distance, Func<PathFinding.Location, bool> AcceptableSpace, out List<PathFinding.Location> availableSpacesList, int numToFind)
	{
		startingVector = Vector2Int.RoundToInt(startingVector);
		availableSpacesList = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		PathFinding.placesTried = new List<Vector2> { startingVector };
		List<PathFinding.Location> list = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		List<string> list2 = new List<string> { "e", "w", "n", "s" };
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return;
			}
			PathFinding.Location location = list[0];
			list.RemoveAt(0);
			Random rnd = new Random();
			list2 = list2.OrderBy((string x) => rnd.Next()).ToList<string>();
			for (int i = 0; i < list2.Count; i++)
			{
				Vector2 vector = PathFinding.SearchDirection(location.position, list2[i]);
				if (!PathFinding.placesTried.Contains(vector))
				{
					PathFinding.placesTried.Add(vector);
					List<Vector2> list3 = new List<Vector2>(location.path);
					list3.Add(location.position);
					PathFinding.Location location2 = new PathFinding.Location(vector, list3, 0);
					if (AcceptableSpace(location2))
					{
						availableSpacesList.Add(location2);
						if (availableSpacesList.Count >= numToFind)
						{
							return;
						}
						if (location.path.Count < distance)
						{
							list.Insert(Random.Range(0, list.Count), new PathFinding.Location(vector, list3, 0));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004A9C File Offset: 0x00002C9C
	public static Transform SearchForItem(Item2 startingItem, Vector2 startingVector, int distance, Func<PathFinding.Location, Transform> AcceptableSpace, Func<PathFinding.Location, Transform> WinningSpace, out List<Vector2> pathList, string[] directions = null)
	{
		pathList = new List<Vector2>();
		PathFinding.placesTried = new List<Vector2> { startingVector };
		List<PathFinding.Location> list = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		if (directions == null)
		{
			directions = new string[] { "e", "w", "n", "s" };
		}
		foreach (Vector2 vector in startingItem.GetComponentInParent<ItemMovement>().FindAllColliders())
		{
			if (!PathFinding.placesTried.Contains(vector))
			{
				list.Add(new PathFinding.Location(vector, new List<Vector2> { vector }, 0));
			}
		}
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return null;
			}
			PathFinding.Location location = list[0];
			list.RemoveAt(0);
			for (int i = 0; i < directions.Length; i++)
			{
				Vector2 vector2 = PathFinding.SearchDirection(location.position, directions[i]);
				if (!PathFinding.placesTried.Contains(vector2))
				{
					PathFinding.placesTried.Add(vector2);
					List<Vector2> list2 = new List<Vector2>(location.path);
					list2.Add(location.position);
					PathFinding.Location location2 = new PathFinding.Location(vector2, list2, 0);
					Transform transform = AcceptableSpace(location2);
					if (transform != null)
					{
						Transform transform2 = WinningSpace(location2);
						if (transform2)
						{
							pathList = location2.path;
							return transform2;
						}
						if (location.path.Count < distance)
						{
							list.Add(new PathFinding.Location(vector2, list2, 0));
							if (transform)
							{
								foreach (Vector2 vector3 in transform.GetComponentInParent<ItemMovement>().FindAllColliders())
								{
									if (!PathFinding.placesTried.Contains(vector3))
									{
										list.Add(new PathFinding.Location(vector3, new List<Vector2>(list2) { vector3 }, 0));
									}
								}
							}
						}
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004CF4 File Offset: 0x00002EF4
	public static bool TransformFound(List<PathFinding.TransformAndPath> list, Transform t)
	{
		using (List<PathFinding.TransformAndPath>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.t == t)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004D50 File Offset: 0x00002F50
	public static List<PathFinding.TransformAndPath> SearchForItems(Item2 startingItem, Vector2 startingVector, int distance, Func<PathFinding.Location, Transform> AcceptableSpace, Func<PathFinding.Location, Transform> WinningSpace, string[] directions = null)
	{
		List<PathFinding.TransformAndPath> list = new List<PathFinding.TransformAndPath>();
		PathFinding.placesTried = new List<Vector2> { startingVector };
		List<PathFinding.Location> list2 = new List<PathFinding.Location>
		{
			new PathFinding.Location(startingVector, new List<Vector2>(), 0)
		};
		if (directions == null)
		{
			directions = new string[] { "e", "w", "n", "s" };
		}
		foreach (Vector2 vector in startingItem.GetComponentInParent<ItemMovement>().FindAllColliders())
		{
			if (!PathFinding.placesTried.Contains(vector))
			{
				List<Vector2> list3 = new List<Vector2>();
				list3.Add(vector);
				PathFinding.placesTried.Add(vector);
				list2.Add(new PathFinding.Location(vector, list3, 0));
			}
		}
		foreach (PathFinding.Location location in list2)
		{
			Transform transform = WinningSpace(location);
			if (transform && !PathFinding.TransformFound(list, transform))
			{
				list.Add(new PathFinding.TransformAndPath
				{
					t = transform,
					pathList = location.path
				});
			}
		}
		int num = 0;
		while (list2.Count > 0)
		{
			num++;
			if (num > 2000)
			{
				Debug.Log("Too many loops! Pathfinding stopped!");
				return null;
			}
			PathFinding.Location location2 = list2[0];
			list2.RemoveAt(0);
			for (int i = 0; i < directions.Length; i++)
			{
				Vector2 vector2 = PathFinding.SearchDirection(location2.position, directions[i]);
				if (!PathFinding.placesTried.Contains(vector2))
				{
					PathFinding.placesTried.Add(vector2);
					List<Vector2> list4 = new List<Vector2>(location2.path);
					list4.Add(location2.position);
					PathFinding.Location location3 = new PathFinding.Location(vector2, list4, 0);
					Transform transform2 = WinningSpace(location3);
					if (transform2 && !PathFinding.TransformFound(list, transform2))
					{
						list.Add(new PathFinding.TransformAndPath
						{
							t = transform2,
							pathList = location3.path
						});
					}
					Transform transform3 = AcceptableSpace(location3);
					if (transform3 != null && location2.path.Count < distance)
					{
						list2.Add(new PathFinding.Location(vector2, list4, 0));
						if (transform3)
						{
							foreach (Vector2 vector3 in transform3.GetComponentInParent<ItemMovement>().FindAllColliders())
							{
								if (!PathFinding.placesTried.Contains(vector3))
								{
									list2.Add(new PathFinding.Location(vector3, new List<Vector2>(list4) { vector3 }, 0));
								}
							}
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x04000039 RID: 57
	private const int maxSearchDepth = 2000;

	// Token: 0x0400003A RID: 58
	public static List<Vector2> placesTried;

	// Token: 0x0400003B RID: 59
	private static Dictionary<string, Vector2> directionLookup = new Dictionary<string, Vector2>
	{
		{
			"n",
			Vector2.up
		},
		{
			"s",
			Vector2.up * -1f
		},
		{
			"e",
			Vector2.right
		},
		{
			"w",
			Vector2.right * -1f
		},
		{
			"ne",
			Vector2.right + Vector2.up
		},
		{
			"nw",
			Vector2.right * -1f + Vector2.up
		},
		{
			"se",
			Vector2.right + Vector2.up * -1f
		},
		{
			"sw",
			Vector2.right * -1f + Vector2.up * -1f
		}
	};

	// Token: 0x0400003C RID: 60
	public static List<PathFinding.Location> storedLocationsTried;

	// Token: 0x0400003D RID: 61
	public static bool enableDebug = false;

	// Token: 0x0400003E RID: 62
	public static Vector2 endingPosition = new Vector2(0f, 0f);

	// Token: 0x02000240 RID: 576
	public class Location
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x000AE460 File Offset: 0x000AC660
		public Location(Vector2 _position, List<Vector2> _path, int _strikeNumber = 0)
		{
			this.position = _position;
			this.path = _path;
			this.strikeNumber = _strikeNumber;
		}

		// Token: 0x04000EA7 RID: 3751
		public Vector2 position;

		// Token: 0x04000EA8 RID: 3752
		public List<Vector2> path;

		// Token: 0x04000EA9 RID: 3753
		public int rankValue;

		// Token: 0x04000EAA RID: 3754
		public int strikeNumber;
	}

	// Token: 0x02000241 RID: 577
	public class TransformAndPath
	{
		// Token: 0x04000EAB RID: 3755
		public Transform t;

		// Token: 0x04000EAC RID: 3756
		public List<Vector2> pathList;
	}
}
