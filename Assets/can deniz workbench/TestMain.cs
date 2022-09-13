using System;
using Classes;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UIElements;


public class TestMain : MonoBehaviour
{
    public GameObject visual;
    public GameObject otherVisual;
    public GameObject otherOtherVisual; 
    public GameObject otherOtherOtherVisual;

    public GameObject otherVisual5;
    public string s;
    private Timer _timer;
    
    
    protected InterstitialAd Interstitial;
    protected RewardedAd RewardedAd;
    protected Action OnAdClosedAction;
        
        
        protected void RequestInterstitial()
        {
            string adUnitId;

            #if UNITY_ANDROID
                        adUnitId = "ca-app-pub-3940256099942544/5224354917";
            #elif UNITY_IPHONE
                        adUnitId = "ca-app-pub-3940256099942544/1712485313";
            #else
                        adUnitId = "unexpected_platform";
            #endif


            Debug.Log(adUnitId);
            // Initialize an InterstitialAd.
            //this.Interstitial = new InterstitialAd(adUnitId);
            this.RewardedAd = new RewardedAd(adUnitId);


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
            this.RewardedAd.LoadAd(request);

        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            Debug.Log("yokluğun çok zor");
            //MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log("vur bu akılsız başı");
            //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "+ args.Message);
        }

        public void HandleOnAdOpening(object sender, EventArgs args)
        {
            Debug.Log("sensiz olamadım");
            //MonoBehaviour.print("HandleAdOpening event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            Debug.Log("işte kuzu kuzu geldim");
            //MonoBehaviour.print("HandleAdClosed event received");
            OnAdClosedAction();
        }
    
    
    
    private UIDocument _uiDocument;
    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer();
        /*
        var r = new Random(6);

        var s = "";
        for (int i = 0; i < 500; i++)
        {
            var r2 = r.Next(0,4);
            s += $"{r2}";
        }
        */

        var uiw = 1000;
        var uih = 1000f / (float)Screen.width * (float)Screen.height;
        
        //_uiDocument = gameObject.GetComponent<UIDocument>();
        //_uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        //1170, 2532
        //_uiDocument.panelSettings.referenceResolution = new Vector2Int(uiw, (int)uih);
        //_uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        //_uiDocument.panelSettings.match = 0f;

        RequestInterstitial();

        //var root = _uiDocument.rootVisualElement;


        _timer.addEvent(1f, () =>
        {
            if (RewardedAd.IsLoaded())
            {
                
                RewardedAd.Show();
            }
        });
        /*
        var b = new ButtonClickable(1f,"test/ASZ3c_9",Color.gray, () =>
        {
            
        });

        b.style.position = Position.Absolute;
        b.style.top = 100f;
        b.style.left = 100f;
        
        root.Add(b);
        */
        /*
        var im1 = new VisualElement();
        var r = Resources.Load<Sprite>("test/ASZ3c_9");
        //Debug.Log($"{r.packingMode}");
        im1.style.backgroundImage = new StyleBackground(r);
        //im1.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
        
        im1.style.width = 600f;
        im1.style.height = 400f;
        im1.style.position = Position.Absolute;
        im1.style.left = 200f;
        im1.style.top = 100f;
        //im1.scaleMode = ScaleMode.StretchToFill;
        root.Add(im1);
        
        
        var im2 = new VisualElement();
        var r2 = Resources.Load<Sprite>("test/ASZ3c_9");
        
        
        //im2.sprite = r2;
        im2.style.width = 600f;
        im2.style.height = 400f;
        im2.style.backgroundImage = new StyleBackground(r2);
        
        im2.style.unityBackgroundScaleMode = ScaleMode.StretchToFill;
        im2.style.position = Position.Absolute;
        im2.style.left = 200f;
        im2.style.top = 700f;
        root.Add(im2);

        var im3 = new VisualElement();
        var r3 = Resources.Load<Sprite>("test/ASZ3c_9");
        
        
        //im2.sprite = r2;
        im3.style.width = 600f;
        im3.style.height = 400f;
        im3.style.backgroundImage = new StyleBackground(r3);
        
        im3.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
        im3.style.position = Position.Absolute;
        im3.style.left = 200f;
        im3.style.top = 1200f;
        root.Add(im3);
        
        im3.RegisterCallback<MouseDownEvent>((e) =>
        {
            im3.style.unityBackgroundImageTintColor = Color.red;
        });
        im3.RegisterCallback<MouseUpEvent>((e) =>
        {
            im3.style.unityBackgroundImageTintColor = Color.white;
        });

        im3.RegisterCallback<MouseLeaveEvent>(e =>
        {
            im3.style.unityBackgroundImageTintColor = Color.white;
        });
        */
        /*
        var root = _uiDocument.rootVisualElement;
        root.style.backgroundColor = Color.green;

        root.style.justifyContent = Justify.Center;
        root.style.alignContent = Align.Center;
        
        var ve2 = new VisualElement();
        
        ve2.style.backgroundColor = Color.red;
        ve2.style.width = uiw*0.5f;
        ve2.style.height = uih*0.5f;
        ve2.style.position = Position.Absolute;
        
        ve2.style.left = uiw*0.25f;
        ve2.style.top = uih*0.25f;
        
        
        root.Add(ve2);
        
        
        var ve = new VisualElement();
        
        ve.style.backgroundColor = Color.blue;
        ve.style.width = uiw*0.5f-20f;
        ve.style.height = uih*0.5f-20f;
        ve.style.position = Position.Absolute;
        //ve.style.right = 10f;
        //ve.style.bottom = 10f;
        ve.style.left = uiw*0.25f+10f;
        ve.style.top = uih*0.25f+10f;
        
        
        root.Add(ve);
*/

