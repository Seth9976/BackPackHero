using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005D RID: 93
	[Preserve]
	[ES3Properties(new string[] { "sharedMesh" })]
	public class ES3Type_MeshFilter : ES3ComponentType
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public ES3Type_MeshFilter()
			: base(typeof(MeshFilter))
		{
			ES3Type_MeshFilter.Instance = this;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B4CC File Offset: 0x000096CC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			MeshFilter meshFilter = (MeshFilter)obj;
			writer.WritePropertyByRef("sharedMesh", meshFilter.sharedMesh);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B4F4 File Offset: 0x000096F4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			MeshFilter meshFilter = (MeshFilter)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "sharedMesh")
					{
						meshFilter.sharedMesh = reader.Read<Mesh>(ES3Type_Mesh.Instance);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x0400008F RID: 143
		public static ES3Type Instance;
	}
}
