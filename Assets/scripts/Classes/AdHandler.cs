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


            if (Constants.RealAd)
            {
                #if UNITY_ANDROID
                                adUnitId = "ca-app-pub-3705202925859468/1312861608";
                #elif UNITY_IPHONE
                                adUnitId = "ca-app-pub-3705202925859468/1664215396";
                #else
                                adUnitId = "unexpected_platform";
                #endif
            }
            else
            {
                #if UNITY_ANDROID
                                adUnitId = "ca-app-pub-3940256099942544/5224354917";
                #elif UNITY_IPHONE
                                adUnitId = "ca-app-pub-3940256099942544/1712485313";
                #else
                                adUnitId = "unexpected_platform";
                #endif
            }
            
            
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
            string adUnitId;
            if (Constants.RealAd)
            {
                //ca-app-pub-3705202925859468~2614464863
                #if UNITY_ANDROID
                                adUnitId = "ca-app-pub-3705202925859468/4575796684";
                #elif UNITY_IPHONE
                                adUnitId = "ca-app-pub-3705202925859468/5986603789";
                #else
                                adUnitId = "unexpected_platform";
                #endif
            }
            else
            {
                #if UNITY_ANDROID
                                adUnitId = "ca-app-pub-3940256099942544/1033173712";
                #elif UNITY_IPHONE
                                adUnitId = "ca-app-pub-3940256099942544/4411468910";
                #else
                                adUnitId = "unexpected_platform";
                #endif
            }
            
            
            

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