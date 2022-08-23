using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WMHud
{
    private VisualElement _topBar;
    private VisualElement _bottomBar;
    
    public WMHud(Rect topBarRect, Rect bottomBarRect)
    {

        
        _topBar = new VisualElement();

        _topBar.style.position = Position.Absolute;
        _topBar.style.left = topBarRect.x;
        _topBar.style.bottom = topBarRect.y;
        _topBar.style.height = topBarRect.height;
        _topBar.style.width = topBarRect.width;
        _topBar.style.backgroundColor = new Color(1f,1f,0f,0.6f);


        _bottomBar = new VisualElement();
        
        _bottomBar.style.position = Position.Absolute;
        _bottomBar.style.left = bottomBarRect.x;
        _bottomBar.style.bottom = bottomBarRect.y;
        _bottomBar.style.height = bottomBarRect.height;
        _bottomBar.style.width = bottomBarRect.width;
        _bottomBar.style.backgroundColor = new Color(1f,0f,0f,0.6f);
    }


    public void AddToVisualElement(VisualElement ve)
    {
        ve.Add(_topBar);
        ve.Add(_bottomBar);
    }
    
    public void RemoveFromVisualElement(VisualElement ve)
    {
        ve.Remove(_topBar);
        ve.Remove(_bottomBar);
    }


        //var root = gameObject.GetComponent<UIDocument>().rootVisualElement;
    
}
