using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000099 RID: 153
	[Preserve]
	[ES3Properties(new string[] { "dynamicFriction", "staticFriction", "bounciness", "frictionCombine", "bounceCombine" })]
	public class ES3Type_PhysicMaterial : ES3ObjectType
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0001933C File Offset: 0x0001753C
		public ES3Type_PhysicMaterial()
			: base(typeof(PhysicMaterial))
		{
			ES3Type_PhysicMaterial.Instance = this;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00019354 File Offset: 0x00017554
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			PhysicMaterial physicMaterial = (PhysicMaterial)obj;
			writer.WriteProperty("dynamicFriction", physicMaterial.dynamicFriction, ES3Type_float.Instance);
			writer.WriteProperty("staticFriction", physicMaterial.staticFriction, ES3Type_float.Instance);
			writer.WriteProperty("bounciness", physicMaterial.bounciness, ES3Type_float.Instance);
			writer.WriteProperty("frictionCombine", physicMaterial.frictionCombine);
			writer.WriteProperty("bounceCombine", physicMaterial.bounceCombine);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000193E8 File Offset: 0x000175E8
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			PhysicMaterial physicMaterial = (PhysicMaterial)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "dynamicFriction"))
				{
					if (!(text == "staticFriction"))
					{
						if (!(text == "bounciness"))
						{
							if (!(text == "frictionCombine"))
							{
								if (!(text == "bounceCombine"))
								{
									reader.Skip();
								}
								else
								{
									physicMaterial.bounceCombine = reader.Read<PhysicMaterialCombine>();
								}
							}
							else
							{
								physicMaterial.frictionCombine = reader.Read<PhysicMaterialCombine>();
							}
						}
						else
						{
							physicMaterial.bounciness = reader.Read<float>(ES3Type_float.Instance);
						}
					}
					else
					{
						physicMaterial.staticFriction = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					physicMaterial.dynamicFriction = reader.Read<float>(ES3Type_float.Instance);
				}
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000194E8 File Offset: 0x000176E8
		protected override object ReadObject<T>(ES3Reader reader)
		{
			PhysicMaterial physicMaterial = new PhysicMaterial();
			this.ReadObject<T>(reader, physicMaterial);
			return physicMaterial;
		}

		// Token: 0x040000CF RID: 207
		public static ES3Type Instance;
	}
}
