using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000016 RID: 22
	public class ES3Type_ES3PrefabInternal : ES3Type
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x000066B4 File Offset: 0x000048B4
		public ES3Type_ES3PrefabInternal()
			: base(typeof(ES3Type_ES3PrefabInternal))
		{
			ES3Type_ES3PrefabInternal.Instance = this;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000066CC File Offset: 0x000048CC
		public override void Write(object obj, ES3Writer writer)
		{
			ES3Prefab es3Prefab = (ES3Prefab)obj;
			writer.WriteProperty("prefabId", es3Prefab.prefabId.ToString(), ES3Type_string.Instance);
			writer.WriteProperty("refs", es3Prefab.GetReferences());
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000670C File Offset: 0x0000490C
		public override object Read<T>(ES3Reader reader)
		{
			long num = reader.ReadRefProperty();
			if (ES3ReferenceMgrBase.Current == null)
			{
				return null;
			}
			ES3Prefab prefab = ES3ReferenceMgrBase.Current.GetPrefab(num, false);
			if (prefab == null)
			{
				throw new MissingReferenceException("Prefab with ID " + num.ToString() + " could not be found.\nPress the 'Refresh References' button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in the scene to refresh prefabs.");
			}
			GameObject gameObject = Object.Instantiate<GameObject>(prefab.gameObject);
			ES3Prefab component = gameObject.GetComponent<ES3Prefab>();
			if (component == null)
			{
				throw new MissingReferenceException("Prefab with ID " + num.ToString() + " was found, but it does not have an ES3Prefab component attached.");
			}
			this.ReadInto<T>(reader, gameObject);
			return component.gameObject;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000067A4 File Offset: 0x000049A4
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Dictionary<ES3Ref, ES3Ref> dictionary = reader.ReadProperty<Dictionary<ES3Ref, ES3Ref>>(ES3Type_ES3RefDictionary.Instance);
			Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
			foreach (KeyValuePair<ES3Ref, ES3Ref> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key.id, keyValuePair.Value.id);
			}
			if (ES3ReferenceMgrBase.Current == null)
			{
				return;
			}
			((GameObject)obj).GetComponent<ES3Prefab>().ApplyReferences(dictionary2);
		}

		// Token: 0x04000049 RID: 73
		public static ES3Type Instance = new ES3Type_ES3PrefabInternal();
	}
}
