using System;
using System.Runtime.CompilerServices;
using Pathfinding.Drawing.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x0200004D RID: 77
	[BurstCompile(FloatMode = FloatMode.Default)]
	internal struct GeometryBuilderJob : IJob
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000BB08 File Offset: 0x00009D08
		private unsafe static void Add<[IsUnmanaged] T>(UnsafeAppendBuffer* buffer, T value) where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>();
			*(T*)(buffer->Ptr + buffer->Length) = value;
			buffer->Length = buffer->Length + num;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BB3C File Offset: 0x00009D3C
		private unsafe static void Reserve(UnsafeAppendBuffer* buffer, int size)
		{
			int num = buffer->Length + size;
			if (num > buffer->Capacity)
			{
				buffer->SetCapacity(math.max(num, buffer->Capacity * 2));
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000BB6F File Offset: 0x00009D6F
		internal static float3 PerspectiveDivide(float4 p)
		{
			return p.xyz * math.rcp(p.w);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BB88 File Offset: 0x00009D88
		private unsafe void AddText(ushort* text, CommandBuilder.TextData textData, Color32 color)
		{
			float3 @float = GeometryBuilderJob.PerspectiveDivide(math.mul(this.currentMatrix, new float4(textData.center, 1f)));
			this.AddTextInternal(text, @float, math.mul(this.cameraRotation, new float3(1f, 0f, 0f)), math.mul(this.cameraRotation, new float3(0f, 1f, 0f)), textData.alignment, textData.sizeInPixels, true, textData.numCharacters, color);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000BC10 File Offset: 0x00009E10
		private unsafe void AddText3D(ushort* text, CommandBuilder.TextData3D textData, Color32 color)
		{
			float3 @float = GeometryBuilderJob.PerspectiveDivide(math.mul(this.currentMatrix, new float4(textData.center, 1f)));
			float4x4 float4x = math.mul(this.currentMatrix, new float4x4(textData.rotation, float3.zero));
			this.AddTextInternal(text, @float, float4x.c0.xyz, float4x.c1.xyz, textData.alignment, textData.size, false, textData.numCharacters, color);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BC90 File Offset: 0x00009E90
		private unsafe void AddTextInternal(ushort* text, float3 pivot, float3 right, float3 up, LabelAlignment alignment, float size, bool sizeIsInPixels, int numCharacters, Color32 color)
		{
			float num = math.abs(math.dot(pivot - this.cameraPosition, math.mul(this.cameraRotation, new float3(0f, 0f, 1f))));
			float num2 = this.cameraDepthToPixelSize.x * num + this.cameraDepthToPixelSize.y;
			float num3 = size;
			if (sizeIsInPixels)
			{
				num3 *= num2;
			}
			right *= num3;
			up *= num3;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 1f;
			for (int i = 0; i < numCharacters; i++)
			{
				ushort num7 = text[i];
				if (num7 == 65535)
				{
					num4 = math.max(num4, num5);
					num5 = 0f;
					num6 += 1f;
				}
				else
				{
					num5 += this.characterInfo[num7].advance;
				}
			}
			num4 = math.max(num4, num5);
			float3 @float = pivot - right * num4 * alignment.relativePivot.x;
			float num8 = 1f - num6;
			float num9 = 0.75f;
			float num10 = math.lerp(num8, num9, alignment.relativePivot.y);
			@float -= up * num10;
			@float += math.mul(this.cameraRotation, new float3(1f, 0f, 0f)) * (num2 * alignment.pixelOffset.x);
			@float += math.mul(this.cameraRotation, new float3(0f, 1f, 0f)) * (num2 * alignment.pixelOffset.y);
			UnsafeAppendBuffer* ptr = &this.buffers->textVertices;
			UnsafeAppendBuffer* ptr2 = &this.buffers->textTriangles;
			GeometryBuilderJob.Reserve(ptr, numCharacters * 4 * UnsafeUtility.SizeOf<GeometryBuilderJob.TextVertex>());
			GeometryBuilderJob.Reserve(ptr2, numCharacters * 6 * UnsafeUtility.SizeOf<int>());
			float3 float2 = @float;
			for (int j = 0; j < numCharacters; j++)
			{
				ushort num11 = text[j];
				if (num11 == 65535)
				{
					float2 -= up;
					@float = float2;
				}
				else
				{
					SDFCharacter sdfcharacter = this.characterInfo[num11];
					int num12 = ptr->Length / UnsafeUtility.SizeOf<GeometryBuilderJob.TextVertex>();
					float3 float3 = @float + sdfcharacter.vertexTopLeft.x * right + sdfcharacter.vertexTopLeft.y * up;
					this.minBounds = math.min(this.minBounds, float3);
					this.maxBounds = math.max(this.maxBounds, float3);
					GeometryBuilderJob.Add<GeometryBuilderJob.TextVertex>(ptr, new GeometryBuilderJob.TextVertex
					{
						position = float3,
						uv = sdfcharacter.uvTopLeft,
						color = color
					});
					float3 = @float + sdfcharacter.vertexTopRight.x * right + sdfcharacter.vertexTopRight.y * up;
					this.minBounds = math.min(this.minBounds, float3);
					this.maxBounds = math.max(this.maxBounds, float3);
					GeometryBuilderJob.Add<GeometryBuilderJob.TextVertex>(ptr, new GeometryBuilderJob.TextVertex
					{
						position = float3,
						uv = sdfcharacter.uvTopRight,
						color = color
					});
					float3 = @float + sdfcharacter.vertexBottomRight.x * right + sdfcharacter.vertexBottomRight.y * up;
					this.minBounds = math.min(this.minBounds, float3);
					this.maxBounds = math.max(this.maxBounds, float3);
					GeometryBuilderJob.Add<GeometryBuilderJob.TextVertex>(ptr, new GeometryBuilderJob.TextVertex
					{
						position = float3,
						uv = sdfcharacter.uvBottomRight,
						color = color
					});
					float3 = @float + sdfcharacter.vertexBottomLeft.x * right + sdfcharacter.vertexBottomLeft.y * up;
					this.minBounds = math.min(this.minBounds, float3);
					this.maxBounds = math.max(this.maxBounds, float3);
					GeometryBuilderJob.Add<GeometryBuilderJob.TextVertex>(ptr, new GeometryBuilderJob.TextVertex
					{
						position = float3,
						uv = sdfcharacter.uvBottomLeft,
						color = color
					});
					GeometryBuilderJob.Add<int>(ptr2, num12);
					GeometryBuilderJob.Add<int>(ptr2, num12 + 1);
					GeometryBuilderJob.Add<int>(ptr2, num12 + 2);
					GeometryBuilderJob.Add<int>(ptr2, num12);
					GeometryBuilderJob.Add<int>(ptr2, num12 + 2);
					GeometryBuilderJob.Add<int>(ptr2, num12 + 3);
					@float += right * sdfcharacter.advance;
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C178 File Offset: 0x0000A378
		private unsafe void AddLine(CommandBuilder.LineData line)
		{
			float3 @float = GeometryBuilderJob.PerspectiveDivide(math.mul(this.currentMatrix, new float4(line.a, 1f)));
			float3 float2 = GeometryBuilderJob.PerspectiveDivide(math.mul(this.currentMatrix, new float4(line.b, 1f)));
			float pixels = this.currentLineWidthData.pixels;
			float3 float3 = math.normalizesafe(float2 - @float, default(float3));
			if (math.any(math.isnan(float3)))
			{
				throw new Exception("Nan line coordinates");
			}
			if (pixels <= 0f)
			{
				return;
			}
			this.minBounds = math.min(this.minBounds, math.min(@float, float2));
			this.maxBounds = math.max(this.maxBounds, math.max(@float, float2));
			UnsafeAppendBuffer* ptr = &this.buffers->vertices;
			GeometryBuilderJob.Reserve(ptr, 4 * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>());
			GeometryBuilderJob.Vertex* ptr2 = (GeometryBuilderJob.Vertex*)(ptr->Ptr + ptr->Length);
			float3 float4 = float3 * pixels;
			float3 float5 = float3 * pixels;
			if (pixels > 1f && this.currentLineWidthData.automaticJoins && ptr->Length > 2 * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>())
			{
				GeometryBuilderJob.Vertex* ptr3 = ptr2 - 1;
				GeometryBuilderJob.Vertex* ptr4 = ptr2 - 2;
				float num = math.dot(float3, this.lastNormalizedLineDir);
				if (math.all(ptr4->position == @float) && this.lastLineWidth == pixels && num >= -0.6f)
				{
					float4 = (float3 + this.lastNormalizedLineDir) * pixels / (1f + num);
					ptr3->uv2 = float4;
					ptr4->uv2 = float4;
				}
			}
			ptr->Length = ptr->Length + 4 * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			*(ptr2++) = new GeometryBuilderJob.Vertex
			{
				position = @float,
				color = this.currentColor,
				uv = new float2(0f, 0f),
				uv2 = float4
			};
			*(ptr2++) = new GeometryBuilderJob.Vertex
			{
				position = @float,
				color = this.currentColor,
				uv = new float2(1f, 0f),
				uv2 = float4
			};
			*(ptr2++) = new GeometryBuilderJob.Vertex
			{
				position = float2,
				color = this.currentColor,
				uv = new float2(0f, 1f),
				uv2 = float5
			};
			*(ptr2++) = new GeometryBuilderJob.Vertex
			{
				position = float2,
				color = this.currentColor,
				uv = new float2(1f, 1f),
				uv2 = float5
			};
			this.lastNormalizedLineDir = float3;
			this.lastLineWidth = pixels;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C488 File Offset: 0x0000A688
		internal static int CircleSteps(float3 center, float radius, float maxPixelError, ref float4x4 currentMatrix, float2 cameraDepthToPixelSize, float3 cameraPosition)
		{
			float4 @float = math.mul(currentMatrix, new float4(center, 1f));
			if (math.abs(@float.w) < 1E-07f)
			{
				return 3;
			}
			float3 float2 = GeometryBuilderJob.PerspectiveDivide(@float);
			float num = math.sqrt(math.max(math.max(math.lengthsq(currentMatrix.c0.xyz), math.lengthsq(currentMatrix.c1.xyz)), math.lengthsq(currentMatrix.c2.xyz))) / @float.w;
			float num2 = radius * num;
			float num3 = math.length(float2 - cameraPosition);
			float num4 = cameraDepthToPixelSize.x * num3 + cameraDepthToPixelSize.y;
			float num5 = 1f - maxPixelError * num4 / num2;
			if (num5 >= 0f)
			{
				return (int)math.ceil(3.1415927f / math.acos(num5));
			}
			return 3;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C560 File Offset: 0x0000A760
		private void AddCircle(CommandBuilder.CircleData circle)
		{
			if (math.all(circle.normal == 0f))
			{
				return;
			}
			circle.normal = math.normalize(circle.normal);
			if (circle.normal.y < 0f)
			{
				circle.normal = -circle.normal;
			}
			float3 @float;
			if (math.all(math.abs(circle.normal - new float3(0f, 1f, 0f)) < 0.001f))
			{
				@float = new float3(0f, 0f, 1f);
			}
			else
			{
				@float = math.normalizesafe(math.cross(circle.normal, new float3(0f, 1f, 0f)), default(float3));
			}
			float3 float2 = @float;
			float3 normal = circle.normal;
			float3 float3 = math.cross(normal, float2);
			float4x4 float4x = this.currentMatrix;
			this.currentMatrix = math.mul(this.currentMatrix, new float4x4(new float4(float2, 0f) * circle.radius, new float4(normal, 0f) * circle.radius, new float4(float3, 0f) * circle.radius, new float4(circle.center, 1f)));
			this.AddCircle(new CommandBuilder.CircleXZData
			{
				center = new float3(0f, 0f, 0f),
				radius = 1f,
				startAngle = 0f,
				endAngle = 6.2831855f
			});
			this.currentMatrix = float4x;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C710 File Offset: 0x0000A910
		private unsafe void AddDisc(CommandBuilder.CircleData circle)
		{
			if (math.all(circle.normal == 0f))
			{
				return;
			}
			int num = GeometryBuilderJob.CircleSteps(circle.center, circle.radius, this.maxPixelError, ref this.currentMatrix, this.cameraDepthToPixelSize, this.cameraPosition);
			circle.normal = math.normalize(circle.normal);
			float3 @float;
			if (math.all(math.abs(circle.normal - new float3(0f, 1f, 0f)) < 0.001f))
			{
				@float = new float3(0f, 0f, 1f);
			}
			else
			{
				@float = math.cross(circle.normal, new float3(0f, 1f, 0f));
			}
			float num2 = 1f / (float)num;
			UnsafeAppendBuffer* ptr = &this.buffers->solidVertices;
			UnsafeAppendBuffer* ptr2 = &this.buffers->solidTriangles;
			GeometryBuilderJob.Reserve(ptr, num * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>());
			GeometryBuilderJob.Reserve(ptr2, 3 * (num - 2) * UnsafeUtility.SizeOf<int>());
			float4x4 float4x = math.mul(this.currentMatrix, Matrix4x4.TRS(circle.center, Quaternion.LookRotation(circle.normal, @float), new Vector3(circle.radius, circle.radius, circle.radius)));
			float3 float2 = this.minBounds;
			float3 float3 = this.maxBounds;
			int num3 = ptr->Length / UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			for (int i = 0; i < num; i++)
			{
				float num4;
				float num5;
				math.sincos(math.lerp(0f, 6.2831855f, (float)i * num2), out num4, out num5);
				float3 float4 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(num5, num4, 0f, 1f)));
				float2 = math.min(float2, float4);
				float3 = math.max(float3, float4);
				GeometryBuilderJob.Add<GeometryBuilderJob.Vertex>(ptr, new GeometryBuilderJob.Vertex
				{
					position = float4,
					color = this.currentColor,
					uv = new float2(0f, 0f),
					uv2 = new float3(0f, 0f, 0f)
				});
			}
			this.minBounds = float2;
			this.maxBounds = float3;
			for (int j = 0; j < num - 2; j++)
			{
				GeometryBuilderJob.Add<int>(ptr2, num3);
				GeometryBuilderJob.Add<int>(ptr2, num3 + j + 1);
				GeometryBuilderJob.Add<int>(ptr2, num3 + j + 2);
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C998 File Offset: 0x0000AB98
		private void AddSphereOutline(CommandBuilder.SphereData circle)
		{
			float4 @float = math.mul(this.currentMatrix, new float4(circle.center, 1f));
			if (math.abs(@float.w) < 1E-07f)
			{
				return;
			}
			float3 float2 = GeometryBuilderJob.PerspectiveDivide(@float);
			float num = math.sqrt(math.max(math.max(math.lengthsq(this.currentMatrix.c0.xyz), math.lengthsq(this.currentMatrix.c1.xyz)), math.lengthsq(this.currentMatrix.c2.xyz))) / @float.w;
			float num2 = circle.radius * num;
			if (this.cameraIsOrthographic)
			{
				float4x4 float4x = this.currentMatrix;
				this.currentMatrix = float4x4.identity;
				this.AddCircle(new CommandBuilder.CircleData
				{
					center = float2,
					normal = math.mul(this.cameraRotation, new float3(0f, 0f, 1f)),
					radius = num2
				});
				this.currentMatrix = float4x;
				return;
			}
			float num3 = math.length(this.cameraPosition - float2);
			if (num3 <= num2)
			{
				return;
			}
			float num4 = num2 * num2 / num3;
			float num5 = math.sqrt(num2 * num2 - num4 * num4);
			float3 float3 = math.normalize(this.cameraPosition - float2);
			float4x4 float4x2 = this.currentMatrix;
			this.currentMatrix = float4x4.identity;
			this.AddCircle(new CommandBuilder.CircleData
			{
				center = float2 + float3 * num4,
				normal = float3,
				radius = num5
			});
			this.currentMatrix = float4x2;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		private unsafe void AddCircle(CommandBuilder.CircleXZData circle)
		{
			circle.endAngle = math.clamp(circle.endAngle, circle.startAngle - 6.2831855f, circle.startAngle + 6.2831855f);
			float4x4 float4x = math.mul(this.currentMatrix, new float4x4(new float4(circle.radius, 0f, 0f, 0f), new float4(0f, circle.radius, 0f, 0f), new float4(0f, 0f, circle.radius, 0f), new float4(circle.center, 1f)));
			int num = GeometryBuilderJob.CircleSteps(float3.zero, 1f, this.maxPixelError, ref float4x, this.cameraDepthToPixelSize, this.cameraPosition);
			float pixels = this.currentLineWidthData.pixels;
			if (pixels < 0f)
			{
				return;
			}
			int num2 = num * 4 * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			GeometryBuilderJob.Reserve(&this.buffers->vertices, num2);
			GeometryBuilderJob.Vertex* ptr = (GeometryBuilderJob.Vertex*)(this.buffers->vertices.Ptr + this.buffers->vertices.Length);
			DrawingData.ProcessedBuilderData.MeshBuffers* ptr2 = this.buffers;
			ptr2->vertices.Length = ptr2->vertices.Length + num2;
			float num3;
			float num4;
			math.sincos(circle.startAngle, out num3, out num4);
			float3 @float = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(num4, 0f, num3, 1f)));
			float3 float2 = math.normalizesafe(math.mul(float4x, new float4(-num3, 0f, num4, 0f)).xyz, default(float3)) * pixels;
			float num5 = math.rcp((float)num);
			for (int i = 1; i <= num; i++)
			{
				float num6;
				float num7;
				math.sincos(math.lerp(circle.startAngle, circle.endAngle, (float)i * num5), out num6, out num7);
				float3 float3 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(num7, 0f, num6, 1f)));
				float3 float4 = math.normalizesafe(math.mul(float4x, new float4(-num6, 0f, num7, 0f)).xyz, default(float3)) * pixels;
				*(ptr++) = new GeometryBuilderJob.Vertex
				{
					position = @float,
					color = this.currentColor,
					uv = new float2(0f, 0f),
					uv2 = float2
				};
				*(ptr++) = new GeometryBuilderJob.Vertex
				{
					position = @float,
					color = this.currentColor,
					uv = new float2(1f, 0f),
					uv2 = float2
				};
				*(ptr++) = new GeometryBuilderJob.Vertex
				{
					position = float3,
					color = this.currentColor,
					uv = new float2(0f, 1f),
					uv2 = float4
				};
				*(ptr++) = new GeometryBuilderJob.Vertex
				{
					position = float3,
					color = this.currentColor,
					uv = new float2(1f, 1f),
					uv2 = float4
				};
				@float = float3;
				float2 = float4;
			}
			float3 float5 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(-1f, 0f, 0f, 1f)));
			float3 float6 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(0f, -1f, 0f, 1f)));
			float3 float7 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(1f, 0f, 0f, 1f)));
			float3 float8 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(0f, 1f, 0f, 1f)));
			this.minBounds = math.min(math.min(math.min(math.min(float5, float6), float7), float8), this.minBounds);
			this.maxBounds = math.max(math.max(math.max(math.max(float5, float6), float7), float8), this.maxBounds);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		private unsafe void AddDisc(CommandBuilder.CircleXZData circle)
		{
			int num = GeometryBuilderJob.CircleSteps(circle.center, circle.radius, this.maxPixelError, ref this.currentMatrix, this.cameraDepthToPixelSize, this.cameraPosition);
			circle.endAngle = math.clamp(circle.endAngle, circle.startAngle - 6.2831855f, circle.startAngle + 6.2831855f);
			float num2 = 1f / (float)num;
			UnsafeAppendBuffer* ptr = &this.buffers->solidVertices;
			UnsafeAppendBuffer* ptr2 = &this.buffers->solidTriangles;
			GeometryBuilderJob.Reserve(ptr, (2 + num) * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>());
			GeometryBuilderJob.Reserve(ptr2, 3 * num * UnsafeUtility.SizeOf<int>());
			float4x4 float4x = math.mul(this.currentMatrix, Matrix4x4.Translate(circle.center) * Matrix4x4.Scale(new Vector3(circle.radius, circle.radius, circle.radius)));
			float3 @float = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(0f, 0f, 0f, 1f)));
			GeometryBuilderJob.Add<GeometryBuilderJob.Vertex>(ptr, new GeometryBuilderJob.Vertex
			{
				position = @float,
				color = this.currentColor,
				uv = new float2(0f, 0f),
				uv2 = new float3(0f, 0f, 0f)
			});
			float3 float2 = math.min(this.minBounds, @float);
			float3 float3 = math.max(this.maxBounds, @float);
			int num3 = ptr->Length / UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			for (int i = 0; i <= num; i++)
			{
				float num4;
				float num5;
				math.sincos(math.lerp(circle.startAngle, circle.endAngle, (float)i * num2), out num4, out num5);
				float3 float4 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(num5, 0f, num4, 1f)));
				float2 = math.min(float2, float4);
				float3 = math.max(float3, float4);
				GeometryBuilderJob.Add<GeometryBuilderJob.Vertex>(ptr, new GeometryBuilderJob.Vertex
				{
					position = float4,
					color = this.currentColor,
					uv = new float2(0f, 0f),
					uv2 = new float3(0f, 0f, 0f)
				});
			}
			this.minBounds = float2;
			this.maxBounds = float3;
			for (int j = 0; j < num; j++)
			{
				GeometryBuilderJob.Add<int>(ptr2, num3 - 1);
				GeometryBuilderJob.Add<int>(ptr2, num3 + j);
				GeometryBuilderJob.Add<int>(ptr2, num3 + j + 1);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000D238 File Offset: 0x0000B438
		private unsafe void AddSolidTriangle(CommandBuilder.TriangleData triangle)
		{
			UnsafeAppendBuffer* ptr = &this.buffers->solidVertices;
			UnsafeAppendBuffer* ptr2 = &this.buffers->solidTriangles;
			GeometryBuilderJob.Reserve(ptr, 3 * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>());
			GeometryBuilderJob.Reserve(ptr2, 3 * UnsafeUtility.SizeOf<int>());
			float4x4 float4x = this.currentMatrix;
			float3 @float = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(triangle.a, 1f)));
			float3 float2 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(triangle.b, 1f)));
			float3 float3 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, new float4(triangle.c, 1f)));
			int num = ptr->Length / UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			this.minBounds = math.min(math.min(math.min(this.minBounds, @float), float2), float3);
			this.maxBounds = math.max(math.max(math.max(this.maxBounds, @float), float2), float3);
			GeometryBuilderJob.Add<GeometryBuilderJob.Vertex>(ptr, new GeometryBuilderJob.Vertex
			{
				position = @float,
				color = this.currentColor,
				uv = new float2(0f, 0f),
				uv2 = new float3(0f, 0f, 0f)
			});
			GeometryBuilderJob.Add<GeometryBuilderJob.Vertex>(ptr, new GeometryBuilderJob.Vertex
			{
				position = float2,
				color = this.currentColor,
				uv = new float2(0f, 0f),
				uv2 = new float3(0f, 0f, 0f)
			});
			GeometryBuilderJob.Add<GeometryBuilderJob.Vertex>(ptr, new GeometryBuilderJob.Vertex
			{
				position = float3,
				color = this.currentColor,
				uv = new float2(0f, 0f),
				uv2 = new float3(0f, 0f, 0f)
			});
			GeometryBuilderJob.Add<int>(ptr2, num);
			GeometryBuilderJob.Add<int>(ptr2, num + 1);
			GeometryBuilderJob.Add<int>(ptr2, num + 2);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000D438 File Offset: 0x0000B638
		private void AddWireBox(CommandBuilder.BoxData box)
		{
			float3 @float = box.center - box.size * 0.5f;
			float3 float2 = box.center + box.size * 0.5f;
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(@float.x, @float.y, @float.z),
				b = new float3(float2.x, @float.y, @float.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(float2.x, @float.y, @float.z),
				b = new float3(float2.x, @float.y, float2.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(float2.x, @float.y, float2.z),
				b = new float3(@float.x, @float.y, float2.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(@float.x, @float.y, float2.z),
				b = new float3(@float.x, @float.y, @float.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(@float.x, float2.y, @float.z),
				b = new float3(float2.x, float2.y, @float.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(float2.x, float2.y, @float.z),
				b = new float3(float2.x, float2.y, float2.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(float2.x, float2.y, float2.z),
				b = new float3(@float.x, float2.y, float2.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(@float.x, float2.y, float2.z),
				b = new float3(@float.x, float2.y, @float.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(@float.x, @float.y, @float.z),
				b = new float3(@float.x, float2.y, @float.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(float2.x, @float.y, @float.z),
				b = new float3(float2.x, float2.y, @float.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(float2.x, @float.y, float2.z),
				b = new float3(float2.x, float2.y, float2.z)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(@float.x, @float.y, float2.z),
				b = new float3(@float.x, float2.y, float2.z)
			});
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000D804 File Offset: 0x0000BA04
		private void AddPlane(CommandBuilder.PlaneData plane)
		{
			float4x4 float4x = this.currentMatrix;
			this.currentMatrix = math.mul(this.currentMatrix, float4x4.TRS(plane.center, plane.rotation, new float3(plane.size.x * 0.5f, 1f, plane.size.y * 0.5f)));
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(-1f, 0f, -1f),
				b = new float3(1f, 0f, -1f)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(1f, 0f, -1f),
				b = new float3(1f, 0f, 1f)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(1f, 0f, 1f),
				b = new float3(-1f, 0f, 1f)
			});
			this.AddLine(new CommandBuilder.LineData
			{
				a = new float3(-1f, 0f, 1f),
				b = new float3(-1f, 0f, -1f)
			});
			this.currentMatrix = float4x;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000D984 File Offset: 0x0000BB84
		private unsafe void AddBox(CommandBuilder.BoxData box)
		{
			UnsafeAppendBuffer* ptr = &this.buffers->solidVertices;
			UnsafeAppendBuffer* ptr2 = &this.buffers->solidTriangles;
			GeometryBuilderJob.Reserve(ptr, GeometryBuilderJob.BoxVertices.Length * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>());
			GeometryBuilderJob.Reserve(ptr2, GeometryBuilderJob.BoxTriangles.Length * UnsafeUtility.SizeOf<int>());
			float3 @float = box.size * 0.5f;
			float4x4 float4x = math.mul(this.currentMatrix, new float4x4(new float4(@float.x, 0f, 0f, 0f), new float4(0f, @float.y, 0f, 0f), new float4(0f, 0f, @float.z, 0f), new float4(box.center, 1f)));
			float3 float2 = this.minBounds;
			float3 float3 = this.maxBounds;
			int num = ptr->Length / UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			GeometryBuilderJob.Vertex* ptr3 = (GeometryBuilderJob.Vertex*)(ptr->Ptr + ptr->Length);
			for (int i = 0; i < GeometryBuilderJob.BoxVertices.Length; i++)
			{
				float3 float4 = GeometryBuilderJob.PerspectiveDivide(math.mul(float4x, GeometryBuilderJob.BoxVertices[i]));
				float2 = math.min(float2, float4);
				float3 = math.max(float3, float4);
				*(ptr3++) = new GeometryBuilderJob.Vertex
				{
					position = float4,
					color = this.currentColor,
					uv = new float2(0f, 0f),
					uv2 = new float3(0f, 0f, 0f)
				};
			}
			ptr->Length += GeometryBuilderJob.BoxVertices.Length * UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>();
			this.minBounds = float2;
			this.maxBounds = float3;
			int* ptr4 = (int*)(ptr2->Ptr + ptr2->Length);
			for (int j = 0; j < GeometryBuilderJob.BoxTriangles.Length; j++)
			{
				*(ptr4++) = num + GeometryBuilderJob.BoxTriangles[j];
			}
			ptr2->Length += GeometryBuilderJob.BoxTriangles.Length * UnsafeUtility.SizeOf<int>();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		[MethodImpl(256)]
		public unsafe void Next(ref UnsafeAppendBuffer.Reader reader, ref NativeArray<float4x4> matrixStack, ref NativeArray<Color32> colorStack, ref NativeArray<CommandBuilder.LineWidthData> lineWidthStack, ref int matrixStackSize, ref int colorStackSize, ref int lineWidthStackSize)
		{
			CommandBuilder.Command command = reader.ReadNext<CommandBuilder.Command>();
			CommandBuilder.Command command2 = command & (CommandBuilder.Command)255;
			Color32 color = default(Color32);
			if ((command & CommandBuilder.Command.PushColorInline) != CommandBuilder.Command.PushColor)
			{
				color = this.currentColor;
				this.currentColor = reader.ReadNext<Color32>();
			}
			switch (command2)
			{
			case CommandBuilder.Command.PushColor:
				if (colorStackSize >= colorStack.Length)
				{
					colorStackSize--;
				}
				colorStack[colorStackSize] = this.currentColor;
				colorStackSize++;
				this.currentColor = reader.ReadNext<Color32>();
				break;
			case CommandBuilder.Command.PopColor:
				if (colorStackSize > 0)
				{
					colorStackSize--;
					this.currentColor = colorStack[colorStackSize];
				}
				break;
			case CommandBuilder.Command.PushMatrix:
				if (matrixStackSize >= matrixStack.Length)
				{
					matrixStackSize--;
				}
				matrixStack[matrixStackSize] = this.currentMatrix;
				matrixStackSize++;
				this.currentMatrix = math.mul(this.currentMatrix, reader.ReadNext<float4x4>());
				break;
			case CommandBuilder.Command.PushSetMatrix:
				if (matrixStackSize >= matrixStack.Length)
				{
					matrixStackSize--;
				}
				matrixStack[matrixStackSize] = this.currentMatrix;
				matrixStackSize++;
				this.currentMatrix = reader.ReadNext<float4x4>();
				break;
			case CommandBuilder.Command.PopMatrix:
				if (matrixStackSize > 0)
				{
					matrixStackSize--;
					this.currentMatrix = matrixStack[matrixStackSize];
				}
				break;
			case CommandBuilder.Command.Line:
				this.AddLine(reader.ReadNext<CommandBuilder.LineData>());
				break;
			case CommandBuilder.Command.Circle:
				this.AddCircle(reader.ReadNext<CommandBuilder.CircleData>());
				break;
			case CommandBuilder.Command.CircleXZ:
				this.AddCircle(reader.ReadNext<CommandBuilder.CircleXZData>());
				break;
			case CommandBuilder.Command.Disc:
				this.AddDisc(reader.ReadNext<CommandBuilder.CircleData>());
				break;
			case CommandBuilder.Command.DiscXZ:
				this.AddDisc(reader.ReadNext<CommandBuilder.CircleXZData>());
				break;
			case CommandBuilder.Command.SphereOutline:
				this.AddSphereOutline(reader.ReadNext<CommandBuilder.SphereData>());
				break;
			case CommandBuilder.Command.Box:
				this.AddBox(reader.ReadNext<CommandBuilder.BoxData>());
				break;
			case CommandBuilder.Command.WirePlane:
				this.AddPlane(reader.ReadNext<CommandBuilder.PlaneData>());
				break;
			case CommandBuilder.Command.WireBox:
				this.AddWireBox(reader.ReadNext<CommandBuilder.BoxData>());
				break;
			case CommandBuilder.Command.SolidTriangle:
				this.AddSolidTriangle(reader.ReadNext<CommandBuilder.TriangleData>());
				break;
			case CommandBuilder.Command.PushPersist:
				reader.ReadNext<CommandBuilder.PersistData>();
				break;
			case CommandBuilder.Command.Text:
			{
				CommandBuilder.TextData textData = reader.ReadNext<CommandBuilder.TextData>();
				ushort* ptr = (ushort*)reader.ReadNext(UnsafeUtility.SizeOf<ushort>() * textData.numCharacters);
				this.AddText(ptr, textData, this.currentColor);
				break;
			}
			case CommandBuilder.Command.Text3D:
			{
				CommandBuilder.TextData3D textData3D = reader.ReadNext<CommandBuilder.TextData3D>();
				ushort* ptr2 = (ushort*)reader.ReadNext(UnsafeUtility.SizeOf<ushort>() * textData3D.numCharacters);
				this.AddText3D(ptr2, textData3D, this.currentColor);
				break;
			}
			case CommandBuilder.Command.PushLineWidth:
				if (lineWidthStackSize >= lineWidthStack.Length)
				{
					lineWidthStackSize--;
				}
				lineWidthStack[lineWidthStackSize] = this.currentLineWidthData;
				lineWidthStackSize++;
				this.currentLineWidthData = reader.ReadNext<CommandBuilder.LineWidthData>();
				this.currentLineWidthData.pixels = this.currentLineWidthData.pixels * this.lineWidthMultiplier;
				break;
			case CommandBuilder.Command.PopLineWidth:
				if (lineWidthStackSize > 0)
				{
					lineWidthStackSize--;
					this.currentLineWidthData = lineWidthStack[lineWidthStackSize];
				}
				break;
			case CommandBuilder.Command.CaptureState:
				this.buffers->capturedState.Add<DrawingData.ProcessedBuilderData.CapturedState>(new DrawingData.ProcessedBuilderData.CapturedState
				{
					color = this.currentColor,
					matrix = this.currentMatrix
				});
				break;
			}
			if ((command & CommandBuilder.Command.PushColorInline) != CommandBuilder.Command.PushColor)
			{
				this.currentColor = color;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000DF24 File Offset: 0x0000C124
		private unsafe void CreateTriangles()
		{
			ref UnsafeAppendBuffer ptr = ref *(&this.buffers->vertices);
			UnsafeAppendBuffer* ptr2 = &this.buffers->triangles;
			int num = ptr.Length / UnsafeUtility.SizeOf<GeometryBuilderJob.Vertex>() / 4;
			int num2 = num * 6 * UnsafeUtility.SizeOf<int>();
			if (num2 >= ptr2->Capacity)
			{
				ptr2->SetCapacity(math.ceilpow2(num2));
			}
			int* ptr3 = (int*)ptr2->Ptr;
			int i = 0;
			int num3 = 0;
			while (i < num)
			{
				*(ptr3++) = num3;
				*(ptr3++) = num3 + 1;
				*(ptr3++) = num3 + 2;
				*(ptr3++) = num3 + 1;
				*(ptr3++) = num3 + 3;
				*(ptr3++) = num3 + 2;
				i++;
				num3 += 4;
			}
			ptr2->Length = num2;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public unsafe void Execute()
		{
			this.buffers->vertices.Reset();
			this.buffers->triangles.Reset();
			this.buffers->solidVertices.Reset();
			this.buffers->solidTriangles.Reset();
			this.buffers->textVertices.Reset();
			this.buffers->textTriangles.Reset();
			this.buffers->capturedState.Reset();
			this.currentLineWidthData.pixels = this.currentLineWidthData.pixels * this.lineWidthMultiplier;
			this.minBounds = new float3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			this.maxBounds = new float3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			NativeArray<float4x4> nativeArray = new NativeArray<float4x4>(32, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<Color32> nativeArray2 = new NativeArray<Color32>(32, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<CommandBuilder.LineWidthData> nativeArray3 = new NativeArray<CommandBuilder.LineWidthData>(32, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			UnsafeAppendBuffer.Reader reader = this.buffers->splitterOutput.AsReader();
			while (reader.Offset < reader.Size)
			{
				this.Next(ref reader, ref nativeArray, ref nativeArray2, ref nativeArray3, ref num, ref num2, ref num3);
			}
			this.CreateTriangles();
			Bounds* ptr = &this.buffers->bounds;
			*ptr = new Bounds((this.minBounds + this.maxBounds) * 0.5f, this.maxBounds - this.minBounds);
			if (math.any(math.isnan(ptr->min)) && (this.buffers->vertices.Length > 0 || this.buffers->solidTriangles.Length > 0))
			{
				*ptr = new Bounds(Vector3.zero, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity));
			}
		}

		// Token: 0x0400012A RID: 298
		[NativeDisableUnsafePtrRestriction]
		public unsafe DrawingData.ProcessedBuilderData.MeshBuffers* buffers;

		// Token: 0x0400012B RID: 299
		[NativeDisableUnsafePtrRestriction]
		public unsafe SDFCharacter* characterInfo;

		// Token: 0x0400012C RID: 300
		public int characterInfoLength;

		// Token: 0x0400012D RID: 301
		public Color32 currentColor;

		// Token: 0x0400012E RID: 302
		public float4x4 currentMatrix;

		// Token: 0x0400012F RID: 303
		public CommandBuilder.LineWidthData currentLineWidthData;

		// Token: 0x04000130 RID: 304
		public float lineWidthMultiplier;

		// Token: 0x04000131 RID: 305
		private float3 minBounds;

		// Token: 0x04000132 RID: 306
		private float3 maxBounds;

		// Token: 0x04000133 RID: 307
		public float3 cameraPosition;

		// Token: 0x04000134 RID: 308
		public quaternion cameraRotation;

		// Token: 0x04000135 RID: 309
		public float2 cameraDepthToPixelSize;

		// Token: 0x04000136 RID: 310
		public float maxPixelError;

		// Token: 0x04000137 RID: 311
		public bool cameraIsOrthographic;

		// Token: 0x04000138 RID: 312
		private float3 lastNormalizedLineDir;

		// Token: 0x04000139 RID: 313
		private float lastLineWidth;

		// Token: 0x0400013A RID: 314
		public const float MaxCirclePixelError = 0.5f;

		// Token: 0x0400013B RID: 315
		public const int VerticesPerCharacter = 4;

		// Token: 0x0400013C RID: 316
		public const int TrianglesPerCharacter = 6;

		// Token: 0x0400013D RID: 317
		internal static readonly float4[] BoxVertices = new float4[]
		{
			new float4(-1f, -1f, -1f, 1f),
			new float4(-1f, -1f, 1f, 1f),
			new float4(-1f, 1f, -1f, 1f),
			new float4(-1f, 1f, 1f, 1f),
			new float4(1f, -1f, -1f, 1f),
			new float4(1f, -1f, 1f, 1f),
			new float4(1f, 1f, -1f, 1f),
			new float4(1f, 1f, 1f, 1f)
		};

		// Token: 0x0400013E RID: 318
		internal static readonly int[] BoxTriangles = new int[]
		{
			0, 1, 5, 0, 5, 4, 7, 3, 2, 7,
			2, 6, 0, 1, 3, 0, 3, 2, 4, 5,
			7, 4, 7, 6, 1, 3, 7, 1, 7, 5,
			0, 2, 6, 0, 6, 4
		};

		// Token: 0x0400013F RID: 319
		public const int MaxStackSize = 32;

		// Token: 0x0200004E RID: 78
		public struct Vertex
		{
			// Token: 0x04000140 RID: 320
			public float3 position;

			// Token: 0x04000141 RID: 321
			public float3 uv2;

			// Token: 0x04000142 RID: 322
			public Color32 color;

			// Token: 0x04000143 RID: 323
			public float2 uv;
		}

		// Token: 0x0200004F RID: 79
		public struct TextVertex
		{
			// Token: 0x04000144 RID: 324
			public float3 position;

			// Token: 0x04000145 RID: 325
			public Color32 color;

			// Token: 0x04000146 RID: 326
			public float2 uv;
		}
	}
}
