using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000095 RID: 149
	[Preserve]
	[ES3Properties(new string[]
	{
		"bounds", "subMeshCount", "boneWeights", "bindposes", "vertices", "normals", "tangents", "uv", "uv2", "uv3",
		"uv4", "colors32", "triangles", "subMeshes"
	})]
	public class ES3Type_Mesh : ES3UnityObjectType
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00017CC0 File Offset: 0x00015EC0
		public ES3Type_Mesh()
			: base(typeof(Mesh))
		{
			ES3Type_Mesh.Instance = this;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00017CD8 File Offset: 0x00015ED8
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Mesh mesh = (Mesh)obj;
			if (!mesh.isReadable)
			{
				ES3Debug.LogWarning("Easy Save cannot save the vertices for this Mesh because it is not marked as readable, so it will be stored by reference. To save the vertex data for this Mesh, check the 'Read/Write Enabled' checkbox in its Import Settings.", mesh, 0);
				return;
			}
			writer.WriteProperty("vertices", mesh.vertices, ES3Type_Vector3Array.Instance);
			writer.WriteProperty("triangles", mesh.triangles, ES3Type_intArray.Instance);
			writer.WriteProperty("bounds", mesh.bounds, ES3Type_Bounds.Instance);
			writer.WriteProperty("boneWeights", mesh.boneWeights, ES3Type_BoneWeightArray.Instance);
			writer.WriteProperty("bindposes", mesh.bindposes, ES3Type_Matrix4x4Array.Instance);
			writer.WriteProperty("normals", mesh.normals, ES3Type_Vector3Array.Instance);
			writer.WriteProperty("tangents", mesh.tangents, ES3Type_Vector4Array.Instance);
			writer.WriteProperty("uv", mesh.uv, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("uv2", mesh.uv2, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("uv3", mesh.uv3, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("uv4", mesh.uv4, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("colors32", mesh.colors32, ES3Type_Color32Array.Instance);
			writer.WriteProperty("subMeshCount", mesh.subMeshCount, ES3Type_int.Instance);
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				writer.WriteProperty("subMesh" + i.ToString(), mesh.GetTriangles(i), ES3Type_intArray.Instance);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00017E60 File Offset: 0x00016060
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Mesh mesh = new Mesh();
			this.ReadUnityObject<T>(reader, mesh);
			return mesh;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00017E7C File Offset: 0x0001607C
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			Mesh mesh = (Mesh)obj;
			if (mesh == null)
			{
				return;
			}
			if (!mesh.isReadable)
			{
				ES3Debug.LogWarning("Easy Save cannot load the vertices for this Mesh because it is not marked as readable, so it will be loaded by reference. To load the vertex data for this Mesh, check the 'Read/Write Enabled' checkbox in its Import Settings.", mesh, 0);
			}
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!mesh.isReadable)
				{
					reader.Skip();
				}
				else
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2323447011U)
					{
						if (num <= 1029089800U)
						{
							if (num != 247908339U)
							{
								if (num != 502968136U)
								{
									if (num == 1029089800U)
									{
										if (text == "bounds")
										{
											mesh.bounds = reader.Read<Bounds>(ES3Type_Bounds.Instance);
											continue;
										}
									}
								}
								else if (text == "boneWeights")
								{
									mesh.boneWeights = reader.Read<BoneWeight[]>(ES3Type_BoneWeightArray.Instance);
									continue;
								}
							}
							else if (text == "normals")
							{
								mesh.normals = reader.Read<Vector3[]>(ES3Type_Vector3Array.Instance);
								continue;
							}
						}
						else if (num != 1229132946U)
						{
							if (num != 2082523534U)
							{
								if (num == 2323447011U)
								{
									if (text == "tangents")
									{
										mesh.tangents = reader.Read<Vector4[]>(ES3Type_Vector4Array.Instance);
										continue;
									}
								}
							}
							else if (text == "vertices")
							{
								mesh.vertices = reader.Read<Vector3[]>(ES3Type_Vector3Array.Instance);
								continue;
							}
						}
						else if (text == "uv")
						{
							mesh.uv = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
							continue;
						}
					}
					else if (num <= 3685293843U)
					{
						if (num != 3226776912U)
						{
							if (num != 3634721602U)
							{
								if (num == 3685293843U)
								{
									if (text == "subMeshCount")
									{
										mesh.subMeshCount = reader.Read<int>(ES3Type_int.Instance);
										for (int i = 0; i < mesh.subMeshCount; i++)
										{
											mesh.SetTriangles(reader.ReadProperty<int[]>(ES3Type_intArray.Instance), i);
										}
										continue;
									}
								}
							}
							else if (text == "triangles")
							{
								mesh.triangles = reader.Read<int[]>(ES3Type_intArray.Instance);
								continue;
							}
						}
						else if (text == "bindposes")
						{
							mesh.bindposes = reader.Read<Matrix4x4[]>(ES3Type_Matrix4x4Array.Instance);
							continue;
						}
					}
					else if (num <= 4103698400U)
					{
						if (num != 4074257916U)
						{
							if (num == 4103698400U)
							{
								if (text == "uv2")
								{
									mesh.uv2 = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
									continue;
								}
							}
						}
						else if (text == "colors32")
						{
							mesh.colors32 = reader.Read<Color32[]>(ES3Type_Color32Array.Instance);
							continue;
						}
					}
					else if (num != 4120476019U)
					{
						if (num == 4204364114U)
						{
							if (text == "uv4")
							{
								mesh.uv4 = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
								continue;
							}
						}
					}
					else if (text == "uv3")
					{
						mesh.uv3 = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
						continue;
					}
					reader.Skip();
				}
			}
		}

		// Token: 0x040000CB RID: 203
		public static ES3Type Instance;
	}
}
