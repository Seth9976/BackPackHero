using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001B5 RID: 437
	[Preserve]
	[ES3Properties(new string[] { "radius", "density", "isTrigger", "offset", "enabled" })]
	public class ES3UserType_CircleCollider2D : ES3ComponentType
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x000A204D File Offset: 0x000A024D
		public ES3UserType_CircleCollider2D()
			: base(typeof(CircleCollider2D))
		{
			ES3UserType_CircleCollider2D.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000A206C File Offset: 0x000A026C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			CircleCollider2D circleCollider2D = (CircleCollider2D)obj;
			writer.WriteProperty("radius", circleCollider2D.radius, ES3Type_float.Instance);
			writer.WriteProperty("density", circleCollider2D.density, ES3Type_float.Instance);
			writer.WriteProperty("isTrigger", circleCollider2D.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("offset", circleCollider2D.offset, ES3Type_Vector2.Instance);
			writer.WriteProperty("enabled", circleCollider2D.enabled, ES3Type_bool.Instance);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000A2108 File Offset: 0x000A0308
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			CircleCollider2D circleCollider2D = (CircleCollider2D)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "radius"))
				{
					if (!(text == "density"))
					{
						if (!(text == "isTrigger"))
						{
							if (!(text == "offset"))
							{
								if (!(text == "enabled"))
								{
									reader.Skip();
								}
								else
								{
									circleCollider2D.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								}
							}
							else
							{
								circleCollider2D.offset = reader.Read<Vector2>(ES3Type_Vector2.Instance);
							}
						}
						else
						{
							circleCollider2D.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
						}
					}
					else
					{
						circleCollider2D.density = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					circleCollider2D.radius = reader.Read<float>(ES3Type_float.Instance);
				}
			}
		}

		// Token: 0x04000DE1 RID: 3553
		public static ES3Type Instance;
	}
}
