using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200006E RID: 110
	public class ObjImporter
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00022ABC File Offset: 0x00020CBC
		public static Mesh ImportFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Debug.LogError("No file was found at '" + filePath + "'");
				return null;
			}
			ObjImporter.meshStruct meshStruct = ObjImporter.createMeshStruct(filePath);
			ObjImporter.populateMeshStruct(ref meshStruct);
			Vector3[] array = new Vector3[meshStruct.faceData.Length];
			Vector2[] array2 = new Vector2[meshStruct.faceData.Length];
			Vector3[] array3 = new Vector3[meshStruct.faceData.Length];
			int num = 0;
			foreach (Vector3 vector in meshStruct.faceData)
			{
				array[num] = meshStruct.vertices[(int)vector.x - 1];
				if (vector.y >= 1f)
				{
					array2[num] = meshStruct.uv[(int)vector.y - 1];
				}
				if (vector.z >= 1f)
				{
					array3[num] = meshStruct.normals[(int)vector.z - 1];
				}
				num++;
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.uv = array2;
			mesh.normals = array3;
			mesh.triangles = meshStruct.triangles;
			mesh.RecalculateBounds();
			return mesh;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00022BF4 File Offset: 0x00020DF4
		private static ObjImporter.meshStruct createMeshStruct(string filename)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			ObjImporter.meshStruct meshStruct = default(ObjImporter.meshStruct);
			meshStruct.fileName = filename;
			StreamReader streamReader = File.OpenText(filename);
			string text = streamReader.ReadToEnd();
			streamReader.Dispose();
			using (StringReader stringReader = new StringReader(text))
			{
				string text2 = stringReader.ReadLine();
				char[] array = new char[] { ' ' };
				while (text2 != null)
				{
					if (!text2.StartsWith("f ") && !text2.StartsWith("v ") && !text2.StartsWith("vt ") && !text2.StartsWith("vn "))
					{
						text2 = stringReader.ReadLine();
						if (text2 != null)
						{
							text2 = text2.Replace("  ", " ");
						}
					}
					else
					{
						text2 = text2.Trim();
						string[] array2 = text2.Split(array, 50);
						string text3 = array2[0];
						if (!(text3 == "v"))
						{
							if (!(text3 == "vt"))
							{
								if (!(text3 == "vn"))
								{
									if (text3 == "f")
									{
										num5 = num5 + array2.Length - 1;
										num += 3 * (array2.Length - 2);
									}
								}
								else
								{
									num4++;
								}
							}
							else
							{
								num3++;
							}
						}
						else
						{
							num2++;
						}
						text2 = stringReader.ReadLine();
						if (text2 != null)
						{
							text2 = text2.Replace("  ", " ");
						}
					}
				}
			}
			meshStruct.triangles = new int[num];
			meshStruct.vertices = new Vector3[num2];
			meshStruct.uv = new Vector2[num3];
			meshStruct.normals = new Vector3[num4];
			meshStruct.faceData = new Vector3[num5];
			return meshStruct;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00022DC8 File Offset: 0x00020FC8
		private static void populateMeshStruct(ref ObjImporter.meshStruct mesh)
		{
			StreamReader streamReader = File.OpenText(mesh.fileName);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			using (StringReader stringReader = new StringReader(text))
			{
				string text2 = stringReader.ReadLine();
				char[] array = new char[] { ' ' };
				char[] array2 = new char[] { '/' };
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				while (text2 != null)
				{
					if (!text2.StartsWith("f ") && !text2.StartsWith("v ") && !text2.StartsWith("vt ") && !text2.StartsWith("vn ") && !text2.StartsWith("g ") && !text2.StartsWith("usemtl ") && !text2.StartsWith("mtllib ") && !text2.StartsWith("vt1 ") && !text2.StartsWith("vt2 ") && !text2.StartsWith("vc ") && !text2.StartsWith("usemap "))
					{
						text2 = stringReader.ReadLine();
						if (text2 != null)
						{
							text2 = text2.Replace("  ", " ");
						}
					}
					else
					{
						text2 = text2.Trim();
						string[] array3 = text2.Split(array, 50);
						string text3 = array3[0];
						uint num8 = <PrivateImplementationDetails>.ComputeStringHash(text3);
						if (num8 <= 1179241374U)
						{
							if (num8 <= 1128908517U)
							{
								if (num8 != 990293175U)
								{
									if (num8 == 1128908517U)
									{
										if (text3 == "vn")
										{
											mesh.normals[num4] = new Vector3(Convert.ToSingle(array3[1]), Convert.ToSingle(array3[2]), Convert.ToSingle(array3[3]));
											num4++;
										}
									}
								}
								else if (!(text3 == "mtllib"))
								{
								}
							}
							else if (num8 != 1146808303U)
							{
								if (num8 != 1163585922U)
								{
									if (num8 == 1179241374U)
									{
										if (!(text3 == "vc"))
										{
										}
									}
								}
								else if (text3 == "vt1")
								{
									mesh.uv[num6] = new Vector2(Convert.ToSingle(array3[1]), Convert.ToSingle(array3[2]));
									num6++;
								}
							}
							else if (text3 == "vt2")
							{
								mesh.uv[num7] = new Vector2(Convert.ToSingle(array3[1]), Convert.ToSingle(array3[2]));
								num7++;
							}
						}
						else if (num8 <= 1498016135U)
						{
							if (num8 != 1297068826U)
							{
								if (num8 != 1328799683U)
								{
									if (num8 == 1498016135U)
									{
										if (text3 == "vt")
										{
											mesh.uv[num5] = new Vector2(Convert.ToSingle(array3[1]), Convert.ToSingle(array3[2]));
											num5++;
										}
									}
								}
								else if (!(text3 == "usemtl"))
								{
								}
							}
							else if (!(text3 == "usemap"))
							{
							}
						}
						else if (num8 != 3792446982U)
						{
							if (num8 != 3809224601U)
							{
								if (num8 == 4077666505U)
								{
									if (text3 == "v")
									{
										mesh.vertices[num3] = new Vector3(Convert.ToSingle(array3[1]), Convert.ToSingle(array3[2]), Convert.ToSingle(array3[3]));
										num3++;
									}
								}
							}
							else if (text3 == "f")
							{
								int num9 = 1;
								List<int> list = new List<int>();
								while (num9 < array3.Length && (array3[num9] ?? "").Length > 0)
								{
									Vector3 vector = default(Vector3);
									string[] array4 = array3[num9].Split(array2, 3);
									vector.x = (float)Convert.ToInt32(array4[0]);
									if (array4.Length > 1)
									{
										if (array4[1] != "")
										{
											vector.y = (float)Convert.ToInt32(array4[1]);
										}
										vector.z = (float)Convert.ToInt32(array4[2]);
									}
									num9++;
									mesh.faceData[num2] = vector;
									list.Add(num2);
									num2++;
								}
								num9 = 1;
								while (num9 + 2 < array3.Length)
								{
									mesh.triangles[num] = list[0];
									num++;
									mesh.triangles[num] = list[num9];
									num++;
									mesh.triangles[num] = list[num9 + 1];
									num++;
									num9++;
								}
							}
						}
						else if (!(text3 == "g"))
						{
						}
						text2 = stringReader.ReadLine();
						if (text2 != null)
						{
							text2 = text2.Replace("  ", " ");
						}
					}
				}
			}
		}

		// Token: 0x0200012E RID: 302
		private struct meshStruct
		{
			// Token: 0x040006F9 RID: 1785
			public Vector3[] vertices;

			// Token: 0x040006FA RID: 1786
			public Vector3[] normals;

			// Token: 0x040006FB RID: 1787
			public Vector2[] uv;

			// Token: 0x040006FC RID: 1788
			public int[] triangles;

			// Token: 0x040006FD RID: 1789
			public Vector3[] faceData;

			// Token: 0x040006FE RID: 1790
			public string fileName;
		}
	}
}
