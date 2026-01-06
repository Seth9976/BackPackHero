using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200006A RID: 106
	[Preserve]
	[ES3Properties(new string[] { "localPosition", "localRotation", "localScale", "parent", "siblingIndex" })]
	public class ES3Type_Transform : ES3ComponentType
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public ES3Type_Transform()
			: base(typeof(Transform))
		{
			ES3Type_Transform.Instance = this;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000DE94 File Offset: 0x0000C094
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Transform transform = (Transform)obj;
			writer.WritePropertyByRef("parent", transform.parent);
			writer.WriteProperty("localPosition", transform.localPosition);
			writer.WriteProperty("localRotation", transform.localRotation);
			writer.WriteProperty("localScale", transform.localScale);
			writer.WriteProperty("siblingIndex", transform.GetSiblingIndex());
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000DF14 File Offset: 0x0000C114
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Transform transform = (Transform)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "parent"))
				{
					if (!(text == "localPosition"))
					{
						if (!(text == "localRotation"))
						{
							if (!(text == "localScale"))
							{
								if (!(text == "siblingIndex"))
								{
									reader.Skip();
								}
								else
								{
									transform.SetSiblingIndex(reader.Read<int>());
								}
							}
							else
							{
								transform.localScale = reader.Read<Vector3>();
							}
						}
						else
						{
							transform.localRotation = reader.Read<Quaternion>();
						}
					}
					else
					{
						transform.localPosition = reader.Read<Vector3>();
					}
				}
				else
				{
					transform.SetParent(reader.Read<Transform>());
				}
			}
		}

		// Token: 0x0400009C RID: 156
		public static int countRead;

		// Token: 0x0400009D RID: 157
		public static ES3Type Instance;
	}
}
