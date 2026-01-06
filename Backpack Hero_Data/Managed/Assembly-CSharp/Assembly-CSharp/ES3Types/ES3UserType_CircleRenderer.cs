using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001B7 RID: 439
	[Preserve]
	[ES3Properties(new string[] { "radius", "squarePrefab", "col", "waterTile" })]
	public class ES3UserType_CircleRenderer : ES3ComponentType
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x000A222D File Offset: 0x000A042D
		public ES3UserType_CircleRenderer()
			: base(typeof(CircleRenderer))
		{
			ES3UserType_CircleRenderer.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000A224C File Offset: 0x000A044C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			CircleRenderer circleRenderer = (CircleRenderer)obj;
			writer.WritePrivateField("radius", circleRenderer);
			writer.WritePrivateFieldByRef("squarePrefab", circleRenderer);
			writer.WritePrivateFieldByRef("col", circleRenderer);
			writer.WritePrivateFieldByRef("waterTile", circleRenderer);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000A2290 File Offset: 0x000A0490
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			CircleRenderer circleRenderer = (CircleRenderer)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "radius"))
				{
					if (!(text == "squarePrefab"))
					{
						if (!(text == "col"))
						{
							if (!(text == "waterTile"))
							{
								reader.Skip();
							}
							else
							{
								reader.SetPrivateField("waterTile", reader.Read<RuleTile>(), circleRenderer);
							}
						}
						else
						{
							reader.SetPrivateField("col", reader.Read<Collider2D>(), circleRenderer);
						}
					}
					else
					{
						reader.SetPrivateField("squarePrefab", reader.Read<GameObject>(), circleRenderer);
					}
				}
				else
				{
					reader.SetPrivateField("radius", reader.Read<float>(), circleRenderer);
				}
			}
		}

		// Token: 0x04000DE3 RID: 3555
		public static ES3Type Instance;
	}
}
