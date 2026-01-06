using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000068 RID: 104
	[Preserve]
	[ES3Properties(new string[] { "center", "radius", "enabled", "isTrigger", "contactOffset", "sharedMaterial" })]
	public class ES3Type_SphereCollider : ES3ComponentType
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000D461 File Offset: 0x0000B661
		public ES3Type_SphereCollider()
			: base(typeof(SphereCollider))
		{
			ES3Type_SphereCollider.Instance = this;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D47C File Offset: 0x0000B67C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SphereCollider sphereCollider = (SphereCollider)obj;
			writer.WriteProperty("center", sphereCollider.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("radius", sphereCollider.radius, ES3Type_float.Instance);
			writer.WriteProperty("enabled", sphereCollider.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("isTrigger", sphereCollider.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("contactOffset", sphereCollider.contactOffset, ES3Type_float.Instance);
			writer.WritePropertyByRef("material", sphereCollider.sharedMaterial);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D528 File Offset: 0x0000B728
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SphereCollider sphereCollider = (SphereCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "center"))
				{
					if (!(text == "radius"))
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
										sphereCollider.sharedMaterial = reader.Read<PhysicMaterial>();
									}
								}
								else
								{
									sphereCollider.contactOffset = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								sphereCollider.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
							}
						}
						else
						{
							sphereCollider.enabled = reader.Read<bool>(ES3Type_bool.Instance);
						}
					}
					else
					{
						sphereCollider.radius = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					sphereCollider.center = reader.Read<Vector3>(ES3Type_Vector3.Instance);
				}
			}
		}

		// Token: 0x0400009A RID: 154
		public static ES3Type Instance;
	}
}
