using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001E1 RID: 481
	[Preserve]
	[ES3Properties(new string[] { "gridParent", "topRenderer", "leftRenderer", "rightRenderer", "bottomRenderer", "leftDecalRenderer", "rightDecalRenderer", "bottomDecalRenderer", "backgroundRenderer" })]
	public class ES3UserType_ModularBackpack : ES3ComponentType
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x000A5965 File Offset: 0x000A3B65
		public ES3UserType_ModularBackpack()
			: base(typeof(ModularBackpack))
		{
			ES3UserType_ModularBackpack.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000A5984 File Offset: 0x000A3B84
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ModularBackpack modularBackpack = (ModularBackpack)obj;
			writer.WritePropertyByRef("gridParent", modularBackpack.gridParent);
			writer.WritePropertyByRef("topRenderer", modularBackpack.topRenderer);
			writer.WritePropertyByRef("leftRenderer", modularBackpack.leftRenderer);
			writer.WritePropertyByRef("rightRenderer", modularBackpack.rightRenderer);
			writer.WritePropertyByRef("bottomRenderer", modularBackpack.bottomRenderer);
			writer.WritePropertyByRef("leftDecalRenderer", modularBackpack.leftDecalRenderer);
			writer.WritePropertyByRef("rightDecalRenderer", modularBackpack.rightDecalRenderer);
			writer.WritePropertyByRef("bottomDecalRenderer", modularBackpack.bottomDecalRenderer);
			writer.WritePropertyByRef("backgroundRenderer", modularBackpack.backgroundRenderer);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000A5A34 File Offset: 0x000A3C34
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ModularBackpack modularBackpack = (ModularBackpack)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1303785659U)
				{
					if (num <= 1034015742U)
					{
						if (num != 670283334U)
						{
							if (num == 1034015742U)
							{
								if (text == "leftDecalRenderer")
								{
									modularBackpack.leftDecalRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
									continue;
								}
							}
						}
						else if (text == "backgroundRenderer")
						{
							modularBackpack.backgroundRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
							continue;
						}
					}
					else if (num != 1242269687U)
					{
						if (num == 1303785659U)
						{
							if (text == "topRenderer")
							{
								modularBackpack.topRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
								continue;
							}
						}
					}
					else if (text == "leftRenderer")
					{
						modularBackpack.leftRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
						continue;
					}
				}
				else if (num <= 2008233208U)
				{
					if (num != 1348788533U)
					{
						if (num == 2008233208U)
						{
							if (text == "bottomDecalRenderer")
							{
								modularBackpack.bottomDecalRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
								continue;
							}
						}
					}
					else if (text == "rightDecalRenderer")
					{
						modularBackpack.rightDecalRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
						continue;
					}
				}
				else if (num != 2073721105U)
				{
					if (num != 2407624958U)
					{
						if (num == 3235292329U)
						{
							if (text == "bottomRenderer")
							{
								modularBackpack.bottomRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
								continue;
							}
						}
					}
					else if (text == "rightRenderer")
					{
						modularBackpack.rightRenderer = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
						continue;
					}
				}
				else if (text == "gridParent")
				{
					modularBackpack.gridParent = reader.Read<Transform>(ES3Type_Transform.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E0D RID: 3597
		public static ES3Type Instance;
	}
}
