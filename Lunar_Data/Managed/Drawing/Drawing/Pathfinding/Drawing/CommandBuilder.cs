using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Pathfinding.Drawing
{
	// Token: 0x02000009 RID: 9
	[BurstCompile]
	public struct CommandBuilder : IDisposable
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002399 File Offset: 0x00000599
		internal unsafe CommandBuilder(UnsafeAppendBuffer* buffer, GCHandle gizmos, int threadIndex, DrawingData.BuilderData.BitPackedMeta uniqueID)
		{
			this.buffer = buffer;
			this.gizmos = gizmos;
			this.threadIndex = threadIndex;
			this.uniqueID = uniqueID;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023B8 File Offset: 0x000005B8
		internal CommandBuilder(DrawingData gizmos, DrawingData.Hasher hasher, RedrawScope frameRedrawScope, RedrawScope customRedrawScope, bool isGizmos, bool isBuiltInCommandBuilder, int sceneModeVersion)
		{
			this.gizmos = GCHandle.Alloc(gizmos, GCHandleType.Normal);
			this.threadIndex = 0;
			this.uniqueID = gizmos.data.Reserve(isBuiltInCommandBuilder);
			gizmos.data.Get(this.uniqueID).Init(hasher, frameRedrawScope, customRedrawScope, isGizmos, gizmos.GetNextDrawOrderIndex(), sceneModeVersion);
			this.buffer = gizmos.data.Get(this.uniqueID).bufferPtr;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002439 File Offset: 0x00000639
		internal unsafe int BufferSize
		{
			get
			{
				return this.buffer->Length;
			}
			set
			{
				this.buffer->Length = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002447 File Offset: 0x00000647
		public CommandBuilder2D xy
		{
			get
			{
				return new CommandBuilder2D(this, true);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002455 File Offset: 0x00000655
		public CommandBuilder2D xz
		{
			get
			{
				return new CommandBuilder2D(this, false);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000024D8 File Offset: 0x000006D8
		public Camera[] cameraTargets
		{
			get
			{
				if (this.gizmos.IsAllocated && this.gizmos.Target != null)
				{
					DrawingData drawingData = this.gizmos.Target as DrawingData;
					if (drawingData.data.StillExists(this.uniqueID))
					{
						return drawingData.data.Get(this.uniqueID).meta.cameraTargets;
					}
				}
				throw new Exception("Cannot get cameraTargets because the command builder has already been disposed or does not exist.");
			}
			set
			{
				if (this.uniqueID.isBuiltInCommandBuilder)
				{
					throw new Exception("You cannot set the camera targets for a built-in command builder. Create a custom command builder instead.");
				}
				if (this.gizmos.IsAllocated && this.gizmos.Target != null)
				{
					DrawingData drawingData = this.gizmos.Target as DrawingData;
					if (!drawingData.data.StillExists(this.uniqueID))
					{
						throw new Exception("Cannot set cameraTargets because the command builder has already been disposed or does not exist.");
					}
					drawingData.data.Get(this.uniqueID).meta.cameraTargets = value;
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002560 File Offset: 0x00000760
		public void Dispose()
		{
			if (this.uniqueID.isBuiltInCommandBuilder)
			{
				throw new Exception("You cannot dispose a built-in command builder");
			}
			this.DisposeInternal();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002580 File Offset: 0x00000780
		public void DisposeAfter(JobHandle dependency, AllowedDelay allowedDelay = AllowedDelay.EndOfFrame)
		{
			if (!this.gizmos.IsAllocated)
			{
				throw new Exception("You cannot dispose an invalid command builder. Are you trying to dispose it twice?");
			}
			try
			{
				if (this.gizmos.IsAllocated && this.gizmos.Target != null)
				{
					DrawingData drawingData = this.gizmos.Target as DrawingData;
					if (!drawingData.data.StillExists(this.uniqueID))
					{
						throw new Exception("Cannot dispose the command builder because the drawing manager has been destroyed");
					}
					drawingData.data.Get(this.uniqueID).SubmitWithDependency(this.gizmos, dependency, allowedDelay);
				}
			}
			finally
			{
				this = default(CommandBuilder);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002628 File Offset: 0x00000828
		internal void DisposeInternal()
		{
			if (!this.gizmos.IsAllocated)
			{
				throw new Exception("You cannot dispose an invalid command builder. Are you trying to dispose it twice?");
			}
			try
			{
				if (this.gizmos.IsAllocated && this.gizmos.Target != null)
				{
					DrawingData drawingData = this.gizmos.Target as DrawingData;
					if (!drawingData.data.StillExists(this.uniqueID))
					{
						throw new Exception("Cannot dispose the command builder because the drawing manager has been destroyed");
					}
					drawingData.data.Get(this.uniqueID).Submit(this.gizmos.Target as DrawingData);
				}
			}
			finally
			{
				this.gizmos.Free();
				this = default(CommandBuilder);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026E0 File Offset: 0x000008E0
		public void DiscardAndDispose()
		{
			if (this.uniqueID.isBuiltInCommandBuilder)
			{
				throw new Exception("You cannot dispose a built-in command builder");
			}
			this.DiscardAndDisposeInternal();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002700 File Offset: 0x00000900
		internal void DiscardAndDisposeInternal()
		{
			try
			{
				if (this.gizmos.IsAllocated && this.gizmos.Target != null)
				{
					DrawingData drawingData = this.gizmos.Target as DrawingData;
					if (!drawingData.data.StillExists(this.uniqueID))
					{
						throw new Exception("Cannot dispose the command builder because the drawing manager has been destroyed");
					}
					drawingData.data.Release(this.uniqueID);
				}
			}
			finally
			{
				if (this.gizmos.IsAllocated)
				{
					this.gizmos.Free();
				}
				this = default(CommandBuilder);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002798 File Offset: 0x00000998
		public void Preallocate(int size)
		{
			this.Reserve(size);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027A4 File Offset: 0x000009A4
		[MethodImpl(256)]
		private unsafe void Reserve(int additionalSpace)
		{
			if (Hint.Unlikely(this.threadIndex >= 0))
			{
				this.buffer += this.threadIndex;
				this.threadIndex = -1;
			}
			int num = this.buffer->Length + additionalSpace;
			if (num > this.buffer->Capacity)
			{
				this.buffer->SetCapacity(math.max(num, this.buffer->Length * 2));
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002820 File Offset: 0x00000A20
		[BurstDiscard]
		private void AssertBufferExists()
		{
			if (!this.gizmos.IsAllocated || this.gizmos.Target == null || !(this.gizmos.Target as DrawingData).data.StillExists(this.uniqueID))
			{
				this = default(CommandBuilder);
				throw new Exception("This command builder no longer exists. Are you trying to draw to a command builder which has already been disposed?");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000287B File Offset: 0x00000A7B
		[BurstDiscard]
		private static void AssertNotRendering()
		{
			if (!GizmoContext.drawingGizmos && !JobsUtility.IsExecutingJob && (Time.renderedFrameCount & 127) == 0 && StackTraceUtility.ExtractStackTrace().Contains("OnDrawGizmos"))
			{
				throw new Exception("You are trying to use Draw.* functions from within Unity's OnDrawGizmos function. Use this package's gizmo callbacks instead (see the documentation).");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028B1 File Offset: 0x00000AB1
		[MethodImpl(256)]
		internal void Reserve<A>() where A : struct
		{
			this.Reserve(UnsafeUtility.SizeOf<CommandBuilder.Command>() + UnsafeUtility.SizeOf<A>());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028C4 File Offset: 0x00000AC4
		[MethodImpl(256)]
		internal void Reserve<A, B>() where A : struct where B : struct
		{
			this.Reserve(UnsafeUtility.SizeOf<CommandBuilder.Command>() * 2 + UnsafeUtility.SizeOf<A>() + UnsafeUtility.SizeOf<B>());
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028DF File Offset: 0x00000ADF
		[MethodImpl(256)]
		internal void Reserve<A, B, C>() where A : struct where B : struct where C : struct
		{
			this.Reserve(UnsafeUtility.SizeOf<CommandBuilder.Command>() * 3 + UnsafeUtility.SizeOf<A>() + UnsafeUtility.SizeOf<B>() + UnsafeUtility.SizeOf<C>());
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002900 File Offset: 0x00000B00
		[MethodImpl(256)]
		internal static uint ConvertColor(Color color)
		{
			if (X86.Sse2.IsSse2Supported)
			{
				int4 @int = (int4)(255f * new float4(color.r, color.g, color.b, color.a) + 0.5f);
				v128 v = new v128(@int.x, @int.y, @int.z, @int.w);
				v128 v2 = X86.Sse2.packs_epi32(v, v);
				return X86.Sse2.packus_epi16(v2, v2).UInt0;
			}
			uint num = (uint)Mathf.Clamp((int)(color.r * 255f + 0.5f), 0, 255);
			uint num2 = (uint)Mathf.Clamp((int)(color.g * 255f + 0.5f), 0, 255);
			uint num3 = (uint)Mathf.Clamp((int)(color.b * 255f + 0.5f), 0, 255);
			return (uint)((Mathf.Clamp((int)(color.a * 255f + 0.5f), 0, 255) << 24) | (int)((int)num3 << 16) | (int)((int)num2 << 8) | (int)num);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A04 File Offset: 0x00000C04
		internal unsafe void Add<T>(T value) where T : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeAppendBuffer* ptr = this.buffer;
			int length = ptr->Length;
			Hint.Assume(ptr->Ptr != null);
			Hint.Assume(ptr->Ptr + length != null);
			UnsafeUtility.CopyStructureToPtr<T>(ref value, (void*)(ptr->Ptr + length));
			ptr->Length = length + num;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A64 File Offset: 0x00000C64
		[BurstDiscard]
		public CommandBuilder.ScopeMatrix WithMatrix(Matrix4x4 matrix)
		{
			this.PushMatrix(matrix);
			return new CommandBuilder.ScopeMatrix
			{
				builder = this
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A90 File Offset: 0x00000C90
		[BurstDiscard]
		public CommandBuilder.ScopeMatrix WithMatrix(float3x3 matrix)
		{
			this.PushMatrix(new float4x4(matrix, float3.zero));
			return new CommandBuilder.ScopeMatrix
			{
				builder = this
			};
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AC4 File Offset: 0x00000CC4
		[BurstDiscard]
		public CommandBuilder.ScopeColor WithColor(Color color)
		{
			this.PushColor(color);
			return new CommandBuilder.ScopeColor
			{
				builder = this
			};
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AF0 File Offset: 0x00000CF0
		[BurstDiscard]
		public CommandBuilder.ScopePersist WithDuration(float duration)
		{
			this.PushDuration(duration);
			return new CommandBuilder.ScopePersist
			{
				builder = this
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B1C File Offset: 0x00000D1C
		[BurstDiscard]
		public CommandBuilder.ScopeLineWidth WithLineWidth(float pixels, bool automaticJoins = true)
		{
			this.PushLineWidth(pixels, automaticJoins);
			return new CommandBuilder.ScopeLineWidth
			{
				builder = this
			};
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B47 File Offset: 0x00000D47
		[BurstDiscard]
		public CommandBuilder.ScopeMatrix InLocalSpace(Transform transform)
		{
			return this.WithMatrix(transform.localToWorldMatrix);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B58 File Offset: 0x00000D58
		[BurstDiscard]
		public CommandBuilder.ScopeMatrix InScreenSpace(Camera camera)
		{
			return this.WithMatrix(camera.cameraToWorldMatrix * camera.nonJitteredProjectionMatrix.inverse * Matrix4x4.TRS(new Vector3(-1f, -1f, 0f), Quaternion.identity, new Vector3(2f / (float)camera.pixelWidth, 2f / (float)camera.pixelHeight, 1f)));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BCB File Offset: 0x00000DCB
		public void PushMatrix(Matrix4x4 matrix)
		{
			this.Reserve<float4x4>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushMatrix);
			this.Add<Matrix4x4>(matrix);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BE1 File Offset: 0x00000DE1
		public void PushMatrix(float4x4 matrix)
		{
			this.Reserve<float4x4>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushMatrix);
			this.Add<float4x4>(matrix);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BF7 File Offset: 0x00000DF7
		public void PushSetMatrix(Matrix4x4 matrix)
		{
			this.Reserve<float4x4>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushSetMatrix);
			this.Add<float4x4>(matrix);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C12 File Offset: 0x00000E12
		public void PushSetMatrix(float4x4 matrix)
		{
			this.Reserve<float4x4>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushSetMatrix);
			this.Add<float4x4>(matrix);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C28 File Offset: 0x00000E28
		public void PopMatrix()
		{
			this.Reserve(4);
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PopMatrix);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C38 File Offset: 0x00000E38
		public void PushColor(Color color)
		{
			this.Reserve<Color32>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColor);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C53 File Offset: 0x00000E53
		public void PopColor()
		{
			this.Reserve(4);
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PopColor);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C64 File Offset: 0x00000E64
		public unsafe void PushDuration(float duration)
		{
			this.Reserve<CommandBuilder.PersistData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushPersist);
			this.Add<CommandBuilder.PersistData>(new CommandBuilder.PersistData
			{
				endTime = *SharedDrawingData.BurstTime.Data + duration
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CA2 File Offset: 0x00000EA2
		public void PopDuration()
		{
			this.Reserve(4);
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PopPersist);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002CB3 File Offset: 0x00000EB3
		[Obsolete("Renamed to PushDuration for consistency")]
		public void PushPersist(float duration)
		{
			this.PushDuration(duration);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002CBC File Offset: 0x00000EBC
		[Obsolete("Renamed to PopDuration for consistency")]
		public void PopPersist()
		{
			this.PopDuration();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public void PushLineWidth(float pixels, bool automaticJoins = true)
		{
			if (pixels < 0f)
			{
				throw new ArgumentOutOfRangeException("pixels", "Line width must be positive");
			}
			this.Reserve<CommandBuilder.LineWidthData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushLineWidth);
			this.Add<CommandBuilder.LineWidthData>(new CommandBuilder.LineWidthData
			{
				pixels = pixels,
				automaticJoins = automaticJoins
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D16 File Offset: 0x00000F16
		public void PopLineWidth()
		{
			this.Reserve(4);
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PopLineWidth);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D28 File Offset: 0x00000F28
		public void Line(float3 a, float3 b)
		{
			this.Reserve<CommandBuilder.LineData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Line);
			this.Add<CommandBuilder.LineData>(new CommandBuilder.LineData
			{
				a = a,
				b = b
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D64 File Offset: 0x00000F64
		public unsafe void Line(Vector3 a, Vector3 b)
		{
			this.Reserve<CommandBuilder.LineData>();
			int bufferSize = this.BufferSize;
			int num = bufferSize + 4 + 24;
			byte* ptr = this.buffer->Ptr + bufferSize;
			*(int*)ptr = 5;
			CommandBuilder.LineDataV3* ptr2 = (CommandBuilder.LineDataV3*)(ptr + 4);
			ptr2->a = a;
			ptr2->b = b;
			this.buffer->Length = num;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public unsafe void Line(Vector3 a, Vector3 b, Color color)
		{
			this.Reserve<Color32, CommandBuilder.LineData>();
			int bufferSize = this.BufferSize;
			int num = bufferSize + 4 + 24 + 4;
			byte* ptr = this.buffer->Ptr + bufferSize;
			*(int*)ptr = 261;
			*(int*)(ptr + 4) = (int)CommandBuilder.ConvertColor(color);
			CommandBuilder.LineDataV3* ptr2 = (CommandBuilder.LineDataV3*)(ptr + 8);
			ptr2->a = a;
			ptr2->b = b;
			this.buffer->Length = num;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E12 File Offset: 0x00001012
		public void Ray(float3 origin, float3 direction)
		{
			this.Line(origin, origin + direction);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E22 File Offset: 0x00001022
		public void Ray(Ray ray, float length)
		{
			this.Line(ray.origin, ray.origin + ray.direction * length);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E4C File Offset: 0x0000104C
		public void Arc(float3 center, float3 start, float3 end)
		{
			float3 @float = start - center;
			float3 float2 = end - center;
			float3 float3 = math.cross(float2, @float);
			if (math.any(float3 != 0f) && math.all(math.isfinite(float3)))
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(center, Quaternion.LookRotation(@float, float3), Vector3.one);
				float num = Vector3.SignedAngle(@float, float2, float3) * 0.017453292f;
				this.PushMatrix(matrix4x);
				this.CircleXZInternal(float3.zero, math.length(@float), 1.5707964f, 1.5707964f - num);
				this.PopMatrix();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002EFC File Offset: 0x000010FC
		[Obsolete("Use Draw.xz.Circle instead")]
		public void CircleXZ(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.CircleXZInternal(center, radius, startAngle, endAngle);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F0C File Offset: 0x0000110C
		internal void CircleXZInternal(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.Reserve<CommandBuilder.CircleXZData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.CircleXZ);
			this.Add<CommandBuilder.CircleXZData>(new CommandBuilder.CircleXZData
			{
				center = center,
				radius = radius,
				startAngle = startAngle,
				endAngle = endAngle
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F58 File Offset: 0x00001158
		internal void CircleXZInternal(float3 center, float radius, float startAngle, float endAngle, Color color)
		{
			this.Reserve<Color32, CommandBuilder.CircleXZData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PopColor | CommandBuilder.Command.PushMatrix | CommandBuilder.Command.PopMatrix);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.CircleXZData>(new CommandBuilder.CircleXZData
			{
				center = center,
				radius = radius,
				startAngle = startAngle,
				endAngle = endAngle
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FB3 File Offset: 0x000011B3
		[Obsolete("Use Draw.xy.Circle instead")]
		public void CircleXY(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.PushMatrix(CommandBuilder.XZtoXYPlaneMatrix);
			this.CircleXZ(new float3(center.x, -center.z, center.y), radius, startAngle, endAngle);
			this.PopMatrix();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void Circle(float3 center, float3 normal, float radius)
		{
			this.Reserve<CommandBuilder.CircleData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Circle);
			this.Add<CommandBuilder.CircleData>(new CommandBuilder.CircleData
			{
				center = center,
				normal = normal,
				radius = radius
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000302C File Offset: 0x0000122C
		public void SolidArc(float3 center, float3 start, float3 end)
		{
			float3 @float = start - center;
			float3 float2 = end - center;
			float3 float3 = math.cross(float2, @float);
			if (math.any(float3))
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(center, Quaternion.LookRotation(@float, float3), Vector3.one);
				float num = Vector3.SignedAngle(@float, float2, float3) * 0.017453292f;
				this.PushMatrix(matrix4x);
				this.SolidCircleXZInternal(float3.zero, math.length(@float), 1.5707964f, 1.5707964f - num);
				this.PopMatrix();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000030C5 File Offset: 0x000012C5
		[Obsolete("Use Draw.xz.SolidCircle instead")]
		public void SolidCircleXZ(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.SolidCircleXZInternal(center, radius, startAngle, endAngle);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030D4 File Offset: 0x000012D4
		internal void SolidCircleXZInternal(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.Reserve<CommandBuilder.CircleXZData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.DiscXZ);
			this.Add<CommandBuilder.CircleXZData>(new CommandBuilder.CircleXZData
			{
				center = center,
				radius = radius,
				startAngle = startAngle,
				endAngle = endAngle
			});
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003120 File Offset: 0x00001320
		internal void SolidCircleXZInternal(float3 center, float radius, float startAngle, float endAngle, Color color)
		{
			this.Reserve<Color32, CommandBuilder.CircleXZData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PopColor | CommandBuilder.Command.Disc);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.CircleXZData>(new CommandBuilder.CircleXZData
			{
				center = center,
				radius = radius,
				startAngle = startAngle,
				endAngle = endAngle
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000317B File Offset: 0x0000137B
		[Obsolete("Use Draw.xy.SolidCircle instead")]
		public void SolidCircleXY(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.PushMatrix(CommandBuilder.XZtoXYPlaneMatrix);
			this.SolidCircleXZInternal(new float3(center.x, -center.z, center.y), radius, startAngle, endAngle);
			this.PopMatrix();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000031B0 File Offset: 0x000013B0
		public void SolidCircle(float3 center, float3 normal, float radius)
		{
			this.Reserve<CommandBuilder.CircleData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Disc);
			this.Add<CommandBuilder.CircleData>(new CommandBuilder.CircleData
			{
				center = center,
				normal = normal,
				radius = radius
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000031F4 File Offset: 0x000013F4
		public void SphereOutline(float3 center, float radius)
		{
			this.Reserve<CommandBuilder.SphereData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.SphereOutline);
			this.Add<CommandBuilder.SphereData>(new CommandBuilder.SphereData
			{
				center = center,
				radius = radius
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000322E File Offset: 0x0000142E
		public void WireCylinder(float3 bottom, float3 top, float radius)
		{
			this.WireCylinder(bottom, top - bottom, math.length(top - bottom), radius);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000324C File Offset: 0x0000144C
		public void WireCylinder(float3 position, float3 up, float height, float radius)
		{
			up = math.normalizesafe(up, default(float3));
			if (math.all(up == 0f) || math.any(math.isnan(up)) || math.isnan(height) || math.isnan(radius))
			{
				return;
			}
			float3 @float;
			float3 float2;
			CommandBuilder.OrthonormalBasis(up, out @float, out float2);
			this.PushMatrix(new float4x4(new float4(@float * radius, 0f), new float4(up * height, 0f), new float4(float2 * radius, 0f), new float4(position, 1f)));
			this.CircleXZInternal(float3.zero, 1f, 0f, 6.2831855f);
			if (height > 0f)
			{
				this.CircleXZInternal(new float3(0f, 1f, 0f), 1f, 0f, 6.2831855f);
				this.Line(new float3(1f, 0f, 0f), new float3(1f, 1f, 0f));
				this.Line(new float3(-1f, 0f, 0f), new float3(-1f, 1f, 0f));
				this.Line(new float3(0f, 0f, 1f), new float3(0f, 1f, 1f));
				this.Line(new float3(0f, 0f, -1f), new float3(0f, 1f, -1f));
			}
			this.PopMatrix();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000033FC File Offset: 0x000015FC
		private static void OrthonormalBasis(float3 normal, out float3 basis1, out float3 basis2)
		{
			basis1 = math.cross(normal, new float3(1f, 1f, 1f));
			if (math.all(basis1 == 0f))
			{
				basis1 = math.cross(normal, new float3(-1f, 1f, 1f));
			}
			basis1 = math.normalizesafe(basis1, default(float3));
			basis2 = math.cross(normal, basis1);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000348C File Offset: 0x0000168C
		public void WireCapsule(float3 start, float3 end, float radius)
		{
			float3 @float = end - start;
			float num = math.length(@float);
			if ((double)num < 0.0001)
			{
				this.WireSphere(start, radius);
				return;
			}
			float3 float2 = @float / num;
			this.WireCapsule(start - float2 * radius, float2, num + 2f * radius, radius);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000034E4 File Offset: 0x000016E4
		public void WireCapsule(float3 position, float3 direction, float length, float radius)
		{
			direction = math.normalizesafe(direction, default(float3));
			if (math.all(direction == 0f) || math.any(math.isnan(direction)) || math.isnan(length) || math.isnan(radius))
			{
				return;
			}
			if (radius <= 0f)
			{
				this.Line(position, position + direction * length);
				return;
			}
			length = math.max(length, radius * 2f);
			float3 @float;
			float3 float2;
			CommandBuilder.OrthonormalBasis(direction, out @float, out float2);
			this.PushMatrix(new float4x4(new float4(@float, 0f), new float4(direction, 0f), new float4(float2, 0f), new float4(position, 1f)));
			this.CircleXZInternal(new float3(0f, radius, 0f), radius, 0f, 6.2831855f);
			this.PushMatrix(CommandBuilder.XZtoXYPlaneMatrix);
			this.CircleXZInternal(new float3(0f, 0f, radius), radius, 3.1415927f, 6.2831855f);
			this.PopMatrix();
			this.PushMatrix(CommandBuilder.XZtoYZPlaneMatrix);
			this.CircleXZInternal(new float3(radius, 0f, 0f), radius, 1.5707964f, 4.712389f);
			this.PopMatrix();
			if (length > 0f)
			{
				float num = length - radius;
				this.CircleXZInternal(new float3(0f, num, 0f), radius, 0f, 6.2831855f);
				this.PushMatrix(CommandBuilder.XZtoXYPlaneMatrix);
				this.CircleXZInternal(new float3(0f, 0f, num), radius, 0f, 3.1415927f);
				this.PopMatrix();
				this.PushMatrix(CommandBuilder.XZtoYZPlaneMatrix);
				this.CircleXZInternal(new float3(num, 0f, 0f), radius, -1.5707964f, 1.5707964f);
				this.PopMatrix();
				this.Line(new float3(radius, radius, 0f), new float3(radius, num, 0f));
				this.Line(new float3(-radius, radius, 0f), new float3(-radius, num, 0f));
				this.Line(new float3(0f, radius, radius), new float3(0f, num, radius));
				this.Line(new float3(0f, radius, -radius), new float3(0f, num, -radius));
			}
			this.PopMatrix();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003758 File Offset: 0x00001958
		public void WireSphere(float3 position, float radius)
		{
			this.SphereOutline(position, radius);
			this.Circle(position, new float3(1f, 0f, 0f), radius);
			this.Circle(position, new float3(0f, 1f, 0f), radius);
			this.Circle(position, new float3(0f, 0f, 1f), radius);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000037C4 File Offset: 0x000019C4
		[BurstDiscard]
		public void Polyline(List<Vector3> points, bool cycle = false)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Count > 1)
			{
				this.Line(points[points.Count - 1], points[0]);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003824 File Offset: 0x00001A24
		public void Polyline<T>(T points, bool cycle = false) where T : IReadOnlyList<float3>
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Count > 1)
			{
				this.Line(points[points.Count - 1], points[0]);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000038B4 File Offset: 0x00001AB4
		[BurstDiscard]
		public void Polyline(Vector3[] points, bool cycle = false)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003908 File Offset: 0x00001B08
		[BurstDiscard]
		public void Polyline(float3[] points, bool cycle = false)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000395C File Offset: 0x00001B5C
		public void Polyline(NativeArray<float3> points, bool cycle = false)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000039C0 File Offset: 0x00001BC0
		public void DashedLine(float3 a, float3 b, float dash, float gap)
		{
			CommandBuilder.PolylineWithSymbol polylineWithSymbol = new CommandBuilder.PolylineWithSymbol(CommandBuilder.SymbolDecoration.None, gap, 0f, dash + gap, false);
			polylineWithSymbol.MoveTo(ref this, a);
			polylineWithSymbol.MoveTo(ref this, b);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000039F4 File Offset: 0x00001BF4
		public void DashedPolyline(List<Vector3> points, float dash, float gap)
		{
			CommandBuilder.PolylineWithSymbol polylineWithSymbol = new CommandBuilder.PolylineWithSymbol(CommandBuilder.SymbolDecoration.None, gap, 0f, dash + gap, false);
			for (int i = 0; i < points.Count; i++)
			{
				polylineWithSymbol.MoveTo(ref this, points[i]);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003A38 File Offset: 0x00001C38
		public void WireBox(float3 center, float3 size)
		{
			this.Reserve<CommandBuilder.BoxData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.WireBox);
			this.Add<CommandBuilder.BoxData>(new CommandBuilder.BoxData
			{
				center = center,
				size = size
			});
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A72 File Offset: 0x00001C72
		public void WireBox(float3 center, quaternion rotation, float3 size)
		{
			this.PushMatrix(float4x4.TRS(center, rotation, size));
			this.WireBox(float3.zero, new float3(1f, 1f, 1f));
			this.PopMatrix();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public void WireBox(Bounds bounds)
		{
			this.WireBox(bounds.center, bounds.size);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public void WireMesh(Mesh mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException();
			}
			Mesh.MeshDataArray meshDataArray = Mesh.AcquireReadOnlyMeshData(mesh);
			Mesh.MeshData meshData = meshDataArray[0];
			CommandBuilder.JobWireMesh.JobWireMeshFunctionPointer(ref meshData, ref this);
			meshDataArray.Dispose();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003B08 File Offset: 0x00001D08
		public unsafe void WireMesh(NativeArray<float3> vertices, NativeArray<int> triangles)
		{
			CommandBuilder.JobWireMesh.WireMesh((float3*)vertices.GetUnsafeReadOnlyPtr<float3>(), (int*)triangles.GetUnsafeReadOnlyPtr<int>(), vertices.Length, triangles.Length, ref this);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B2A File Offset: 0x00001D2A
		public void SolidMesh(Mesh mesh)
		{
			this.SolidMeshInternal(mesh, false);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B34 File Offset: 0x00001D34
		private void SolidMeshInternal(Mesh mesh, bool temporary, Color color)
		{
			this.PushColor(color);
			this.SolidMeshInternal(mesh, temporary);
			this.PopColor();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003B4C File Offset: 0x00001D4C
		private void SolidMeshInternal(Mesh mesh, bool temporary)
		{
			(this.gizmos.Target as DrawingData).data.Get(this.uniqueID).meshes.Add(new DrawingData.SubmittedMesh
			{
				mesh = mesh,
				temporary = temporary
			});
			this.Reserve(4);
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.CaptureState);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003BAC File Offset: 0x00001DAC
		[BurstDiscard]
		public void SolidMesh(List<Vector3> vertices, List<int> triangles, List<Color> colors)
		{
			if (vertices.Count != colors.Count)
			{
				throw new ArgumentException("Number of colors must be the same as the number of vertices");
			}
			Mesh mesh = (this.gizmos.Target as DrawingData).GetMesh(vertices.Count);
			mesh.Clear();
			mesh.SetVertices(vertices);
			mesh.SetTriangles(triangles, 0);
			mesh.SetColors(colors);
			mesh.UploadMeshData(false);
			this.SolidMeshInternal(mesh, true);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003C1C File Offset: 0x00001E1C
		[BurstDiscard]
		public void SolidMesh(Vector3[] vertices, int[] triangles, Color[] colors, int vertexCount, int indexCount)
		{
			if (vertices.Length != colors.Length)
			{
				throw new ArgumentException("Number of colors must be the same as the number of vertices");
			}
			Mesh mesh = (this.gizmos.Target as DrawingData).GetMesh(vertices.Length);
			mesh.Clear();
			mesh.SetVertices(vertices, 0, vertexCount);
			mesh.SetTriangles(triangles, 0, indexCount, 0, true, 0);
			mesh.SetColors(colors, 0, vertexCount);
			mesh.UploadMeshData(false);
			this.SolidMeshInternal(mesh, true);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003C8C File Offset: 0x00001E8C
		public void Cross(float3 position, float size = 1f)
		{
			size *= 0.5f;
			this.Line(position - new float3(size, 0f, 0f), position + new float3(size, 0f, 0f));
			this.Line(position - new float3(0f, size, 0f), position + new float3(0f, size, 0f));
			this.Line(position - new float3(0f, 0f, size), position + new float3(0f, 0f, size));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003D38 File Offset: 0x00001F38
		[Obsolete("Use Draw.xz.Cross instead")]
		public void CrossXZ(float3 position, float size = 1f)
		{
			size *= 0.5f;
			this.Line(position - new float3(size, 0f, 0f), position + new float3(size, 0f, 0f));
			this.Line(position - new float3(0f, 0f, size), position + new float3(0f, 0f, size));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[Obsolete("Use Draw.xy.Cross instead")]
		public void CrossXY(float3 position, float size = 1f)
		{
			size *= 0.5f;
			this.Line(position - new float3(size, 0f, 0f), position + new float3(size, 0f, 0f));
			this.Line(position - new float3(0f, size, 0f), position + new float3(0f, size, 0f));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003E30 File Offset: 0x00002030
		public static float3 EvaluateCubicBezier(float3 p0, float3 p1, float3 p2, float3 p3, float t)
		{
			t = math.clamp(t, 0f, 1f);
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003EA8 File Offset: 0x000020A8
		public void Bezier(float3 p0, float3 p1, float3 p2, float3 p3)
		{
			float3 @float = p0;
			for (int i = 1; i <= 20; i++)
			{
				float num = (float)i / 20f;
				float3 float2 = CommandBuilder.EvaluateCubicBezier(p0, p1, p2, p3, num);
				this.Line(@float, float2);
				@float = float2;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003EE4 File Offset: 0x000020E4
		public void CatmullRom(List<Vector3> points)
		{
			if (points.Count < 2)
			{
				return;
			}
			if (points.Count == 2)
			{
				this.Line(points[0], points[1]);
				return;
			}
			int count = points.Count;
			this.CatmullRom(points[0], points[0], points[1], points[2]);
			int num = 0;
			while (num + 3 < count)
			{
				this.CatmullRom(points[num], points[num + 1], points[num + 2], points[num + 3]);
				num++;
			}
			this.CatmullRom(points[count - 3], points[count - 2], points[count - 1], points[count - 1]);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003FE0 File Offset: 0x000021E0
		public void CatmullRom(float3 p0, float3 p1, float3 p2, float3 p3)
		{
			float3 @float = (-p0 + 6f * p1 + 1f * p2) * 0.16666667f;
			float3 float2 = (p1 + 6f * p2 - p3) * 0.16666667f;
			this.Bezier(p1, @float, float2, p2);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000404F File Offset: 0x0000224F
		public void Arrow(float3 from, float3 to)
		{
			this.ArrowRelativeSizeHead(from, to, CommandBuilder.DEFAULT_UP, 0.2f);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004064 File Offset: 0x00002264
		public void Arrow(float3 from, float3 to, float3 up, float headSize)
		{
			float num = math.lengthsq(to - from);
			if (num > 1E-06f)
			{
				this.ArrowRelativeSizeHead(from, to, up, headSize * math.rsqrt(num));
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004098 File Offset: 0x00002298
		public void ArrowRelativeSizeHead(float3 from, float3 to, float3 up, float headFraction)
		{
			this.Line(from, to);
			float3 @float = to - from;
			float3 float2 = math.cross(@float, up);
			if (math.all(float2 == 0f))
			{
				float2 = math.cross(new float3(1f, 0f, 0f), @float);
			}
			if (math.all(float2 == 0f))
			{
				float2 = math.cross(new float3(0f, 1f, 0f), @float);
			}
			float2 = math.normalizesafe(float2, default(float3)) * math.length(@float);
			this.Line(to, to - (@float + float2) * headFraction);
			this.Line(to, to - (@float - float2) * headFraction);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004168 File Offset: 0x00002368
		public void Arrowhead(float3 center, float3 direction, float radius)
		{
			this.Arrowhead(center, direction, CommandBuilder.DEFAULT_UP, radius);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004178 File Offset: 0x00002378
		public void Arrowhead(float3 center, float3 direction, float3 up, float radius)
		{
			if (math.all(direction == 0f))
			{
				return;
			}
			direction = math.normalizesafe(direction, default(float3));
			float3 @float = math.cross(direction, up);
			float3 float2 = center - radius * 0.5f * 0.5f * direction;
			float3 float3 = float2 + radius * direction;
			float3 float4 = float2 - radius * 0.5f * direction + radius * 0.866025f * @float;
			float3 float5 = float2 - radius * 0.5f * direction - radius * 0.866025f * @float;
			this.Line(float3, float4);
			this.Line(float4, float2);
			this.Line(float2, float5);
			this.Line(float5, float3);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004250 File Offset: 0x00002450
		public void ArrowheadArc(float3 origin, float3 direction, float offset, float width = 60f)
		{
			if (!math.any(direction))
			{
				return;
			}
			if (offset < 0f)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset == 0f)
			{
				return;
			}
			Quaternion quaternion = Quaternion.LookRotation(direction, CommandBuilder.DEFAULT_UP);
			this.PushMatrix(Matrix4x4.TRS(origin, quaternion, Vector3.one));
			float num = 1.5707964f - width * 0.008726646f;
			float num2 = 1.5707964f + width * 0.008726646f;
			this.CircleXZInternal(float3.zero, offset, num, num2);
			float3 @float = new float3(math.cos(num), 0f, math.sin(num)) * offset;
			float3 float2 = new float3(math.cos(num2), 0f, math.sin(num2)) * offset;
			float3 float3 = new float3(0f, 0f, 1.4142f * offset);
			this.Line(@float, float3);
			this.Line(float3, float2);
			this.PopMatrix();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004348 File Offset: 0x00002548
		public void WireGrid(float3 center, quaternion rotation, int2 cells, float2 totalSize)
		{
			cells = math.max(cells, new int2(1, 1));
			this.PushMatrix(float4x4.TRS(center, rotation, new Vector3(totalSize.x, 0f, totalSize.y)));
			int x = cells.x;
			int y = cells.y;
			for (int i = 0; i <= x; i++)
			{
				this.Line(new float3((float)i / (float)x - 0.5f, 0f, -0.5f), new float3((float)i / (float)x - 0.5f, 0f, 0.5f));
			}
			for (int j = 0; j <= y; j++)
			{
				this.Line(new float3(-0.5f, 0f, (float)j / (float)y - 0.5f), new float3(0.5f, 0f, (float)j / (float)y - 0.5f));
			}
			this.PopMatrix();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000442E File Offset: 0x0000262E
		public void WireTriangle(float3 a, float3 b, float3 c)
		{
			this.Line(a, b);
			this.Line(b, c);
			this.Line(c, a);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004448 File Offset: 0x00002648
		[Obsolete("Use Draw.xz.WireRectangle instead")]
		public void WireRectangleXZ(float3 center, float2 size)
		{
			this.WireRectangle(center, quaternion.identity, size);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004457 File Offset: 0x00002657
		public void WireRectangle(float3 center, quaternion rotation, float2 size)
		{
			this.WirePlane(center, rotation, size);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004464 File Offset: 0x00002664
		[Obsolete("Use Draw.xy.WireRectangle instead")]
		public void WireRectangle(Rect rect)
		{
			this.xy.WireRectangle(rect);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004480 File Offset: 0x00002680
		public void WireTriangle(float3 center, quaternion rotation, float radius)
		{
			this.WirePolygon(center, 3, rotation, radius);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000448C File Offset: 0x0000268C
		public void WirePentagon(float3 center, quaternion rotation, float radius)
		{
			this.WirePolygon(center, 5, rotation, radius);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004498 File Offset: 0x00002698
		public void WireHexagon(float3 center, quaternion rotation, float radius)
		{
			this.WirePolygon(center, 6, rotation, radius);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000044A4 File Offset: 0x000026A4
		public void WirePolygon(float3 center, int vertices, quaternion rotation, float radius)
		{
			this.PushMatrix(float4x4.TRS(center, rotation, new float3(radius, radius, radius)));
			float3 @float = new float3(0f, 0f, 1f);
			for (int i = 1; i <= vertices; i++)
			{
				float num = 6.2831855f * ((float)i / (float)vertices);
				float3 float2 = new float3(math.sin(num), 0f, math.cos(num));
				this.Line(@float, float2);
				@float = float2;
			}
			this.PopMatrix();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004520 File Offset: 0x00002720
		[Obsolete("Use Draw.xy.SolidRectangle instead")]
		public void SolidRectangle(Rect rect)
		{
			this.xy.SolidRectangle(rect);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000453C File Offset: 0x0000273C
		public void SolidPlane(float3 center, float3 normal, float2 size)
		{
			if (math.any(normal))
			{
				this.SolidPlane(center, Quaternion.LookRotation(CommandBuilder.calculateTangent(normal), normal), size);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000456C File Offset: 0x0000276C
		public void SolidPlane(float3 center, quaternion rotation, float2 size)
		{
			this.PushMatrix(float4x4.TRS(center, rotation, new float3(size.x, 0f, size.y)));
			this.Reserve<CommandBuilder.BoxData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Box);
			this.Add<CommandBuilder.BoxData>(new CommandBuilder.BoxData
			{
				center = 0,
				size = 1
			});
			this.PopMatrix();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000045DC File Offset: 0x000027DC
		private static float3 calculateTangent(float3 normal)
		{
			float3 @float = math.cross(new float3(0f, 1f, 0f), normal);
			if (math.all(@float == 0f))
			{
				@float = math.cross(new float3(1f, 0f, 0f), normal);
			}
			return @float;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004632 File Offset: 0x00002832
		public void WirePlane(float3 center, float3 normal, float2 size)
		{
			if (math.any(normal))
			{
				this.WirePlane(center, Quaternion.LookRotation(CommandBuilder.calculateTangent(normal), normal), size);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004660 File Offset: 0x00002860
		public void WirePlane(float3 center, quaternion rotation, float2 size)
		{
			this.Reserve<CommandBuilder.PlaneData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.WirePlane);
			this.Add<CommandBuilder.PlaneData>(new CommandBuilder.PlaneData
			{
				center = center,
				rotation = rotation,
				size = size
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000046A2 File Offset: 0x000028A2
		public void PlaneWithNormal(float3 center, float3 normal, float2 size)
		{
			if (math.any(normal))
			{
				this.PlaneWithNormal(center, Quaternion.LookRotation(CommandBuilder.calculateTangent(normal), normal), size);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000046D0 File Offset: 0x000028D0
		public void PlaneWithNormal(float3 center, quaternion rotation, float2 size)
		{
			this.SolidPlane(center, rotation, size);
			this.WirePlane(center, rotation, size);
			this.ArrowRelativeSizeHead(center, center + math.mul(rotation, new float3(0f, 1f, 0f)) * 0.5f, math.mul(rotation, new float3(0f, 0f, 1f)), 0.2f);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004740 File Offset: 0x00002940
		public void SolidTriangle(float3 a, float3 b, float3 c)
		{
			this.Reserve<CommandBuilder.TriangleData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.SolidTriangle);
			this.Add<CommandBuilder.TriangleData>(new CommandBuilder.TriangleData
			{
				a = a,
				b = b,
				c = c
			});
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004784 File Offset: 0x00002984
		public void SolidBox(float3 center, float3 size)
		{
			this.Reserve<CommandBuilder.BoxData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Box);
			this.Add<CommandBuilder.BoxData>(new CommandBuilder.BoxData
			{
				center = center,
				size = size
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000047BE File Offset: 0x000029BE
		public void SolidBox(Bounds bounds)
		{
			this.SolidBox(bounds.center, bounds.size);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000047DE File Offset: 0x000029DE
		public void SolidBox(float3 center, quaternion rotation, float3 size)
		{
			this.PushMatrix(float4x4.TRS(center, rotation, size));
			this.SolidBox(float3.zero, Vector3.one);
			this.PopMatrix();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004809 File Offset: 0x00002A09
		public void Label3D(float3 position, quaternion rotation, string text, float size)
		{
			this.Label3D(position, rotation, text, size, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000481C File Offset: 0x00002A1C
		public void Label3D(float3 position, quaternion rotation, string text, float size, LabelAlignment alignment)
		{
			this.AssertBufferExists();
			DrawingData drawingData = this.gizmos.Target as DrawingData;
			this.Reserve<CommandBuilder.TextData3D>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Text3D);
			this.Add<CommandBuilder.TextData3D>(new CommandBuilder.TextData3D
			{
				center = position,
				rotation = rotation,
				numCharacters = text.Length,
				size = size,
				alignment = alignment
			});
			this.Reserve(UnsafeUtility.SizeOf<ushort>() * text.Length);
			foreach (char c in text)
			{
				ushort num = (ushort)drawingData.fontData.GetIndex(c);
				this.Add<ushort>(num);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000048CE File Offset: 0x00002ACE
		public void Label2D(float3 position, string text, float sizeInPixels = 14f)
		{
			this.Label2D(position, text, sizeInPixels, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000048E0 File Offset: 0x00002AE0
		public void Label2D(float3 position, string text, float sizeInPixels, LabelAlignment alignment)
		{
			this.AssertBufferExists();
			DrawingData drawingData = this.gizmos.Target as DrawingData;
			this.Reserve<CommandBuilder.TextData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Text);
			this.Add<CommandBuilder.TextData>(new CommandBuilder.TextData
			{
				center = position,
				numCharacters = text.Length,
				sizeInPixels = sizeInPixels,
				alignment = alignment
			});
			this.Reserve(UnsafeUtility.SizeOf<ushort>() * text.Length);
			foreach (char c in text)
			{
				ushort num = (ushort)drawingData.fontData.GetIndex(c);
				this.Add<ushort>(num);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004989 File Offset: 0x00002B89
		public void Label2D(float3 position, ref FixedString32Bytes text, float sizeInPixels = 14f)
		{
			this.Label2D(position, ref text, sizeInPixels, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004999 File Offset: 0x00002B99
		public void Label2D(float3 position, ref FixedString64Bytes text, float sizeInPixels = 14f)
		{
			this.Label2D(position, ref text, sizeInPixels, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000049A9 File Offset: 0x00002BA9
		public void Label2D(float3 position, ref FixedString128Bytes text, float sizeInPixels = 14f)
		{
			this.Label2D(position, ref text, sizeInPixels, LabelAlignment.MiddleLeft);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000049B9 File Offset: 0x00002BB9
		public void Label2D(float3 position, ref FixedString512Bytes text, float sizeInPixels = 14f)
		{
			this.Label2D(position, ref text, sizeInPixels, LabelAlignment.MiddleLeft);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000049C9 File Offset: 0x00002BC9
		public void Label2D(float3 position, ref FixedString32Bytes text, float sizeInPixels, LabelAlignment alignment)
		{
			this.Label2D(position, text.GetUnsafePtr(), text.Length, sizeInPixels, alignment);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000049E1 File Offset: 0x00002BE1
		public void Label2D(float3 position, ref FixedString64Bytes text, float sizeInPixels, LabelAlignment alignment)
		{
			this.Label2D(position, text.GetUnsafePtr(), text.Length, sizeInPixels, alignment);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000049F9 File Offset: 0x00002BF9
		public void Label2D(float3 position, ref FixedString128Bytes text, float sizeInPixels, LabelAlignment alignment)
		{
			this.Label2D(position, text.GetUnsafePtr(), text.Length, sizeInPixels, alignment);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004A11 File Offset: 0x00002C11
		public void Label2D(float3 position, ref FixedString512Bytes text, float sizeInPixels, LabelAlignment alignment)
		{
			this.Label2D(position, text.GetUnsafePtr(), text.Length, sizeInPixels, alignment);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004A2C File Offset: 0x00002C2C
		internal unsafe void Label2D(float3 position, byte* text, int byteCount, float sizeInPixels, LabelAlignment alignment)
		{
			this.AssertBufferExists();
			this.Reserve<CommandBuilder.TextData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Text);
			this.Add<CommandBuilder.TextData>(new CommandBuilder.TextData
			{
				center = position,
				numCharacters = byteCount,
				sizeInPixels = sizeInPixels,
				alignment = alignment
			});
			this.Reserve(UnsafeUtility.SizeOf<ushort>() * byteCount);
			for (int i = 0; i < byteCount; i++)
			{
				ushort num = (ushort)text[i];
				if (num >= 128)
				{
					num = 63;
				}
				if (num == 10)
				{
					num = ushort.MaxValue;
				}
				if (num != 13)
				{
					this.Add<ushort>(num);
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004ABE File Offset: 0x00002CBE
		public void Label3D(float3 position, quaternion rotation, ref FixedString32Bytes text, float size)
		{
			this.Label3D(position, rotation, ref text, size, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public void Label3D(float3 position, quaternion rotation, ref FixedString64Bytes text, float size)
		{
			this.Label3D(position, rotation, ref text, size, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004AE2 File Offset: 0x00002CE2
		public void Label3D(float3 position, quaternion rotation, ref FixedString128Bytes text, float size)
		{
			this.Label3D(position, rotation, ref text, size, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public void Label3D(float3 position, quaternion rotation, ref FixedString512Bytes text, float size)
		{
			this.Label3D(position, rotation, ref text, size, LabelAlignment.MiddleLeft);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004B06 File Offset: 0x00002D06
		public void Label3D(float3 position, quaternion rotation, ref FixedString32Bytes text, float size, LabelAlignment alignment)
		{
			this.Label3D(position, rotation, text.GetUnsafePtr(), text.Length, size, alignment);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004B20 File Offset: 0x00002D20
		public void Label3D(float3 position, quaternion rotation, ref FixedString64Bytes text, float size, LabelAlignment alignment)
		{
			this.Label3D(position, rotation, text.GetUnsafePtr(), text.Length, size, alignment);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004B3A File Offset: 0x00002D3A
		public void Label3D(float3 position, quaternion rotation, ref FixedString128Bytes text, float size, LabelAlignment alignment)
		{
			this.Label3D(position, rotation, text.GetUnsafePtr(), text.Length, size, alignment);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004B54 File Offset: 0x00002D54
		public void Label3D(float3 position, quaternion rotation, ref FixedString512Bytes text, float size, LabelAlignment alignment)
		{
			this.Label3D(position, rotation, text.GetUnsafePtr(), text.Length, size, alignment);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004B70 File Offset: 0x00002D70
		internal unsafe void Label3D(float3 position, quaternion rotation, byte* text, int byteCount, float size, LabelAlignment alignment)
		{
			this.AssertBufferExists();
			this.Reserve<CommandBuilder.TextData3D>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.Text3D);
			this.Add<CommandBuilder.TextData3D>(new CommandBuilder.TextData3D
			{
				center = position,
				rotation = rotation,
				numCharacters = byteCount,
				size = size,
				alignment = alignment
			});
			this.Reserve(UnsafeUtility.SizeOf<ushort>() * byteCount);
			for (int i = 0; i < byteCount; i++)
			{
				ushort num = (ushort)text[i];
				if (num >= 128)
				{
					num = 63;
				}
				if (num == 10)
				{
					num = ushort.MaxValue;
				}
				this.Add<ushort>(num);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004C08 File Offset: 0x00002E08
		public void Line(float3 a, float3 b, Color color)
		{
			this.Reserve<Color32, CommandBuilder.LineData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PopColor | CommandBuilder.Command.PopMatrix);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.LineData>(new CommandBuilder.LineData
			{
				a = a,
				b = b
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004C51 File Offset: 0x00002E51
		public void Ray(float3 origin, float3 direction, Color color)
		{
			this.Line(origin, origin + direction, color);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004C62 File Offset: 0x00002E62
		public void Ray(Ray ray, float length, Color color)
		{
			this.Line(ray.origin, ray.origin + ray.direction * length, color);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C8C File Offset: 0x00002E8C
		public void Arc(float3 center, float3 start, float3 end, Color color)
		{
			this.PushColor(color);
			float3 @float = start - center;
			float3 float2 = end - center;
			float3 float3 = math.cross(float2, @float);
			if (math.any(float3 != 0f) && math.all(math.isfinite(float3)))
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(center, Quaternion.LookRotation(@float, float3), Vector3.one);
				float num = Vector3.SignedAngle(@float, float2, float3) * 0.017453292f;
				this.PushMatrix(matrix4x);
				this.CircleXZInternal(float3.zero, math.length(@float), 1.5707964f, 1.5707964f - num);
				this.PopMatrix();
			}
			this.PopColor();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004D4A File Offset: 0x00002F4A
		[Obsolete("Use Draw.xz.Circle instead")]
		public void CircleXZ(float3 center, float radius, float startAngle, float endAngle, Color color)
		{
			this.CircleXZInternal(center, radius, startAngle, endAngle, color);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004D59 File Offset: 0x00002F59
		[Obsolete("Use Draw.xz.Circle instead")]
		public void CircleXZ(float3 center, float radius, Color color)
		{
			this.CircleXZ(center, radius, 0f, 6.2831855f, color);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004D70 File Offset: 0x00002F70
		public void Circle(float3 center, float3 normal, float radius, Color color)
		{
			this.Reserve<Color32, CommandBuilder.CircleData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PushMatrix | CommandBuilder.Command.PopMatrix);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.CircleData>(new CommandBuilder.CircleData
			{
				center = center,
				normal = normal,
				radius = radius
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public void WireCylinder(float3 bottom, float3 top, float radius, Color color)
		{
			this.WireCylinder(bottom, top - bottom, math.length(top - bottom), radius, color);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public void WireCylinder(float3 position, float3 up, float height, float radius, Color color)
		{
			up = math.normalizesafe(up, default(float3));
			if (math.all(up == 0f) || math.any(math.isnan(up)) || math.isnan(height) || math.isnan(radius))
			{
				return;
			}
			this.PushColor(color);
			float3 @float;
			float3 float2;
			CommandBuilder.OrthonormalBasis(up, out @float, out float2);
			this.PushMatrix(new float4x4(new float4(@float * radius, 0f), new float4(up * height, 0f), new float4(float2 * radius, 0f), new float4(position, 1f)));
			this.CircleXZInternal(float3.zero, 1f, 0f, 6.2831855f);
			if (height > 0f)
			{
				this.CircleXZInternal(new float3(0f, 1f, 0f), 1f, 0f, 6.2831855f);
				this.Line(new float3(1f, 0f, 0f), new float3(1f, 1f, 0f));
				this.Line(new float3(-1f, 0f, 0f), new float3(-1f, 1f, 0f));
				this.Line(new float3(0f, 0f, 1f), new float3(0f, 1f, 1f));
				this.Line(new float3(0f, 0f, -1f), new float3(0f, 1f, -1f));
			}
			this.PopMatrix();
			this.PopColor();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004FA4 File Offset: 0x000031A4
		public void WireCapsule(float3 start, float3 end, float radius, Color color)
		{
			this.PushColor(color);
			float3 @float = end - start;
			float num = math.length(@float);
			if ((double)num < 0.0001)
			{
				this.WireSphere(start, radius);
			}
			else
			{
				float3 float2 = @float / num;
				this.WireCapsule(start - float2 * radius, float2, num + 2f * radius, radius);
			}
			this.PopColor();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000500C File Offset: 0x0000320C
		public void WireCapsule(float3 position, float3 direction, float length, float radius, Color color)
		{
			direction = math.normalizesafe(direction, default(float3));
			if (math.all(direction == 0f) || math.any(math.isnan(direction)) || math.isnan(length) || math.isnan(radius))
			{
				return;
			}
			this.PushColor(color);
			if (radius <= 0f)
			{
				this.Line(position, position + direction * length);
			}
			else
			{
				length = math.max(length, radius * 2f);
				float3 @float;
				float3 float2;
				CommandBuilder.OrthonormalBasis(direction, out @float, out float2);
				this.PushMatrix(new float4x4(new float4(@float, 0f), new float4(direction, 0f), new float4(float2, 0f), new float4(position, 1f)));
				this.CircleXZInternal(new float3(0f, radius, 0f), radius, 0f, 6.2831855f);
				this.PushMatrix(CommandBuilder.XZtoXYPlaneMatrix);
				this.CircleXZInternal(new float3(0f, 0f, radius), radius, 3.1415927f, 6.2831855f);
				this.PopMatrix();
				this.PushMatrix(CommandBuilder.XZtoYZPlaneMatrix);
				this.CircleXZInternal(new float3(radius, 0f, 0f), radius, 1.5707964f, 4.712389f);
				this.PopMatrix();
				if (length > 0f)
				{
					float num = length - radius;
					this.CircleXZInternal(new float3(0f, num, 0f), radius, 0f, 6.2831855f);
					this.PushMatrix(CommandBuilder.XZtoXYPlaneMatrix);
					this.CircleXZInternal(new float3(0f, 0f, num), radius, 0f, 3.1415927f);
					this.PopMatrix();
					this.PushMatrix(CommandBuilder.XZtoYZPlaneMatrix);
					this.CircleXZInternal(new float3(num, 0f, 0f), radius, -1.5707964f, 1.5707964f);
					this.PopMatrix();
					this.Line(new float3(radius, radius, 0f), new float3(radius, num, 0f));
					this.Line(new float3(-radius, radius, 0f), new float3(-radius, num, 0f));
					this.Line(new float3(0f, radius, radius), new float3(0f, num, radius));
					this.Line(new float3(0f, radius, -radius), new float3(0f, num, -radius));
				}
				this.PopMatrix();
			}
			this.PopColor();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005294 File Offset: 0x00003494
		public void WireSphere(float3 position, float radius, Color color)
		{
			this.PushColor(color);
			this.SphereOutline(position, radius);
			this.Circle(position, new float3(1f, 0f, 0f), radius);
			this.Circle(position, new float3(0f, 1f, 0f), radius);
			this.Circle(position, new float3(0f, 0f, 1f), radius);
			this.PopColor();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000530C File Offset: 0x0000350C
		[BurstDiscard]
		public void Polyline(List<Vector3> points, bool cycle, Color color)
		{
			this.PushColor(color);
			for (int i = 0; i < points.Count - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Count > 1)
			{
				this.Line(points[points.Count - 1], points[0]);
			}
			this.PopColor();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005376 File Offset: 0x00003576
		[BurstDiscard]
		public void Polyline(List<Vector3> points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005384 File Offset: 0x00003584
		[BurstDiscard]
		public void Polyline(Vector3[] points, bool cycle, Color color)
		{
			this.PushColor(color);
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
			this.PopColor();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000053E5 File Offset: 0x000035E5
		[BurstDiscard]
		public void Polyline(Vector3[] points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000053F0 File Offset: 0x000035F0
		[BurstDiscard]
		public void Polyline(float3[] points, bool cycle, Color color)
		{
			this.PushColor(color);
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
			this.PopColor();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005451 File Offset: 0x00003651
		[BurstDiscard]
		public void Polyline(float3[] points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000545C File Offset: 0x0000365C
		public void Polyline(NativeArray<float3> points, bool cycle, Color color)
		{
			this.PushColor(color);
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
			this.PopColor();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000054CD File Offset: 0x000036CD
		public void Polyline(NativeArray<float3> points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000054D8 File Offset: 0x000036D8
		public void WireBox(float3 center, float3 size, Color color)
		{
			this.Reserve<Color32, CommandBuilder.BoxData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PopColor | CommandBuilder.Command.PopMatrix | CommandBuilder.Command.Disc);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.BoxData>(new CommandBuilder.BoxData
			{
				center = center,
				size = size
			});
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005524 File Offset: 0x00003724
		public void WireBox(float3 center, quaternion rotation, float3 size, Color color)
		{
			this.PushColor(color);
			this.PushMatrix(float4x4.TRS(center, rotation, size));
			this.WireBox(float3.zero, new float3(1f, 1f, 1f));
			this.PopMatrix();
			this.PopColor();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005572 File Offset: 0x00003772
		public void WireBox(Bounds bounds, Color color)
		{
			this.WireBox(bounds.center, bounds.size, color);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005594 File Offset: 0x00003794
		public void WireMesh(Mesh mesh, Color color)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException();
			}
			this.PushColor(color);
			Mesh.MeshDataArray meshDataArray = Mesh.AcquireReadOnlyMeshData(mesh);
			Mesh.MeshData meshData = meshDataArray[0];
			CommandBuilder.JobWireMesh.JobWireMeshFunctionPointer(ref meshData, ref this);
			meshDataArray.Dispose();
			this.PopColor();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000055E1 File Offset: 0x000037E1
		public unsafe void WireMesh(NativeArray<float3> vertices, NativeArray<int> triangles, Color color)
		{
			this.PushColor(color);
			CommandBuilder.JobWireMesh.WireMesh((float3*)vertices.GetUnsafeReadOnlyPtr<float3>(), (int*)triangles.GetUnsafeReadOnlyPtr<int>(), vertices.Length, triangles.Length, ref this);
			this.PopColor();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005610 File Offset: 0x00003810
		public void Cross(float3 position, float size, Color color)
		{
			this.PushColor(color);
			size *= 0.5f;
			this.Line(position - new float3(size, 0f, 0f), position + new float3(size, 0f, 0f));
			this.Line(position - new float3(0f, size, 0f), position + new float3(0f, size, 0f));
			this.Line(position - new float3(0f, 0f, size), position + new float3(0f, 0f, size));
			this.PopColor();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000056C9 File Offset: 0x000038C9
		public void Cross(float3 position, Color color)
		{
			this.Cross(position, 1f, color);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000056D8 File Offset: 0x000038D8
		[Obsolete("Use Draw.xz.Cross instead")]
		public void CrossXZ(float3 position, float size, Color color)
		{
			this.PushColor(color);
			size *= 0.5f;
			this.Line(position - new float3(size, 0f, 0f), position + new float3(size, 0f, 0f));
			this.Line(position - new float3(0f, 0f, size), position + new float3(0f, 0f, size));
			this.PopColor();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000575F File Offset: 0x0000395F
		[Obsolete("Use Draw.xz.Cross instead")]
		public void CrossXZ(float3 position, Color color)
		{
			this.CrossXZ(position, 1f, color);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005770 File Offset: 0x00003970
		[Obsolete("Use Draw.xy.Cross instead")]
		public void CrossXY(float3 position, float size, Color color)
		{
			this.PushColor(color);
			size *= 0.5f;
			this.Line(position - new float3(size, 0f, 0f), position + new float3(size, 0f, 0f));
			this.Line(position - new float3(0f, size, 0f), position + new float3(0f, size, 0f));
			this.PopColor();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000057F7 File Offset: 0x000039F7
		[Obsolete("Use Draw.xy.Cross instead")]
		public void CrossXY(float3 position, Color color)
		{
			this.CrossXY(position, 1f, color);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005808 File Offset: 0x00003A08
		public void Bezier(float3 p0, float3 p1, float3 p2, float3 p3, Color color)
		{
			this.PushColor(color);
			float3 @float = p0;
			for (int i = 1; i <= 20; i++)
			{
				float num = (float)i / 20f;
				float3 float2 = CommandBuilder.EvaluateCubicBezier(p0, p1, p2, p3, num);
				this.Line(@float, float2);
				@float = float2;
			}
			this.PopColor();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005851 File Offset: 0x00003A51
		public void Arrow(float3 from, float3 to, Color color)
		{
			this.ArrowRelativeSizeHead(from, to, CommandBuilder.DEFAULT_UP, 0.2f, color);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005868 File Offset: 0x00003A68
		public void Arrow(float3 from, float3 to, float3 up, float headSize, Color color)
		{
			this.PushColor(color);
			float num = math.lengthsq(to - from);
			if (num > 1E-06f)
			{
				this.ArrowRelativeSizeHead(from, to, up, headSize * math.rsqrt(num));
			}
			this.PopColor();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000058AC File Offset: 0x00003AAC
		public void ArrowRelativeSizeHead(float3 from, float3 to, float3 up, float headFraction, Color color)
		{
			this.PushColor(color);
			this.Line(from, to);
			float3 @float = to - from;
			float3 float2 = math.cross(@float, up);
			if (math.all(float2 == 0f))
			{
				float2 = math.cross(new float3(1f, 0f, 0f), @float);
			}
			if (math.all(float2 == 0f))
			{
				float2 = math.cross(new float3(0f, 1f, 0f), @float);
			}
			float2 = math.normalizesafe(float2, default(float3)) * math.length(@float);
			this.Line(to, to - (@float + float2) * headFraction);
			this.Line(to, to - (@float - float2) * headFraction);
			this.PopColor();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000598C File Offset: 0x00003B8C
		public void ArrowheadArc(float3 origin, float3 direction, float offset, float width, Color color)
		{
			if (!math.any(direction))
			{
				return;
			}
			if (offset < 0f)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset == 0f)
			{
				return;
			}
			this.PushColor(color);
			Quaternion quaternion = Quaternion.LookRotation(direction, CommandBuilder.DEFAULT_UP);
			this.PushMatrix(Matrix4x4.TRS(origin, quaternion, Vector3.one));
			float num = 1.5707964f - width * 0.008726646f;
			float num2 = 1.5707964f + width * 0.008726646f;
			this.CircleXZInternal(float3.zero, offset, num, num2);
			float3 @float = new float3(math.cos(num), 0f, math.sin(num)) * offset;
			float3 float2 = new float3(math.cos(num2), 0f, math.sin(num2)) * offset;
			float3 float3 = new float3(0f, 0f, 1.4142f * offset);
			this.Line(@float, float3);
			this.Line(float3, float2);
			this.PopMatrix();
			this.PopColor();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005A91 File Offset: 0x00003C91
		public void ArrowheadArc(float3 origin, float3 direction, float offset, Color color)
		{
			this.ArrowheadArc(origin, direction, offset, 60f, color);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public void WireGrid(float3 center, quaternion rotation, int2 cells, float2 totalSize, Color color)
		{
			this.PushColor(color);
			cells = math.max(cells, new int2(1, 1));
			this.PushMatrix(float4x4.TRS(center, rotation, new Vector3(totalSize.x, 0f, totalSize.y)));
			int x = cells.x;
			int y = cells.y;
			for (int i = 0; i <= x; i++)
			{
				this.Line(new float3((float)i / (float)x - 0.5f, 0f, -0.5f), new float3((float)i / (float)x - 0.5f, 0f, 0.5f));
			}
			for (int j = 0; j <= y; j++)
			{
				this.Line(new float3(-0.5f, 0f, (float)j / (float)y - 0.5f), new float3(0.5f, 0f, (float)j / (float)y - 0.5f));
			}
			this.PopMatrix();
			this.PopColor();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005B98 File Offset: 0x00003D98
		public void WireRectangle(float3 center, quaternion rotation, float2 size, Color color)
		{
			this.WirePlane(center, rotation, size, color);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005BA8 File Offset: 0x00003DA8
		[Obsolete("Use Draw.xy.WireRectangle instead")]
		public void WireRectangle(Rect rect, Color color)
		{
			this.xy.WireRectangle(rect, color);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005BC5 File Offset: 0x00003DC5
		public void WirePlane(float3 center, float3 normal, float2 size, Color color)
		{
			this.PushColor(color);
			if (math.any(normal))
			{
				this.WirePlane(center, Quaternion.LookRotation(CommandBuilder.calculateTangent(normal), normal), size);
			}
			this.PopColor();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005C00 File Offset: 0x00003E00
		public void WirePlane(float3 center, quaternion rotation, float2 size, Color color)
		{
			this.Reserve<Color32, CommandBuilder.PlaneData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PopMatrix | CommandBuilder.Command.Disc);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.PlaneData>(new CommandBuilder.PlaneData
			{
				center = center,
				rotation = rotation,
				size = size
			});
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005C54 File Offset: 0x00003E54
		public void SolidBox(float3 center, float3 size, Color color)
		{
			this.Reserve<Color32, CommandBuilder.BoxData>();
			this.Add<CommandBuilder.Command>(CommandBuilder.Command.PushColorInline | CommandBuilder.Command.PopColor | CommandBuilder.Command.PushMatrix | CommandBuilder.Command.Disc);
			this.Add<uint>(CommandBuilder.ConvertColor(color));
			this.Add<CommandBuilder.BoxData>(new CommandBuilder.BoxData
			{
				center = center,
				size = size
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005C9D File Offset: 0x00003E9D
		public void SolidBox(Bounds bounds, Color color)
		{
			this.SolidBox(bounds.center, bounds.size, color);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005CBE File Offset: 0x00003EBE
		public void SolidBox(float3 center, quaternion rotation, float3 size, Color color)
		{
			this.PushColor(color);
			this.PushMatrix(float4x4.TRS(center, rotation, size));
			this.SolidBox(float3.zero, Vector3.one);
			this.PopMatrix();
			this.PopColor();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005D30 File Offset: 0x00003F30
		public static void Initialize$JobWireMesh_WireMesh_0000021A$BurstDirectCall()
		{
			CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.Initialize();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005D37 File Offset: 0x00003F37
		public static void Initialize$JobWireMesh_Execute_0000021B$BurstDirectCall()
		{
			CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.Initialize();
		}

		// Token: 0x04000018 RID: 24
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeAppendBuffer* buffer;

		// Token: 0x04000019 RID: 25
		private GCHandle gizmos;

		// Token: 0x0400001A RID: 26
		[NativeSetThreadIndex]
		private int threadIndex;

		// Token: 0x0400001B RID: 27
		private DrawingData.BuilderData.BitPackedMeta uniqueID;

		// Token: 0x0400001C RID: 28
		private static readonly float3 DEFAULT_UP = new float3(0f, 1f, 0f);

		// Token: 0x0400001D RID: 29
		internal static readonly float4x4 XZtoXYPlaneMatrix = float4x4.RotateX(-1.5707964f);

		// Token: 0x0400001E RID: 30
		internal static readonly float4x4 XZtoYZPlaneMatrix = float4x4.RotateZ(1.5707964f);

		// Token: 0x0200000A RID: 10
		[Flags]
		internal enum Command
		{
			// Token: 0x04000020 RID: 32
			PushColorInline = 256,
			// Token: 0x04000021 RID: 33
			PushColor = 0,
			// Token: 0x04000022 RID: 34
			PopColor = 1,
			// Token: 0x04000023 RID: 35
			PushMatrix = 2,
			// Token: 0x04000024 RID: 36
			PushSetMatrix = 3,
			// Token: 0x04000025 RID: 37
			PopMatrix = 4,
			// Token: 0x04000026 RID: 38
			Line = 5,
			// Token: 0x04000027 RID: 39
			Circle = 6,
			// Token: 0x04000028 RID: 40
			CircleXZ = 7,
			// Token: 0x04000029 RID: 41
			Disc = 8,
			// Token: 0x0400002A RID: 42
			DiscXZ = 9,
			// Token: 0x0400002B RID: 43
			SphereOutline = 10,
			// Token: 0x0400002C RID: 44
			Box = 11,
			// Token: 0x0400002D RID: 45
			WirePlane = 12,
			// Token: 0x0400002E RID: 46
			WireBox = 13,
			// Token: 0x0400002F RID: 47
			SolidTriangle = 14,
			// Token: 0x04000030 RID: 48
			PushPersist = 15,
			// Token: 0x04000031 RID: 49
			PopPersist = 16,
			// Token: 0x04000032 RID: 50
			Text = 17,
			// Token: 0x04000033 RID: 51
			Text3D = 18,
			// Token: 0x04000034 RID: 52
			PushLineWidth = 19,
			// Token: 0x04000035 RID: 53
			PopLineWidth = 20,
			// Token: 0x04000036 RID: 54
			CaptureState = 21
		}

		// Token: 0x0200000B RID: 11
		internal struct TriangleData
		{
			// Token: 0x04000037 RID: 55
			public float3 a;

			// Token: 0x04000038 RID: 56
			public float3 b;

			// Token: 0x04000039 RID: 57
			public float3 c;
		}

		// Token: 0x0200000C RID: 12
		internal struct LineData
		{
			// Token: 0x0400003A RID: 58
			public float3 a;

			// Token: 0x0400003B RID: 59
			public float3 b;
		}

		// Token: 0x0200000D RID: 13
		internal struct LineDataV3
		{
			// Token: 0x0400003C RID: 60
			public Vector3 a;

			// Token: 0x0400003D RID: 61
			public Vector3 b;
		}

		// Token: 0x0200000E RID: 14
		internal struct CircleXZData
		{
			// Token: 0x0400003E RID: 62
			public float3 center;

			// Token: 0x0400003F RID: 63
			public float radius;

			// Token: 0x04000040 RID: 64
			public float startAngle;

			// Token: 0x04000041 RID: 65
			public float endAngle;
		}

		// Token: 0x0200000F RID: 15
		internal struct CircleData
		{
			// Token: 0x04000042 RID: 66
			public float3 center;

			// Token: 0x04000043 RID: 67
			public float3 normal;

			// Token: 0x04000044 RID: 68
			public float radius;
		}

		// Token: 0x02000010 RID: 16
		internal struct SphereData
		{
			// Token: 0x04000045 RID: 69
			public float3 center;

			// Token: 0x04000046 RID: 70
			public float radius;
		}

		// Token: 0x02000011 RID: 17
		internal struct BoxData
		{
			// Token: 0x04000047 RID: 71
			public float3 center;

			// Token: 0x04000048 RID: 72
			public float3 size;
		}

		// Token: 0x02000012 RID: 18
		internal struct PlaneData
		{
			// Token: 0x04000049 RID: 73
			public float3 center;

			// Token: 0x0400004A RID: 74
			public quaternion rotation;

			// Token: 0x0400004B RID: 75
			public float2 size;
		}

		// Token: 0x02000013 RID: 19
		internal struct PersistData
		{
			// Token: 0x0400004C RID: 76
			public float endTime;
		}

		// Token: 0x02000014 RID: 20
		internal struct LineWidthData
		{
			// Token: 0x0400004D RID: 77
			public float pixels;

			// Token: 0x0400004E RID: 78
			public bool automaticJoins;
		}

		// Token: 0x02000015 RID: 21
		internal struct TextData
		{
			// Token: 0x0400004F RID: 79
			public float3 center;

			// Token: 0x04000050 RID: 80
			public LabelAlignment alignment;

			// Token: 0x04000051 RID: 81
			public float sizeInPixels;

			// Token: 0x04000052 RID: 82
			public int numCharacters;
		}

		// Token: 0x02000016 RID: 22
		internal struct TextData3D
		{
			// Token: 0x04000053 RID: 83
			public float3 center;

			// Token: 0x04000054 RID: 84
			public quaternion rotation;

			// Token: 0x04000055 RID: 85
			public LabelAlignment alignment;

			// Token: 0x04000056 RID: 86
			public float size;

			// Token: 0x04000057 RID: 87
			public int numCharacters;
		}

		// Token: 0x02000017 RID: 23
		public struct ScopeMatrix : IDisposable
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x00005D3E File Offset: 0x00003F3E
			public void Dispose()
			{
				this.builder.PopMatrix();
				this.builder.buffer = null;
			}

			// Token: 0x04000058 RID: 88
			internal CommandBuilder builder;
		}

		// Token: 0x02000018 RID: 24
		public struct ScopeColor : IDisposable
		{
			// Token: 0x060000CA RID: 202 RVA: 0x00005D58 File Offset: 0x00003F58
			public void Dispose()
			{
				this.builder.PopColor();
				this.builder.buffer = null;
			}

			// Token: 0x04000059 RID: 89
			internal CommandBuilder builder;
		}

		// Token: 0x02000019 RID: 25
		public struct ScopePersist : IDisposable
		{
			// Token: 0x060000CB RID: 203 RVA: 0x00005D72 File Offset: 0x00003F72
			public void Dispose()
			{
				this.builder.PopDuration();
				this.builder.buffer = null;
			}

			// Token: 0x0400005A RID: 90
			internal CommandBuilder builder;
		}

		// Token: 0x0200001A RID: 26
		public struct ScopeEmpty : IDisposable
		{
			// Token: 0x060000CC RID: 204 RVA: 0x00002094 File Offset: 0x00000294
			public void Dispose()
			{
			}
		}

		// Token: 0x0200001B RID: 27
		public struct ScopeLineWidth : IDisposable
		{
			// Token: 0x060000CD RID: 205 RVA: 0x00005D8C File Offset: 0x00003F8C
			public void Dispose()
			{
				this.builder.PopLineWidth();
				this.builder.buffer = null;
			}

			// Token: 0x0400005B RID: 91
			internal CommandBuilder builder;
		}

		// Token: 0x0200001C RID: 28
		public enum SymbolDecoration
		{
			// Token: 0x0400005D RID: 93
			None,
			// Token: 0x0400005E RID: 94
			ArrowHead,
			// Token: 0x0400005F RID: 95
			Circle
		}

		// Token: 0x0200001D RID: 29
		public struct PolylineWithSymbol
		{
			// Token: 0x060000CE RID: 206 RVA: 0x00005DA8 File Offset: 0x00003FA8
			public PolylineWithSymbol(CommandBuilder.SymbolDecoration symbol, float symbolSize, float symbolPadding, float symbolSpacing, bool reverseSymbols = false)
			{
				if (symbolSpacing <= 1.1754944E-38f)
				{
					throw new ArgumentOutOfRangeException("symbolSpacing", "Symbol spacing must be greater than zero");
				}
				if (symbolSize <= 1.1754944E-38f)
				{
					throw new ArgumentOutOfRangeException("symbolSize", "Symbol size must be greater than zero");
				}
				if (symbolPadding < 0f)
				{
					throw new ArgumentOutOfRangeException("symbolPadding", "Symbol padding must non-negative");
				}
				this.prev = float3.zero;
				this.symbol = symbol;
				this.symbolSize = symbolSize;
				this.symbolPadding = symbolPadding;
				this.symbolSpacing = math.max(0f, symbolSpacing - symbolPadding * 2f - symbolSize);
				this.reverseSymbols = reverseSymbols;
				this.symbolOffset = ((symbol == CommandBuilder.SymbolDecoration.ArrowHead) ? (-0.25f * symbolSize) : 0f);
				if (reverseSymbols)
				{
					this.symbolOffset = -this.symbolOffset;
				}
				this.symbolOffset += 0.5f * symbolSize;
				this.offset = -1f;
				this.odd = false;
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00005E94 File Offset: 0x00004094
			public void MoveTo(ref CommandBuilder draw, float3 next)
			{
				if (this.offset == -1f)
				{
					this.offset = this.symbolSpacing * 0.5f;
					this.prev = next;
					return;
				}
				float num = math.length(next - this.prev);
				float num2 = math.rcp(num);
				float3 @float = next - this.prev;
				float3 float2 = default(float3);
				if (this.symbol != CommandBuilder.SymbolDecoration.None)
				{
					float2 = math.normalizesafe(math.cross(@float, math.cross(@float, new float3(0f, 1f, 0f))), default(float3));
					if (math.all(float2 == 0f))
					{
						float2 = new float3(0f, 0f, 1f);
					}
				}
				if (this.reverseSymbols)
				{
					@float = -@float;
				}
				if (this.offset > 0f && !this.odd)
				{
					draw.Line(this.prev, math.lerp(this.prev, next, math.min(this.offset * num2, 1f)));
				}
				while (this.offset < num)
				{
					if (!this.odd)
					{
						float3 float3 = math.lerp(this.prev, next, (this.offset + this.symbolOffset) * num2);
						switch (this.symbol)
						{
						case CommandBuilder.SymbolDecoration.None:
							break;
						case CommandBuilder.SymbolDecoration.ArrowHead:
							draw.Arrowhead(float3, @float, float2, this.symbolSize);
							break;
						case CommandBuilder.SymbolDecoration.Circle:
							goto IL_01CA;
						default:
							goto IL_01CA;
						}
						IL_01DF:
						this.offset += this.symbolSize + this.symbolPadding;
						goto IL_01F9;
						IL_01CA:
						draw.Circle(float3, float2, this.symbolSize * 0.5f);
						goto IL_01DF;
					}
					float3 float4 = math.lerp(this.prev, next, this.offset * num2);
					this.offset += this.symbolSpacing;
					float3 float5 = math.lerp(this.prev, next, math.min(this.offset * num2, 1f));
					draw.Line(float4, float5);
					this.offset += this.symbolPadding;
					IL_01F9:
					this.odd = !this.odd;
				}
				this.offset -= num;
				this.prev = next;
			}

			// Token: 0x04000060 RID: 96
			private float3 prev;

			// Token: 0x04000061 RID: 97
			private float offset;

			// Token: 0x04000062 RID: 98
			private readonly float symbolSize;

			// Token: 0x04000063 RID: 99
			private readonly float symbolSpacing;

			// Token: 0x04000064 RID: 100
			private readonly float symbolPadding;

			// Token: 0x04000065 RID: 101
			private readonly float symbolOffset;

			// Token: 0x04000066 RID: 102
			private readonly CommandBuilder.SymbolDecoration symbol;

			// Token: 0x04000067 RID: 103
			private readonly bool reverseSymbols;

			// Token: 0x04000068 RID: 104
			private bool odd;
		}

		// Token: 0x0200001E RID: 30
		[BurstCompile]
		private class JobWireMesh
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x000060CA File Offset: 0x000042CA
			[BurstCompile]
			public unsafe static void WireMesh(float3* verts, int* indices, int vertexCount, int indexCount, ref CommandBuilder draw)
			{
				CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.Invoke(verts, indices, vertexCount, indexCount, ref draw);
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x000060D7 File Offset: 0x000042D7
			[BurstCompile]
			[MonoPInvokeCallback(typeof(CommandBuilder.JobWireMesh.JobWireMeshDelegate))]
			private static void Execute(ref Mesh.MeshData rawMeshData, ref CommandBuilder draw)
			{
				CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.Invoke(ref rawMeshData, ref draw);
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00006114 File Offset: 0x00004314
			[BurstCompile]
			[MethodImpl(256)]
			public unsafe static void WireMesh$BurstManaged(float3* verts, int* indices, int vertexCount, int indexCount, ref CommandBuilder draw)
			{
				NativeHashMap<int2, bool> nativeHashMap = new NativeHashMap<int2, bool>(indexCount, Allocator.Temp);
				for (int i = 0; i < indexCount; i += 3)
				{
					int num = indices[i];
					int num2 = indices[i + 1];
					int num3 = indices[i + 2];
					if (num < 0 || num2 < 0 || num3 < 0 || num >= vertexCount || num2 >= vertexCount || num3 >= vertexCount)
					{
						throw new Exception("Invalid vertex index. Index out of bounds");
					}
					int num4 = math.min(num, num2);
					int num5 = math.max(num, num2);
					if (!nativeHashMap.ContainsKey(new int2(num4, num5)))
					{
						nativeHashMap.Add(new int2(num4, num5), true);
						draw.Line(verts[num4], verts[num5]);
					}
					num4 = math.min(num2, num3);
					num5 = math.max(num2, num3);
					if (!nativeHashMap.ContainsKey(new int2(num4, num5)))
					{
						nativeHashMap.Add(new int2(num4, num5), true);
						draw.Line(verts[num4], verts[num5]);
					}
					num4 = math.min(num3, num);
					num5 = math.max(num3, num);
					if (!nativeHashMap.ContainsKey(new int2(num4, num5)))
					{
						nativeHashMap.Add(new int2(num4, num5), true);
						draw.Line(verts[num4], verts[num5]);
					}
				}
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x000062A4 File Offset: 0x000044A4
			[BurstCompile]
			[MonoPInvokeCallback(typeof(CommandBuilder.JobWireMesh.JobWireMeshDelegate))]
			[MethodImpl(256)]
			public unsafe static void Execute$BurstManaged(ref Mesh.MeshData rawMeshData, ref CommandBuilder draw)
			{
				int num = 0;
				for (int i = 0; i < rawMeshData.subMeshCount; i++)
				{
					num = math.max(num, rawMeshData.GetSubMesh(i).indexCount);
				}
				NativeArray<int> nativeArray = new NativeArray<int>(num, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				NativeArray<Vector3> nativeArray2 = new NativeArray<Vector3>(rawMeshData.vertexCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				rawMeshData.GetVertices(nativeArray2);
				for (int j = 0; j < rawMeshData.subMeshCount; j++)
				{
					SubMeshDescriptor subMesh = rawMeshData.GetSubMesh(j);
					rawMeshData.GetIndices(nativeArray, j, true);
					CommandBuilder.JobWireMesh.WireMesh((float3*)nativeArray2.GetUnsafeReadOnlyPtr<Vector3>(), (int*)nativeArray.GetUnsafeReadOnlyPtr<int>(), nativeArray2.Length, subMesh.indexCount, ref draw);
				}
			}

			// Token: 0x04000069 RID: 105
			public static readonly CommandBuilder.JobWireMesh.JobWireMeshDelegate JobWireMeshFunctionPointer = BurstCompiler.CompileFunctionPointer<CommandBuilder.JobWireMesh.JobWireMeshDelegate>(new CommandBuilder.JobWireMesh.JobWireMeshDelegate(CommandBuilder.JobWireMesh.Execute)).Invoke;

			// Token: 0x0200001F RID: 31
			// (Invoke) Token: 0x060000D7 RID: 215
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate void JobWireMeshDelegate(ref Mesh.MeshData rawMeshData, ref CommandBuilder draw);

			// Token: 0x02000020 RID: 32
			// (Invoke) Token: 0x060000DB RID: 219
			public unsafe delegate void WireMesh_0000021A$PostfixBurstDelegate(float3* verts, int* indices, int vertexCount, int indexCount, ref CommandBuilder draw);

			// Token: 0x02000021 RID: 33
			internal static class WireMesh_0000021A$BurstDirectCall
			{
				// Token: 0x060000DE RID: 222 RVA: 0x00006344 File Offset: 0x00004544
				[BurstDiscard]
				private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.Pointer == 0)
					{
						CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.DeferredCompilation, methodof(CommandBuilder.JobWireMesh.WireMesh$BurstManaged(float3*, int*, int, int, ref CommandBuilder)).MethodHandle, typeof(CommandBuilder.JobWireMesh.WireMesh_0000021A$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.Pointer;
				}

				// Token: 0x060000DF RID: 223 RVA: 0x00006370 File Offset: 0x00004570
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x060000E0 RID: 224 RVA: 0x00006388 File Offset: 0x00004588
				public unsafe static void Constructor()
				{
					CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(CommandBuilder.JobWireMesh.WireMesh(float3*, int*, int, int, ref CommandBuilder)).MethodHandle);
				}

				// Token: 0x060000E1 RID: 225 RVA: 0x00002094 File Offset: 0x00000294
				public static void Initialize()
				{
				}

				// Token: 0x060000E2 RID: 226 RVA: 0x00006399 File Offset: 0x00004599
				// Note: this type is marked as 'beforefieldinit'.
				static WireMesh_0000021A$BurstDirectCall()
				{
					CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.Constructor();
				}

				// Token: 0x060000E3 RID: 227 RVA: 0x000063A0 File Offset: 0x000045A0
				public unsafe static void Invoke(float3* verts, int* indices, int vertexCount, int indexCount, ref CommandBuilder draw)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = CommandBuilder.JobWireMesh.WireMesh_0000021A$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							calli(System.Void(Unity.Mathematics.float3*,System.Int32*,System.Int32,System.Int32,Pathfinding.Drawing.CommandBuilder&), verts, indices, vertexCount, indexCount, ref draw, functionPointer);
							return;
						}
					}
					CommandBuilder.JobWireMesh.WireMesh$BurstManaged(verts, indices, vertexCount, indexCount, ref draw);
				}

				// Token: 0x0400006A RID: 106
				private static IntPtr Pointer;

				// Token: 0x0400006B RID: 107
				private static IntPtr DeferredCompilation;
			}

			// Token: 0x02000022 RID: 34
			// (Invoke) Token: 0x060000E5 RID: 229
			public delegate void Execute_0000021B$PostfixBurstDelegate(ref Mesh.MeshData rawMeshData, ref CommandBuilder draw);

			// Token: 0x02000023 RID: 35
			internal static class Execute_0000021B$BurstDirectCall
			{
				// Token: 0x060000E8 RID: 232 RVA: 0x000063DB File Offset: 0x000045DB
				[BurstDiscard]
				private static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.Pointer == 0)
					{
						CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.DeferredCompilation, methodof(CommandBuilder.JobWireMesh.Execute$BurstManaged(ref Mesh.MeshData, ref CommandBuilder)).MethodHandle, typeof(CommandBuilder.JobWireMesh.Execute_0000021B$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.Pointer;
				}

				// Token: 0x060000E9 RID: 233 RVA: 0x00006408 File Offset: 0x00004608
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x060000EA RID: 234 RVA: 0x00006420 File Offset: 0x00004620
				public static void Constructor()
				{
					CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(CommandBuilder.JobWireMesh.Execute(ref Mesh.MeshData, ref CommandBuilder)).MethodHandle);
				}

				// Token: 0x060000EB RID: 235 RVA: 0x00002094 File Offset: 0x00000294
				public static void Initialize()
				{
				}

				// Token: 0x060000EC RID: 236 RVA: 0x00006431 File Offset: 0x00004631
				// Note: this type is marked as 'beforefieldinit'.
				static Execute_0000021B$BurstDirectCall()
				{
					CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.Constructor();
				}

				// Token: 0x060000ED RID: 237 RVA: 0x00006438 File Offset: 0x00004638
				public static void Invoke(ref Mesh.MeshData rawMeshData, ref CommandBuilder draw)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = CommandBuilder.JobWireMesh.Execute_0000021B$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							calli(System.Void(UnityEngine.Mesh/MeshData&,Pathfinding.Drawing.CommandBuilder&), ref rawMeshData, ref draw, functionPointer);
							return;
						}
					}
					CommandBuilder.JobWireMesh.Execute$BurstManaged(ref rawMeshData, ref draw);
				}

				// Token: 0x0400006C RID: 108
				private static IntPtr Pointer;

				// Token: 0x0400006D RID: 109
				private static IntPtr DeferredCompilation;
			}
		}
	}
}
