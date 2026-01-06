using System;
using System.Collections.Generic;

namespace Unity.Services.Analytics
{
	// Token: 0x020001B0 RID: 432
	public class StandardEventSample
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x000A12E8 File Offset: 0x0009F4E8
		public static void RecordMinimalAdImpressionEvent()
		{
			AdImpressionParameters adImpressionParameters = new AdImpressionParameters
			{
				AdCompletionStatus = AdCompletionStatus.Completed,
				AdProvider = AdProvider.UnityAds,
				PlacementName = "PLACEMENTNAME",
				PlacementID = "PLACEMENTID"
			};
			AnalyticsService.Instance.AdImpression(adImpressionParameters);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000A1334 File Offset: 0x0009F534
		public static void RecordCompleteAdImpressionEvent()
		{
			AdImpressionParameters adImpressionParameters = new AdImpressionParameters
			{
				AdCompletionStatus = AdCompletionStatus.Completed,
				AdProvider = AdProvider.UnityAds,
				PlacementName = "PLACEMENTNAME",
				PlacementID = "PLACEMENTID",
				PlacementType = new AdPlacementType?(AdPlacementType.BANNER),
				AdEcpmUsd = new double?(123.4),
				SdkVersion = "123.4",
				AdImpressionID = "IMPRESSIVE",
				AdStoreDstID = "DSTID",
				AdMediaType = "MOVIE",
				AdTimeWatchedMs = new long?(1234L),
				AdTimeCloseButtonShownMs = new long?(5678L),
				AdLengthMs = new long?(2345L),
				AdHasClicked = new bool?(false),
				AdSource = "ADSRC",
				AdStatusCallback = "STATCALL"
			};
			AnalyticsService.Instance.AdImpression(adImpressionParameters);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000A142C File Offset: 0x0009F62C
		public static void RecordSaleTransactionWithOnlyRequiredValues()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = default(Product),
				ProductsSpent = default(Product),
				TransactionName = "emptySale",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000A147C File Offset: 0x0009F67C
		public static void RecordSaleTransactionWithRealCurrency()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					RealCurrency = new RealCurrency?(new RealCurrency
					{
						RealCurrencyType = "EUR",
						RealCurrencyAmount = AnalyticsService.Instance.ConvertCurrencyToMinorUnits("EUR", 3.99)
					})
				},
				ProductsSpent = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "thePickOfDestiny",
							ItemAmount = 1L,
							ItemType = "collectable"
						}
					}
				},
				TransactionName = "sellItem",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000A154C File Offset: 0x0009F74C
		public static void RecordSaleTransactionWithVirtualCurrency()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					VirtualCurrencies = new List<VirtualCurrency>
					{
						new VirtualCurrency
						{
							VirtualCurrencyType = VirtualCurrencyType.GRIND,
							VirtualCurrencyAmount = 125000L,
							VirtualCurrencyName = "Cor"
						}
					}
				},
				ProductsSpent = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "elucidator",
							ItemAmount = 1L,
							ItemType = "sword"
						}
					}
				},
				TransactionName = "sellItem",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000A1618 File Offset: 0x0009F818
		public static void RecordSaleTransactionWithMultipleVirtualCurrencies()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					VirtualCurrencies = new List<VirtualCurrency>
					{
						new VirtualCurrency
						{
							VirtualCurrencyType = VirtualCurrencyType.PREMIUM,
							VirtualCurrencyAmount = 100L,
							VirtualCurrencyName = "Soul Points"
						},
						new VirtualCurrency
						{
							VirtualCurrencyType = VirtualCurrencyType.GRIND,
							VirtualCurrencyAmount = 50000L,
							VirtualCurrencyName = "Gold Coins"
						}
					}
				},
				ProductsSpent = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "darkRepulser",
							ItemAmount = 1L,
							ItemType = "weapon"
						}
					}
				},
				TransactionName = "sellItem",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000A1714 File Offset: 0x0009F914
		public static void RecordSaleEventWithOneItem()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "cabbage",
							ItemAmount = 50L,
							ItemType = "food"
						}
					}
				},
				ProductsSpent = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "marketStall",
							ItemAmount = 1L,
							ItemType = "special"
						}
					}
				},
				TransactionName = "tradeItems",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000A17E4 File Offset: 0x0009F9E4
		public static void RecordSaleEventWithMultipleItems()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "pancake",
							ItemAmount = 2L,
							ItemType = "food"
						},
						new Item
						{
							ItemName = "whippedCream",
							ItemAmount = 165L,
							ItemType = "food"
						}
					}
				},
				ProductsSpent = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "flour",
							ItemAmount = 100L,
							ItemType = "food"
						},
						new Item
						{
							ItemName = "egg",
							ItemAmount = 1L,
							ItemType = "food"
						},
						new Item
						{
							ItemName = "milk",
							ItemAmount = 200L,
							ItemType = "food"
						},
						new Item
						{
							ItemName = "salt",
							ItemAmount = 1L,
							ItemType = "food"
						},
						new Item
						{
							ItemName = "heavyCream",
							ItemAmount = 150L,
							ItemType = "food"
						},
						new Item
						{
							ItemName = "sugar",
							ItemAmount = 15L,
							ItemType = "food"
						}
					}
				},
				TransactionName = "tradeItems",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000A19E0 File Offset: 0x0009FBE0
		public static void RecordSaleEventWithOptionalParameters()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				PaymentCountry = "PL",
				ProductID = "productid987",
				RevenueValidated = new long?(999L),
				TransactionID = "0118-999-881-999-119-725-3",
				TransactionReceipt = "transactionrecepit",
				TransactionReceiptSignature = "signature",
				TransactionServer = new TransactionServer?(TransactionServer.APPLE),
				TransactorID = "transactorid-0118-999-881-999-119-725-3",
				StoreItemSkuID = "storeitemskuid",
				StoreItemID = "storeitemid",
				StoreID = "storeid",
				StoreSourceID = "storesourceid",
				ProductsReceived = default(Product),
				ProductsSpent = default(Product),
				TransactionName = "transactionName",
				TransactionType = TransactionType.SALE
			});
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000A1AC8 File Offset: 0x0009FCC8
		public static void RecordAcquisitionSourceEventWithOnlyRequiredValues()
		{
			AnalyticsService.Instance.AcquisitionSource(new AcquisitionSourceParameters
			{
				Channel = "CHNL",
				CampaignId = "123-456-efg",
				CreativeId = "cre-ati-vei-d",
				CampaignName = "Interstitial:Halloween21",
				Provider = "AppsFlyer"
			});
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000A1B1C File Offset: 0x0009FD1C
		public static void RecordAcquisitionSourceEventWithOptionalParameters()
		{
			AnalyticsService.Instance.AcquisitionSource(new AcquisitionSourceParameters
			{
				Channel = "CHNL",
				CampaignId = "123-456-efg",
				CreativeId = "cre-ati-vei-d",
				CampaignName = "Interstitial:Halloween21",
				Provider = "AppsFlyer",
				CampaignType = "CPI",
				Cost = new float?(123.4f),
				CostCurrency = "BGN",
				Network = "Ironsource"
			});
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000A1BA0 File Offset: 0x0009FDA0
		public static void RecordPurchaseEventWithOneItem()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "nerveGear",
							ItemAmount = 1L,
							ItemType = "electronics"
						}
					}
				},
				ProductsSpent = new Product
				{
					RealCurrency = new RealCurrency?(new RealCurrency
					{
						RealCurrencyAmount = AnalyticsService.Instance.ConvertCurrencyToMinorUnits("JPY", 39800.0),
						RealCurrencyType = "JPY"
					})
				},
				TransactionName = "itemPurchase",
				TransactionType = TransactionType.PURCHASE
			});
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000A1C70 File Offset: 0x0009FE70
		public static void RecordPurchaseEventWithMultipleItems()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "magicarp",
							ItemAmount = 1L,
							ItemType = "pokemon"
						},
						new Item
						{
							ItemName = "rareCandy",
							ItemAmount = 20L,
							ItemType = "item"
						}
					}
				},
				ProductsSpent = new Product
				{
					VirtualCurrencies = new List<VirtualCurrency>
					{
						new VirtualCurrency
						{
							VirtualCurrencyType = VirtualCurrencyType.GRIND,
							VirtualCurrencyAmount = 200500L,
							VirtualCurrencyName = "Pokemon Dollar"
						}
					}
				},
				TransactionName = "itemPurchase",
				TransactionType = TransactionType.PURCHASE
			});
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000A1D70 File Offset: 0x0009FF70
		public static void RecordPurchaseEventWithMultipleCurrencies()
		{
			AnalyticsService.Instance.Transaction(new TransactionParameters
			{
				ProductsReceived = new Product
				{
					Items = new List<Item>
					{
						new Item
						{
							ItemName = "holySwordExcalibur",
							ItemAmount = 1L,
							ItemType = "weapon"
						}
					}
				},
				ProductsSpent = new Product
				{
					VirtualCurrencies = new List<VirtualCurrency>
					{
						new VirtualCurrency
						{
							VirtualCurrencyType = VirtualCurrencyType.GRIND,
							VirtualCurrencyAmount = 4000000L,
							VirtualCurrencyName = "Cor"
						},
						new VirtualCurrency
						{
							VirtualCurrencyType = VirtualCurrencyType.PREMIUM,
							VirtualCurrencyAmount = 50000L,
							VirtualCurrencyName = "Credit"
						}
					}
				},
				TransactionName = "itemPurchase",
				TransactionType = TransactionType.PURCHASE
			});
		}
	}
}
