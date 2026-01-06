using System;
using Pathfinding.Drawing;
using Pathfinding.ECS.RVO;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020002A3 RID: 675
	public struct RVOQuadtreeBurst
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x0006379C File Offset: 0x0006199C
		static RVOQuadtreeBurst()
		{
			for (int i = 0; i < 256; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (((i >> j) & 1) != 0)
					{
						RVOQuadtreeBurst.ChildLookup[i] = (byte)j;
						break;
					}
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x000637EC File Offset: 0x000619EC
		public Rect bounds
		{
			get
			{
				if (!this.boundingBoxBuffer.IsCreated)
				{
					return default(Rect);
				}
				return Rect.MinMaxRect(this.boundingBoxBuffer[0].x, this.boundingBoxBuffer[0].y, this.boundingBoxBuffer[1].x, this.boundingBoxBuffer[1].y);
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00063859 File Offset: 0x00061A59
		private static int InnerNodeCountUpperBound(int numAgents, MovementPlane movementPlane)
		{
			return (((movementPlane == MovementPlane.Arbitrary) ? 8 : 4) * 10 * numAgents + 16 - 1) / 16;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00063870 File Offset: 0x00061A70
		public void Dispose()
		{
			this.agents.Dispose();
			this.childPointers.Dispose();
			this.boundingBoxBuffer.Dispose();
			this.agentCountBuffer.Dispose();
			this.maxSpeeds.Dispose();
			this.maxRadius.Dispose();
			this.nodeAreas.Dispose();
			this.agentPositions.Dispose();
			this.agentRadii.Dispose();
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000638E0 File Offset: 0x00061AE0
		private void Reserve(int minSize)
		{
			if (!this.boundingBoxBuffer.IsCreated)
			{
				this.boundingBoxBuffer = new NativeArray<float3>(4, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.agentCountBuffer = new NativeArray<int>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			int num = math.ceilpow2(minSize);
			Memory.Realloc<int>(ref this.agents, num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Memory.Realloc<float3>(ref this.agentPositions, num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Memory.Realloc<float>(ref this.agentRadii, num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Memory.Realloc<int>(ref this.childPointers, RVOQuadtreeBurst.InnerNodeCountUpperBound(num, this.movementPlane), Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Memory.Realloc<float>(ref this.maxSpeeds, this.childPointers.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Memory.Realloc<float>(ref this.nodeAreas, this.childPointers.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Memory.Realloc<float>(ref this.maxRadius, this.childPointers.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000639A8 File Offset: 0x00061BA8
		public RVOQuadtreeBurst.JobBuild BuildJob(NativeArray<float3> agentPositions, NativeArray<AgentIndex> agentVersions, NativeArray<float> agentSpeeds, NativeArray<float> agentRadii, int numAgents, MovementPlane movementPlane)
		{
			if (numAgents >= 32767)
			{
				throw new Exception("Too many agents. Cannot have more than " + 32767.ToString());
			}
			this.Reserve(numAgents);
			this.movementPlane = movementPlane;
			return new RVOQuadtreeBurst.JobBuild
			{
				agents = this.agents,
				agentVersions = agentVersions,
				agentPositions = agentPositions,
				agentSpeeds = agentSpeeds,
				agentRadii = agentRadii,
				outMaxSpeeds = this.maxSpeeds,
				outMaxRadius = this.maxRadius,
				outArea = this.nodeAreas,
				outAgentRadii = this.agentRadii,
				outAgentPositions = this.agentPositions,
				outBoundingBox = this.boundingBoxBuffer,
				outAgentCount = this.agentCountBuffer,
				outChildPointers = this.childPointers,
				numAgents = numAgents,
				movementPlane = movementPlane
			};
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00063A9C File Offset: 0x00061C9C
		public void QueryKNearest(RVOQuadtreeBurst.QuadtreeQuery query)
		{
			if (!this.agents.IsCreated)
			{
				return;
			}
			float positiveInfinity = float.PositiveInfinity;
			for (int i = 0; i < query.maxCount; i++)
			{
				query.result[query.outputStartIndex + i] = -1;
			}
			for (int j = 0; j < query.maxCount; j++)
			{
				query.resultDistances[j] = float.PositiveInfinity;
			}
			this.QueryRec(ref query, 0, this.boundingBoxBuffer[0], this.boundingBoxBuffer[1], ref positiveInfinity);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00063B2C File Offset: 0x00061D2C
		private void QueryRec(ref RVOQuadtreeBurst.QuadtreeQuery query, int treeNodeIndex, float3 nodeMin, float3 nodeMax, ref float maxRadius)
		{
			float num = math.min(math.max((this.maxSpeeds[treeNodeIndex] + query.speed) * query.timeHorizon, query.agentRadius) + query.agentRadius, maxRadius);
			float3 position = query.position;
			if ((this.childPointers[treeNodeIndex] & 1073741824) != 0)
			{
				int maxCount = query.maxCount;
				int num2 = this.childPointers[treeNodeIndex] & 32767;
				int num3 = (this.childPointers[treeNodeIndex] >> 15) & 32767;
				NativeArray<int> result = query.result;
				NativeArray<float> resultDistances = query.resultDistances;
				for (int i = num2; i < num3; i++)
				{
					int num4 = this.agents[i];
					float num5 = math.lengthsq(position - this.agentPositions[num4]);
					if (num5 < num * num)
					{
						int j = 0;
						while (j < maxCount)
						{
							if (num5 < resultDistances[j])
							{
								for (int k = maxCount - 1; k > j; k--)
								{
									result[query.outputStartIndex + k] = result[query.outputStartIndex + k - 1];
									resultDistances[k] = resultDistances[k - 1];
								}
								result[query.outputStartIndex + j] = num4;
								resultDistances[j] = num5;
								if (j == maxCount - 1)
								{
									maxRadius = math.min(maxRadius, math.sqrt(num5));
									num = math.min(num, maxRadius);
									break;
								}
								break;
							}
							else
							{
								j++;
							}
						}
					}
				}
				return;
			}
			int num6 = this.childPointers[treeNodeIndex];
			float3 @float = (nodeMin + nodeMax) * 0.5f;
			if (this.movementPlane == MovementPlane.Arbitrary)
			{
				int num7 = ((position.x < @float.x) ? 0 : 4) | ((position.y < @float.y) ? 0 : 2) | ((position.z < @float.z) ? 0 : 1);
				bool3 @bool = new bool3((num7 & 4) != 0, (num7 & 2) != 0, (num7 & 1) != 0);
				float3 float2 = math.select(nodeMin, @float, @bool);
				float3 float3 = math.select(@float, nodeMax, @bool);
				this.QueryRec(ref query, num6 + num7, float2, float3, ref maxRadius);
				num = math.min(num, maxRadius);
				bool3 bool2 = position - num < @float;
				bool3 bool3 = position + num > @float;
				int3 @int = math.select(new int3(240, 204, 170), new int3(255, 255, 255), bool2);
				int3 int2 = math.select(new int3(15, 51, 85), new int3(255, 255, 255), bool3);
				int3 int3 = @int & int2;
				int num8 = int3.x & int3.y & int3.z;
				byte b;
				for (num8 &= ~(1 << num7); num8 != 0; num8 &= ~(1 << (int)b))
				{
					b = RVOQuadtreeBurst.ChildLookup[num8];
					bool3 bool4 = new bool3((b & 4) > 0, (b & 2) > 0, (b & 1) > 0);
					float3 float4 = math.select(nodeMin, @float, bool4);
					float3 float5 = math.select(@float, nodeMax, bool4);
					this.QueryRec(ref query, num6 + (int)b, float4, float5, ref maxRadius);
					num = math.min(num, maxRadius);
				}
				return;
			}
			if (this.movementPlane == MovementPlane.XY)
			{
				int num9 = ((position.x < @float.x) ? 0 : 2) | ((position.y < @float.y) ? 0 : 1);
				bool3 bool5 = new bool3((num9 & 2) != 0, (num9 & 1) != 0, false);
				float3 float6 = math.select(nodeMin, @float, bool5);
				float3 float7 = math.select(@float, nodeMax, bool5);
				this.QueryRec(ref query, num6 + num9, float6, float7, ref maxRadius);
				num = math.min(num, maxRadius);
				bool2 bool6 = position.xy - num < @float.xy;
				bool2 bool7 = position.xy + num > @float.xy;
				bool4 bool8 = new bool4(bool6.x & bool6.y, bool6.x & bool7.y, bool7.x & bool6.y, bool7.x & bool7.y);
				int num10 = (bool8.x ? 1 : 0) | (bool8.y ? 2 : 0) | (bool8.z ? 4 : 0) | (bool8.w ? 8 : 0);
				byte b2;
				for (num10 &= ~(1 << num9); num10 != 0; num10 &= ~(1 << (int)b2))
				{
					b2 = RVOQuadtreeBurst.ChildLookup[num10];
					bool3 bool9 = new bool3((b2 & 2) > 0, (b2 & 1) > 0, false);
					float3 float8 = math.select(nodeMin, @float, bool9);
					float3 float9 = math.select(@float, nodeMax, bool9);
					this.QueryRec(ref query, num6 + (int)b2, float8, float9, ref maxRadius);
					num = math.min(num, maxRadius);
				}
				return;
			}
			int num11 = ((position.x < @float.x) ? 0 : 2) | ((position.z < @float.z) ? 0 : 1);
			bool3 bool10 = new bool3((num11 & 2) != 0, false, (num11 & 1) != 0);
			float3 float10 = math.select(nodeMin, @float, bool10);
			float3 float11 = math.select(@float, nodeMax, bool10);
			this.QueryRec(ref query, num6 + num11, float10, float11, ref maxRadius);
			num = math.min(num, maxRadius);
			bool2 bool11 = position.xz - num < @float.xz;
			bool2 bool12 = position.xz + num > @float.xz;
			bool4 bool13 = new bool4(bool11.x & bool11.y, bool11.x & bool12.y, bool12.x & bool11.y, bool12.x & bool12.y);
			int num12 = (bool13.x ? 1 : 0) | (bool13.y ? 2 : 0) | (bool13.z ? 4 : 0) | (bool13.w ? 8 : 0);
			byte b3;
			for (num12 &= ~(1 << num11); num12 != 0; num12 &= ~(1 << (int)b3))
			{
				b3 = RVOQuadtreeBurst.ChildLookup[num12];
				bool3 bool14 = new bool3((b3 & 2) > 0, false, (b3 & 1) > 0);
				float3 float12 = math.select(nodeMin, @float, bool14);
				float3 float13 = math.select(@float, nodeMax, bool14);
				this.QueryRec(ref query, num6 + (int)b3, float12, float13, ref maxRadius);
				num = math.min(num, maxRadius);
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000641DC File Offset: 0x000623DC
		public float QueryArea(float3 position, float radius)
		{
			if (!this.agents.IsCreated || this.agentCountBuffer[0] == 0)
			{
				return 0f;
			}
			return 3.1415927f * this.QueryAreaRec(0, position, radius, this.boundingBoxBuffer[0], this.boundingBoxBuffer[1]);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00064234 File Offset: 0x00062434
		private float QueryAreaRec(int treeNodeIndex, float3 p, float radius, float3 nodeMin, float3 nodeMax)
		{
			float3 @float = (nodeMin + nodeMax) * 0.5f;
			float num = math.length(nodeMax - @float);
			float num2 = math.lengthsq(@float - p);
			float num3 = this.maxRadius[treeNodeIndex];
			float num4 = radius - (num + num3);
			if (num4 > 0f && num2 < num4 * num4)
			{
				return this.nodeAreas[treeNodeIndex];
			}
			if (num2 > (radius + (num + num3)) * (radius + (num + num3)))
			{
				return 0f;
			}
			if ((this.childPointers[treeNodeIndex] & 1073741824) != 0)
			{
				int num5 = this.childPointers[treeNodeIndex] & 32767;
				int num6 = (this.childPointers[treeNodeIndex] >> 15) & 32767;
				float num7 = 0f;
				float num8 = 0f;
				for (int i = num5; i < num6; i++)
				{
					int num9 = this.agents[i];
					num7 += this.agentRadii[num9] * this.agentRadii[num9];
					float num10 = math.lengthsq(p - this.agentPositions[num9]);
					float num11 = this.agentRadii[num9];
					if (num10 < (radius + num11) * (radius + num11))
					{
						float num12 = radius - num11;
						float num13 = ((num10 < num12 * num12) ? 1f : (1f - (math.sqrt(num10) - num12) / (2f * num11)));
						num8 += num11 * num11 * num13;
					}
				}
				return num8;
			}
			float num14 = 0f;
			int num15 = this.childPointers[treeNodeIndex];
			float num16 = radius + num3;
			if (this.movementPlane == MovementPlane.Arbitrary)
			{
				bool3 @bool = p - num16 < @float;
				bool3 bool2 = p + num16 > @float;
				if (@bool[0])
				{
					if (@bool[1])
					{
						if (@bool[2])
						{
							num14 += this.QueryAreaRec(num15, p, radius, new float3(nodeMin.x, nodeMin.y, nodeMin.z), new float3(@float.x, @float.y, @float.z));
						}
						if (bool2[2])
						{
							num14 += this.QueryAreaRec(num15 + 1, p, radius, new float3(nodeMin.x, nodeMin.y, @float.z), new float3(@float.x, @float.y, nodeMax.z));
						}
					}
					if (bool2[1])
					{
						if (@bool[2])
						{
							num14 += this.QueryAreaRec(num15 + 2, p, radius, new float3(nodeMin.x, @float.y, nodeMin.z), new float3(@float.x, nodeMax.y, @float.z));
						}
						if (bool2[2])
						{
							num14 += this.QueryAreaRec(num15 + 3, p, radius, new float3(nodeMin.x, @float.y, @float.z), new float3(@float.x, nodeMax.y, nodeMax.z));
						}
					}
				}
				if (bool2[0])
				{
					if (@bool[1])
					{
						if (@bool[2])
						{
							num14 += this.QueryAreaRec(num15 + 4, p, radius, new float3(@float.x, nodeMin.y, nodeMin.z), new float3(nodeMax.x, @float.y, @float.z));
						}
						if (bool2[2])
						{
							num14 += this.QueryAreaRec(num15 + 5, p, radius, new float3(@float.x, nodeMin.y, @float.z), new float3(nodeMax.x, @float.y, nodeMax.z));
						}
					}
					if (bool2[1])
					{
						if (@bool[2])
						{
							num14 += this.QueryAreaRec(num15 + 6, p, radius, new float3(@float.x, @float.y, nodeMin.z), new float3(nodeMax.x, nodeMax.y, @float.z));
						}
						if (bool2[2])
						{
							num14 += this.QueryAreaRec(num15 + 7, p, radius, new float3(@float.x, @float.y, @float.z), new float3(nodeMax.x, nodeMax.y, nodeMax.z));
						}
					}
				}
			}
			else if (this.movementPlane == MovementPlane.XY)
			{
				bool2 bool3 = (p - num16).xy < @float.xy;
				bool2 bool4 = (p + num16).xy > @float.xy;
				if (bool3[0])
				{
					if (bool3[1])
					{
						num14 += this.QueryAreaRec(num15, p, radius, new float3(nodeMin.x, nodeMin.y, nodeMin.z), new float3(@float.x, @float.y, nodeMax.z));
					}
					if (bool4[1])
					{
						num14 += this.QueryAreaRec(num15 + 1, p, radius, new float3(nodeMin.x, @float.y, nodeMin.z), new float3(@float.x, nodeMax.y, nodeMax.z));
					}
				}
				if (bool4[0])
				{
					if (bool3[1])
					{
						num14 += this.QueryAreaRec(num15 + 2, p, radius, new float3(@float.x, nodeMin.y, nodeMin.z), new float3(nodeMax.x, @float.y, nodeMax.z));
					}
					if (bool4[1])
					{
						num14 += this.QueryAreaRec(num15 + 3, p, radius, new float3(@float.x, @float.y, nodeMin.z), new float3(nodeMax.x, nodeMax.y, nodeMax.z));
					}
				}
			}
			else
			{
				bool2 bool5 = (p - num16).xz < @float.xz;
				bool2 bool6 = (p + num16).xz > @float.xz;
				if (bool5[0])
				{
					if (bool5[1])
					{
						num14 += this.QueryAreaRec(num15, p, radius, new float3(nodeMin.x, nodeMin.y, nodeMin.z), new float3(@float.x, nodeMax.y, @float.z));
					}
					if (bool6[1])
					{
						num14 += this.QueryAreaRec(num15 + 1, p, radius, new float3(nodeMin.x, nodeMin.y, @float.z), new float3(@float.x, nodeMax.y, nodeMax.z));
					}
				}
				if (bool6[0])
				{
					if (bool5[1])
					{
						num14 += this.QueryAreaRec(num15 + 2, p, radius, new float3(@float.x, nodeMin.y, nodeMin.z), new float3(nodeMax.x, nodeMax.y, @float.z));
					}
					if (bool6[1])
					{
						num14 += this.QueryAreaRec(num15 + 3, p, radius, new float3(@float.x, nodeMin.y, @float.z), new float3(nodeMax.x, nodeMax.y, nodeMax.z));
					}
				}
			}
			return num14;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000649E4 File Offset: 0x00062BE4
		public void DebugDraw(CommandBuilder draw)
		{
			if (!this.agentCountBuffer.IsCreated)
			{
				return;
			}
			int num = this.agentCountBuffer[0];
			if (num == 0)
			{
				return;
			}
			this.DebugDraw(0, this.boundingBoxBuffer[0], this.boundingBoxBuffer[1], draw);
			for (int i = 0; i < num; i++)
			{
				draw.Cross(this.agentPositions[this.agents[i]], 0.5f, Palette.Colorbrewer.Set1.Red);
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00064A64 File Offset: 0x00062C64
		private void DebugDraw(int nodeIndex, float3 nodeMin, float3 nodeMax, CommandBuilder draw)
		{
			float3 @float = (nodeMin + nodeMax) * 0.5f;
			draw.WireBox(@float, nodeMax - nodeMin, Palette.Colorbrewer.Set1.Orange);
			if ((this.childPointers[nodeIndex] & 1073741824) != 0)
			{
				int num = this.childPointers[nodeIndex] & 32767;
				int num2 = (this.childPointers[nodeIndex] >> 15) & 32767;
				for (int i = num; i < num2; i++)
				{
					draw.Line(@float, this.agentPositions[this.agents[i]], Color.black);
				}
				return;
			}
			int num3 = this.childPointers[nodeIndex];
			if (this.movementPlane == MovementPlane.Arbitrary)
			{
				this.DebugDraw(num3, new float3(nodeMin.x, nodeMin.y, nodeMin.z), new float3(@float.x, @float.y, @float.z), draw);
				this.DebugDraw(num3 + 1, new float3(nodeMin.x, nodeMin.y, @float.z), new float3(@float.x, @float.y, nodeMax.z), draw);
				this.DebugDraw(num3 + 2, new float3(nodeMin.x, @float.y, nodeMin.z), new float3(@float.x, nodeMax.y, @float.z), draw);
				this.DebugDraw(num3 + 3, new float3(nodeMin.x, @float.y, @float.z), new float3(@float.x, nodeMax.y, nodeMax.z), draw);
				this.DebugDraw(num3 + 4, new float3(@float.x, nodeMin.y, nodeMin.z), new float3(nodeMax.x, @float.y, @float.z), draw);
				this.DebugDraw(num3 + 5, new float3(@float.x, nodeMin.y, @float.z), new float3(nodeMax.x, @float.y, nodeMax.z), draw);
				this.DebugDraw(num3 + 6, new float3(@float.x, @float.y, nodeMin.z), new float3(nodeMax.x, nodeMax.y, @float.z), draw);
				this.DebugDraw(num3 + 7, new float3(@float.x, @float.y, @float.z), new float3(nodeMax.x, nodeMax.y, nodeMax.z), draw);
				return;
			}
			if (this.movementPlane == MovementPlane.XY)
			{
				this.DebugDraw(num3, new float3(nodeMin.x, nodeMin.y, nodeMin.z), new float3(@float.x, @float.y, nodeMax.z), draw);
				this.DebugDraw(num3 + 1, new float3(nodeMin.x, @float.y, nodeMin.z), new float3(@float.x, nodeMax.y, nodeMax.z), draw);
				this.DebugDraw(num3 + 2, new float3(@float.x, nodeMin.y, nodeMin.z), new float3(nodeMax.x, @float.y, nodeMax.z), draw);
				this.DebugDraw(num3 + 3, new float3(@float.x, @float.y, nodeMin.z), new float3(nodeMax.x, nodeMax.y, nodeMax.z), draw);
				return;
			}
			this.DebugDraw(num3, new float3(nodeMin.x, nodeMin.y, nodeMin.z), new float3(@float.x, nodeMax.y, @float.z), draw);
			this.DebugDraw(num3 + 1, new float3(nodeMin.x, nodeMin.y, @float.z), new float3(@float.x, nodeMax.y, nodeMax.z), draw);
			this.DebugDraw(num3 + 2, new float3(@float.x, nodeMin.y, nodeMin.z), new float3(nodeMax.x, nodeMax.y, @float.z), draw);
			this.DebugDraw(num3 + 3, new float3(@float.x, nodeMin.y, @float.z), new float3(nodeMax.x, nodeMax.y, nodeMax.z), draw);
		}

		// Token: 0x04000C24 RID: 3108
		private const int LeafSize = 16;

		// Token: 0x04000C25 RID: 3109
		private const int MaxDepth = 10;

		// Token: 0x04000C26 RID: 3110
		private NativeArray<int> agents;

		// Token: 0x04000C27 RID: 3111
		private NativeArray<int> childPointers;

		// Token: 0x04000C28 RID: 3112
		private NativeArray<float3> boundingBoxBuffer;

		// Token: 0x04000C29 RID: 3113
		private NativeArray<int> agentCountBuffer;

		// Token: 0x04000C2A RID: 3114
		private NativeArray<float3> agentPositions;

		// Token: 0x04000C2B RID: 3115
		private NativeArray<float> agentRadii;

		// Token: 0x04000C2C RID: 3116
		private NativeArray<float> maxSpeeds;

		// Token: 0x04000C2D RID: 3117
		private NativeArray<float> maxRadius;

		// Token: 0x04000C2E RID: 3118
		private NativeArray<float> nodeAreas;

		// Token: 0x04000C2F RID: 3119
		private MovementPlane movementPlane;

		// Token: 0x04000C30 RID: 3120
		private const int LeafNodeBit = 1073741824;

		// Token: 0x04000C31 RID: 3121
		private const int BitPackingShift = 15;

		// Token: 0x04000C32 RID: 3122
		private const int BitPackingMask = 32767;

		// Token: 0x04000C33 RID: 3123
		private const int MaxAgents = 32767;

		// Token: 0x04000C34 RID: 3124
		private static readonly byte[] ChildLookup = new byte[256];

		// Token: 0x020002A4 RID: 676
		[BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast)]
		public struct JobBuild : IJob
		{
			// Token: 0x0600100F RID: 4111 RVA: 0x00064EB4 File Offset: 0x000630B4
			private static int Partition(NativeSlice<int> indices, int startIndex, int endIndex, NativeSlice<float> coordinates, float splitPoint)
			{
				for (int i = startIndex; i < endIndex; i++)
				{
					if (coordinates[indices[i]] > splitPoint)
					{
						endIndex--;
						int num = indices[i];
						indices[i] = indices[endIndex];
						indices[endIndex] = num;
						i--;
					}
				}
				return endIndex;
			}

			// Token: 0x06001010 RID: 4112 RVA: 0x00064F0C File Offset: 0x0006310C
			private void BuildNode(float3 boundsMin, float3 boundsMax, int depth, int agentsStart, int agentsEnd, int nodeOffset, ref int firstFreeChild)
			{
				if (agentsEnd - agentsStart <= 16 || depth >= 10)
				{
					this.outChildPointers[nodeOffset] = agentsStart | (agentsEnd << 15) | 1073741824;
					return;
				}
				if (this.movementPlane == MovementPlane.Arbitrary)
				{
					NativeSlice<float> nativeSlice = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(0);
					NativeSlice<float> nativeSlice2 = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(4);
					NativeSlice<float> nativeSlice3 = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(8);
					float3 @float = (boundsMin + boundsMax) * 0.5f;
					int num = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, agentsEnd, nativeSlice, @float.x);
					int num2 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, num, nativeSlice2, @float.y);
					int num3 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, num, agentsEnd, nativeSlice2, @float.y);
					int num4 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, num2, nativeSlice3, @float.z);
					int num5 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, num2, num, nativeSlice3, @float.z);
					int num6 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, num, num3, nativeSlice3, @float.z);
					int num7 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, num3, agentsEnd, nativeSlice3, @float.z);
					int num8 = firstFreeChild;
					this.outChildPointers[nodeOffset] = num8;
					firstFreeChild += 8;
					float3 float2 = @float;
					this.BuildNode(new float3(boundsMin.x, boundsMin.y, boundsMin.z), new float3(float2.x, float2.y, float2.z), depth + 1, agentsStart, num4, num8, ref firstFreeChild);
					this.BuildNode(new float3(boundsMin.x, boundsMin.y, float2.z), new float3(float2.x, float2.y, boundsMax.z), depth + 1, num4, num2, num8 + 1, ref firstFreeChild);
					this.BuildNode(new float3(boundsMin.x, float2.y, boundsMin.z), new float3(float2.x, boundsMax.y, float2.z), depth + 1, num2, num5, num8 + 2, ref firstFreeChild);
					this.BuildNode(new float3(boundsMin.x, float2.y, float2.z), new float3(float2.x, boundsMax.y, boundsMax.z), depth + 1, num5, num, num8 + 3, ref firstFreeChild);
					this.BuildNode(new float3(float2.x, boundsMin.y, boundsMin.z), new float3(boundsMax.x, float2.y, float2.z), depth + 1, num, num6, num8 + 4, ref firstFreeChild);
					this.BuildNode(new float3(float2.x, boundsMin.y, float2.z), new float3(boundsMax.x, float2.y, boundsMax.z), depth + 1, num6, num3, num8 + 5, ref firstFreeChild);
					this.BuildNode(new float3(float2.x, float2.y, boundsMin.z), new float3(boundsMax.x, boundsMax.y, float2.z), depth + 1, num3, num7, num8 + 6, ref firstFreeChild);
					this.BuildNode(new float3(float2.x, float2.y, float2.z), new float3(boundsMax.x, boundsMax.y, boundsMax.z), depth + 1, num7, agentsEnd, num8 + 7, ref firstFreeChild);
					return;
				}
				if (this.movementPlane == MovementPlane.XY)
				{
					NativeSlice<float> nativeSlice4 = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(0);
					NativeSlice<float> nativeSlice5 = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(4);
					float3 float3 = (boundsMin + boundsMax) * 0.5f;
					int num9 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, agentsEnd, nativeSlice4, float3.x);
					int num10 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, num9, nativeSlice5, float3.y);
					int num11 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, num9, agentsEnd, nativeSlice5, float3.y);
					int num12 = firstFreeChild;
					this.outChildPointers[nodeOffset] = num12;
					firstFreeChild += 4;
					this.BuildNode(new float3(boundsMin.x, boundsMin.y, boundsMin.z), new float3(float3.x, float3.y, boundsMax.z), depth + 1, agentsStart, num10, num12, ref firstFreeChild);
					this.BuildNode(new float3(boundsMin.x, float3.y, boundsMin.z), new float3(float3.x, boundsMax.y, boundsMax.z), depth + 1, num10, num9, num12 + 1, ref firstFreeChild);
					this.BuildNode(new float3(float3.x, boundsMin.y, boundsMin.z), new float3(boundsMax.x, float3.y, boundsMax.z), depth + 1, num9, num11, num12 + 2, ref firstFreeChild);
					this.BuildNode(new float3(float3.x, float3.y, boundsMin.z), new float3(boundsMax.x, boundsMax.y, boundsMax.z), depth + 1, num11, agentsEnd, num12 + 3, ref firstFreeChild);
					return;
				}
				NativeSlice<float> nativeSlice6 = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(0);
				NativeSlice<float> nativeSlice7 = new NativeSlice<float3>(this.agentPositions).SliceWithStride<float>(8);
				float3 float4 = (boundsMin + boundsMax) * 0.5f;
				int num13 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, agentsEnd, nativeSlice6, float4.x);
				int num14 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, agentsStart, num13, nativeSlice7, float4.z);
				int num15 = RVOQuadtreeBurst.JobBuild.Partition(this.agents, num13, agentsEnd, nativeSlice7, float4.z);
				int num16 = firstFreeChild;
				this.outChildPointers[nodeOffset] = num16;
				firstFreeChild += 4;
				this.BuildNode(new float3(boundsMin.x, boundsMin.y, boundsMin.z), new float3(float4.x, boundsMax.y, float4.z), depth + 1, agentsStart, num14, num16, ref firstFreeChild);
				this.BuildNode(new float3(boundsMin.x, boundsMin.y, float4.z), new float3(float4.x, boundsMax.y, boundsMax.z), depth + 1, num14, num13, num16 + 1, ref firstFreeChild);
				this.BuildNode(new float3(float4.x, boundsMin.y, boundsMin.z), new float3(boundsMax.x, boundsMax.y, float4.z), depth + 1, num13, num15, num16 + 2, ref firstFreeChild);
				this.BuildNode(new float3(float4.x, boundsMin.y, float4.z), new float3(boundsMax.x, boundsMax.y, boundsMax.z), depth + 1, num15, agentsEnd, num16 + 3, ref firstFreeChild);
			}

			// Token: 0x06001011 RID: 4113 RVA: 0x00065680 File Offset: 0x00063880
			private void CalculateSpeeds(int nodeCount)
			{
				for (int i = nodeCount - 1; i >= 0; i--)
				{
					if ((this.outChildPointers[i] & 1073741824) != 0)
					{
						int num = this.outChildPointers[i] & 32767;
						int num2 = (this.outChildPointers[i] >> 15) & 32767;
						float num3 = 0f;
						for (int j = num; j < num2; j++)
						{
							num3 = math.max(num3, this.agentSpeeds[this.agents[j]]);
						}
						this.outMaxSpeeds[i] = num3;
						float num4 = 0f;
						for (int k = num; k < num2; k++)
						{
							num4 = math.max(num4, this.agentRadii[this.agents[k]]);
						}
						this.outMaxRadius[i] = num4;
						float num5 = 0f;
						for (int l = num; l < num2; l++)
						{
							num5 += this.agentRadii[this.agents[l]] * this.agentRadii[this.agents[l]];
						}
						this.outArea[i] = num5;
					}
					else
					{
						int num6 = this.outChildPointers[i];
						if (this.movementPlane == MovementPlane.Arbitrary)
						{
							float num7 = 0f;
							float num8 = 0f;
							float num9 = 0f;
							for (int m = 0; m < 8; m++)
							{
								num7 = math.max(num7, this.outMaxSpeeds[num6 + m]);
								num8 = math.max(num8, this.outMaxSpeeds[num6 + m]);
								num9 += this.outArea[num6 + m];
							}
							this.outMaxSpeeds[i] = num7;
							this.outMaxRadius[i] = num8;
							this.outArea[i] = num9;
						}
						else
						{
							this.outMaxSpeeds[i] = math.max(math.max(this.outMaxSpeeds[num6], this.outMaxSpeeds[num6 + 1]), math.max(this.outMaxSpeeds[num6 + 2], this.outMaxSpeeds[num6 + 3]));
							this.outMaxRadius[i] = math.max(math.max(this.outMaxRadius[num6], this.outMaxRadius[num6 + 1]), math.max(this.outMaxRadius[num6 + 2], this.outMaxRadius[num6 + 3]));
							this.outArea[i] = this.outArea[num6] + this.outArea[num6 + 1] + this.outArea[num6 + 2] + this.outArea[num6 + 3];
						}
					}
				}
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x00065974 File Offset: 0x00063B74
			public void Execute()
			{
				float3 @float = float.PositiveInfinity;
				float3 float2 = float.NegativeInfinity;
				int num = 0;
				for (int i = 0; i < this.numAgents; i++)
				{
					if (this.agentVersions[i].Valid)
					{
						this.agents[num++] = i;
						@float = math.min(@float, this.agentPositions[i]);
						float2 = math.max(float2, this.agentPositions[i]);
					}
				}
				this.outAgentCount[0] = num;
				if (num == 0)
				{
					this.outBoundingBox[0] = (this.outBoundingBox[1] = float3.zero);
					return;
				}
				this.outBoundingBox[0] = @float;
				this.outBoundingBox[1] = float2;
				int num2 = 1;
				this.BuildNode(@float, float2, 0, 0, num, 0, ref num2);
				this.CalculateSpeeds(num2);
				NativeArray<float3>.Copy(this.agentPositions, this.outAgentPositions, this.numAgents);
				NativeArray<float>.Copy(this.agentRadii, this.outAgentRadii, this.numAgents);
			}

			// Token: 0x04000C35 RID: 3125
			public NativeArray<int> agents;

			// Token: 0x04000C36 RID: 3126
			[ReadOnly]
			public NativeArray<float3> agentPositions;

			// Token: 0x04000C37 RID: 3127
			[ReadOnly]
			public NativeArray<AgentIndex> agentVersions;

			// Token: 0x04000C38 RID: 3128
			[ReadOnly]
			public NativeArray<float> agentSpeeds;

			// Token: 0x04000C39 RID: 3129
			[ReadOnly]
			public NativeArray<float> agentRadii;

			// Token: 0x04000C3A RID: 3130
			[WriteOnly]
			public NativeArray<float3> outBoundingBox;

			// Token: 0x04000C3B RID: 3131
			[WriteOnly]
			public NativeArray<int> outAgentCount;

			// Token: 0x04000C3C RID: 3132
			public NativeArray<int> outChildPointers;

			// Token: 0x04000C3D RID: 3133
			public NativeArray<float> outMaxSpeeds;

			// Token: 0x04000C3E RID: 3134
			public NativeArray<float> outMaxRadius;

			// Token: 0x04000C3F RID: 3135
			public NativeArray<float> outArea;

			// Token: 0x04000C40 RID: 3136
			[WriteOnly]
			public NativeArray<float3> outAgentPositions;

			// Token: 0x04000C41 RID: 3137
			[WriteOnly]
			public NativeArray<float> outAgentRadii;

			// Token: 0x04000C42 RID: 3138
			public int numAgents;

			// Token: 0x04000C43 RID: 3139
			public MovementPlane movementPlane;
		}

		// Token: 0x020002A5 RID: 677
		public struct QuadtreeQuery
		{
			// Token: 0x04000C44 RID: 3140
			public float3 position;

			// Token: 0x04000C45 RID: 3141
			public float speed;

			// Token: 0x04000C46 RID: 3142
			public float timeHorizon;

			// Token: 0x04000C47 RID: 3143
			public float agentRadius;

			// Token: 0x04000C48 RID: 3144
			public int outputStartIndex;

			// Token: 0x04000C49 RID: 3145
			public int maxCount;

			// Token: 0x04000C4A RID: 3146
			public NativeArray<int> result;

			// Token: 0x04000C4B RID: 3147
			public NativeArray<float> resultDistances;
		}

		// Token: 0x020002A6 RID: 678
		[BurstCompile]
		public struct DebugDrawJob : IJob
		{
			// Token: 0x06001013 RID: 4115 RVA: 0x00065A92 File Offset: 0x00063C92
			public void Execute()
			{
				this.quadtree.DebugDraw(this.draw);
			}

			// Token: 0x04000C4C RID: 3148
			public CommandBuilder draw;

			// Token: 0x04000C4D RID: 3149
			[ReadOnly]
			public RVOQuadtreeBurst quadtree;
		}
	}
}
