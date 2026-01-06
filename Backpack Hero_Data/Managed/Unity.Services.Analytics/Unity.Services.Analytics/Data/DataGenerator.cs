using System;
using Unity.Services.Analytics.Internal;

namespace Unity.Services.Analytics.Data
{
	// Token: 0x02000045 RID: 69
	internal class DataGenerator : IDataGenerator
	{
		// Token: 0x06000177 RID: 375 RVA: 0x0000536D File Offset: 0x0000356D
		public void SetBuffer(IBuffer buffer)
		{
			this.m_Buffer = buffer;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005378 File Offset: 0x00003578
		public void SdkStartup(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier)
		{
			this.m_Buffer.PushStartEvent("sdkStart", datetime, new long?(1L), true);
			this.m_Buffer.PushString(SdkVersion.SDK_VERSION, "sdkVersion");
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString("com.unity.services.analytics", "sdkName");
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000053E0 File Offset: 0x000035E0
		public void GameRunning(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier)
		{
			this.m_Buffer.PushStartEvent("gameRunning", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005414 File Offset: 0x00003614
		public void NewPlayer(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string deviceModel)
		{
			this.m_Buffer.PushStartEvent("newPlayer", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString(deviceModel, "deviceModel");
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005464 File Offset: 0x00003664
		public void GameStarted(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string idLocalProject, string osVersion, bool isTiny, bool debugDevice, string userLocale)
		{
			this.m_Buffer.PushStartEvent("gameStarted", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString(userLocale, "userLocale");
			if (!string.IsNullOrEmpty(idLocalProject))
			{
				this.m_Buffer.PushString(idLocalProject, "idLocalProject");
			}
			this.m_Buffer.PushString(osVersion, "osVersion");
			this.m_Buffer.PushBool(isTiny, "isTiny");
			this.m_Buffer.PushBool(debugDevice, "debugDevice");
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005508 File Offset: 0x00003708
		public void GameEnded(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, DataGenerator.SessionEndState quitState)
		{
			this.m_Buffer.PushStartEvent("gameEnded", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString(quitState.ToString(), "sessionEndState");
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005564 File Offset: 0x00003764
		public void AdImpression(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, AdImpressionParameters adImpressionParameters)
		{
			this.m_Buffer.PushStartEvent("adImpression", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString(adImpressionParameters.AdCompletionStatus.ToString().ToUpperInvariant(), "adCompletionStatus");
			this.m_Buffer.PushString(adImpressionParameters.AdProvider.ToString().ToUpperInvariant(), "adProvider");
			this.m_Buffer.PushString(adImpressionParameters.PlacementID, "placementId");
			this.m_Buffer.PushString(adImpressionParameters.PlacementName, "placementName");
			double? adEcpmUsd = adImpressionParameters.AdEcpmUsd;
			if (adEcpmUsd != null)
			{
				double valueOrDefault = adEcpmUsd.GetValueOrDefault();
				this.m_Buffer.PushDouble(valueOrDefault, "adEcpmUsd");
			}
			if (adImpressionParameters.PlacementType != null)
			{
				this.m_Buffer.PushString(adImpressionParameters.PlacementType.ToString(), "placementType");
			}
			if (!string.IsNullOrEmpty(adImpressionParameters.SdkVersion))
			{
				this.m_Buffer.PushString(adImpressionParameters.SdkVersion, "adSdkVersion");
			}
			if (!string.IsNullOrEmpty(adImpressionParameters.AdImpressionID))
			{
				this.m_Buffer.PushString(adImpressionParameters.AdImpressionID, "adImpressionID");
			}
			if (!string.IsNullOrEmpty(adImpressionParameters.AdStoreDstID))
			{
				this.m_Buffer.PushString(adImpressionParameters.AdStoreDstID, "adStoreDestinationID");
			}
			if (!string.IsNullOrEmpty(adImpressionParameters.AdMediaType))
			{
				this.m_Buffer.PushString(adImpressionParameters.AdMediaType, "adMediaType");
			}
			long? num = adImpressionParameters.AdTimeWatchedMs;
			if (num != null)
			{
				long valueOrDefault2 = num.GetValueOrDefault();
				this.m_Buffer.PushInt64(valueOrDefault2, "adTimeWatchedMs");
			}
			num = adImpressionParameters.AdTimeCloseButtonShownMs;
			if (num != null)
			{
				long valueOrDefault3 = num.GetValueOrDefault();
				this.m_Buffer.PushInt64(valueOrDefault3, "adTimeCloseButtonShownMs");
			}
			num = adImpressionParameters.AdLengthMs;
			if (num != null)
			{
				long valueOrDefault4 = num.GetValueOrDefault();
				this.m_Buffer.PushInt64(valueOrDefault4, "adLengthMs");
			}
			bool? adHasClicked = adImpressionParameters.AdHasClicked;
			if (adHasClicked != null)
			{
				bool valueOrDefault5 = adHasClicked.GetValueOrDefault();
				this.m_Buffer.PushBool(valueOrDefault5, "adHasClicked");
			}
			if (!string.IsNullOrEmpty(adImpressionParameters.AdSource))
			{
				this.m_Buffer.PushString(adImpressionParameters.AdSource, "adSource");
			}
			if (!string.IsNullOrEmpty(adImpressionParameters.AdStatusCallback))
			{
				this.m_Buffer.PushString(adImpressionParameters.AdStatusCallback, "adStatusCallback");
			}
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005808 File Offset: 0x00003A08
		public void AcquisitionSource(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, AcquisitionSourceParameters acquisitionSourceParameters)
		{
			this.m_Buffer.PushStartEvent("acquisitionSource", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString(acquisitionSourceParameters.Channel, "acquisitionChannel");
			this.m_Buffer.PushString(acquisitionSourceParameters.CampaignId, "acquisitionCampaignId");
			this.m_Buffer.PushString(acquisitionSourceParameters.CreativeId, "acquisitionCreativeId");
			this.m_Buffer.PushString(acquisitionSourceParameters.CampaignName, "acquisitionCampaignName");
			this.m_Buffer.PushString(acquisitionSourceParameters.Provider, "acquisitionProvider");
			if (!string.IsNullOrEmpty(acquisitionSourceParameters.CampaignType))
			{
				this.m_Buffer.PushString(acquisitionSourceParameters.CampaignType, "acquisitionCampaignType");
			}
			if (!string.IsNullOrEmpty(acquisitionSourceParameters.Network))
			{
				this.m_Buffer.PushString(acquisitionSourceParameters.Network, "acquisitionNetwork");
			}
			if (!string.IsNullOrEmpty(acquisitionSourceParameters.CostCurrency))
			{
				this.m_Buffer.PushString(acquisitionSourceParameters.CostCurrency, "acquisitionCostCurrency");
			}
			float? cost = acquisitionSourceParameters.Cost;
			if (cost != null)
			{
				float valueOrDefault = cost.GetValueOrDefault();
				this.m_Buffer.PushFloat(valueOrDefault, "acquisitionCost");
			}
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005954 File Offset: 0x00003B54
		public void Transaction(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, TransactionParameters transactionParameters)
		{
			this.m_Buffer.PushStartEvent("transaction", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			if (!string.IsNullOrEmpty(SdkVersion.SDK_VERSION))
			{
				this.m_Buffer.PushString(SdkVersion.SDK_VERSION, "sdkVersion");
			}
			if (!string.IsNullOrEmpty(transactionParameters.PaymentCountry))
			{
				this.m_Buffer.PushString(transactionParameters.PaymentCountry, "paymentCountry");
			}
			if (!string.IsNullOrEmpty(transactionParameters.ProductID))
			{
				this.m_Buffer.PushString(transactionParameters.ProductID, "productID");
			}
			if (transactionParameters.RevenueValidated != null)
			{
				this.m_Buffer.PushInt64(transactionParameters.RevenueValidated.Value, "revenueValidated");
			}
			if (!string.IsNullOrEmpty(transactionParameters.TransactionID))
			{
				this.m_Buffer.PushString(transactionParameters.TransactionID, "transactionID");
			}
			if (!string.IsNullOrEmpty(transactionParameters.TransactionReceipt))
			{
				this.m_Buffer.PushString(transactionParameters.TransactionReceipt, "transactionReceipt");
			}
			if (!string.IsNullOrEmpty(transactionParameters.TransactionReceiptSignature))
			{
				this.m_Buffer.PushString(transactionParameters.TransactionReceiptSignature, "transactionReceiptSignature");
			}
			if (!string.IsNullOrEmpty((transactionParameters.TransactionServer != null) ? transactionParameters.TransactionServer.GetValueOrDefault().ToString() : null))
			{
				this.m_Buffer.PushString(transactionParameters.TransactionServer.ToString(), "transactionServer");
			}
			if (!string.IsNullOrEmpty(transactionParameters.TransactorID))
			{
				this.m_Buffer.PushString(transactionParameters.TransactorID, "transactorID");
			}
			if (!string.IsNullOrEmpty(transactionParameters.StoreItemSkuID))
			{
				this.m_Buffer.PushString(transactionParameters.StoreItemSkuID, "storeItemSkuID");
			}
			if (!string.IsNullOrEmpty(transactionParameters.StoreItemID))
			{
				this.m_Buffer.PushString(transactionParameters.StoreItemID, "storeItemID");
			}
			if (!string.IsNullOrEmpty(transactionParameters.StoreID))
			{
				this.m_Buffer.PushString(transactionParameters.StoreID, "storeID");
			}
			if (!string.IsNullOrEmpty(transactionParameters.StoreSourceID))
			{
				this.m_Buffer.PushString(transactionParameters.StoreSourceID, "storeSourceID");
			}
			this.m_Buffer.PushString(transactionParameters.TransactionName, "transactionName");
			this.m_Buffer.PushString(transactionParameters.TransactionType.ToString(), "transactionType");
			this.SetProduct("productsReceived", transactionParameters.ProductsReceived);
			this.SetProduct("productsSpent", transactionParameters.ProductsSpent);
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005BFC File Offset: 0x00003DFC
		public void TransactionFailed(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, TransactionFailedParameters parameters)
		{
			this.m_Buffer.PushStartEvent("transactionFailed", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			if (!string.IsNullOrEmpty(SdkVersion.SDK_VERSION))
			{
				this.m_Buffer.PushString(SdkVersion.SDK_VERSION, "sdkVersion");
			}
			if (!string.IsNullOrEmpty(parameters.PaymentCountry))
			{
				this.m_Buffer.PushString(parameters.PaymentCountry, "paymentCountry");
			}
			if (!string.IsNullOrEmpty(parameters.ProductID))
			{
				this.m_Buffer.PushString(parameters.ProductID, "productID");
			}
			if (parameters.RevenueValidated != null)
			{
				this.m_Buffer.PushInt64(parameters.RevenueValidated.Value, "revenueValidated");
			}
			if (!string.IsNullOrEmpty(parameters.TransactionID))
			{
				this.m_Buffer.PushString(parameters.TransactionID, "transactionID");
			}
			if (!string.IsNullOrEmpty((parameters.TransactionServer != null) ? parameters.TransactionServer.GetValueOrDefault().ToString() : null))
			{
				this.m_Buffer.PushString(parameters.TransactionServer.ToString(), "transactionServer");
			}
			if (parameters.EngagementID != null)
			{
				this.m_Buffer.PushInt64(parameters.EngagementID.Value, "engagementID");
			}
			if (!string.IsNullOrEmpty(parameters.GameStoreID))
			{
				this.m_Buffer.PushString(parameters.GameStoreID, "gameStoreID");
			}
			if (!string.IsNullOrEmpty(parameters.AmazonUserID))
			{
				this.m_Buffer.PushString(parameters.AmazonUserID, "amazonUserID");
			}
			if (parameters.IsInitiator != null)
			{
				this.m_Buffer.PushBool(parameters.IsInitiator.Value, "isInitiator");
			}
			if (!string.IsNullOrEmpty(parameters.StoreItemSkuID))
			{
				this.m_Buffer.PushString(parameters.StoreItemSkuID, "storeItemSkuID");
			}
			if (!string.IsNullOrEmpty(parameters.StoreItemID))
			{
				this.m_Buffer.PushString(parameters.StoreItemID, "storeItemID");
			}
			if (!string.IsNullOrEmpty(parameters.StoreID))
			{
				this.m_Buffer.PushString(parameters.StoreID, "storeID");
			}
			if (!string.IsNullOrEmpty(parameters.StoreSourceID))
			{
				this.m_Buffer.PushString(parameters.StoreSourceID, "storeSourceID");
			}
			this.m_Buffer.PushString(parameters.TransactionName, "transactionName");
			this.m_Buffer.PushString(parameters.TransactionType.ToString(), "transactionType");
			this.SetProduct("productsReceived", parameters.ProductsReceived);
			this.SetProduct("productsSpent", parameters.ProductsSpent);
			this.m_Buffer.PushString(parameters.FailureReason, "failureReason");
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005EEC File Offset: 0x000040EC
		public void ClientDevice(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string cpuType, string gpuType, long cpuCores, long ramTotal, long screenWidth, long screenHeight, long screenDPI)
		{
			this.m_Buffer.PushStartEvent("clientDevice", datetime, new long?(1L), true);
			commonParams.SerializeCommonEventParams(ref this.m_Buffer, callingMethodIdentifier);
			this.m_Buffer.PushString(cpuType, "cpuType");
			this.m_Buffer.PushString(gpuType, "gpuType");
			this.m_Buffer.PushInt64(cpuCores, "cpuCores");
			this.m_Buffer.PushInt64(ramTotal, "ramTotal");
			this.m_Buffer.PushInt64(screenWidth, "screenWidth");
			this.m_Buffer.PushInt64(screenHeight, "screenHeight");
			this.m_Buffer.PushInt64(screenDPI, "screenResolution");
			this.m_Buffer.PushEndEvent();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005FA8 File Offset: 0x000041A8
		private void SetProduct(string productName, Product product)
		{
			this.m_Buffer.PushObjectStart(productName);
			if (product.RealCurrency != null)
			{
				this.m_Buffer.PushObjectStart("realCurrency");
				this.m_Buffer.PushString(product.RealCurrency.Value.RealCurrencyType, "realCurrencyType");
				this.m_Buffer.PushInt64(product.RealCurrency.Value.RealCurrencyAmount, "realCurrencyAmount");
				this.m_Buffer.PushObjectEnd();
			}
			if (product.VirtualCurrencies != null && product.VirtualCurrencies.Count != 0)
			{
				this.m_Buffer.PushArrayStart("virtualCurrencies");
				foreach (VirtualCurrency virtualCurrency in product.VirtualCurrencies)
				{
					this.m_Buffer.PushObjectStart(null);
					this.m_Buffer.PushObjectStart("virtualCurrency");
					this.m_Buffer.PushString(virtualCurrency.VirtualCurrencyName, "virtualCurrencyName");
					this.m_Buffer.PushString(virtualCurrency.VirtualCurrencyType.ToString(), "virtualCurrencyType");
					this.m_Buffer.PushInt64(virtualCurrency.VirtualCurrencyAmount, "virtualCurrencyAmount");
					this.m_Buffer.PushObjectEnd();
					this.m_Buffer.PushObjectEnd();
				}
				this.m_Buffer.PushArrayEnd();
			}
			if (product.Items != null && product.Items.Count != 0)
			{
				this.m_Buffer.PushArrayStart("items");
				foreach (Item item in product.Items)
				{
					this.m_Buffer.PushObjectStart(null);
					this.m_Buffer.PushObjectStart("item");
					this.m_Buffer.PushString(item.ItemName, "itemName");
					this.m_Buffer.PushString(item.ItemType, "itemType");
					this.m_Buffer.PushInt64(item.ItemAmount, "itemAmount");
					this.m_Buffer.PushObjectEnd();
					this.m_Buffer.PushObjectEnd();
				}
				this.m_Buffer.PushArrayEnd();
			}
			this.m_Buffer.PushObjectEnd();
		}

		// Token: 0x04000101 RID: 257
		private IBuffer m_Buffer;

		// Token: 0x0200005C RID: 92
		internal enum SessionEndState
		{
			// Token: 0x04000178 RID: 376
			PAUSED,
			// Token: 0x04000179 RID: 377
			KILLEDINBACKGROUND,
			// Token: 0x0400017A RID: 378
			KILLEDINFOREGROUND,
			// Token: 0x0400017B RID: 379
			QUIT
		}
	}
}
