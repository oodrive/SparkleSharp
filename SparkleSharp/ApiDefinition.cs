using System;
using Foundation;
using ObjCRuntime;

namespace SparkleSharp
{

    // @interface SUAppcast : NSObject <NSURLDownloadDelegate>
    [BaseType (typeof (NSObject))]
    public interface SUAppcast
    {
        // @property (copy) NSString * userAgentString;
        [Export ("userAgentString")]
        string UserAgentString { get; set; }

        // @property (copy) NSDictionary * httpHeaders;
        [Export ("httpHeaders", ArgumentSemantic.Copy)]
        NSDictionary HttpHeaders { get; set; }

        // -(void)fetchAppcastFromURL:(NSURL *)url completionBlock:(void (^)(NSError *))err;
        [Export ("fetchAppcastFromURL:completionBlock:")]
        void FetchAppcastFromURL (NSUrl url, Action<NSError> err);

        // -(SUAppcast *)copyWithoutDeltaUpdates;
        [Export ("copyWithoutDeltaUpdates")]
        SUAppcast CopyWithoutDeltaUpdates { get; }

        // @property (readonly, copy) NSArray * items;
        [Export ("items", ArgumentSemantic.Copy)]
        NSObject [] Items { get; }

        // -(instancetype)initWithArray:(NSArray *)array;
        [Export("initWithArray:")]
        IntPtr Constructor(NSArray array);
    }

    // @interface SUAppcastItem : NSObject
    [BaseType (typeof (NSObject))]
    public interface SUAppcastItem
    {
        // @property (readonly, copy) NSString * title;
        [Export ("title", ArgumentSemantic.Copy)]
        string Title { get; }

        // @property (copy, readonly) NSString *dateString;
        [Export ("dateString", ArgumentSemantic.Copy)]
        NSString DateString { get; }

        // @property (readonly, copy) NSString * itemDescription;
        [Export ("itemDescription", ArgumentSemantic.Copy)]
        string ItemDescription { get; }

        // @property (readonly, strong) NSURL * releaseNotesURL;
        [Export ("releaseNotesURL", ArgumentSemantic.Strong)]
        NSUrl ReleaseNotesURL { get; }

        // @property (strong, readonly) SUSignatures *signatures;
        // Signatures cannot be mapped, because SUSignatures interface is not exported in Sparkle

        // @property (readonly, copy) NSString * minimumSystemVersion;
        [Export ("minimumSystemVersion", ArgumentSemantic.Copy)]
        string MinimumSystemVersion { get; }

        // @property (readonly, copy) NSString * maximumSystemVersion;
        [Export ("maximumSystemVersion", ArgumentSemantic.Copy)]
        string MaximumSystemVersion { get; }

        // @property (readonly, strong) NSURL * fileURL;
        [Export ("fileURL", ArgumentSemantic.Strong)]
        NSUrl FileURL { get; }

        // @property (readonly, copy) NSString * versionString;
        [Export ("versionString", ArgumentSemantic.Copy)]
        string VersionString { get; }

        // @property (copy, readonly) NSString *osString;
        [Export( "osString", ArgumentSemantic.Copy)]
        string OSString { get; }

        // @property (readonly, copy) NSString * displayVersionString;
        [Export ("displayVersionString", ArgumentSemantic.Copy)]
        string DisplayVersionString { get; }

        // @property (readonly, copy) NSDictionary * deltaUpdates;
        [Export ("deltaUpdates", ArgumentSemantic.Copy)]
        NSDictionary DeltaUpdates { get; }

        // @property (readonly, strong) NSURL * infoURL;
        [Export ("infoURL", ArgumentSemantic.Strong)]
        NSUrl InfoURL { get; }

        // -(instancetype)initWithDictionary:(NSDictionary *)dict;
        [Export ("initWithDictionary:")]
        IntPtr Constructor (NSDictionary dict);

        // -(instancetype)initWithDictionary:(NSDictionary *)dict failureReason:(NSString **)error;
        [Export ("initWithDictionary:failureReason:")]
        IntPtr Constructor (NSDictionary dict, out string error);

        // @property (readonly, getter = isDeltaUpdate) BOOL deltaUpdate;
        [Export ("deltaUpdate")]
        bool DeltaUpdate { [Bind ("isDeltaUpdate")] get; }

