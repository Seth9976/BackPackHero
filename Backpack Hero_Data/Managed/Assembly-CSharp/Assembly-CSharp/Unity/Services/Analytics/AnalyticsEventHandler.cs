using System;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Services.Analytics
{
	// Token: 0x020001AE RID: 430
	public class AnalyticsEventHandler : MonoBehaviour
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x000A1054 File Offset: 0x0009F254
		private void Awake()
		{
			Application.logMessageReceived += this.OnLogMessageReceived;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000A1068 File Offset: 0x0009F268
		private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
		{
			if (this.consoleOutput == null)
			{
				return;
			}
			Text text = this.consoleOutput;
			text.text += string.Format("{0}: {1}\n", type, condition);
			this.consoleScrollRect.normalizedPosition = Vector2.zero;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000A10BB File Offset: 0x0009F2BB
		private void OnDestroy()
		{
			Application.logMessageReceived -= this.OnLogMessageReceived;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000A10D0 File Offset: 0x0009F2D0
		private async void Start()
		{
			await UnityServices.InitializeAsync();
			await AnalyticsService.Instance.CheckForRequiredConsents();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000A10FF File Offset: 0x0009F2FF
		public void RecordMinimalAdImpressionEvent()
		{
			StandardEventSample.RecordMinimalAdImpressionEvent();
			Debug.Log("Record Minimal Ad Impression Event Finished");
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000A1110 File Offset: 0x0009F310
		public void RecordCompleteAdImpressionEvent()
		{
			StandardEventSample.RecordCompleteAdImpressionEvent();
			Debug.Log("Record Complete Ad Impression Event Finished");
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000A1121 File Offset: 0x0009F321
		public void RecordSaleTransactionWithOnlyRequiredValues()
		{
			StandardEventSample.RecordSaleTransactionWithOnlyRequiredValues();
			Debug.Log("Record Sale Transaction With Only Required Values Finished");
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000A1132 File Offset: 0x0009F332
		public void RecordSaleTransactionWithRealCurrency()
		{
			StandardEventSample.RecordSaleTransactionWithRealCurrency();
			Debug.Log("Record Sale Transaction With Real Currency Finished");
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000A1143 File Offset: 0x0009F343
		public void RecordSaleTransactionWithVirtualCurrency()
		{
			StandardEventSample.RecordSaleTransactionWithVirtualCurrency();
			Debug.Log("Record Sale Transaction With Virtual Currency Finished");
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000A1154 File Offset: 0x0009F354
		public void RecordSaleTransactionWithMultipleVirtualCurrencies()
		{
			StandardEventSample.RecordSaleTransactionWithMultipleVirtualCurrencies();
			Debug.Log("Record Sale Transaction With Multiple Virtual Currencies Finished");
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000A1165 File Offset: 0x0009F365
		public void RecordSaleEventWithOneItem()
		{
			StandardEventSample.RecordSaleEventWithOneItem();
			Debug.Log("Record Sale Event With One Item Finished");
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000A1176 File Offset: 0x0009F376
		public void RecordSaleEventWithMultipleItems()
		{
			StandardEventSample.RecordSaleEventWithMultipleItems();
			Debug.Log("Record Sale Event With Multiple Items Finished");
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000A1187 File Offset: 0x0009F387
		public void RecordSaleEventWithOptionalParameters()
		{
			StandardEventSample.RecordSaleEventWithOptionalParameters();
			Debug.Log("Record Sale Event With Optional Parameters Finished");
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000A1198 File Offset: 0x0009F398
		public void RecordAcquisitionSourceEventWithOnlyRequiredValues()
		{
			StandardEventSample.RecordAcquisitionSourceEventWithOnlyRequiredValues();
			Debug.Log("Record Acquisition Source Event With Only Required Values Finished");
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000A11A9 File Offset: 0x0009F3A9
		public void RecordAcquisitionSourceEventWithOptionalParameters()
		{
			StandardEventSample.RecordAcquisitionSourceEventWithOptionalParameters();
			Debug.Log("Record Acquisition Source Event With Optional Parameters Finished");
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000A11BA File Offset: 0x0009F3BA
		public void RecordPurchaseEventWithOneItem()
		{
			StandardEventSample.RecordPurchaseEventWithOneItem();
			Debug.Log("Record Purchase Event With One Item Finished");
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000A11CB File Offset: 0x0009F3CB
		public void RecordPurchaseEventWithMultipleItems()
		{
			StandardEventSample.RecordPurchaseEventWithMultipleItems();
			Debug.Log("Record Purchase Event With Multiple Items Finished");
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000A11DC File Offset: 0x0009F3DC
		public void RecordPurchaseEventWithMultipleCurrencies()
		{
			StandardEventSample.RecordPurchaseEventWithMultipleCurrencies();
			Debug.Log("Record Purchase Event With Multiple Currencies Finished");
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000A11ED File Offset: 0x0009F3ED
		public void RecordCustomEventWithNoParameters()
		{
			CustomEventSample.RecordCustomEventWithNoParameters();
			Debug.Log("Record Custom Event With No Parameters Finished");
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000A11FE File Offset: 0x0009F3FE
		public void RecordCustomEventWithParameters()
		{
			CustomEventSample.RecordCustomEventWithParameters();
			Debug.Log("Record Custom Event With Parameters Finished");
		}

		// Token: 0x04000DDB RID: 3547
		[SerializeField]
		private Text consoleOutput;

		// Token: 0x04000DDC RID: 3548
		[SerializeField]
		private ScrollRect consoleScrollRect;
	}
}
