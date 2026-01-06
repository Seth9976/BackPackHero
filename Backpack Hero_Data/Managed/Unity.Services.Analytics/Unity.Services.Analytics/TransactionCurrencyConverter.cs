using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.Analytics
{
	// Token: 0x02000010 RID: 16
	internal class TransactionCurrencyConverter
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002F04 File Offset: 0x00001104
		public long Convert(string currencyCode, double value)
		{
			if (string.IsNullOrEmpty(currencyCode))
			{
				throw new ArgumentNullException("currencyCode", "Currency code cannot be null or empty.");
			}
			if (this.m_CurrencyCodeToMinorUnits == null)
			{
				this.LoadCurrencyCodeDictionary();
			}
			string text = currencyCode.ToUpperInvariant();
			int num;
			if (this.m_CurrencyCodeToMinorUnits.TryGetValue(text, out num))
			{
				return (long)(value * Math.Pow(10.0, (double)num));
			}
			Debug.LogWarning("Unknown currency provided to convert method, no conversion will be performed and returned value will be 0.");
			return 0L;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F70 File Offset: 0x00001170
		public void LoadCurrencyCodeDictionary()
		{
			TextAsset textAsset = Resources.Load<TextAsset>("iso4217");
			if (textAsset)
			{
				string text = textAsset.text;
				if (string.IsNullOrEmpty(text))
				{
					Debug.LogWarning("Error loading currency definitions, no conversions will be performed.");
					this.m_CurrencyCodeToMinorUnits = new Dictionary<string, int>();
					return;
				}
				try
				{
					this.m_CurrencyCodeToMinorUnits = JsonConvert.DeserializeObject<Dictionary<string, int>>(text);
				}
				catch (JsonException ex)
				{
					Debug.LogWarning("Failed to deserialize JSON for currency conversion, no conversions will be performed");
					Debug.LogWarning(ex.Message);
					this.m_CurrencyCodeToMinorUnits = new Dictionary<string, int>();
				}
			}
		}

		// Token: 0x04000054 RID: 84
		private Dictionary<string, int> m_CurrencyCodeToMinorUnits;
	}
}
