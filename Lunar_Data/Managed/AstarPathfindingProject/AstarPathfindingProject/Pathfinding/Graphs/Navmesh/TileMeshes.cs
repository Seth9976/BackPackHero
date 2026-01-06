using System;
using System.IO;
using System.IO.Compression;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001C9 RID: 457
	public struct TileMeshes
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x00045CCC File Offset: 0x00043ECC
		public void Rotate(int rotation)
		{
			rotation = -rotation;
			rotation = (rotation % 4 + 4) % 4;
			if (rotation == 0)
			{
				return;
			}
			int2x2 int2x = new int2x2(0, -1, 1, 0);
			int2x2 int2x2 = int2x2.identity;
			for (int i = 0; i < rotation; i++)
			{
				int2x2 = math.mul(int2x2, int2x);
			}
			Int3 @int = (Int3)new Vector3(this.tileWorldSize.x, 0f, this.tileWorldSize.y);
			int2 int2 = -math.min(int2.zero, math.mul(int2x2, new int2(@int.x, @int.z)));
			int2 int3 = new int2(this.tileRect.Width, this.tileRect.Height);
			int2 int4 = -math.min(int2.zero, math.mul(int2x2, int3 - 1));
			TileMesh[] array = new TileMesh[this.tileMeshes.Length];
			int2 int5 = ((rotation % 2 == 0) ? int3 : new int2(int3.y, int3.x));
			for (int j = 0; j < int3.y; j++)
			{
				for (int k = 0; k < int3.x; k++)
				{
					Int3[] verticesInTileSpace = this.tileMeshes[k + j * int3.x].verticesInTileSpace;
					for (int l = 0; l < verticesInTileSpace.Length; l++)
					{
						Int3 int6 = verticesInTileSpace[l];
						int2 int7 = math.mul(int2x2, new int2(int6.x, int6.z)) + int2;
						verticesInTileSpace[l] = new Int3(int7.x, int6.y, int7.y);
					}
					int2 int8 = math.mul(int2x2, new int2(k, j)) + int4;
					array[int8.x + int8.y * int5.x] = this.tileMeshes[k + j * int3.x];
				}
			}
			this.tileMeshes = array;
			this.tileWorldSize = ((rotation % 2 == 0) ? this.tileWorldSize : new Vector2(this.tileWorldSize.y, this.tileWorldSize.x));
			this.tileRect = new IntRect(this.tileRect.xmin, this.tileRect.ymin, this.tileRect.xmin + int5.x - 1, this.tileRect.ymin + int5.y - 1);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00045F54 File Offset: 0x00044154
		public byte[] Serialize()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(new DeflateStream(memoryStream, CompressionMode.Compress));
			binaryWriter.Write(0);
			binaryWriter.Write(this.tileRect.Width);
			binaryWriter.Write(this.tileRect.Height);
			binaryWriter.Write(this.tileWorldSize.x);
			binaryWriter.Write(this.tileWorldSize.y);
			for (int i = 0; i < this.tileRect.Height; i++)
			{
				for (int j = 0; j < this.tileRect.Width; j++)
				{
					TileMesh tileMesh = this.tileMeshes[i * this.tileRect.Width + j];
					binaryWriter.Write(tileMesh.triangles.Length);
					binaryWriter.Write(tileMesh.verticesInTileSpace.Length);
					for (int k = 0; k < tileMesh.verticesInTileSpace.Length; k++)
					{
						Int3 @int = tileMesh.verticesInTileSpace[k];
						binaryWriter.Write(@int.x);
						binaryWriter.Write(@int.y);
						binaryWriter.Write(@int.z);
					}
					for (int l = 0; l < tileMesh.triangles.Length; l++)
					{
						binaryWriter.Write(tileMesh.triangles[l]);
					}
					for (int m = 0; m < tileMesh.tags.Length; m++)
					{
						binaryWriter.Write(tileMesh.tags[m]);
					}
				}
			}
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000460DC File Offset: 0x000442DC
		public static TileMeshes Deserialize(byte[] bytes)
		{
			BinaryReader binaryReader = new BinaryReader(new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress));
			if (binaryReader.ReadInt32() != 0)
			{
				throw new Exception("Invalid data. Unexpected version number.");
			}
			int num = binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			Vector2 vector = new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
			if (num < 0 || num2 < 0)
			{
				throw new Exception("Invalid bounds");
			}
			IntRect intRect = new IntRect(0, 0, num - 1, num2 - 1);
			TileMesh[] array = new TileMesh[num * num2];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int[] array2 = new int[binaryReader.ReadInt32()];
					Int3[] array3 = new Int3[binaryReader.ReadInt32()];
					uint[] array4 = new uint[array2.Length / 3];
					for (int k = 0; k < array3.Length; k++)
					{
						array3[k] = new Int3(binaryReader.ReadInt32(), binaryReader.ReadInt32(), binaryReader.ReadInt32());
					}
					for (int l = 0; l < array2.Length; l++)
					{
						array2[l] = binaryReader.ReadInt32();
					}
					for (int m = 0; m < array4.Length; m++)
					{
						array4[m] = binaryReader.ReadUInt32();
					}
					array[j + i * num] = new TileMesh
					{
						triangles = array2,
						verticesInTileSpace = array3,
						tags = array4
					};
				}
			}
			return new TileMeshes
			{
				tileMeshes = array,
				tileRect = intRect,
				tileWorldSize = vector
			};
		}

		// Token: 0x04000863 RID: 2147
		public TileMesh[] tileMeshes;

		// Token: 0x04000864 RID: 2148
		public IntRect tileRect;

		// Token: 0x04000865 RID: 2149
		public Vector2 tileWorldSize;
	}
}
