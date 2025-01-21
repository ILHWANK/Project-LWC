using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class AdMobBanner : MonoBehaviour
{
    string adUnitId;

    BannerView _bannerView;

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IOS
adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
adUnitId = "unexpected_platform";
#endif

        //Banner 임시 처리
        //LoadAd();

    }

    public void LoadAd()
    {
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest.Builder().AddKeyword("unity-admob-sample").Build();

        Debug.Log("Loading banner Ad");
        _bannerView.LoadAd(adRequest);
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        if (_bannerView != null)
        {
            DestroyAd();
        }

        _bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
    }

    private void ListenToAdEvents()
    {
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response :" +
                _bannerView.GetResponseInfo());
        };
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("BannerView failed to load an ad with error: " +
                error);
        };
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Banner view paid {0} {1}, ",
                adValue.Value, adValue.CurrencyCode));
        };
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression");
        };
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked");
        };
        _bannerView.OnAdFullScreenContentOpened += (null);
        {
            Debug.Log("Banner View full Screen context closed");
        };
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}
