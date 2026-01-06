using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200021D RID: 541
	[Preserve]
	[ES3Properties(new string[]
	{
		"sprite", "drawMode", "size", "adaptiveModeThreshold", "tileMode", "color", "flipX", "flipY", "enabled", "shadowCastingMode",
		"receiveShadows", "motionVectorGenerationMode", "lightProbeUsage", "reflectionProbeUsage", "sortingLayerName", "sortingLayerID", "sortingOrder", "lightProbeProxyVolumeOverride", "probeAnchor", "lightmapIndex",
		"realtimeLightmapIndex", "lightmapScaleOffset", "realtimeLightmapScaleOffset", "sharedMaterials"
	})]
	public class ES3UserType_SpriteRenderer : ES3ComponentType
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x000A9741 File Offset: 0x000A7941
		public ES3UserType_SpriteRenderer()
			: base(typeof(SpriteRenderer))
		{
			ES3UserType_SpriteRenderer.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000A9760 File Offset: 0x000A7960
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)obj;
			writer.WritePropertyByRef("sprite", spriteRenderer.sprite);
			writer.WriteProperty("drawMode", spriteRenderer.drawMode, ES3TypeMgr.GetOrCreateES3Type(typeof(SpriteDrawMode), true));
			writer.WriteProperty("size", spriteRenderer.size, ES3Type_Vector2.Instance);
			writer.WriteProperty("adaptiveModeThreshold", spriteRenderer.adaptiveModeThreshold, ES3Type_float.Instance);
			writer.WriteProperty("tileMode", spriteRenderer.tileMode, ES3TypeMgr.GetOrCreateES3Type(typeof(SpriteTileMode), true));
			writer.WriteProperty("color", spriteRenderer.color, ES3Type_Color.Instance);
			writer.WriteProperty("flipX", spriteRenderer.flipX, ES3Type_bool.Instance);
			writer.WriteProperty("flipY", spriteRenderer.flipY, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", spriteRenderer.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("shadowCastingMode", spriteRenderer.shadowCastingMode, ES3Type_enum.Instance);
			writer.WriteProperty("receiveShadows", spriteRenderer.receiveShadows, ES3Type_bool.Instance);
			writer.WriteProperty("motionVectorGenerationMode", spriteRenderer.motionVectorGenerationMode, ES3Type_enum.Instance);
			writer.WriteProperty("lightProbeUsage", spriteRenderer.lightProbeUsage, ES3Type_enum.Instance);
			writer.WriteProperty("reflectionProbeUsage", spriteRenderer.reflectionProbeUsage, ES3Type_enum.Instance);
			writer.WriteProperty("sortingLayerName", spriteRenderer.sortingLayerName, ES3Type_string.Instance);
			writer.WriteProperty("sortingLayerID", spriteRenderer.sortingLayerID, ES3Type_int.Instance);
			writer.WriteProperty("sortingOrder", spriteRenderer.sortingOrder, ES3Type_int.Instance);
			writer.WritePropertyByRef("lightProbeProxyVolumeOverride", spriteRenderer.lightProbeProxyVolumeOverride);
			writer.WritePropertyByRef("probeAnchor", spriteRenderer.probeAnchor);
			writer.WriteProperty("lightmapIndex", spriteRenderer.lightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("realtimeLightmapIndex", spriteRenderer.realtimeLightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("lightmapScaleOffset", spriteRenderer.lightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("realtimeLightmapScaleOffset", spriteRenderer.realtimeLightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("sharedMaterials", spriteRenderer.sharedMaterials, ES3Type_MaterialArray.Instance);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000A99EC File Offset: 0x000A7BEC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1435400483U)
				{
					if (num <= 597743964U)
					{
						if (num <= 369730773U)
						{
							if (num != 49525662U)
							{
								if (num != 258071601U)
								{
									if (num == 369730773U)
									{
										if (text == "lightmapIndex")
										{
											spriteRenderer.lightmapIndex = reader.Read<int>(ES3Type_int.Instance);
											continue;
										}
									}
								}
								else if (text == "reflectionProbeUsage")
								{
									spriteRenderer.reflectionProbeUsage = reader.Read<ReflectionProbeUsage>(ES3Type_enum.Instance);
									continue;
								}
							}
							else if (text == "enabled")
							{
								spriteRenderer.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 394058932U)
						{
							if (num != 560621451U)
							{
								if (num == 597743964U)
								{
									if (text == "size")
									{
										spriteRenderer.size = reader.Read<Vector2>(ES3Type_Vector2.Instance);
										continue;
									}
								}
							}
							else if (text == "receiveShadows")
							{
								spriteRenderer.receiveShadows = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (text == "lightmapScaleOffset")
						{
							spriteRenderer.lightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
							continue;
						}
					}
					else if (num <= 1031692888U)
					{
						if (num != 825991302U)
						{
							if (num != 899577978U)
							{
								if (num == 1031692888U)
								{
									if (text == "color")
									{
										spriteRenderer.color = reader.Read<Color>(ES3Type_Color.Instance);
										continue;
									}
								}
							}
							else if (text == "realtimeLightmapIndex")
							{
								spriteRenderer.realtimeLightmapIndex = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
						else if (text == "tileMode")
						{
							spriteRenderer.tileMode = reader.Read<SpriteTileMode>();
							continue;
						}
					}
					else if (num != 1039612288U)
					{
						if (num != 1402830231U)
						{
							if (num == 1435400483U)
							{
								if (text == "sortingLayerName")
								{
									spriteRenderer.sortingLayerName = reader.Read<string>(ES3Type_string.Instance);
									continue;
								}
							}
						}
						else if (text == "adaptiveModeThreshold")
						{
							spriteRenderer.adaptiveModeThreshold = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "probeAnchor")
					{
						spriteRenderer.probeAnchor = reader.Read<Transform>(ES3Type_Transform.Instance);
						continue;
					}
				}
				else if (num <= 2104822524U)
				{
					if (num <= 1681590497U)
					{
						if (num != 1587639706U)
						{
							if (num != 1604417325U)
							{
								if (num == 1681590497U)
								{
									if (text == "sortingOrder")
									{
										spriteRenderer.sortingOrder = reader.Read<int>(ES3Type_int.Instance);
										continue;
									}
								}
							}
							else if (text == "flipY")
							{
								spriteRenderer.flipY = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (text == "flipX")
						{
							spriteRenderer.flipX = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 2056288458U)
					{
						if (num != 2066010489U)
						{
							if (num == 2104822524U)
							{
								if (text == "drawMode")
								{
									spriteRenderer.drawMode = reader.Read<SpriteDrawMode>();
									continue;
								}
							}
						}
						else if (text == "motionVectorGenerationMode")
						{
							spriteRenderer.motionVectorGenerationMode = reader.Read<MotionVectorGenerationMode>(ES3Type_enum.Instance);
							continue;
						}
					}
					else if (text == "sharedMaterials")
					{
						spriteRenderer.sharedMaterials = reader.Read<Material[]>(ES3Type_MaterialArray.Instance);
						continue;
					}
				}
				else if (num <= 3415540015U)
				{
					if (num != 2179094556U)
					{
						if (num != 2844334693U)
						{
							if (num == 3415540015U)
							{
								if (text == "realtimeLightmapScaleOffset")
								{
									spriteRenderer.realtimeLightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
									continue;
								}
							}
						}
						else if (text == "shadowCastingMode")
						{
							spriteRenderer.shadowCastingMode = reader.Read<ShadowCastingMode>(ES3Type_enum.Instance);
							continue;
						}
					}
					else if (text == "sprite")
					{
						spriteRenderer.sprite = reader.Read<Sprite>(ES3Type_Sprite.Instance);
						continue;
					}
				}
				else if (num != 3653878719U)
				{
					if (num != 4124654845U)
					{
						if (num == 4199108346U)
						{
							if (text == "lightProbeUsage")
							{
								spriteRenderer.lightProbeUsage = reader.Read<LightProbeUsage>(ES3Type_enum.Instance);
								continue;
							}
						}
					}
					else if (text == "sortingLayerID")
					{
						spriteRenderer.sortingLayerID = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (text == "lightProbeProxyVolumeOverride")
				{
					spriteRenderer.lightProbeProxyVolumeOverride = reader.Read<GameObject>(ES3Type_GameObject.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E49 RID: 3657
		public static ES3Type Instance;
	}
}
