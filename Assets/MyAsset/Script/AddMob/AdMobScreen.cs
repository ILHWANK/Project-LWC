using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMobScreen : MonoBehaviour
{
    string adUnitId;

    private InterstitialAd interstitialAd;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
            {

            });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IOS
adUnitId = "ca-app-pub-3940256099942544/4411468910"
#endif


    }

    public void LoadinterstitialAd()
    {
        if(interstitialAd!=null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad");

        var adRequest = new AdRequest.Builder().AddKeyword("unity-admob-sample").Build();

        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad" +
                        "with error: " + error);

                    return;
                }

                Debug.Log("interstitial ad loaded with response:" + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdPaid +=(AdValue adValue) =>
        {
            //보상 주기
            Debug.Log(string.Format("interestitial ad paid {0} {1}.",
                adValue.Value, adValue.CurrencyCode));
        };
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
         {
             Debug.Log("interstitial ad was clicked");
         };
        ad.OnAdFullScreenContentOpened += () =>
         {
             Debug.Log("Interstitial ad full screen content opened");
         };
        ad.OnAdFullScreenContentClosed += () =>
         {
             Debug.Log("Interstitial ad full screen content closed");
         };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
         {
             Debug.LogError("interestial ad failed to open full screen content" +
                 "with error : " + error);
         };
    }

    private void RegisterReloadHandler(InterstitialAd ad) //수동으로 광고 재로드 선언필요
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            LoadinterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
          {
              Debug.LogError("Interstitial ad failed to open full screen content" + "with error: " + error);

              LoadinterstitialAd();
          };
    }
}
