using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdmob : MonoBehaviour
{
    private BannerView bannerView;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
    string sdUniId = "unexpected_platform"; 
#endif

        if(this.bannerView !=null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        //this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
