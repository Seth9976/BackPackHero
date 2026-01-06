using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000054 RID: 84
	[Preserve]
	[ES3Properties(new string[] { "center", "size", "enabled", "isTrigger", "contactOffset", "sharedMaterial" })]
	public class ES3Type_BoxCollider : ES3ComponentType
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000999B File Offset: 0x00007B9B
		public ES3Type_BoxCollider()
			: base(typeof(BoxCollider))
		{
			ES3Type_BoxCollider.Instance = this;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000099B4 File Offset: 0x00007BB4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			BoxCollider boxCollider = (BoxCollider)obj;
			writer.WriteProperty("center", boxCollider.center);
			writer.WriteProperty("size", boxCollider.size);
			writer.WriteProperty("enabled", boxCollider.enabled);
			writer.WriteProperty("isTrigger", boxCollider.isTrigger);
			writer.WriteProperty("contactOffset", boxCollider.contactOffset);
			writer.WritePropertyByRef("material", boxCollider.sharedMaterial);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009A48 File Offset: 0x00007C48
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			BoxCollider boxCollider = (BoxCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "center"))
				{
					if (!(text == "size"))
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
										boxCollider.sharedMaterial = reader.Read<PhysicMaterial>();
									}
								}
								else
								{
									boxCollider.contactOffset = reader.Read<float>();
								}
							}
							else
							{
								boxCollider.isTrigger = reader.Read<bool>();
							}
						}
						else
						{
							boxCollider.enabled = reader.Read<bool>();
						}
					}
					else
					{
						boxCollider.size = reader.Read<Vector3>();
					}
				}
				else
				{
					boxCollider.center = reader.Read<Vector3>();
				}
			}
		}

		// Token: 0x04000086 RID: 134
		public static ES3Type Instance;
	}
}
