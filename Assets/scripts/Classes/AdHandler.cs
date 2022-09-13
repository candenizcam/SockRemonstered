using GoogleMobileAds.Api;

namespace Classes
{
    public class AdHandler
    {
        public AdHandler()
        {

        
        }


        public static RewardedAd RequestRewardedAd()
        {
            string adUnitId;

            #if UNITY_ANDROID
                adUnitId = "ca-app-pub-3940256099942544/5224354917";
            #elif UNITY_IPHONE
                adUnitId = "ca-app-pub-3940256099942544/1712485313";
            #else
                adUnitId = "unexpected_platform";
            #endif
            // Initialize an InterstitialAd.
            //this.Interstitial = new InterstitialAd(adUnitId);
            var rewardedAd = new RewardedAd(adUnitId);


            // Called when an ad request has successfully loaded.
            //this.Interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            //this.Interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            //this.Interstitial.OnAdOpening += HandleOnAdOpening;
            // Called when the ad is closed.
            //this.Interstitial.OnAdClosed += HandleOnAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            rewardedAd.LoadAd(request);
            return rewardedAd;
        }
        
        
        public static InterstitialAd RequestInterstitial()
        {
            #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/1033173712";
            #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
            #else
                string adUnitId = "unexpected_platform";
            #endif

            // Initialize an InterstitialAd.
            var interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            //interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            //interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            //interstitial.OnAdOpening += HandleOnAdOpening;
            // Called when the ad is closed.
            //interstitial.OnAdClosed += HandleOnAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);

            return interstitial;
        }   
    }
}