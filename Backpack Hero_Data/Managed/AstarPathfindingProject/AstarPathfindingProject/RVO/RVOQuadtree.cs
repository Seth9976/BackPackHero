using System;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000D5 RID: 213
	public class RVOQuadtree
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x0003BEAD File Offset: 0x0003A0AD
		public void Clear()
		{
			this.nodes[0] = default(RVOQuadtree.Node);
			this.filledNodes = 1;
			this.maxRadius = 0f;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0003BED3 File Offset: 0x0003A0D3
		public void SetBounds(Rect r)
		{
			this.bounds = r;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0003BEDC File Offset: 0x0003A0DC
		private int GetNodeIndex()
		{
			if (this.filledNodes + 4 >= this.nodes.Length)
			{
				RVOQuadtree.Node[] array = new RVOQuadtree.Node[this.nodes.Length * 2];
				for (int i = 0; i < this.nodes.Length; i++)
				{
					array[i] = this.nodes[i];
				}
				this.nodes = array;
			}
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			return this.filledNodes - 4;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0003C044 File Offset: 0x0003A244
		public void Insert(Agent agent)
		{
			int num = 0;
			Rect rect = this.bounds;
			Vector2 vector = new Vector2(agent.position.x, agent.position.y);
			agent.next = null;
			this.maxRadius = Math.Max(agent.radius, this.maxRadius);
			int num2 = 0;
			for (;;)
			{
				num2++;
				if (this.nodes[num].child00 == num)
				{
					if (this.nodes[num].count < 15 || num2 > 10)
					{
						break;
					}
					this.nodes[num].child00 = this.GetNodeIndex();
					this.nodes[num].Distribute(this.nodes, rect);
				}
				if (this.nodes[num].child00 != num)
				{
					Vector2 center = rect.center;
					if (vector.x > center.x)
					{
						if (vector.y > center.y)
						{
							num = this.nodes[num].child00 + 3;
							rect = Rect.MinMaxRect(center.x, center.y, rect.xMax, rect.yMax);
						}
						else
						{
							num = this.nodes[num].child00 + 2;
							rect = Rect.MinMaxRect(center.x, rect.yMin, rect.xMax, center.y);
						}
					}
					else if (vector.y > center.y)
					{
						num = this.nodes[num].child00 + 1;
						rect = Rect.MinMaxRect(rect.xMin, center.y, center.x, rect.yMax);
					}
					else
					{
						num = this.nodes[num].child00;
						rect = Rect.MinMaxRect(rect.xMin, rect.yMin, center.x, center.y);
					}
				}
			}
			this.nodes[num].Add(agent);
			RVOQuadtree.Node[] array = this.nodes;
			int num3 = num;
			array[num3].count = array[num3].count + 1;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0003C25B File Offset: 0x0003A45B
		public void CalculateSpeeds()
		{
			this.nodes[0].CalculateMaxSpeed(this.nodes, 0);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0003C278 File Offset: 0x0003A478
		public void Query(Vector2 p, float speed, float timeHorizon, float agentRadius, Agent agent)
		{
			RVOQuadtree.QuadtreeQuery quadtreeQuery = default(RVOQuadtree.QuadtreeQuery);
			quadtreeQuery.p = p;
			quadtreeQuery.speed = speed;
			quadtreeQuery.timeHorizon = timeHorizon;
			quadtreeQuery.maxRadius = float.PositiveInfinity;
			quadtreeQuery.agentRadius = agentRadius;
			quadtreeQuery.agent = agent;
			quadtreeQuery.nodes = this.nodes;
			quadtreeQuery.QueryRec(0, this.bounds);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0003C2DE File Offset: 0x0003A4DE
		public void DebugDraw()
		{
			this.DebugDrawRec(0, this.bounds);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003C2F0 File Offset: 0x0003A4F0
		private void DebugDrawRec(int i, Rect r)
		{
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMin), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMin), Color.white);
			if (this.nodes[i].child00 != i)
			{
				Vector2 center = r.center;
				this.DebugDrawRec(this.nodes[i].child00 + 3, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
				this.DebugDrawRec(this.nodes[i].child00 + 2, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
				this.DebugDrawRec(this.nodes[i].child00 + 1, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
				this.DebugDrawRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
			}
			for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
			{
				Vector2 position = this.nodes[i].linkedList.position;
				Debug.DrawLine(new Vector3(position.x, 0f, position.y) + Vector3.up, new Vector3(agent.position.x, 0f, agent.position.y) + Vector3.up, new Color(1f, 1f, 0f, 0.5f));
			}
		}

		// Token: 0x04000553 RID: 1363
		private const int LeafSize = 15;

		// Token: 0x04000554 RID: 1364
		private float maxRadius;

		// Token: 0x04000555 RID: 1365
		private RVOQuadtree.Node[] nodes = new RVOQuadtree.Node[16];

		// Token: 0x04000556 RID: 1366
		private int filledNodes = 1;

		// Token: 0x04000557 RID: 1367
		private Rect bounds;

		// Token: 0x02000160 RID: 352
		private struct Node
		{
			// Token: 0x06000B39 RID: 2873 RVA: 0x000468B0 File Offset: 0x00044AB0
			public void Add(Agent agent)
			{
				agent.next = this.linkedList;
				this.linkedList = agent;
			}

			// Token: 0x06000B3A RID: 2874 RVA: 0x000468C8 File Offset: 0x00044AC8
			public void Distribute(RVOQuadtree.Node[] nodes, Rect r)
			{
				Vector2 center = r.center;
				while (this.linkedList != null)
				{
					Agent next = this.linkedList.next;
					int num = this.child00 + ((this.linkedList.position.x > center.x) ? 2 : 0) + ((this.linkedList.position.y > center.y) ? 1 : 0);
					nodes[num].Add(this.linkedList);
					this.linkedList = next;
				}
				this.count = 0;
			}

			// Token: 0x06000B3B RID: 2875 RVA: 0x00046954 File Offset: 0x00044B54
			public float CalculateMaxSpeed(RVOQuadtree.Node[] nodes, int index)
			{
				if (this.child00 == index)
				{
					for (Agent next = this.linkedList; next != null; next = next.next)
					{
						this.maxSpeed = Math.Max(this.maxSpeed, next.CalculatedSpeed);
					}
				}
				else
				{
					this.maxSpeed = Math.Max(nodes[this.child00].CalculateMaxSpeed(nodes, this.child00), nodes[this.child00 + 1].CalculateMaxSpeed(nodes, this.child00 + 1));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child00 + 2].CalculateMaxSpeed(nodes, this.child00 + 2));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child00 + 3].CalculateMaxSpeed(nodes, this.child00 + 3));
				}
				return this.maxSpeed;
			}

			// Token: 0x040007DB RID: 2011
			public int child00;

			// Token: 0x040007DC RID: 2012
			public Agent linkedList;

			// Token: 0x040007DD RID: 2013
			public byte count;

			// Token: 0x040007DE RID: 2014
			public float maxSpeed;
		}

		// Token: 0x02000161 RID: 353
		private struct QuadtreeQuery
		{
			// Token: 0x06000B3C RID: 2876 RVA: 0x00046A38 File Offset: 0x00044C38
			public void QueryRec(int i, Rect r)
			{
				float num = Math.Min(Math.Max((this.nodes[i].maxSpeed + this.speed) * this.timeHorizon, this.agentRadius) + this.agentRadius, this.maxRadius);
				if (this.nodes[i].child00 == i)
				{
					for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
					{
						float num2 = this.agent.InsertAgentNeighbour(agent, num * num);
						if (num2 < this.maxRadius * this.maxRadius)
						{
							this.maxRadius = Mathf.Sqrt(num2);
						}
					}
					return;
				}
				Vector2 center = r.center;
				if (this.p.x - num < center.x)
				{
					if (this.p.y - num < center.y)
					{
						this.QueryRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
						num = Math.Min(num, this.maxRadius);
					}
					if (this.p.y + num > center.y)
					{
						this.QueryRec(this.nodes[i].child00 + 1, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
						num = Math.Min(num, this.maxRadius);
					}
				}
				if (this.p.x + num > center.x)
				{
					if (this.p.y - num < center.y)
					{
						this.QueryRec(this.nodes[i].child00 + 2, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
						num = Math.Min(num, this.maxRadius);
					}
					if (this.p.y + num > center.y)
					{
						this.QueryRec(this.nodes[i].child00 + 3, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
					}
				}
			}

			// Token: 0x040007DF RID: 2015
			public Vector2 p;

			// Token: 0x040007E0 RID: 2016
			public float speed;

			// Token: 0x040007E1 RID: 2017
			public float timeHorizon;

			// Token: 0x040007E2 RID: 2018
			public float agentRadius;

			// Token: 0x040007E3 RID: 2019
			public float maxRadius;

			// Token: 0x040007E4 RID: 2020
			public Agent agent;

			// Token: 0x040007E5 RID: 2021
			public RVOQuadtree.Node[] nodes;
		}
	}
}
