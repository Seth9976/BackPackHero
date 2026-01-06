using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009B RID: 155
	[Preserve]
	[ES3Properties(new string[] { "bounciness", "friction" })]
	public class ES3Type_PhysicsMaterial2D : ES3ObjectType
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00019521 File Offset: 0x00017721
		public ES3Type_PhysicsMaterial2D()
			: base(typeof(PhysicsMaterial2D))
		{
			ES3Type_PhysicsMaterial2D.Instance = this;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001953C File Offset: 0x0001773C
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			PhysicsMaterial2D physicsMaterial2D = (PhysicsMaterial2D)obj;
			writer.WriteProperty("bounciness", physicsMaterial2D.bounciness, ES3Type_float.Instance);
			writer.WriteProperty("friction", physicsMaterial2D.friction, ES3Type_float.Instance);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00019588 File Offset: 0x00017788
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			PhysicsMaterial2D physicsMaterial2D = (PhysicsMaterial2D)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "bounciness"))
				{
					if (!(text == "friction"))
					{
						reader.Skip();
					}
					else
					{
						physicsMaterial2D.friction = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					physicsMaterial2D.bounciness = reader.Read<float>(ES3Type_float.Instance);
				}
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001962C File Offset: 0x0001782C
		protected override object ReadObject<T>(ES3Reader reader)
		{
			PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D();
			this.ReadObject<T>(reader, physicsMaterial2D);
			return physicsMaterial2D;
		}

		// Token: 0x040000D1 RID: 209
		public static ES3Type Instance;
	}
}
