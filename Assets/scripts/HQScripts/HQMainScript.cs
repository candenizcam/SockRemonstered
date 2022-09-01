using System.Collections;
using System.Collections.Generic;
using Classes;
using HQScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HQMainScript : MonoBehaviour
{
    private System.Random _random;
    private HQLayout _mainCamera;
    private UIDocument _uiDocument;
    private Timer _timer;
    private float _timeHolder;
    private HQHud _hqHud;
    public bool ResetSaves = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (ResetSaves)
        {
            Debug.LogWarning("warning, saves are reset");
            SerialGameData.ResetSaves();
        }
        
        _random = new System.Random();
        _timer = new Timer();
        var sgd = SerialGameData.LoadOrGenerate();
        
        
        _uiDocument = gameObject.GetComponent<UIDocument>();
        _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        
        _mainCamera = new HQLayout(Camera.main,190f,220f);

        _hqHud = new HQHud(_mainCamera);
        _hqHud.AddToVisualElement(_uiDocument.rootVisualElement);
        var h = sgd.getHeartsAndRem();
        _hqHud.UpdateInfo(sgd.coins,h.hearts,h.rem);
        _hqHud.PlayButtonAction = () =>
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var nl = Constants.GetNextLevel(sgd.nextLevel);
            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
        };
        
        _timer.addEvent(1f, () =>
        {
            var sgd = SerialGameData.Load();
            var h = sgd.getHeartsAndRem();
            _hqHud.UpdateInfo(sgd.coins,h.hearts,h.rem);
        },true);
    }

    // Update is called once per frame
    void Update()
    {
        
        _timer.Update(Time.deltaTime);
        _hqHud.Update();



    }
}