        //Debug.Log($"screen, width {Screen.width}, height {Screen.height}");

        //Debug.Log($"{Screen.resolutions}, {Screen.currentResolution}");

        //Debug.Log($"camera main target width: {Camera.main.scaledPixelWidth}, height: {Camera.main.scaledPixelHeight}");

        //Debug.Log($"os: {Camera.main.orthographicSize}");

        //Camera.main.orthographicSize = Screen.height / 200f;

        /*
        // Debug.Log($"camera main rect width: {Camera.main.rect.width}, height: {Camera.main.rect.height}"); 1,1
        
        Debug.Log($"camera main pixel width: {Camera.main.pixelWidth}, height: {Camera.main.pixelHeight}");
        
        //Debug.Log($"camera main target width: {Camera.main.targetTexture.width}, height: {Camera.main.targetTexture.height}");
        
        Debug.Log($"camera main target width: {Camera.main.scaledPixelWidth}, height: {Camera.main.scaledPixelHeight}");
        
        // Debug.Log($"Camera.main.transform.lossyScale x: {Camera.main.transform.lossyScale.x}, y: {Camera.main.transform.lossyScale.y}"); 1,1
        
        Debug.Log($"v2 screen world {Camera.main.ViewportToScreenPoint(new Vector3(1,1,0))} {Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))}");

        var gc1 = visual.GetComponent<SpriteRenderer>();
        rendererStats("1",gc1);
       
        var gc5 = otherVisual5.GetComponent<SpriteRenderer>();
        rendererStats("5",gc5);

        Debug.Log($"orth size {Camera.main.orthographicSize}");
        
        Debug.Log($"v2 screen world {Camera.main.ViewportToScreenPoint(new Vector3(1,1,0))} {Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))}");
        
        //Camera.main.orthographicSize *= 2f;

        
        var gc12 = visual.GetComponent<SpriteRenderer>();
        rendererStats("12",gc12);
       
        var gc52 = otherVisual5.GetComponent<SpriteRenderer>();
        rendererStats("52",gc52);
        
        Debug.Log($"orth size {Camera.main.orthographicSize}");
        
        Debug.Log($"v2 screen world {Camera.main.ViewportToScreenPoint(new Vector3(1,1,0))} {Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))}");

        visual.transform.localScale = new Vector3(2f, 2f, 1f);
        rendererStats("12",gc12);
       
        otherVisual5.transform.localScale = new Vector3(2f, 2f, 1f);
        rendererStats("52",gc52);
        */
    }

    void rendererStats(string pre, SpriteRenderer sr)
    {
        var bounds = sr.sprite.bounds;
        Debug.Log($"{pre} size .x: {sr.size.x}, .y: {sr.size.y}, {sr.transform.lossyScale.x}, {sr.transform.lossyScale.y}");
        
        
        //Debug.Log($"{pre} sprite rect width: {sr.sprite.rect.width}, height: {sr.sprite.rect.height}");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        _timer.Update(Time.deltaTime);
        
    }
}
