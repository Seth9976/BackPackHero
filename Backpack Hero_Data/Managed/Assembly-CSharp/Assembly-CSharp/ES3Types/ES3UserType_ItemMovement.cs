using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001D5 RID: 469
	[Preserve]
	[ES3Properties(new string[]
	{
		"mouseParticleSystem", "itemBackgroundBordersParent", "itemHighlightBordersParent", "mousePreview", "cardPrefab", "itemBackgroundBorderPrefab", "itemHighlightParentPrefab", "itemHighlightPrefab", "timeToDisplayCard", "inGrid",
		"itemCreationParticlesPrefab", "curseSymbol", "mouseCurseSymbol", "returnsToOutOfInventoryPosition", "outOfInventoryPosition", "outOfInventoryRotation", "seenBefore", "highestLeftGridPosition", "wasRotated", "myParticles",
		"enabled", "name"
	})]
	public class ES3UserType_ItemMovement : ES3ComponentType
	{
		// Token: 0x06001171 RID: 4465 RVA: 0x000A4881 File Offset: 0x000A2A81
		public ES3UserType_ItemMovement()
			: base(typeof(ItemMovement))
		{
			ES3UserType_ItemMovement.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000A48A0 File Offset: 0x000A2AA0
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ItemMovement itemMovement = (ItemMovement)obj;
			writer.WritePropertyByRef("mouseParticleSystem", itemMovement.mouseParticleSystem);
			writer.WritePropertyByRef("itemBackgroundBordersParent", itemMovement.itemBackgroundBordersParent);
			writer.WritePropertyByRef("itemHighlightBordersParent", itemMovement.itemHighlightBordersParent);
			writer.WritePropertyByRef("mousePreview", itemMovement.mousePreview);
			writer.WritePrivateFieldByRef("cardPrefab", itemMovement);
			writer.WritePrivateFieldByRef("itemBackgroundBorderPrefab", itemMovement);
			writer.WritePrivateFieldByRef("itemHighlightParentPrefab", itemMovement);
			writer.WritePrivateFieldByRef("itemHighlightPrefab", itemMovement);
			writer.WritePrivateField("timeToDisplayCard", itemMovement);
			writer.WriteProperty("inGrid", itemMovement.inGrid, ES3Type_bool.Instance);
			writer.WritePrivateFieldByRef("itemCreationParticlesPrefab", itemMovement);
			writer.WritePropertyByRef("curseSymbol", itemMovement.curseSymbol);
			writer.WritePropertyByRef("mouseCurseSymbol", itemMovement.mouseCurseSymbol);
			writer.WriteProperty("returnsToOutOfInventoryPosition", itemMovement.returnsToOutOfInventoryPosition, ES3Type_bool.Instance);
			writer.WriteProperty("outOfInventoryPosition", itemMovement.outOfInventoryPosition, ES3Type_Vector3.Instance);
			writer.WriteProperty("outOfInventoryRotation", itemMovement.outOfInventoryRotation, ES3Type_Quaternion.Instance);
			writer.WriteProperty("seenBefore", itemMovement.seenBefore, ES3Type_bool.Instance);
			writer.WriteProperty("highestLeftGridPosition", itemMovement.highestLeftGridPosition, ES3Type_Vector2.Instance);
			writer.WritePrivateField("wasRotated", itemMovement);
			writer.WritePropertyByRef("myParticles", itemMovement.myParticles);
			writer.WriteProperty("enabled", itemMovement.enabled, ES3Type_bool.Instance);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000A4A3C File Offset: 0x000A2C3C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ItemMovement itemMovement = (ItemMovement)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1946097913U)
				{
					if (num <= 854094369U)
					{
						if (num <= 179522217U)
						{
							if (num != 49525662U)
							{
								if (num == 179522217U)
								{
									if (text == "returnsToOutOfInventoryPosition")
									{
										itemMovement.returnsToOutOfInventoryPosition = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "enabled")
							{
								itemMovement.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 331786073U)
						{
							if (num != 699108745U)
							{
								if (num == 854094369U)
								{
									if (text == "curseSymbol")
									{
										itemMovement.curseSymbol = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
										continue;
									}
								}
							}
							else if (text == "cardPrefab")
							{
								reader.SetPrivateField("cardPrefab", reader.Read<GameObject>(), itemMovement);
								continue;
							}
						}
						else if (text == "highestLeftGridPosition")
						{
							itemMovement.highestLeftGridPosition = reader.Read<Vector2>(ES3Type_Vector2.Instance);
							continue;
						}
					}
					else if (num <= 1435471671U)
					{
						if (num != 952011968U)
						{
							if (num == 1435471671U)
							{
								if (text == "timeToDisplayCard")
								{
									reader.SetPrivateField("timeToDisplayCard", reader.Read<float>(), itemMovement);
									continue;
								}
							}
						}
						else if (text == "itemHighlightParentPrefab")
						{
							reader.SetPrivateField("itemHighlightParentPrefab", reader.Read<GameObject>(), itemMovement);
							continue;
						}
					}
					else if (num != 1707893632U)
					{
						if (num != 1796066010U)
						{
							if (num == 1946097913U)
							{
								if (text == "itemHighlightBordersParent")
								{
									itemMovement.itemHighlightBordersParent = reader.Read<Transform>(ES3Type_Transform.Instance);
									continue;
								}
							}
						}
						else if (text == "myParticles")
						{
							itemMovement.myParticles = reader.Read<GameObject>(ES3Type_GameObject.Instance);
							continue;
						}
					}
					else if (text == "mousePreview")
					{
						itemMovement.mousePreview = reader.Read<Transform>(ES3Type_Transform.Instance);
						continue;
					}
				}
				else if (num <= 3234648278U)
				{
					if (num <= 2553095750U)
					{
						if (num != 2061496439U)
						{
							if (num == 2553095750U)
							{
								if (text == "inGrid")
								{
									itemMovement.inGrid = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "mouseParticleSystem")
						{
							itemMovement.mouseParticleSystem = reader.Read<Transform>(ES3Type_Transform.Instance);
							continue;
						}
					}
					else if (num != 2733702111U)
					{
						if (num != 2808049756U)
						{
							if (num == 3234648278U)
							{
								if (text == "mouseCurseSymbol")
								{
									itemMovement.mouseCurseSymbol = reader.Read<SpriteRenderer>(ES3UserType_SpriteRenderer.Instance);
									continue;
								}
							}
						}
						else if (text == "outOfInventoryRotation")
						{
							itemMovement.outOfInventoryRotation = reader.Read<Quaternion>(ES3Type_Quaternion.Instance);
							continue;
						}
					}
					else if (text == "wasRotated")
					{
						reader.SetPrivateField("wasRotated", reader.Read<bool>(), itemMovement);
						continue;
					}
				}
				else if (num <= 3887635969U)
				{
					if (num != 3501041221U)
					{
						if (num != 3708348152U)
						{
							if (num == 3887635969U)
							{
								if (text == "outOfInventoryPosition")
								{
									itemMovement.outOfInventoryPosition = reader.Read<Vector3>(ES3Type_Vector3.Instance);
									continue;
								}
							}
						}
						else if (text == "itemHighlightPrefab")
						{
							reader.SetPrivateField("itemHighlightPrefab", reader.Read<GameObject>(), itemMovement);
							continue;
						}
					}
					else if (text == "itemBackgroundBordersParent")
					{
						itemMovement.itemBackgroundBordersParent = reader.Read<Transform>(ES3Type_Transform.Instance);
						continue;
					}
				}
				else if (num != 3965602910U)
				{
					if (num != 3980141372U)
					{
						if (num == 4284664885U)
						{
							if (text == "seenBefore")
							{
								itemMovement.seenBefore = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "itemCreationParticlesPrefab")
					{
						reader.SetPrivateField("itemCreationParticlesPrefab", reader.Read<GameObject>(), itemMovement);
						continue;
					}
				}
				else if (text == "itemBackgroundBorderPrefab")
				{
					reader.SetPrivateField("itemBackgroundBorderPrefab", reader.Read<GameObject>(), itemMovement);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E01 RID: 3585
		public static ES3Type Instance;
	}
}
