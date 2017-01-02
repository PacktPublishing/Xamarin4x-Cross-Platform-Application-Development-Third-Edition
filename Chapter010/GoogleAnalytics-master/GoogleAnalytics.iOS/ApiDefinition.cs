using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace GoogleAnalytics.iOS
{
	// @protocol GAILogger <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GAILogger
	{
		// @required @property (assign, nonatomic) GAILogLevel logLevel;
		[Abstract]
		[Export ("logLevel", ArgumentSemantic.Assign)]
		GAILogLevel LogLevel { get; set; }

		// @required -(void)verbose:(NSString *)message;
		[Abstract]
		[Export ("verbose:")]
		void Verbose (string message);

		// @required -(void)info:(NSString *)message;
		[Abstract]
		[Export ("info:")]
		void Info (string message);

		// @required -(void)warning:(NSString *)message;
		[Abstract]
		[Export ("warning:")]
		void Warning (string message);

		// @required -(void)error:(NSString *)message;
		[Abstract]
		[Export ("error:")]
		void Error (string message);
	}

	// @interface GAITrackedViewController : UIViewController
	[BaseType (typeof(UIViewController))]
	interface GAITrackedViewController
	{
		// @property (assign, nonatomic) id<GAITracker> tracker;
		[Export ("tracker", ArgumentSemantic.Assign)]
		GAITracker Tracker { get; set; }

		// @property (copy, nonatomic) NSString * screenName;
		[Export ("screenName")]
		string ScreenName { get; set; }
	}

	// @protocol GAITracker <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GAITracker
	{
		// @required @property (readonly, nonatomic) NSString * name;
		[Abstract]
		[Export ("name")]
		string Name { get; }

		// @required @property (nonatomic) BOOL allowIDFACollection;
		[Abstract]
		[Export ("allowIDFACollection")]
		bool AllowIDFACollection { get; set; }

		// @required -(void)set:(NSString *)parameterName value:(NSString *)value;
		[Abstract]
		[Export ("set:value:")]
		void Set (string parameterName, string value);

		// @required -(NSString *)get:(NSString *)parameterName;
		[Abstract]
		[Export ("get:")]
		string Get (string parameterName);

		// @required -(void)send:(NSDictionary *)parameters;
		[Abstract]
		[Export ("send:")]
		void Send (NSDictionary parameters);
	}

	//[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern NSString *const kGAIProduct;
		[Field ("kGAIProduct", "__Internal")]
		NSString kGAIProduct { get; }

		// extern NSString *const kGAIVersion;
		[Field ("kGAIVersion", "__Internal")]
		NSString kGAIVersion { get; }

		// extern NSString *const kGAIErrorDomain;
		[Field ("kGAIErrorDomain", "__Internal")]
		NSString kGAIErrorDomain { get; }
	}

	// @interface GAI : NSObject
	[BaseType (typeof(NSObject))]
	interface GAI
	{
		// @property (assign, nonatomic) id<GAITracker> defaultTracker;
		[Export ("defaultTracker", ArgumentSemantic.Assign)]
		GAITracker DefaultTracker { get; set; }

		// @property (retain, nonatomic) id<GAILogger> logger;
		[Export ("logger", ArgumentSemantic.Retain)]
		GAILogger Logger { get; set; }

		// @property (assign, nonatomic) BOOL optOut;
		[Export ("optOut")]
		bool OptOut { get; set; }

		// @property (assign, nonatomic) NSTimeInterval dispatchInterval;
		[Export ("dispatchInterval")]
		double DispatchInterval { get; set; }

		// @property (assign, nonatomic) BOOL trackUncaughtExceptions;
		[Export ("trackUncaughtExceptions")]
		bool TrackUncaughtExceptions { get; set; }

		// @property (assign, nonatomic) BOOL dryRun;
		[Export ("dryRun")]
		bool DryRun { get; set; }

		// +(GAI *)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		//[Verify (MethodToProperty)]
		GAI SharedInstance { get; }

		// -(id<GAITracker>)trackerWithName:(NSString *)name trackingId:(NSString *)trackingId;
		[Export ("trackerWithName:trackingId:")]
		GAITracker TrackerWithName (string name, string trackingId);

		// -(id<GAITracker>)trackerWithTrackingId:(NSString *)trackingId;
		[Export ("trackerWithTrackingId:")]
		GAITracker TrackerWithTrackingId (string trackingId);

		// -(void)removeTrackerByName:(NSString *)name;
		[Export ("removeTrackerByName:")]
		void RemoveTrackerByName (string name);

		// -(void)dispatch;
		[Export ("dispatch")]
		void Dispatch ();

		// -(void)dispatchWithCompletionHandler:(void (^)(GAIDispatchResult))completionHandler;
		[Export ("dispatchWithCompletionHandler:")]
		void DispatchWithCompletionHandler (Action<GAIDispatchResult> completionHandler);
	}

	// @interface GAIEcommerceProduct : NSObject
	[BaseType (typeof(NSObject))]
	interface GAIEcommerceProduct
	{
		// -(GAIEcommerceProduct *)setId:(NSString *)productId;
		[Export ("setId:")]
		GAIEcommerceProduct SetId (string productId);

		// -(GAIEcommerceProduct *)setName:(NSString *)productName;
		[Export ("setName:")]
		GAIEcommerceProduct SetName (string productName);

		// -(GAIEcommerceProduct *)setBrand:(NSString *)productBrand;
		[Export ("setBrand:")]
		GAIEcommerceProduct SetBrand (string productBrand);

		// -(GAIEcommerceProduct *)setCategory:(NSString *)productCategory;
		[Export ("setCategory:")]
		GAIEcommerceProduct SetCategory (string productCategory);

		// -(GAIEcommerceProduct *)setVariant:(NSString *)productVariant;
		[Export ("setVariant:")]
		GAIEcommerceProduct SetVariant (string productVariant);

		// -(GAIEcommerceProduct *)setPrice:(NSNumber *)productPrice;
		[Export ("setPrice:")]
		GAIEcommerceProduct SetPrice (NSNumber productPrice);

		// -(GAIEcommerceProduct *)setQuantity:(NSNumber *)productQuantity;
		[Export ("setQuantity:")]
		GAIEcommerceProduct SetQuantity (NSNumber productQuantity);

		// -(GAIEcommerceProduct *)setCouponCode:(NSString *)productCouponCode;
		[Export ("setCouponCode:")]
		GAIEcommerceProduct SetCouponCode (string productCouponCode);

		// -(GAIEcommerceProduct *)setPosition:(NSNumber *)productPosition;
		[Export ("setPosition:")]
		GAIEcommerceProduct SetPosition (NSNumber productPosition);

		// -(GAIEcommerceProduct *)setCustomDimension:(NSUInteger)index value:(NSString *)value;
		[Export ("setCustomDimension:value:")]
		GAIEcommerceProduct SetCustomDimension (nuint index, string value);

		// -(GAIEcommerceProduct *)setCustomMetric:(NSUInteger)index value:(NSNumber *)value;
		[Export ("setCustomMetric:value:")]
		GAIEcommerceProduct SetCustomMetric (nuint index, NSNumber value);

		// -(NSDictionary *)buildWithIndex:(NSUInteger)index;
		[Export ("buildWithIndex:")]
		NSDictionary BuildWithIndex (nuint index);

		// -(NSDictionary *)buildWithListIndex:(NSUInteger)lIndex index:(NSUInteger)index;
		[Export ("buildWithListIndex:index:")]
		NSDictionary BuildWithListIndex (nuint lIndex, nuint index);
	}

	// @interface GAIEcommerceProductAction : NSObject
	[BaseType (typeof(NSObject))]
	interface GAIEcommerceProductAction
	{
		// -(GAIEcommerceProductAction *)setAction:(NSString *)productAction;
		[Export ("setAction:")]
		GAIEcommerceProductAction SetAction (string productAction);

		// -(GAIEcommerceProductAction *)setTransactionId:(NSString *)transactionId;
		[Export ("setTransactionId:")]
		GAIEcommerceProductAction SetTransactionId (string transactionId);

		// -(GAIEcommerceProductAction *)setAffiliation:(NSString *)affiliation;
		[Export ("setAffiliation:")]
		GAIEcommerceProductAction SetAffiliation (string affiliation);

		// -(GAIEcommerceProductAction *)setRevenue:(NSNumber *)revenue;
		[Export ("setRevenue:")]
		GAIEcommerceProductAction SetRevenue (NSNumber revenue);

		// -(GAIEcommerceProductAction *)setTax:(NSNumber *)tax;
		[Export ("setTax:")]
		GAIEcommerceProductAction SetTax (NSNumber tax);

		// -(GAIEcommerceProductAction *)setShipping:(NSNumber *)shipping;
		[Export ("setShipping:")]
		GAIEcommerceProductAction SetShipping (NSNumber shipping);

		// -(GAIEcommerceProductAction *)setCouponCode:(NSString *)couponCode;
		[Export ("setCouponCode:")]
		GAIEcommerceProductAction SetCouponCode (string couponCode);

		// -(GAIEcommerceProductAction *)setCheckoutStep:(NSNumber *)checkoutStep;
		[Export ("setCheckoutStep:")]
		GAIEcommerceProductAction SetCheckoutStep (NSNumber checkoutStep);

		// -(GAIEcommerceProductAction *)setCheckoutOption:(NSString *)checkoutOption;
		[Export ("setCheckoutOption:")]
		GAIEcommerceProductAction SetCheckoutOption (string checkoutOption);

		// -(GAIEcommerceProductAction *)setProductActionList:(NSString *)productActionList;
		[Export ("setProductActionList:")]
		GAIEcommerceProductAction SetProductActionList (string productActionList);

		// -(GAIEcommerceProductAction *)setProductListSource:(NSString *)productListSource;
		[Export ("setProductListSource:")]
		GAIEcommerceProductAction SetProductListSource (string productListSource);

		// -(NSDictionary *)build;
		[Export ("build")]
		//[Verify (MethodToProperty)]
		NSDictionary Build { get; }
	}

	// @interface GAIEcommercePromotion : NSObject
	[BaseType (typeof(NSObject))]
	interface GAIEcommercePromotion
	{
		// -(GAIEcommercePromotion *)setId:(NSString *)pid;
		[Export ("setId:")]
		GAIEcommercePromotion SetId (string pid);

		// -(GAIEcommercePromotion *)setName:(NSString *)name;
		[Export ("setName:")]
		GAIEcommercePromotion SetName (string name);

		// -(GAIEcommercePromotion *)setCreative:(NSString *)creative;
		[Export ("setCreative:")]
		GAIEcommercePromotion SetCreative (string creative);

		// -(GAIEcommercePromotion *)setPosition:(NSString *)position;
		[Export ("setPosition:")]
		GAIEcommercePromotion SetPosition (string position);

		// -(NSDictionary *)buildWithIndex:(NSUInteger)index;
		[Export ("buildWithIndex:")]
		NSDictionary BuildWithIndex (nuint index);
	}

	// @interface GAIDictionaryBuilder : NSObject
	[BaseType (typeof(NSObject))]
	interface GAIDictionaryBuilder
	{
		// -(GAIDictionaryBuilder *)set:(NSString *)value forKey:(NSString *)key;
		[Export ("set:forKey:")]
		GAIDictionaryBuilder Set (string value, string key);

		// -(GAIDictionaryBuilder *)setAll:(NSDictionary *)params;
		[Export ("setAll:")]
		GAIDictionaryBuilder SetAll (NSDictionary @params);

		// -(NSString *)get:(NSString *)paramName;
		[Export ("get:")]
		string Get (string paramName);

		// -(NSMutableDictionary *)build;
		[Export ("build")]
		//[Verify (MethodToProperty)]
		NSMutableDictionary Build { get; }

		// -(GAIDictionaryBuilder *)setCampaignParametersFromUrl:(NSString *)urlString;
		[Export ("setCampaignParametersFromUrl:")]
		GAIDictionaryBuilder SetCampaignParametersFromUrl (string urlString);

		// +(GAIDictionaryBuilder *)createAppView __attribute__((deprecated("Use createScreenView instead.")));
		[Static]
		[Export ("createAppView")]
		//[Verify (MethodToProperty)]
		GAIDictionaryBuilder CreateAppView { get; }

		// +(GAIDictionaryBuilder *)createScreenView;
		[Static]
		[Export ("createScreenView")]
		//[Verify (MethodToProperty)]
		GAIDictionaryBuilder CreateScreenView { get; }

		// +(GAIDictionaryBuilder *)createEventWithCategory:(NSString *)category action:(NSString *)action label:(NSString *)label value:(NSNumber *)value;
		[Static]
		[Export ("createEventWithCategory:action:label:value:")]
		GAIDictionaryBuilder CreateEventWithCategory (string category, string action, string label, NSNumber value);

		// +(GAIDictionaryBuilder *)createExceptionWithDescription:(NSString *)description withFatal:(NSNumber *)fatal;
		[Static]
		[Export ("createExceptionWithDescription:withFatal:")]
		GAIDictionaryBuilder CreateExceptionWithDescription (string description, NSNumber fatal);

		// +(GAIDictionaryBuilder *)createItemWithTransactionId:(NSString *)transactionId name:(NSString *)name sku:(NSString *)sku category:(NSString *)category price:(NSNumber *)price quantity:(NSNumber *)quantity currencyCode:(NSString *)currencyCode;
		[Static]
		[Export ("createItemWithTransactionId:name:sku:category:price:quantity:currencyCode:")]
		GAIDictionaryBuilder CreateItemWithTransactionId (string transactionId, string name, string sku, string category, NSNumber price, NSNumber quantity, string currencyCode);

		// +(GAIDictionaryBuilder *)createSocialWithNetwork:(NSString *)network action:(NSString *)action target:(NSString *)target;
		[Static]
		[Export ("createSocialWithNetwork:action:target:")]
		GAIDictionaryBuilder CreateSocialWithNetwork (string network, string action, string target);

		// +(GAIDictionaryBuilder *)createTimingWithCategory:(NSString *)category interval:(NSNumber *)intervalMillis name:(NSString *)name label:(NSString *)label;
		[Static]
		[Export ("createTimingWithCategory:interval:name:label:")]
		GAIDictionaryBuilder CreateTimingWithCategory (string category, NSNumber intervalMillis, string name, string label);

		// +(GAIDictionaryBuilder *)createTransactionWithId:(NSString *)transactionId affiliation:(NSString *)affiliation revenue:(NSNumber *)revenue tax:(NSNumber *)tax shipping:(NSNumber *)shipping currencyCode:(NSString *)currencyCode;
		[Static]
		[Export ("createTransactionWithId:affiliation:revenue:tax:shipping:currencyCode:")]
		GAIDictionaryBuilder CreateTransactionWithId (string transactionId, string affiliation, NSNumber revenue, NSNumber tax, NSNumber shipping, string currencyCode);

		// -(GAIDictionaryBuilder *)setProductAction:(GAIEcommerceProductAction *)productAction;
		[Export ("setProductAction:")]
		GAIDictionaryBuilder SetProductAction (GAIEcommerceProductAction productAction);

		// -(GAIDictionaryBuilder *)addProduct:(GAIEcommerceProduct *)product;
		[Export ("addProduct:")]
		GAIDictionaryBuilder AddProduct (GAIEcommerceProduct product);

		// -(GAIDictionaryBuilder *)addProductImpression:(GAIEcommerceProduct *)product impressionList:(NSString *)name impressionSource:(NSString *)source;
		[Export ("addProductImpression:impressionList:impressionSource:")]
		GAIDictionaryBuilder AddProductImpression (GAIEcommerceProduct product, string name, string source);

		// -(GAIDictionaryBuilder *)addPromotion:(GAIEcommercePromotion *)promotion;
		[Export ("addPromotion:")]
		GAIDictionaryBuilder AddPromotion (GAIEcommercePromotion promotion);
	}

	//[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern NSString *const kGAIProductId;
		[Field ("kGAIProductId", "__Internal")]
		NSString kGAIProductId { get; }

		// extern NSString *const kGAIProductName;
		[Field ("kGAIProductName", "__Internal")]
		NSString kGAIProductName { get; }

		// extern NSString *const kGAIProductBrand;
		[Field ("kGAIProductBrand", "__Internal")]
		NSString kGAIProductBrand { get; }

		// extern NSString *const kGAIProductCategory;
		[Field ("kGAIProductCategory", "__Internal")]
		NSString kGAIProductCategory { get; }

		// extern NSString *const kGAIProductVariant;
		[Field ("kGAIProductVariant", "__Internal")]
		NSString kGAIProductVariant { get; }

		// extern NSString *const kGAIProductPrice;
		[Field ("kGAIProductPrice", "__Internal")]
		NSString kGAIProductPrice { get; }

		// extern NSString *const kGAIProductQuantity;
		[Field ("kGAIProductQuantity", "__Internal")]
		NSString kGAIProductQuantity { get; }

		// extern NSString *const kGAIProductCouponCode;
		[Field ("kGAIProductCouponCode", "__Internal")]
		NSString kGAIProductCouponCode { get; }

		// extern NSString *const kGAIProductPosition;
		[Field ("kGAIProductPosition", "__Internal")]
		NSString kGAIProductPosition { get; }

		// extern NSString *const kGAIProductAction;
		[Field ("kGAIProductAction", "__Internal")]
		NSString kGAIProductAction { get; }

		// extern NSString *const kGAIPADetail;
		[Field ("kGAIPADetail", "__Internal")]
		NSString kGAIPADetail { get; }

		// extern NSString *const kGAIPAClick;
		[Field ("kGAIPAClick", "__Internal")]
		NSString kGAIPAClick { get; }

		// extern NSString *const kGAIPAAdd;
		[Field ("kGAIPAAdd", "__Internal")]
		NSString kGAIPAAdd { get; }

		// extern NSString *const kGAIPARemove;
		[Field ("kGAIPARemove", "__Internal")]
		NSString kGAIPARemove { get; }

		// extern NSString *const kGAIPACheckout;
		[Field ("kGAIPACheckout", "__Internal")]
		NSString kGAIPACheckout { get; }

		// extern NSString *const kGAIPACheckoutOption;
		[Field ("kGAIPACheckoutOption", "__Internal")]
		NSString kGAIPACheckoutOption { get; }

		// extern NSString *const kGAIPAPurchase;
		[Field ("kGAIPAPurchase", "__Internal")]
		NSString kGAIPAPurchase { get; }

		// extern NSString *const kGAIPARefund;
		[Field ("kGAIPARefund", "__Internal")]
		NSString kGAIPARefund { get; }

		// extern NSString *const kGAIPATransactionId;
		[Field ("kGAIPATransactionId", "__Internal")]
		NSString kGAIPATransactionId { get; }

		// extern NSString *const kGAIPAAffiliation;
		[Field ("kGAIPAAffiliation", "__Internal")]
		NSString kGAIPAAffiliation { get; }

		// extern NSString *const kGAIPARevenue;
		[Field ("kGAIPARevenue", "__Internal")]
		NSString kGAIPARevenue { get; }

		// extern NSString *const kGAIPATax;
		[Field ("kGAIPATax", "__Internal")]
		NSString kGAIPATax { get; }

		// extern NSString *const kGAIPAShipping;
		[Field ("kGAIPAShipping", "__Internal")]
		NSString kGAIPAShipping { get; }

		// extern NSString *const kGAIPACouponCode;
		[Field ("kGAIPACouponCode", "__Internal")]
		NSString kGAIPACouponCode { get; }

		// extern NSString *const kGAICheckoutStep;
		[Field ("kGAICheckoutStep", "__Internal")]
		NSString kGAICheckoutStep { get; }

		// extern NSString *const kGAICheckoutOption;
		[Field ("kGAICheckoutOption", "__Internal")]
		NSString kGAICheckoutOption { get; }

		// extern NSString *const kGAIProductActionList;
		[Field ("kGAIProductActionList", "__Internal")]
		NSString kGAIProductActionList { get; }

		// extern NSString *const kGAIProductListSource;
		[Field ("kGAIProductListSource", "__Internal")]
		NSString kGAIProductListSource { get; }

		// extern NSString *const kGAIImpressionName;
		[Field ("kGAIImpressionName", "__Internal")]
		NSString kGAIImpressionName { get; }

		// extern NSString *const kGAIImpressionListSource;
		[Field ("kGAIImpressionListSource", "__Internal")]
		NSString kGAIImpressionListSource { get; }

		// extern NSString *const kGAIImpressionProduct;
		[Field ("kGAIImpressionProduct", "__Internal")]
		NSString kGAIImpressionProduct { get; }

		// extern NSString *const kGAIImpressionProductId;
		[Field ("kGAIImpressionProductId", "__Internal")]
		NSString kGAIImpressionProductId { get; }

		// extern NSString *const kGAIImpressionProductName;
		[Field ("kGAIImpressionProductName", "__Internal")]
		NSString kGAIImpressionProductName { get; }

		// extern NSString *const kGAIImpressionProductBrand;
		[Field ("kGAIImpressionProductBrand", "__Internal")]
		NSString kGAIImpressionProductBrand { get; }

		// extern NSString *const kGAIImpressionProductCategory;
		[Field ("kGAIImpressionProductCategory", "__Internal")]
		NSString kGAIImpressionProductCategory { get; }

		// extern NSString *const kGAIImpressionProductVariant;
		[Field ("kGAIImpressionProductVariant", "__Internal")]
		NSString kGAIImpressionProductVariant { get; }

		// extern NSString *const kGAIImpressionProductPosition;
		[Field ("kGAIImpressionProductPosition", "__Internal")]
		NSString kGAIImpressionProductPosition { get; }

		// extern NSString *const kGAIImpressionProductPrice;
		[Field ("kGAIImpressionProductPrice", "__Internal")]
		NSString kGAIImpressionProductPrice { get; }

		// extern NSString *const kGAIPromotionId;
		[Field ("kGAIPromotionId", "__Internal")]
		NSString kGAIPromotionId { get; }

		// extern NSString *const kGAIPromotionName;
		[Field ("kGAIPromotionName", "__Internal")]
		NSString kGAIPromotionName { get; }

		// extern NSString *const kGAIPromotionCreative;
		[Field ("kGAIPromotionCreative", "__Internal")]
		NSString kGAIPromotionCreative { get; }

		// extern NSString *const kGAIPromotionPosition;
		[Field ("kGAIPromotionPosition", "__Internal")]
		NSString kGAIPromotionPosition { get; }

		// extern NSString *const kGAIPromotionAction;
		[Field ("kGAIPromotionAction", "__Internal")]
		NSString kGAIPromotionAction { get; }

		// extern NSString *const kGAIPromotionView;
		[Field ("kGAIPromotionView", "__Internal")]
		NSString kGAIPromotionView { get; }

		// extern NSString *const kGAIPromotionClick;
		[Field ("kGAIPromotionClick", "__Internal")]
		NSString kGAIPromotionClick { get; }
	}

	// @interface GAIEcommerceFields : NSObject
	[BaseType (typeof(NSObject))]
	interface GAIEcommerceFields
	{
		// +(NSString *)productFieldForIndex:(NSUInteger)index suffix:(NSString *)suffix;
		[Static]
		[Export ("productFieldForIndex:suffix:")]
		string ProductFieldForIndex (nuint index, string suffix);

		// +(NSString *)impressionListForIndex:(NSUInteger)index;
		[Static]
		[Export ("impressionListForIndex:")]
		string ImpressionListForIndex (nuint index);

		// +(NSString *)productImpressionForList:(NSString *)list index:(NSUInteger)index suffix:(NSString *)Suffix;
		[Static]
		[Export ("productImpressionForList:index:suffix:")]
		string ProductImpressionForList (string list, nuint index, string Suffix);

		// +(NSString *)promotionForIndex:(NSUInteger)index suffix:(NSString *)suffix;
		[Static]
		[Export ("promotionForIndex:suffix:")]
		string PromotionForIndex (nuint index, string suffix);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern NSString *const kGAIUseSecure;
		[Field ("kGAIUseSecure", "__Internal")]
		NSString kGAIUseSecure { get; }

		// extern NSString *const kGAIHitType;
		[Field ("kGAIHitType", "__Internal")]
		NSString kGAIHitType { get; }

		// extern NSString *const kGAITrackingId;
		[Field ("kGAITrackingId", "__Internal")]
		NSString kGAITrackingId { get; }

		// extern NSString *const kGAIClientId;
		[Field ("kGAIClientId", "__Internal")]
		NSString kGAIClientId { get; }

		// extern NSString *const kGAIDataSource;
		[Field ("kGAIDataSource", "__Internal")]
		NSString kGAIDataSource { get; }

		// extern NSString *const kGAIAnonymizeIp;
		[Field ("kGAIAnonymizeIp", "__Internal")]
		NSString kGAIAnonymizeIp { get; }

		// extern NSString *const kGAISessionControl;
		[Field ("kGAISessionControl", "__Internal")]
		NSString kGAISessionControl { get; }

		// extern NSString *const kGAIDeviceModelVersion;
		[Field ("kGAIDeviceModelVersion", "__Internal")]
		NSString kGAIDeviceModelVersion { get; }

		// extern NSString *const kGAIScreenResolution;
		[Field ("kGAIScreenResolution", "__Internal")]
		NSString kGAIScreenResolution { get; }

		// extern NSString *const kGAIViewportSize;
		[Field ("kGAIViewportSize", "__Internal")]
		NSString kGAIViewportSize { get; }

		// extern NSString *const kGAIEncoding;
		[Field ("kGAIEncoding", "__Internal")]
		NSString kGAIEncoding { get; }

		// extern NSString *const kGAIScreenColors;
		[Field ("kGAIScreenColors", "__Internal")]
		NSString kGAIScreenColors { get; }

		// extern NSString *const kGAILanguage;
		[Field ("kGAILanguage", "__Internal")]
		NSString kGAILanguage { get; }

		// extern NSString *const kGAIJavaEnabled;
		[Field ("kGAIJavaEnabled", "__Internal")]
		NSString kGAIJavaEnabled { get; }

		// extern NSString *const kGAIFlashVersion;
		[Field ("kGAIFlashVersion", "__Internal")]
		NSString kGAIFlashVersion { get; }

		// extern NSString *const kGAINonInteraction;
		[Field ("kGAINonInteraction", "__Internal")]
		NSString kGAINonInteraction { get; }

		// extern NSString *const kGAIReferrer;
		[Field ("kGAIReferrer", "__Internal")]
		NSString kGAIReferrer { get; }

		// extern NSString *const kGAILocation;
		[Field ("kGAILocation", "__Internal")]
		NSString kGAILocation { get; }

		// extern NSString *const kGAIHostname;
		[Field ("kGAIHostname", "__Internal")]
		NSString kGAIHostname { get; }

		// extern NSString *const kGAIPage;
		[Field ("kGAIPage", "__Internal")]
		NSString kGAIPage { get; }

		// extern NSString *const kGAIDescription;
		[Field ("kGAIDescription", "__Internal")]
		NSString kGAIDescription { get; }

		// extern NSString *const kGAIScreenName;
		[Field ("kGAIScreenName", "__Internal")]
		NSString kGAIScreenName { get; }

		// extern NSString *const kGAITitle;
		[Field ("kGAITitle", "__Internal")]
		NSString kGAITitle { get; }

		// extern NSString *const kGAIAdMobHitId;
		[Field ("kGAIAdMobHitId", "__Internal")]
		NSString kGAIAdMobHitId { get; }

		// extern NSString *const kGAIAppName;
		[Field ("kGAIAppName", "__Internal")]
		NSString kGAIAppName { get; }

		// extern NSString *const kGAIAppVersion;
		[Field ("kGAIAppVersion", "__Internal")]
		NSString kGAIAppVersion { get; }

		// extern NSString *const kGAIAppId;
		[Field ("kGAIAppId", "__Internal")]
		NSString kGAIAppId { get; }

		// extern NSString *const kGAIAppInstallerId;
		[Field ("kGAIAppInstallerId", "__Internal")]
		NSString kGAIAppInstallerId { get; }

		// extern NSString *const kGAIUserId;
		[Field ("kGAIUserId", "__Internal")]
		NSString kGAIUserId { get; }

		// extern NSString *const kGAIEventCategory;
		[Field ("kGAIEventCategory", "__Internal")]
		NSString kGAIEventCategory { get; }

		// extern NSString *const kGAIEventAction;
		[Field ("kGAIEventAction", "__Internal")]
		NSString kGAIEventAction { get; }

		// extern NSString *const kGAIEventLabel;
		[Field ("kGAIEventLabel", "__Internal")]
		NSString kGAIEventLabel { get; }

		// extern NSString *const kGAIEventValue;
		[Field ("kGAIEventValue", "__Internal")]
		NSString kGAIEventValue { get; }

		// extern NSString *const kGAISocialNetwork;
		[Field ("kGAISocialNetwork", "__Internal")]
		NSString kGAISocialNetwork { get; }

		// extern NSString *const kGAISocialAction;
		[Field ("kGAISocialAction", "__Internal")]
		NSString kGAISocialAction { get; }

		// extern NSString *const kGAISocialTarget;
		[Field ("kGAISocialTarget", "__Internal")]
		NSString kGAISocialTarget { get; }

		// extern NSString *const kGAITransactionId;
		[Field ("kGAITransactionId", "__Internal")]
		NSString kGAITransactionId { get; }

		// extern NSString *const kGAITransactionAffiliation;
		[Field ("kGAITransactionAffiliation", "__Internal")]
		NSString kGAITransactionAffiliation { get; }

		// extern NSString *const kGAITransactionRevenue;
		[Field ("kGAITransactionRevenue", "__Internal")]
		NSString kGAITransactionRevenue { get; }

		// extern NSString *const kGAITransactionShipping;
		[Field ("kGAITransactionShipping", "__Internal")]
		NSString kGAITransactionShipping { get; }

		// extern NSString *const kGAITransactionTax;
		[Field ("kGAITransactionTax", "__Internal")]
		NSString kGAITransactionTax { get; }

		// extern NSString *const kGAICurrencyCode;
		[Field ("kGAICurrencyCode", "__Internal")]
		NSString kGAICurrencyCode { get; }

		// extern NSString *const kGAIItemPrice;
		[Field ("kGAIItemPrice", "__Internal")]
		NSString kGAIItemPrice { get; }

		// extern NSString *const kGAIItemQuantity;
		[Field ("kGAIItemQuantity", "__Internal")]
		NSString kGAIItemQuantity { get; }

		// extern NSString *const kGAIItemSku;
		[Field ("kGAIItemSku", "__Internal")]
		NSString kGAIItemSku { get; }

		// extern NSString *const kGAIItemName;
		[Field ("kGAIItemName", "__Internal")]
		NSString kGAIItemName { get; }

		// extern NSString *const kGAIItemCategory;
		[Field ("kGAIItemCategory", "__Internal")]
		NSString kGAIItemCategory { get; }

		// extern NSString *const kGAICampaignSource;
		[Field ("kGAICampaignSource", "__Internal")]
		NSString kGAICampaignSource { get; }

		// extern NSString *const kGAICampaignMedium;
		[Field ("kGAICampaignMedium", "__Internal")]
		NSString kGAICampaignMedium { get; }

		// extern NSString *const kGAICampaignName;
		[Field ("kGAICampaignName", "__Internal")]
		NSString kGAICampaignName { get; }

		// extern NSString *const kGAICampaignKeyword;
		[Field ("kGAICampaignKeyword", "__Internal")]
		NSString kGAICampaignKeyword { get; }

		// extern NSString *const kGAICampaignContent;
		[Field ("kGAICampaignContent", "__Internal")]
		NSString kGAICampaignContent { get; }

		// extern NSString *const kGAICampaignId;
		[Field ("kGAICampaignId", "__Internal")]
		NSString kGAICampaignId { get; }

		// extern NSString *const kGAICampaignAdNetworkClickId;
		[Field ("kGAICampaignAdNetworkClickId", "__Internal")]
		NSString kGAICampaignAdNetworkClickId { get; }

		// extern NSString *const kGAICampaignAdNetworkId;
		[Field ("kGAICampaignAdNetworkId", "__Internal")]
		NSString kGAICampaignAdNetworkId { get; }

		// extern NSString *const kGAITimingCategory;
		[Field ("kGAITimingCategory", "__Internal")]
		NSString kGAITimingCategory { get; }

		// extern NSString *const kGAITimingVar;
		[Field ("kGAITimingVar", "__Internal")]
		NSString kGAITimingVar { get; }

		// extern NSString *const kGAITimingValue;
		[Field ("kGAITimingValue", "__Internal")]
		NSString kGAITimingValue { get; }

		// extern NSString *const kGAITimingLabel;
		[Field ("kGAITimingLabel", "__Internal")]
		NSString kGAITimingLabel { get; }

		// extern NSString *const kGAIExDescription;
		[Field ("kGAIExDescription", "__Internal")]
		NSString kGAIExDescription { get; }

		// extern NSString *const kGAIExFatal;
		[Field ("kGAIExFatal", "__Internal")]
		NSString kGAIExFatal { get; }

		// extern NSString *const kGAISampleRate;
		[Field ("kGAISampleRate", "__Internal")]
		NSString kGAISampleRate { get; }

		// extern NSString *const kGAIIdfa;
		[Field ("kGAIIdfa", "__Internal")]
		NSString kGAIIdfa { get; }

		// extern NSString *const kGAIAdTargetingEnabled;
		[Field ("kGAIAdTargetingEnabled", "__Internal")]
		NSString kGAIAdTargetingEnabled { get; }

		// extern NSString *const kGAIAppView __attribute__((deprecated("Use kGAIScreenView instead.")));
		[Field ("kGAIAppView", "__Internal")]
		NSString kGAIAppView { get; }

		// extern NSString *const kGAIScreenView;
		[Field ("kGAIScreenView", "__Internal")]
		NSString kGAIScreenView { get; }

		// extern NSString *const kGAIEvent;
		[Field ("kGAIEvent", "__Internal")]
		NSString kGAIEvent { get; }

		// extern NSString *const kGAISocial;
		[Field ("kGAISocial", "__Internal")]
		NSString kGAISocial { get; }

		// extern NSString *const kGAITransaction;
		[Field ("kGAITransaction", "__Internal")]
		NSString kGAITransaction { get; }

		// extern NSString *const kGAIItem;
		[Field ("kGAIItem", "__Internal")]
		NSString kGAIItem { get; }

		// extern NSString *const kGAIException;
		[Field ("kGAIException", "__Internal")]
		NSString kGAIException { get; }

		// extern NSString *const kGAITiming;
		[Field ("kGAITiming", "__Internal")]
		NSString kGAITiming { get; }
	}

	// @interface GAIFields : NSObject
	[BaseType (typeof(NSObject))]
	interface GAIFields
	{
		// +(NSString *)contentGroupForIndex:(NSUInteger)index;
		[Static]
		[Export ("contentGroupForIndex:")]
		string ContentGroupForIndex (nuint index);

		// +(NSString *)customDimensionForIndex:(NSUInteger)index;
		[Static]
		[Export ("customDimensionForIndex:")]
		string CustomDimensionForIndex (nuint index);

		// +(NSString *)customMetricForIndex:(NSUInteger)index;
		[Static]
		[Export ("customMetricForIndex:")]
		string CustomMetricForIndex (nuint index);
	}
}
