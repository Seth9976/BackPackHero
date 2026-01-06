using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000057 RID: 87
	[Preserve]
	[ES3Properties(new string[] { "center", "radius", "height", "direction", "enabled", "isTrigger", "contactOffset", "sharedMaterial" })]
	public class ES3Type_CapsuleCollider : ES3ComponentType
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000A7CC File Offset: 0x000089CC
		public ES3Type_CapsuleCollider()
			: base(typeof(CapsuleCollider))
		{
			ES3Type_CapsuleCollider.Instance = this;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A7E4 File Offset: 0x000089E4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			writer.WriteProperty("center", capsuleCollider.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("radius", capsuleCollider.radius, ES3Type_float.Instance);
			writer.WriteProperty("height", capsuleCollider.height, ES3Type_float.Instance);
			writer.WriteProperty("direction", capsuleCollider.direction, ES3Type_int.Instance);
			writer.WriteProperty("enabled", capsuleCollider.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("isTrigger", capsuleCollider.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("contactOffset", capsuleCollider.contactOffset, ES3Type_float.Instance);
			writer.WritePropertyByRef("material", capsuleCollider.sharedMaterial);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2267167091U)
				{
					if (num <= 93078660U)
					{
						if (num != 49525662U)
						{
							if (num == 93078660U)
							{
								if (text == "center")
								{
									capsuleCollider.center = reader.Read<Vector3>(ES3Type_Vector3.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							capsuleCollider.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 230313139U)
					{
						if (num == 2267167091U)
						{
							if (text == "isTrigger")
							{
								capsuleCollider.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "radius")
					{
						capsuleCollider.radius = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3538210912U)
				{
					if (num != 2750587184U)
					{
						if (num == 3538210912U)
						{
							if (text == "material")
							{
								capsuleCollider.sharedMaterial = reader.Read<PhysicMaterial>();
								continue;
							}
						}
					}
					else if (text == "contactOffset")
					{
						capsuleCollider.contactOffset = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3585981250U)
				{
					if (num == 3748513642U)
					{
						if (text == "direction")
						{
							capsuleCollider.direction = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
				}
				else if (text == "height")
				{
					capsuleCollider.height = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000089 RID: 137
		public static ES3Type Instance;
	}
}
