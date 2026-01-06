using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001F1 RID: 497
	[Preserve]
	[ES3Properties(new string[]
	{
		"spacesForExpansion", "tagsForExpansion", "gridCollider", "overworldBlankSquarePrefab", "necessaryPopulationToDestroy", "sfxNameOnPlacement", "resourcesToAddEachRun", "isDraggable", "collider2Ds", "startingEfficiency",
		"oneOfAKind", "costs", "category", "structureTypes", "modifiers", "structureLayer", "gridSize", "populationPrefab", "populationAdd", "itemDestroyParticlePrefab"
	})]
	public class ES3UserType_Overworld_Structure : ES3ComponentType
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x000A6591 File Offset: 0x000A4791
		public ES3UserType_Overworld_Structure()
			: base(typeof(Overworld_Structure))
		{
			ES3UserType_Overworld_Structure.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000A65B0 File Offset: 0x000A47B0
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_Structure overworld_Structure = (Overworld_Structure)obj;
			writer.WriteProperty("spacesForExpansion", overworld_Structure.spacesForExpansion, ES3Type_int.Instance);
			writer.WriteProperty("tagsForExpansion", overworld_Structure.tagsForExpansion, ES3TypeMgr.GetOrCreateES3Type(typeof(List<GridObject.Tag>), true));
			writer.WritePropertyByRef("gridCollider", overworld_Structure.gridCollider);
			writer.WritePrivateFieldByRef("overworldBlankSquarePrefab", overworld_Structure);
			writer.WriteProperty("necessaryPopulationToDestroy", overworld_Structure.necessaryPopulationToDestroy, ES3Type_int.Instance);
			writer.WritePrivateField("sfxNameOnPlacement", overworld_Structure);
			writer.WriteProperty("resourcesToAddEachRun", overworld_Structure.resourcesToAddEachRun, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_ResourceManager.Resource>), true));
			writer.WritePrivateField("isDraggable", overworld_Structure);
			writer.WritePrivateField("collider2Ds", overworld_Structure);
			writer.WriteProperty("startingEfficiency", overworld_Structure.startingEfficiency, ES3Type_float.Instance);
			writer.WriteProperty("oneOfAKind", overworld_Structure.oneOfAKind, ES3Type_bool.Instance);
			writer.WriteProperty("costs", overworld_Structure.costs, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_ResourceManager.Resource>), true));
			writer.WriteProperty("category", overworld_Structure.category, ES3Type_enum.Instance);
			writer.WriteProperty("structureTypes", overworld_Structure.structureTypes, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_Structure.StructureType>), true));
			writer.WriteProperty("modifiers", overworld_Structure.modifiers, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_Structure.Modifier>), true));
			writer.WriteProperty("structureLayer", overworld_Structure.structureLayer, ES3Type_enum.Instance);
			writer.WriteProperty("gridSize", overworld_Structure.gridSize, ES3Type_enum.Instance);
			writer.WritePrivateFieldByRef("populationPrefab", overworld_Structure);
			writer.WriteProperty("populationAdd", overworld_Structure.populationAdd, ES3Type_int.Instance);
			writer.WritePrivateFieldByRef("itemDestroyParticlePrefab", overworld_Structure);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000A679C File Offset: 0x000A499C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_Structure overworld_Structure = (Overworld_Structure)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2421309104U)
				{
					if (num <= 1092292633U)
					{
						if (num <= 337952529U)
						{
							if (num != 759305U)
							{
								if (num == 337952529U)
								{
									if (text == "costs")
									{
										overworld_Structure.costs = reader.Read<List<Overworld_ResourceManager.Resource>>();
										continue;
									}
								}
							}
							else if (text == "populationAdd")
							{
								overworld_Structure.populationAdd = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
						else if (num != 1020667576U)
						{
							if (num != 1048126817U)
							{
								if (num == 1092292633U)
								{
									if (text == "modifiers")
									{
										overworld_Structure.modifiers = reader.Read<List<Overworld_Structure.Modifier>>();
										continue;
									}
								}
							}
							else if (text == "gridCollider")
							{
								overworld_Structure.gridCollider = reader.Read<BoxCollider2D>(ES3Type_BoxCollider2D.Instance);
								continue;
							}
						}
						else if (text == "overworldBlankSquarePrefab")
						{
							reader.SetPrivateField("overworldBlankSquarePrefab", reader.Read<GameObject>(), overworld_Structure);
							continue;
						}
					}
					else if (num <= 1210003952U)
					{
						if (num != 1110694949U)
						{
							if (num == 1210003952U)
							{
								if (text == "itemDestroyParticlePrefab")
								{
									reader.SetPrivateField("itemDestroyParticlePrefab", reader.Read<GameObject>(), overworld_Structure);
									continue;
								}
							}
						}
						else if (text == "sfxNameOnPlacement")
						{
							reader.SetPrivateField("sfxNameOnPlacement", reader.Read<string>(), overworld_Structure);
							continue;
						}
					}
					else if (num != 1522109160U)
					{
						if (num != 1878635924U)
						{
							if (num == 2421309104U)
							{
								if (text == "populationPrefab")
								{
									reader.SetPrivateField("populationPrefab", reader.Read<GameObject>(), overworld_Structure);
									continue;
								}
							}
						}
						else if (text == "startingEfficiency")
						{
							overworld_Structure.startingEfficiency = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "tagsForExpansion")
					{
						overworld_Structure.tagsForExpansion = reader.Read<List<GridObject.Tag>>();
						continue;
					}
				}
				else if (num <= 3294167814U)
				{
					if (num <= 2792147256U)
					{
						if (num != 2780227371U)
						{
							if (num == 2792147256U)
							{
								if (text == "collider2Ds")
								{
									reader.SetPrivateField("collider2Ds", reader.Read<List<Collider2D>>(), overworld_Structure);
									continue;
								}
							}
						}
						else if (text == "structureLayer")
						{
							overworld_Structure.structureLayer = reader.Read<Overworld_Structure.StructureLayer>(ES3Type_enum.Instance);
							continue;
						}
					}
					else if (num != 2878726232U)
					{
						if (num != 3264274788U)
						{
							if (num == 3294167814U)
							{
								if (text == "spacesForExpansion")
								{
									overworld_Structure.spacesForExpansion = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "isDraggable")
						{
							reader.SetPrivateField("isDraggable", reader.Read<bool>(), overworld_Structure);
							continue;
						}
					}
					else if (text == "resourcesToAddEachRun")
					{
						overworld_Structure.resourcesToAddEachRun = reader.Read<List<Overworld_ResourceManager.Resource>>();
						continue;
					}
				}
				else if (num <= 3475980913U)
				{
					if (num != 3301386160U)
					{
						if (num == 3475980913U)
						{
							if (text == "category")
							{
								overworld_Structure.category = reader.Read<Overworld_BuildingManager.BuildingCategory>(ES3Type_enum.Instance);
								continue;
							}
						}
					}
					else if (text == "necessaryPopulationToDestroy")
					{
						overworld_Structure.necessaryPopulationToDestroy = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num != 3627492160U)
				{
					if (num != 3990779711U)
					{
						if (num == 4169974103U)
						{
							if (text == "oneOfAKind")
							{
								overworld_Structure.oneOfAKind = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "structureTypes")
					{
						overworld_Structure.structureTypes = reader.Read<List<Overworld_Structure.StructureType>>();
						continue;
					}
				}
				else if (text == "gridSize")
				{
					overworld_Structure.gridSize = reader.Read<Overworld_Structure.GridSize>(ES3Type_enum.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E1D RID: 3613
		public static ES3Type Instance;
	}
}
