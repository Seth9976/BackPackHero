using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public struct SerializationData
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00025FB0 File Offset: 0x000241B0
		public string json
		{
			get
			{
				return this._json;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00025FB8 File Offset: 0x000241B8
		public Object[] objectReferences
		{
			get
			{
				return this._objectReferences;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00025FC0 File Offset: 0x000241C0
		public SerializationData(string json, IEnumerable<Object> objectReferences)
		{
			this._json = json;
			this._objectReferences = ((objectReferences != null) ? objectReferences.ToArray<Object>() : null) ?? Empty<Object>.array;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00025FE4 File Offset: 0x000241E4
		public SerializationData(string json, params Object[] objectReferences)
		{
			this = new SerializationData(json, objectReferences);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00025FF0 File Offset: 0x000241F0
		public string ToString(string title)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				if (!string.IsNullOrEmpty(title))
				{
					stringWriter.WriteLine(title);
					stringWriter.WriteLine();
				}
				stringWriter.WriteLine("Object References: ");
				if (this.objectReferences.Length == 0)
				{
					stringWriter.WriteLine("(None)");
				}
				else
				{
					int num = 0;
					foreach (Object @object in this.objectReferences)
					{
						if (@object.IsUnityNull())
						{
							stringWriter.WriteLine(string.Format("{0}: null", num));
						}
						else if (UnityThread.allowsAPI)
						{
							stringWriter.WriteLine(string.Format("{0}: {1} [{2}] \"{3}\"", new object[]
							{
								num,
								@object.GetType().FullName,
								@object.GetHashCode(),
								@object.name
							}));
						}
						else
						{
							stringWriter.WriteLine(string.Format("{0}: {1} [{2}]", num, @object.GetType().FullName, @object.GetHashCode()));
						}
						num++;
					}
				}
				stringWriter.WriteLine();
				stringWriter.WriteLine("JSON: ");
				stringWriter.WriteLine(Serialization.PrettyPrint(this.json));
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00026158 File Offset: 0x00024358
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00026164 File Offset: 0x00024364
		public void ShowString(string title = null)
		{
			string text = Path.GetTempPath() + Guid.NewGuid().ToString() + ".json";
			File.WriteAllText(text, this.ToString(title));
			Process.Start(text);
		}

		// Token: 0x0400020B RID: 523
		[SerializeField]
		private string _json;

		// Token: 0x0400020C RID: 524
		[SerializeField]
		private Object[] _objectReferences;
	}
}