        // @property (readonly, getter = isCriticalUpdate) BOOL criticalUpdate;
        [Export ("criticalUpdate")]
        bool CriticalUpdate { [Bind ("isCriticalUpdate")] get; }

        // @property (getter=isMacOsUpdate, readonly) BOOL macOsUpdate
        [Export ("macOsUpdate")]
        bool MacOSUpdate { [Bind("isMacOsUpdate")] get; }

        // @property (readonly, getter = isInformationOnlyUpdate) BOOL informationOnlyUpdate;
        [Export ("informationOnlyUpdate")]
        bool InformationOnlyUpdate { [Bind ("isInformationOnlyUpdate")] get; }

        // @property (readonly, copy) NSDictionary * propertiesDictionary;
        [Export ("propertiesDictionary", ArgumentSemantic.Copy)]
        NSDictionary PropertiesDictionary { get; }
    }

    // @protocol SUVersionComparison
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    public interface SUVersionComparison
    {
        // @required -(NSComparisonResult)compareVersion:(NSString *)versionA toVersion:(NSString *)versionB;
        [Abstract]
        [Export ("compareVersion:toVersion:")]
        NSComparisonResult CompareVersion (string versionA, string versionB);
    }

    // @interface SUStandardVersionComparator : NSObject <SUVersionComparison>
    [BaseType (typeof (NSObject))]
    public interface SUStandardVersionComparator
    {
        // +(SUStandardVersionComparator *)defaultComparator;
        [Static]
        [Export ("defaultComparator")]
        SUStandardVersionComparator DefaultComparator { get; }

        // -(NSComparisonResult)compareVersion:(NSString *)versionA toVersion:(NSString *)versionB;
        [Export ("compareVersion:toVersion:")]
        NSComparisonResult CompareVersion (string versionA, string versionB);
    }

    // @protocol SUVersionDisplay
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    public interface SUVersionDisplay
    {
        // @required -(void)formatVersion:(NSString **)inOutVersionA andVersion:(NSString **)inOutVersionB;
        [Abstract]
        [Export ("formatVersion:andVersion:")]
        void FormatVersion (out string inOutVersionA, out string inOutVersionB);
    }

    // @interface SUUpdater : NSObject
    [BaseType (typeof (NSObject))]
    public interface SUUpdater
    {
        [Wrap ("WeakDelegate")]
        SUUpdaterDelegate Delegate { get; set; }

        // @property (unsafe_unretained) id<SUUpdaterDelegate> delegate __attribute__((iboutlet));
        [NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // +(SUUpdater *)sharedUpdater;
        [Static]
        [Export ("sharedUpdater")]
        SUUpdater SharedUpdater { get; }

        // +(SUUpdater *)updaterForBundle:(NSBundle *)bundle;
        [Static]
        [Export ("updaterForBundle:")]
        SUUpdater UpdaterForBundle (NSBundle bundle);

        // -(instancetype)initForBundle:(NSBundle *)bundle;
        [Export ("initForBundle:")]
        IntPtr Constructor (NSBundle bundle);

        // @property (readonly, strong) NSBundle * hostBundle;
        [Export ("hostBundle", ArgumentSemantic.Strong)]
        NSBundle HostBundle { get; }

        // @property (readonly, strong) NSBundle * sparkleBundle;
        [Export ("sparkleBundle", ArgumentSemantic.Strong)]
        NSBundle SparkleBundle { get; }

        // @property BOOL automaticallyChecksForUpdates;
        [Export ("automaticallyChecksForUpdates")]
        bool AutomaticallyChecksForUpdates { get; set; }

        // @property NSTimeInterval updateCheckInterval;
        [Export ("updateCheckInterval")]
        double UpdateCheckInterval { get; set; }

        // @property (copy) NSURL * feedURL;
        [Export ("feedURL", ArgumentSemantic.Copy)]
        NSUrl FeedURL { get; set; }

        // @property (copy, nonatomic) NSString * userAgentString;
        [Export ("userAgentString")]
        string UserAgentString { get; set; }

        // @property (copy) NSDictionary * httpHeaders;
        [Export ("httpHeaders", ArgumentSemantic.Copy)]
        NSDictionary HttpHeaders { get; set; }

        // @property BOOL sendsSystemProfile;
        [Export ("sendsSystemProfile")]
        bool SendsSystemProfile { get; set; }

        // @property BOOL automaticallyDownloadsUpdates;
        [Export ("automaticallyDownloadsUpdates")]
        bool AutomaticallyDownloadsUpdates { get; set; }

        // @property (copy, nonatomic) NSString * decryptionPassword;
        [Export ("decryptionPassword")]
        string DecryptionPassword { get; set; }

        // -(void)checkForUpdates:(id)sender __attribute__((ibaction));
        [Export ("checkForUpdates:")]
        void CheckForUpdates (NSObject sender);

        [Export ("checkForUpdatesFromAppcast:")]
        void CheckForUpdates (SUAppcast appcast);

        // -(void)checkForUpdatesInBackground;
        [Export ("checkForUpdatesInBackground")]
        void CheckForUpdatesInBackground ();

        // -(void)installUpdatesIfAvailable;
        [Export ("installUpdatesIfAvailable")]
        void InstallUpdatesIfAvailable ();

        // @property (readonly, copy) NSDate * lastUpdateCheckDate;
        [Export ("lastUpdateCheckDate", ArgumentSemantic.Copy)]
        NSDate LastUpdateCheckDate { get; }

        // -(void)checkForUpdateInformation;
        [Export ("checkForUpdateInformation")]
        void CheckForUpdateInformation ();

        // -(void)resetUpdateCycle;
        [Export ("resetUpdateCycle")]
        void ResetUpdateCycle ();

        // @property (readonly) BOOL updateInProgress;
        [Export ("updateInProgress")]
        bool UpdateInProgress { get; }
    }

