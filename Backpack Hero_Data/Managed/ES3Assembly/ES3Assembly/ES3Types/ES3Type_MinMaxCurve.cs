using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000096 RID: 150
	[Preserve]
	[ES3Properties(new string[] { "mode", "curveMultiplier", "curveMax", "curveMin", "constantMax", "constantMin", "constant", "curve" })]
	public class ES3Type_MinMaxCurve : ES3Type
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00018238 File Offset: 0x00016438
		public ES3Type_MinMaxCurve()
			: base(typeof(ParticleSystem.MinMaxCurve))
		{
			ES3Type_MinMaxCurve.Instance = this;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00018250 File Offset: 0x00016450
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = (ParticleSystem.MinMaxCurve)obj;
			writer.WriteProperty("mode", minMaxCurve.mode);
			writer.WriteProperty("curveMultiplier", minMaxCurve.curveMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("curveMax", minMaxCurve.curveMax, ES3Type_AnimationCurve.Instance);
			writer.WriteProperty("curveMin", minMaxCurve.curveMin, ES3Type_AnimationCurve.Instance);
			writer.WriteProperty("constantMax", minMaxCurve.constantMax, ES3Type_float.Instance);
			writer.WriteProperty("constantMin", minMaxCurve.constantMin, ES3Type_float.Instance);
			writer.WriteProperty("constant", minMaxCurve.constant, ES3Type_float.Instance);
			writer.WriteProperty("curve", minMaxCurve.curve, ES3Type_AnimationCurve.Instance);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00018330 File Offset: 0x00016530
		[Preserve]
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = default(ParticleSystem.MinMaxCurve);
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3569100738U)
				{
					if (num <= 740975083U)
					{
						if (num != 110225957U)
						{
							if (num == 740975083U)
							{
								if (text == "curveMultiplier")
								{
									minMaxCurve.curveMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "constant")
						{
							minMaxCurve.constant = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 2570585620U)
					{
						if (num == 3569100738U)
						{
							if (text == "curveMax")
							{
								minMaxCurve.curveMax = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "curve")
					{
						minMaxCurve.curve = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
						continue;
					}
				}
				else if (num <= 3735493832U)
				{
					if (num != 3713335191U)
					{
						if (num == 3735493832U)
						{
							if (text == "curveMin")
							{
								minMaxCurve.curveMin = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "constantMin")
					{
						minMaxCurve.constantMin = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3949501785U)
				{
					if (num == 3966689298U)
					{
						if (text == "mode")
						{
							minMaxCurve.mode = reader.Read<ParticleSystemCurveMode>();
							continue;
						}
					}
				}
				else if (text == "constantMax")
				{
					minMaxCurve.constantMax = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
			return minMaxCurve;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00018524 File Offset: 0x00016724
		[Preserve]
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = (ParticleSystem.MinMaxCurve)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3569100738U)
				{
					if (num <= 740975083U)
					{
						if (num != 110225957U)
						{
							if (num == 740975083U)
							{
								if (text == "curveMultiplier")
								{
									minMaxCurve.curveMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "constant")
						{
							minMaxCurve.constant = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 2570585620U)
					{
						if (num == 3569100738U)
						{
							if (text == "curveMax")
							{
								minMaxCurve.curveMax = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "curve")
					{
						minMaxCurve.curve = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
						continue;
					}
				}
				else if (num <= 3735493832U)
				{
					if (num != 3713335191U)
					{
						if (num == 3735493832U)
						{
							if (text == "curveMin")
							{
								minMaxCurve.curveMin = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "constantMin")
					{
						minMaxCurve.constantMin = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3949501785U)
				{
					if (num == 3966689298U)
					{
						if (text == "mode")
						{
							minMaxCurve.mode = reader.Read<ParticleSystemCurveMode>();
							continue;
						}
					}
				}
				else if (text == "constantMax")
				{
					minMaxCurve.constantMax = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000CC RID: 204
		public static ES3Type Instance;
	}
}
