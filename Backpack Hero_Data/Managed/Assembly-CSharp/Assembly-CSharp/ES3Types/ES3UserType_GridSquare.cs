using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001CF RID: 463
	[Preserve]
	[ES3Properties(new string[] { "isRunic", "isPouch", "containsItem", "itemCountsAsEmpty", "itemPouch", "enabled", "name" })]
	public class ES3UserType_GridSquare : ES3ComponentType
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x000A3A29 File Offset: 0x000A1C29
		public ES3UserType_GridSquare()
			: base(typeof(GridSquare))
		{
			ES3UserType_GridSquare.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000A3A48 File Offset: 0x000A1C48
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			GridSquare gridSquare = (GridSquare)obj;
			writer.WriteProperty("isRunic", gridSquare.isRunic, ES3Type_bool.Instance);
			writer.WriteProperty("isPouch", gridSquare.isPouch, ES3Type_bool.Instance);
			writer.WriteProperty("containsItem", gridSquare.containsItem, ES3Type_bool.Instance);
			writer.WriteProperty("itemCountsAsEmpty", gridSquare.itemCountsAsEmpty, ES3Type_bool.Instance);
			writer.WritePropertyByRef("itemPouch", gridSquare.itemPouch);
			writer.WriteProperty("enabled", gridSquare.enabled, ES3Type_bool.Instance);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000A3AF4 File Offset: 0x000A1CF4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			GridSquare gridSquare = (GridSquare)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "isRunic"))
				{
					if (!(text == "isPouch"))
					{
						if (!(text == "containsItem"))
						{
							if (!(text == "itemCountsAsEmpty"))
							{
								if (!(text == "itemPouch"))
								{
									if (!(text == "enabled"))
									{
										reader.Skip();
									}
									else
									{
										gridSquare.enabled = reader.Read<bool>(ES3Type_bool.Instance);
									}
								}
								else
								{
									gridSquare.itemPouch = reader.Read<ItemPouch>(ES3UserType_ItemPouch.Instance);
								}
							}
							else
							{
								gridSquare.itemCountsAsEmpty = reader.Read<bool>(ES3Type_bool.Instance);
							}
						}
						else
						{
							gridSquare.containsItem = reader.Read<bool>(ES3Type_bool.Instance);
						}
					}
					else
					{
						gridSquare.isPouch = reader.Read<bool>(ES3Type_bool.Instance);
					}
				}
				else
				{
					gridSquare.isRunic = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x04000DFB RID: 3579
		public static ES3Type Instance;
	}
}