    [Static]
    public partial interface Notifications
    {
        // extern NSString *const SUUpdaterDidFinishLoadingAppCastNotification;
        [Field ("SUUpdaterDidFinishLoadingAppCastNotification", LibraryName = "__Internal")]
        NSString SUUpdaterDidFinishLoadingAppCastNotification { get; }

        // extern NSString *const SUUpdaterDidFindValidUpdateNotification;
        [Field ("SUUpdaterDidFindValidUpdateNotification", LibraryName = "__Internal")]
        NSString SUUpdaterDidFindValidUpdateNotification { get; }

        // extern NSString *const SUUpdaterDidNotFindUpdateNotification;
        [Field ("SUUpdaterDidNotFindUpdateNotification", LibraryName = "__Internal")]
        NSString SUUpdaterDidNotFindUpdateNotification { get; }

        // extern NSString *const SUUpdaterWillRestartNotification;
        [Field ("SUUpdaterWillRestartNotification", LibraryName = "__Internal")]
        NSString SUUpdaterWillRestartNotification { get; }

        // extern NSString *const SUUpdaterAppcastItemNotificationKey;
        [Field ("SUUpdaterAppcastItemNotificationKey", LibraryName = "__Internal")]
        NSString SUUpdaterAppcastItemNotificationKey { get; }

        // extern NSString *const SUUpdaterAppcastNotificationKey;
        [Field ("SUUpdaterAppcastNotificationKey", LibraryName = "__Internal")]
        NSString SUUpdaterAppcastNotificationKey { get; }
    }


