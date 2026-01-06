using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005B RID: 91
	[Preserve]
	[ES3Properties(new string[] { "sharedMesh", "convex", "inflateMesh", "skinWidth", "enabled", "isTrigger", "contactOffset", "sharedMaterial" })]
	public class ES3Type_MeshCollider : ES3ComponentType
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000B2AD File Offset: 0x000094AD
		public ES3Type_MeshCollider()
			: base(typeof(MeshCollider))
		{
			ES3Type_MeshCollider.Instance = this;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B2C8 File Offset: 0x000094C8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			MeshCollider meshCollider = (MeshCollider)obj;
			writer.WritePropertyByRef("sharedMesh", meshCollider.sharedMesh);
			writer.WriteProperty("convex", meshCollider.convex, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", meshCollider.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("isTrigger", meshCollider.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("contactOffset", meshCollider.contactOffset, ES3Type_float.Instance);
			writer.WriteProperty("material", meshCollider.sharedMaterial);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B36C File Offset: 0x0000956C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			MeshCollider meshCollider = (MeshCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "sharedMesh"))
				{
					if (!(text == "convex"))
					{
						if (!(text == "enabled"))
						{
							if (!(text == "isTrigger"))
							{
								if (!(text == "contactOffset"))
								{
									if (!(text == "material"))
									{
										reader.Skip();
									}
									else
									{
										meshCollider.sharedMaterial = reader.Read<PhysicMaterial>(ES3Type_PhysicMaterial.Instance);
									}
								}
								else
								{
									meshCollider.contactOffset = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								meshCollider.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
							}
						}
						else
						{
							meshCollider.enabled = reader.Read<bool>(ES3Type_bool.Instance);
						}
					}
					else
					{
						meshCollider.convex = reader.Read<bool>(ES3Type_bool.Instance);
					}
				}
				else
				{
					meshCollider.sharedMesh = reader.Read<Mesh>(ES3Type_Mesh.Instance);
				}
			}
		}

		// Token: 0x0400008D RID: 141
		public static ES3Type Instance;
	}
}
