using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000080 RID: 128
	[Preserve]
	[ES3Properties(new string[] { "layer", "isStatic", "tag", "name", "hideFlags", "children", "components" })]
	public class ES3Type_GameObject : ES3UnityObjectType
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000F7E9 File Offset: 0x0000D9E9
		public ES3Type_GameObject()
			: base(typeof(GameObject))
		{
			ES3Type_GameObject.Instance = this;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000F804 File Offset: 0x0000DA04
		public override void WriteObject(object obj, ES3Writer writer, ES3.ReferenceMode mode)
		{
			if (base.WriteUsingDerivedType(obj, writer))
			{
				return;
			}
			GameObject gameObject = (GameObject)obj;
			if (mode != ES3.ReferenceMode.ByValue)
			{
				writer.WriteRef(gameObject);
				if (mode == ES3.ReferenceMode.ByRef)
				{
					return;
				}
				ES3Prefab component = gameObject.GetComponent<ES3Prefab>();
				if (component != null)
				{
					writer.WriteProperty("es3Prefab", component, ES3Type_ES3PrefabInternal.Instance);
				}
				writer.WriteProperty("transformID", ES3ReferenceMgrBase.Current.Add(gameObject.transform));
			}
			ES3AutoSave component2 = gameObject.GetComponent<ES3AutoSave>();
			if (component2 == null || component2.saveLayer)
			{
				writer.WriteProperty("layer", gameObject.layer, ES3Type_int.Instance);
			}
			if (component2 == null || component2.saveTag)
			{
				writer.WriteProperty("tag", gameObject.tag, ES3Type_string.Instance);
			}
			if (component2 == null || component2.saveName)
			{
				writer.WriteProperty("name", gameObject.name, ES3Type_string.Instance);
			}
			if (component2 == null || component2.saveHideFlags)
			{
				writer.WriteProperty("hideFlags", gameObject.hideFlags);
			}
			if (component2 == null || component2.saveActive)
			{
				writer.WriteProperty("active", gameObject.activeSelf);
			}
			if ((component2 == null && this.saveChildren) || (component2 != null && component2.saveChildren))
			{
				writer.WriteProperty("children", ES3Type_GameObject.GetChildren(gameObject), ES3.ReferenceMode.ByRefAndValue);
			}
			ES3AutoSave component3 = gameObject.GetComponent<ES3AutoSave>();
			List<Component> list;
			if (component3 != null && component3.componentsToSave != null && component3.componentsToSave.Count > 0)
			{
				list = component3.componentsToSave;
			}
			else
			{
				list = new List<Component>();
				foreach (Component component4 in gameObject.GetComponents<Component>())
				{
					if (component4 != null && ES3TypeMgr.GetES3Type(component4.GetType()) != null)
					{
						list.Add(component4);
					}
				}
			}
			writer.WriteProperty("components", list, ES3.ReferenceMode.ByRefAndValue);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000FA00 File Offset: 0x0000DC00
		protected override object ReadObject<T>(ES3Reader reader)
		{
			Object @object = null;
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			long num = 0L;
			while (!(es3ReferenceMgrBase == null))
			{
				string text = base.ReadPropertyName(reader);
				if (text == "__type")
				{
					return ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).Read<T>(reader);
				}
				if (text == "_ES3Ref")
				{
					num = reader.Read_ref();
					@object = es3ReferenceMgrBase.Get(num, true);
				}
				else if (text == "transformID")
				{
					long num2 = reader.Read_ref();
					if (@object == null)
					{
						@object = this.CreateNewGameObject(es3ReferenceMgrBase, num);
					}
					es3ReferenceMgrBase.Add(((GameObject)@object).transform, num2);
				}
				else if (text == "es3Prefab")
				{
					if (@object != null || ES3ReferenceMgrBase.Current == null)
					{
						reader.ReadInto<GameObject>(@object);
					}
					else
					{
						@object = reader.Read<GameObject>(ES3Type_ES3PrefabInternal.Instance);
						ES3ReferenceMgrBase.Current.Add(@object, num);
					}
				}
				else
				{
					if (text == null)
					{
						return @object;
					}
					reader.overridePropertiesName = text;
					if (@object == null)
					{
						@object = this.CreateNewGameObject(es3ReferenceMgrBase, num);
					}
					this.ReadInto<T>(reader, @object);
					return @object;
				}
			}
			throw new InvalidOperationException("An Easy Save 3 Manager is required to load references. To add one to your scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene");
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000FB28 File Offset: 0x0000DD28
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			GameObject gameObject = (GameObject)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1739180498U)
				{
					if (num <= 128336118U)
					{
						if (num != 36885379U)
						{
							if (num == 128336118U)
							{
								if (text == "layer")
								{
									gameObject.layer = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "prefab")
						{
							continue;
						}
					}
					else if (num != 469045609U)
					{
						if (num == 1739180498U)
						{
							if (text == "children")
							{
								reader.Read<GameObject[]>();
								continue;
							}
						}
					}
					else if (text == "components")
					{
						this.ReadComponents(reader, gameObject);
						continue;
					}
				}
				else if (num <= 2516003219U)
				{
					if (num != 2369371622U)
					{
						if (num == 2516003219U)
						{
							if (text == "tag")
							{
								gameObject.tag = reader.Read<string>(ES3Type_string.Instance);
								continue;
							}
						}
					}
					else if (text == "name")
					{
						gameObject.name = reader.Read<string>(ES3Type_string.Instance);
						continue;
					}
				}
				else if (num != 3043476896U)
				{
					if (num != 3648362799U)
					{
						if (num == 3944566772U)
						{
							if (text == "hideFlags")
							{
								gameObject.hideFlags = reader.Read<HideFlags>();
								continue;
							}
						}
					}
					else if (text == "active")
					{
						gameObject.SetActive(reader.Read<bool>(ES3Type_bool.Instance));
						continue;
					}
				}
				else if (text == "_ES3Ref")
				{
					ES3ReferenceMgrBase.Current.Add(gameObject, reader.Read_ref());
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000FD64 File Offset: 0x0000DF64
		private void ReadComponents(ES3Reader reader, GameObject go)
		{
			if (reader.StartReadCollection())
			{
				return;
			}
			List<Component> list = new List<Component>(go.GetComponents<Component>());
			for (;;)
			{
				if (!reader.StartReadCollectionItem())
				{
					goto IL_0146;
				}
				if (reader.StartReadObject())
				{
					break;
				}
				Type type = null;
				string text;
				for (;;)
				{
					text = base.ReadPropertyName(reader);
					if (!(text == "__type"))
					{
						break;
					}
					type = reader.ReadType();
				}
				if (text == "_ES3Ref")
				{
					if (type == null)
					{
						goto Block_6;
					}
					long num = reader.Read_ref();
					List<Component> list2 = list;
					Predicate<Component> predicate;
					Predicate<Component> <>9__0;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = (Component x) => x.GetType() == type);
					}
					Component component = list2.Find(predicate);
					if (component != null)
					{
						if (ES3ReferenceMgrBase.Current != null)
						{
							ES3ReferenceMgrBase.Current.Add(component, num);
						}
						ES3TypeMgr.GetOrCreateES3Type(type, true).ReadInto<Component>(reader, component);
						list.Remove(component);
					}
					else
					{
						object obj = ES3TypeMgr.GetOrCreateES3Type(type, true).Read<Component>(reader);
						if (obj != null)
						{
							ES3ReferenceMgrBase.Current.Add((Component)obj, num);
						}
					}
				}
				else if (text != null)
				{
					reader.overridePropertiesName = text;
					this.ReadObject<Component>(reader);
				}
				reader.EndReadObject();
				if (reader.EndReadCollectionItem())
				{
					goto IL_0146;
				}
			}
			return;
			Block_6:
			throw new InvalidOperationException("Cannot load Component because no type data has been stored with it, so it's not possible to determine it's type");
			IL_0146:
			reader.EndReadCollection();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		private GameObject CreateNewGameObject(ES3ReferenceMgrBase refMgr, long id)
		{
			GameObject gameObject = new GameObject();
			if (id != 0L)
			{
				refMgr.Add(gameObject, id);
			}
			else
			{
				refMgr.Add(gameObject);
			}
			return gameObject;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public static List<GameObject> GetChildren(GameObject go)
		{
			Transform transform = go.transform;
			List<GameObject> list = new List<GameObject>();
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				list.Add(transform2.gameObject);
			}
			return list;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000FF54 File Offset: 0x0000E154
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000FF56 File Offset: 0x0000E156
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000FF58 File Offset: 0x0000E158
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			return null;
		}

		// Token: 0x040000B3 RID: 179
		private const string prefabPropertyName = "es3Prefab";

		// Token: 0x040000B4 RID: 180
		private const string transformPropertyName = "transformID";

		// Token: 0x040000B5 RID: 181
		public static ES3Type Instance;

		// Token: 0x040000B6 RID: 182
		public bool saveChildren;
	}
}