    // @protocol SUUpdaterDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    public interface SUUpdaterDelegate
    {
		/// <summary>
		///  Returns whether to allow Sparkle to pop up.
		///  For example, this may be used to prevent Sparkle from interrupting a setup assistant.
		///
		/// @optional -(BOOL)updaterMayCheckForUpdates:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export ("updaterMayCheckForUpdates:")]
        bool UpdaterMayCheckForUpdates (SUUpdater updater);

		/// <summary>
		/// Returns additional parameters to append to the appcast URL's query string.
		/// This is potentially based on whether or not Sparkle will also be sending along the system profile.
		///
		/// @optional -(NSArray *)feedParametersForUpdater:(SUUpdater *)updater sendingSystemProfile:(BOOL)sendingProfile;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="sendingProfile">Whether the system profile will also be sent.</param>
		/// <returns>An array of dictionaries with keys: "key", "value", "displayKey", "displayValue", the latter two being
		/// specifically for display to the user.</returns>
		[Export ("feedParametersForUpdater:sendingSystemProfile:")]
        NSObject [] FeedParametersForUpdater (SUUpdater updater, bool sendingProfile);

		/// <summary>
		/// Returns a custom appcast URL.
		/// Override this to dynamically specify the entire URL.
		///
		/// An alternative may be to use SUUpdaterDelegate::feedParametersForUpdater:sendingSystemProfile:
		/// and let the server handle what kind of feed to provide.
		/// 
		/// @optional -(NSString *)feedURLStringForUpdater:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export ("feedURLStringForUpdater:")]
        string FeedURLStringForUpdater (SUUpdater updater);

		/// <summary>
		/// Returns whether Sparkle should prompt the user about automatic update checks.
		/// Use this to override the default behavior.
		/// 
		/// @optional -(BOOL)updaterShouldPromptForPermissionToCheckForUpdates:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <returns></returns>
		[Export ("updaterShouldPromptForPermissionToCheckForUpdates:")]
        bool UpdaterShouldPromptForPermissionToCheckForUpdates (SUUpdater updater);

		/// <summary>
		/// Called after Sparkle has downloaded the appcast from the remote server.
		/// Implement this if you want to do some special handling with the appcast once it finishes loading.
		///
		/// @optional -(void)updater:(SUUpdater *)updater didFinishLoadingAppcast:(SUAppcast *)appcast;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="appcast"></param>
		[Export ("updater:didFinishLoadingAppcast:")]
        void UpdaterDidFinishLoadingAppcast(SUUpdater updater, SUAppcast appcast);

		/// <summary>
		/// Returns the item in the appcast corresponding to the update that should be installed.
		/// If you're using special logic or extensions in your appcast implement this to use your own logic for
		/// finding a valid update, if any, in the given appcast.
		///
		/// @optional -(SUAppcastItem *)bestValidUpdateInAppcast:(SUAppcast *)appcast forUpdater:(SUUpdater *)updater;
		/// </summary>
		/// <param name="appcast">The appcast that was downloaded from the remote server.</param>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <returns></returns>
		[Export ("bestValidUpdateInAppcast:forUpdater:")]
        SUAppcastItem BestValidUpdateInAppcast (SUAppcast appcast, SUUpdater updater);

		/// <summary>
		/// Called when a valid update is found by the update driver.
		///
		/// @optional -(void)updater:(SUUpdater *)updater didFindValidUpdate:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that is proposed to be installed.</param>
		[Export ("updater:didFindValidUpdate:")]
        void UpdaterDidFindValidUpdate(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called just before the scheduled update driver prompts the user to install an update.
		/// 
		/// @optional - (BOOL) updaterShouldShowUpdateAlertForScheduledUpdate:(SUUpdater*) updater forItem:(SUAppcastItem*) item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item"></param>
		/// <returns>true to allow the update prompt to be shown (the default behavior), or false to suppress it.</returns>
		[Export("updaterShouldShowUpdateAlertForScheduledUpdate:forItem:")]
		bool UpdaterShouldShowUpdateAlertForScheduledUpdate(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called after the user dismisses the update alert.
		///
		/// @optional - (void)updater:(SUUpdater *)updater didDismissUpdateAlertPermanently:(BOOL)permanently forItem:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="permanently">true if the alert will not appear again for this update, false if it may reappear.</param>
		/// <param name="item"></param>
		[Export("updater:didDismissUpdateAlertPermanently:forItem:")]
		void UpdaterDidDismissUpdateAlertPermanently(SUUpdater updater, bool permanently, SUAppcastItem item);

		/// <summary>
		/// Called when a valid update is not found
		///
		/// @optional -(void)updaterDidNotFindUpdate:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export("updaterDidNotFindUpdate:")]
		void UpdaterDidNotFindUpdate(SUUpdater updater);

		/// <summary>
		/// Called when the user clicks the Skip This Version button.
		///
		/// @optional - (void) updater:(SUUpdater*) updater userDidSkipThisVersion:(SUAppcastItem*) item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item"></param>
		[Export("updater:userDidSkipThisVersion:")]
		void UserDidSkipThisVersion(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called immediately before downloading the specified update.
		///
		/// @optional -(void)updater:(SUUpdater *)updater willDownloadUpdate:(SUAppcastItem *)item withRequest:(NSMutableURLRequest *)request;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that is proposed to be downloaded.</param>
		/// <param name="request">The mutable URL request that will be used to download the update.</param>
		[Export ("updater:willDownloadUpdate:withRequest:")]
        void UpdaterWillDownloadUpdate(SUUpdater updater, SUAppcastItem item, NSMutableUrlRequest request);

		/// <summary>
		/// Called immediately after succesfull download of the specified update.
		/// 
		/// @optional - (void)updater:(SUUpdater *)updater didDownloadUpdate:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that has been downloaded.</param>
		[Export("updater:didDownloadUpdate:")]
		void UpdaterDidDownloadUpdate(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called after the specified update failed to download.
		///
		/// @optional -(void)updater:(SUUpdater *)updater failedToDownloadUpdate:(SUAppcastItem *)item error:(NSError *)error;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that failed to download.</param>
		/// <param name="error">The error generated by the failed download.</param>
		[Export ("updater:failedToDownloadUpdate:error:")]
        void UpdaterFailedToDownloadUpdate(SUUpdater updater, SUAppcastItem item, NSError error);

		/// <summary>
		/// Called when the user clicks the cancel button while and update is being downloaded.
		///
		/// @optional -(void)userDidCancelDownload:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export ("userDidCancelDownload:")]
        void UserDidCancelDownload (SUUpdater updater);

		/// <summary>
		/// Called immediately before extracting the specified downloaded update.
		///
		/// @optional - (void)updater:(SUUpdater *)updater willExtractUpdate:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item"></param>
		[Export("updater:willExtractUpdate:")]
		void UpdaterWillExtractUpdate(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called immediately after extracting the specified downloaded update.
		///
		/// @optional - (void)updater:(SUUpdater *)updater didExtractUpdate:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that has been extracted.</param>
		[Export("updater:didExtractUpdate:")]
		void UpdaterDidExtractUpdate(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called immediately before installing the specified update.
		/// 
		/// @optional -(void)updater:(SUUpdater *)updater willInstallUpdate:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that is proposed to be installed.</param>
		[Export ("updater:willInstallUpdate:")]
        void UpdaterWillInstallUpdate (SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Returns whether the relaunch should be delayed in order to perform other tasks.
		/// This is not called if the user didn't relaunch on the previous update, in that case it will immediately restart.
		///
		/// @optional -(BOOL)updater:(SUUpdater *)updater shouldPostponeRelaunchForUpdate:(SUAppcastItem *)item untilInvoking:(NSInvocation *)invocation;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that is proposed to be installed.</param>
		/// <param name="invocation">The invocation that must be completed with `[invocation invoke]` before continuing
		/// with the relaunch.</param>
		/// <returns>true to delay the relaunch until invocation is invoked.</returns>
		[Export ("updater:shouldPostponeRelaunchForUpdate:untilInvoking:")]
        bool UpdaterShouldPostponeRelaunchForUpdateUntil (SUUpdater updater, SUAppcastItem item, NSInvocation invocation);

		/// <summary>
		/// Returns whether the relaunch should be delayed in order to perform other tasks.
		/// This is not called if the user didn't relaunch on the previous update, in that case it will immediately restart.
		///
		/// This method acts as a simpler alternative to SUUpdaterDelegate::updater:shouldPostponeRelaunchForUpdate:untilInvoking:
		/// avoiding usage of NSInvocation, which is not available in Swift environments.
		///
		/// @optional - (BOOL)updater:(SUUpdater *)updater shouldPostponeRelaunchForUpdate:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that is proposed to be installed.</param>
		/// <returns>true to delay the relaunch.</returns>
		[Export("updater:shouldPostponeRelaunchForUpdate:")]
		bool UpdaterShouldPostponeRelaunchForUpdate(SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Returns whether the application should be relaunched at all.
		///
		/// @optional -(BOOL)updaterShouldRelaunchApplication:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <returns></returns>
		[Export ("updaterShouldRelaunchApplication:")]
        bool UpdaterShouldRelaunchApplication (SUUpdater updater);

		/// <summary>
		/// Called immediately before relaunching.
		///
		/// @optional -(void)updaterWillRelaunchApplication:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export ("updaterWillRelaunchApplication:")]
        void UpdaterWillRelaunchApplication (SUUpdater updater);

		/// <summary>
		/// Called immediately after relaunching. SUUpdater delegate must be set before applicationDidFinishLaunching:
		/// to catch this event.
		///
		/// @optional - (void)updaterDidRelaunchApplication:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export("updaterDidRelaunchApplication:")]
		void UpdaterDidRelaunchApplication(SUUpdater updater);

		/// <summary>
		/// Returns an object that compares version numbers to determine their arithmetic relation to each other.
		/// This method allows you to provide a custom version comparator. If you don't implement this method or
		/// return null, the standard version comparator will be used.
		///
		/// @optional -(id<SUVersionComparison>)versionComparatorForUpdater:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <returns></returns>
		[Export ("versionComparatorForUpdater:")]
        SUVersionComparison VersionComparatorForUpdater (SUUpdater updater);

		/// <summary>
		/// Returns an object that formats version numbers for display to the user.
		/// If you don't implement this method or return null, the standard version formatter will be used
		///
		/// @optional -(id<SUVersionDisplay>)versionDisplayerForUpdater:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <returns></returns>
		[Export ("versionDisplayerForUpdater:")]
        SUVersionDisplay VersionDisplayerForUpdater (SUUpdater updater);

		/// <summary>
		/// Returns the path which is used to relaunch the client after the update is installed.
		/// The default is the path of the host bundle.
		///
		/// @optional -(NSString *)pathToRelaunchForUpdater:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <returns></returns>
		[Export ("pathToRelaunchForUpdater:")]
        string PathToRelaunchForUpdater (SUUpdater updater);

		/// <summary>
		/// Called before an updater shows a modal alert window, to give the host the opportunity
		/// to hide attached windows that may get in the way.
		///
		/// @optional -(void)updaterWillShowModalAlert:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export ("updaterWillShowModalAlert:")]
        void UpdaterWillShowModalAlert (SUUpdater updater);

		/// <summary>
		/// Called after an updater shows a modal alert window, to give the host the opportunity to hide
		/// attached windows that may get in the way.
		///
		/// @optional -(void)updaterDidShowModalAlert:(SUUpdater *)updater;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		[Export ("updaterDidShowModalAlert:")]
        void UpdaterDidShowModalAlert (SUUpdater updater);

		/// <summary>
		/// Called when an update is scheduled to be silently installed on quit.
		/// This is after an update has been automatically downloaded in the background
		/// (i.e. SUUpdater::automaticallyDownloadsUpdates is true)
		///
		/// @optional -(void)updater:(SUUpdater *)updater willInstallUpdateOnQuit:(SUAppcastItem *)item immediateInstallationInvocation:(NSInvocation *)invocation;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that is proposed to be installed.</param>
		/// <param name="invocation">Can be used to trigger an immediate silent install and relaunch.</param>
		[Export ("updater:willInstallUpdateOnQuit:immediateInstallationInvocation:")]
        void UpdaterWillInstallUpdateOnQuit (SUUpdater updater, SUAppcastItem item, NSInvocation invocation);

		/// <summary>
		/// Calls after an update that was scheduled to be silently installed on quit has been canceled.
		///
		/// @optional -(void)updater:(SUUpdater *)updater didCancelInstallUpdateOnQuit:(SUAppcastItem *)item;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="item">The appcast item corresponding to the update that was proposed to be installed.</param>
		[Export ("updater:didCancelInstallUpdateOnQuit:")]
        void UpdaterDidCancelInstallUpdateOnQuit (SUUpdater updater, SUAppcastItem item);

		/// <summary>
		/// Called after an update is aborted due to an error.
		///
		/// @optional -(void)updater:(SUUpdater *)updater didAbortWithError:(NSError *)error;
		/// </summary>
		/// <param name="updater">The SUUpdater instance.</param>
		/// <param name="error">The error that caused the abort.</param>
		[Export ("updater:didAbortWithError:")]
        void UpdaterDidAbortWithError (SUUpdater updater, NSError error);
    }


    [Static]
    public partial interface Constants
    {
        // extern NSString *const SUSparkleErrorDomain;
        [Field ("SUSparkleErrorDomain", LibraryName = "__Internal")]
        NSString SUSparkleErrorDomain { get; }

    }
}