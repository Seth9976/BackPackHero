using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unity.Services.Analytics
{
	// Token: 0x02000019 RID: 25
	[Obsolete("The interface provided by this static class has moved to AnalyticsService.Instance, and should be accessed from there instead. This API will be removed in an upcoming release.")]
	public static class Events
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003084 File Offset: 0x00001284
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.AdImpression, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static void AdImpression(Events.AdImpressionArgs args)
		{
			AdPlacementType adPlacementType;
			if (!string.IsNullOrEmpty(args.PlacementType) || !Enum.TryParse<AdPlacementType>(args.PlacementType, out adPlacementType))
			{
				adPlacementType = AdPlacementType.BANNER;
			}
			AdImpressionParameters adImpressionParameters = new AdImpressionParameters
			{
				AdCompletionStatus = (Unity.Services.Analytics.AdCompletionStatus)args.AdCompletionStatus,
				AdProvider = (Unity.Services.Analytics.AdProvider)args.AdProvider,
				AdEcpmUsd = args.AdEcpmUsd,
				AdHasClicked = args.AdHasClicked,
				AdImpressionID = args.AdImpressionID,
				AdLengthMs = args.AdLengthMs,
				AdMediaType = args.AdMediaType,
				AdSource = args.AdSource,
				AdStatusCallback = args.AdStatusCallback,
				AdStoreDstID = args.AdStoreDstID,
				AdTimeCloseButtonShownMs = args.AdTimeCloseButtonShownMs,
				AdTimeWatchedMs = args.AdTimeWatchedMs,
				PlacementID = args.PlacementID,
				PlacementName = args.PlacementName,
				PlacementType = new AdPlacementType?(adPlacementType),
				SdkVersion = args.SdkVersion
			};
			AnalyticsService.Instance.AdImpression(adImpressionParameters);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003194 File Offset: 0x00001394
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.CheckForRequiredConsents, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static async Task<List<string>> CheckForRequiredConsents()
		{
			return await AnalyticsService.Instance.CheckForRequiredConsents();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031CF File Offset: 0x000013CF
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.ProvideOptInConsent, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static void ProvideOptInConsent(string identifier, bool consent)
		{
			AnalyticsService.Instance.ProvideOptInConsent(identifier, consent);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031DD File Offset: 0x000013DD
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.CustomData, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static void CustomData(string eventName, IDictionary<string, object> eventParams)
		{
			AnalyticsService.Instance.CustomData(eventName, eventParams);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031EB File Offset: 0x000013EB
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.OptOut, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static void OptOut()
		{
			AnalyticsService.Instance.OptOut();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031F7 File Offset: 0x000013F7
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.Flush, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static void Flush()
		{
			AnalyticsService.Instance.Flush();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003204 File Offset: 0x00001404
		[Obsolete("The interface provided by this method has moved to AnalyticsService.Instance.Transaction, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static void Transaction(Events.TransactionParameters transactionParameters)
		{
			Unity.Services.Analytics.TransactionServer? transactionServer = null;
			if (transactionParameters.transactionServer != null)
			{
				transactionServer = new Unity.Services.Analytics.TransactionServer?((Unity.Services.Analytics.TransactionServer)transactionParameters.transactionServer.Value);
			}
			Unity.Services.Analytics.TransactionParameters transactionParameters2 = new Unity.Services.Analytics.TransactionParameters
			{
				PaymentCountry = transactionParameters.paymentCountry,
				ProductID = transactionParameters.productID,
				RevenueValidated = transactionParameters.revenueValidated,
				TransactionID = transactionParameters.transactionID,
				TransactionReceipt = transactionParameters.transactionReceipt,
				TransactionReceiptSignature = transactionParameters.transactionReceiptSignature,
				TransactionServer = transactionServer,
				TransactorID = transactionParameters.transactorID,
				StoreItemSkuID = transactionParameters.storeItemSkuID,
				StoreItemID = transactionParameters.storeItemID,
				StoreID = transactionParameters.storeID,
				StoreSourceID = transactionParameters.storeSourceID,
				TransactionName = transactionParameters.transactionName,
				TransactionType = (Unity.Services.Analytics.TransactionType)transactionParameters.transactionType,
				ProductsReceived = Events.ConvertProduct(transactionParameters.productsReceived),
				ProductsSpent = Events.ConvertProduct(transactionParameters.productsSpent)
			};
			AnalyticsService.Instance.Transaction(transactionParameters2);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003324 File Offset: 0x00001524
		private static Unity.Services.Analytics.Product ConvertProduct(Events.Product from)
		{
			Unity.Services.Analytics.Product product = default(Unity.Services.Analytics.Product);
			if (from.items != null)
			{
				product.Items = Events.ConvertItems(from.items);
			}
			if (from.realCurrency != null)
			{
				Events.RealCurrency value = from.realCurrency.Value;
				product.RealCurrency = new Unity.Services.Analytics.RealCurrency?(new Unity.Services.Analytics.RealCurrency
				{
					RealCurrencyAmount = value.realCurrencyAmount,
					RealCurrencyType = value.realCurrencyType
				});
			}
			if (from.virtualCurrencies != null)
			{
				product.VirtualCurrencies = from.virtualCurrencies.Select(delegate(Events.VirtualCurrency vc)
				{
					VirtualCurrencyType virtualCurrencyType = VirtualCurrencyType.GRIND;
					if (!string.IsNullOrEmpty(vc.virtualCurrencyType) || !Enum.TryParse<VirtualCurrencyType>(vc.virtualCurrencyType, out virtualCurrencyType))
					{
						virtualCurrencyType = VirtualCurrencyType.GRIND;
					}
					return new Unity.Services.Analytics.VirtualCurrency
					{
						VirtualCurrencyAmount = vc.virtualCurrencyAmount,
						VirtualCurrencyName = vc.virtualCurrencyName,
						VirtualCurrencyType = virtualCurrencyType
					};
				}).ToList<Unity.Services.Analytics.VirtualCurrency>();
			}
			return product;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000033DC File Offset: 0x000015DC
		private static List<Unity.Services.Analytics.Item> ConvertItems(List<Events.Item> from)
		{
			return from.Select((Events.Item i) => new Unity.Services.Analytics.Item
			{
				ItemAmount = i.itemAmount,
				ItemName = i.itemName,
				ItemType = i.itemType
			}).ToList<Unity.Services.Analytics.Item>();
		}

		// Token: 0x04000089 RID: 137
		[Obsolete("The interface provided by this field has moved to AnalyticsService.Instance.PrivacyUrl, and should be accessed from there instead. This API will be removed in an upcoming release.")]
		public static readonly string PrivacyUrl = "https://unity3d.com/legal/privacy-policy";

		// Token: 0x0200004B RID: 75
		[Obsolete("This enum has been moved outside the Events class. Please use that instead. This enum will be removed in an upcoming release.")]
		public enum AdCompletionStatus
		{
			// Token: 0x04000119 RID: 281
			Completed,
			// Token: 0x0400011A RID: 282
			Partial,
			// Token: 0x0400011B RID: 283
			Incomplete
		}

		// Token: 0x0200004C RID: 76
		[Obsolete("This enum has been moved outside the Events class. Please use that instead. This enum will be removed in an upcoming release.")]
		public enum AdProvider
		{
			// Token: 0x0400011D RID: 285
			AdColony,
			// Token: 0x0400011E RID: 286
			AdMob,
			// Token: 0x0400011F RID: 287
			Amazon,
			// Token: 0x04000120 RID: 288
			AppLovin,
			// Token: 0x04000121 RID: 289
			ChartBoost,
			// Token: 0x04000122 RID: 290
			Facebook,
			// Token: 0x04000123 RID: 291
			Fyber,
			// Token: 0x04000124 RID: 292
			Hyprmx,
			// Token: 0x04000125 RID: 293
			Inmobi,
			// Token: 0x04000126 RID: 294
			Maio,
			// Token: 0x04000127 RID: 295
			Pangle,
			// Token: 0x04000128 RID: 296
			Tapjoy,
			// Token: 0x04000129 RID: 297
			UnityAds,
			// Token: 0x0400012A RID: 298
			Vungle,
			// Token: 0x0400012B RID: 299
			IrnSource,
			// Token: 0x0400012C RID: 300
			Other
		}

		// Token: 0x0200004D RID: 77
		[Obsolete("This class has been aligned with other interfaces. Please use AdImpressionParameters with the AnalyticsService.Instance API instead. This class will be removed in an upcoming release")]
		public class AdImpressionArgs
		{
			// Token: 0x0600018E RID: 398 RVA: 0x0000680E File Offset: 0x00004A0E
			public AdImpressionArgs(Events.AdCompletionStatus adCompletionStatus, Events.AdProvider adProvider, string placementID, string placementName)
			{
				this.AdCompletionStatus = adCompletionStatus;
				this.AdProvider = adProvider;
				this.PlacementID = placementID;
				this.PlacementName = placementName;
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600018F RID: 399 RVA: 0x00006833 File Offset: 0x00004A33
			// (set) Token: 0x06000190 RID: 400 RVA: 0x0000683B File Offset: 0x00004A3B
			public Events.AdCompletionStatus AdCompletionStatus { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000191 RID: 401 RVA: 0x00006844 File Offset: 0x00004A44
			// (set) Token: 0x06000192 RID: 402 RVA: 0x0000684C File Offset: 0x00004A4C
			public Events.AdProvider AdProvider { get; set; }

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000193 RID: 403 RVA: 0x00006855 File Offset: 0x00004A55
			// (set) Token: 0x06000194 RID: 404 RVA: 0x0000685D File Offset: 0x00004A5D
			public string PlacementID { get; set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000195 RID: 405 RVA: 0x00006866 File Offset: 0x00004A66
			// (set) Token: 0x06000196 RID: 406 RVA: 0x0000686E File Offset: 0x00004A6E
			public string PlacementName { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00006877 File Offset: 0x00004A77
			// (set) Token: 0x06000198 RID: 408 RVA: 0x0000687F File Offset: 0x00004A7F
			public string PlacementType { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000199 RID: 409 RVA: 0x00006888 File Offset: 0x00004A88
			// (set) Token: 0x0600019A RID: 410 RVA: 0x00006890 File Offset: 0x00004A90
			public double? AdEcpmUsd { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600019B RID: 411 RVA: 0x00006899 File Offset: 0x00004A99
			// (set) Token: 0x0600019C RID: 412 RVA: 0x000068A1 File Offset: 0x00004AA1
			public string SdkVersion { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600019D RID: 413 RVA: 0x000068AA File Offset: 0x00004AAA
			// (set) Token: 0x0600019E RID: 414 RVA: 0x000068B2 File Offset: 0x00004AB2
			public string AdImpressionID { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x0600019F RID: 415 RVA: 0x000068BB File Offset: 0x00004ABB
			// (set) Token: 0x060001A0 RID: 416 RVA: 0x000068C3 File Offset: 0x00004AC3
			public string AdStoreDstID { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060001A1 RID: 417 RVA: 0x000068CC File Offset: 0x00004ACC
			// (set) Token: 0x060001A2 RID: 418 RVA: 0x000068D4 File Offset: 0x00004AD4
			public string AdMediaType { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x000068DD File Offset: 0x00004ADD
			// (set) Token: 0x060001A4 RID: 420 RVA: 0x000068E5 File Offset: 0x00004AE5
			public long? AdTimeWatchedMs { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x000068EE File Offset: 0x00004AEE
			// (set) Token: 0x060001A6 RID: 422 RVA: 0x000068F6 File Offset: 0x00004AF6
			public long? AdTimeCloseButtonShownMs { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x000068FF File Offset: 0x00004AFF
			// (set) Token: 0x060001A8 RID: 424 RVA: 0x00006907 File Offset: 0x00004B07
			public long? AdLengthMs { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006910 File Offset: 0x00004B10
			// (set) Token: 0x060001AA RID: 426 RVA: 0x00006918 File Offset: 0x00004B18
			public bool? AdHasClicked { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060001AB RID: 427 RVA: 0x00006921 File Offset: 0x00004B21
			// (set) Token: 0x060001AC RID: 428 RVA: 0x00006929 File Offset: 0x00004B29
			public string AdSource { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060001AD RID: 429 RVA: 0x00006932 File Offset: 0x00004B32
			// (set) Token: 0x060001AE RID: 430 RVA: 0x0000693A File Offset: 0x00004B3A
			public string AdStatusCallback { get; set; }
		}

		// Token: 0x0200004E RID: 78
		[Obsolete("This enum has been moved outside the Events class, the standalone enum should be used instead. This enum will be removed in an upcoming release.")]
		public enum TransactionServer
		{
			// Token: 0x0400013E RID: 318
			APPLE,
			// Token: 0x0400013F RID: 319
			AMAZON,
			// Token: 0x04000140 RID: 320
			GOOGLE
		}

		// Token: 0x0200004F RID: 79
		[Obsolete("This enum has been moved outside the Events class, the standalone enum should be used instead. This enum will be removed in an upcoming release.")]
		public enum TransactionType
		{
			// Token: 0x04000142 RID: 322
			INVALID,
			// Token: 0x04000143 RID: 323
			SALE,
			// Token: 0x04000144 RID: 324
			PURCHASE,
			// Token: 0x04000145 RID: 325
			TRADE
		}

		// Token: 0x02000050 RID: 80
		[Obsolete("This struct has been moved outside the Events class, and it's parameters now conform to C# guidelines. Please use the standalone struct instead. This struct will be removed in an upcoming release.")]
		public struct Item
		{
			// Token: 0x04000146 RID: 326
			public string itemName;

			// Token: 0x04000147 RID: 327
			public string itemType;

			// Token: 0x04000148 RID: 328
			public long itemAmount;
		}

		// Token: 0x02000051 RID: 81
		[Obsolete("This struct has been moved outside the Events class, and it's parameters now conform to C# guidelines. Please use the standalone struct instead. This struct will be removed in an upcoming release.")]
		public struct VirtualCurrency
		{
			// Token: 0x04000149 RID: 329
			public string virtualCurrencyName;

			// Token: 0x0400014A RID: 330
			public string virtualCurrencyType;

			// Token: 0x0400014B RID: 331
			public long virtualCurrencyAmount;
		}

		// Token: 0x02000052 RID: 82
		[Obsolete("This struct has been moved outside the Events class, and it's parameters now conform to C# guidelines. Please use the standalone struct instead. This struct will be removed in an upcoming release.")]
		public struct RealCurrency
		{
			// Token: 0x0400014C RID: 332
			public string realCurrencyType;

			// Token: 0x0400014D RID: 333
			public long realCurrencyAmount;
		}

		// Token: 0x02000053 RID: 83
		[Obsolete("This struct has been moved outside the Events class, and it's parameters now conform to C# guidelines. Please use the standalone struct instead. This struct will be removed in an upcoming release.")]
		public struct Product
		{
			// Token: 0x0400014E RID: 334
			public Events.RealCurrency? realCurrency;

			// Token: 0x0400014F RID: 335
			public List<Events.VirtualCurrency> virtualCurrencies;

			// Token: 0x04000150 RID: 336
			public List<Events.Item> items;
		}

		// Token: 0x02000054 RID: 84
		[Obsolete("This struct has been moved outside the Events class, and it's parameters now conform to C# guidelines. Please use the standalone struct instead. This struct will be removed in an upcoming release.")]
		public struct TransactionParameters
		{
			// Token: 0x04000151 RID: 337
			[Obsolete]
			public bool? isInitiator;

			// Token: 0x04000152 RID: 338
			public string paymentCountry;

			// Token: 0x04000153 RID: 339
			public string productID;

			// Token: 0x04000154 RID: 340
			public long? revenueValidated;

			// Token: 0x04000155 RID: 341
			public string transactionID;

			// Token: 0x04000156 RID: 342
			public string transactionReceipt;

			// Token: 0x04000157 RID: 343
			public string transactionReceiptSignature;

			// Token: 0x04000158 RID: 344
			public Events.TransactionServer? transactionServer;

			// Token: 0x04000159 RID: 345
			public string transactorID;

			// Token: 0x0400015A RID: 346
			public string storeItemSkuID;

			// Token: 0x0400015B RID: 347
			public string storeItemID;

			// Token: 0x0400015C RID: 348
			public string storeID;

			// Token: 0x0400015D RID: 349
			public string storeSourceID;

			// Token: 0x0400015E RID: 350
			public string transactionName;

			// Token: 0x0400015F RID: 351
			public Events.TransactionType transactionType;

			// Token: 0x04000160 RID: 352
			public Events.Product productsReceived;

			// Token: 0x04000161 RID: 353
			public Events.Product productsSpent;
		}
	}
}
