using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class Shop
    {
        public Image _mainHolder;
        public VisualElement _bgButton;
        private HQLayout _layout;
        public Action BgButtonAction;
        private ShopTabs _shopTabs;
        private ClosetTab _closetTab;
        private PurchaseTab _purchaseTab;
        private CoinTab _coinTab;
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        public Shop(HQLayout hqLayout)
        {
            _layout = hqLayout;

            _bgButton = new Button(BgButtonFunction);
            _bgButton.style.position = Position.Absolute;
            _bgButton.style.left =-10f - Constants.UnsafeLeftUi;
            _bgButton.style.top = -10f - Constants.UnsafeTopUi;
            _bgButton.style.height = Constants.UiHeight+20f;
            _bgButton.style.width = Constants.UiWidth+20f;
            _bgButton.style.backgroundColor = new Color(0.05f, 0.05f, 0.05f, 0.5f);
            _bgButton.style.borderBottomColor = Color.clear;
            _bgButton.style.borderTopColor = Color.clear;
            _bgButton.style.borderRightColor = Color.clear;
            _bgButton.style.borderLeftColor = Color.clear;

            //var top = hqLayout.topBarRect().yMin;
            //var bottom = hqLayout.bottomBarRect().yMin;
            
            _mainHolder = new Image();
            _mainHolder.sprite = Resources.Load<Sprite>("ui/shop/ShopBackground");
            
            _mainHolder.scaleMode = ScaleMode.StretchToFill;
            
            // top: 250, bottom: 250

            var w = Tools.WidthThatFitsToSize(Constants.UiWidth, Constants.UiHeight - 250f -Constants.UnsafeBottomUi-Constants.UnsafeTopUi,
                _mainHolder.sprite.rect.width / _mainHolder.sprite.rect.height);

            var h = w / _mainHolder.sprite.rect.width * _mainHolder.sprite.rect.height;
            
            _mainHolder.style.position = Position.Absolute;
            _mainHolder.style.width = w;
            _mainHolder.style.height = h;
            _mainHolder.style.left = (Constants.UiWidth - w) * 0.5f;
            _mainHolder.style.top = 250f;
            //_mainHolder.style.height = top-bottom;
            //_mainHolder.style.width = _mainHolder.sprite.rect.width*hqLayout.Scale;
            //_mainHolder.style.left = (Screen.width - (_mainHolder.sprite.rect.width * hqLayout.Scale))*0.5f;
            //mainHolder.style.bottom = bottom;
            _mainHolder.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));
            _mainHolder.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _mainHolder.style.color = Constants.GameColours[11];
            
            

            var tabHolder = new VisualElement();
            tabHolder.style.paddingLeft = 23f;
            tabHolder.style.paddingRight = 23f;
            var tabHolderWidth = w - 46f;

            var scale =  w/_mainHolder.sprite.rect.width;
            
            _shopTabs = new ShopTabs(scale, (x) =>
            {
                // 0: closet, 1: shop, 2: coin
                tabHolder.Clear();
                switch (x)
                {
                    case 0:
                    {
                        tabHolder.Add(_closetTab);
                        break;
                    }
                    case 1:
                    {
                        tabHolder.Add(_purchaseTab);
                        break;
                    }
                    case 2:
                    {
                        tabHolder.Add(_coinTab);
                        break;
                    }
                }
                
                
                
            });
            _shopTabs.TabFunc(0);
            _mainHolder.Add(_shopTabs);
            _mainHolder.Add(tabHolder);
            
            // 250- 2000
            var tabHeight = h * 7f / 8f;
            
            _closetTab = new ClosetTab(scale,
                tabHolderWidth,
                tabHeight
                );
            _closetTab.ItemAction = thisItem =>
            {
                if (thisItem.ShopItemType == ShopItemType.Cloth)
                {
                    var sgd = SerialGameData.LoadOrGenerate();
                    var itemType = thisItem.ID.Split("_")[0];
                    for (var i = 0; i < sgd.lineup.Length; i++)
                    {
                        if (sgd.lineup[i].Split("_")[0] == itemType)
                        {
                            if (sgd.lineup[i] == thisItem.ID)
                            {
                                sgd.lineup[i] = $"{itemType}_Raw";
                            }
                            else
                            {
                                sgd.lineup[i] = thisItem.ID;
                            }
                            
                            break;
                        }

                        if (i + 1 == sgd.lineup.Length)
                        {
                            Debug.LogWarning("dress prefix not found");
                        }
                    }
                    sgd.Save();
                }else if (thisItem.ShopItemType == ShopItemType.Furniture)
                {
                    
                    SerialGameData.Apply(sgd =>
                    {
                        if (sgd.activeFurnitures.Contains(thisItem.ID))
                        {
                            sgd.activeFurnitures.Remove(thisItem.ID);
                        }
                        else
                        {
                            sgd.activeFurnitures.Add(thisItem.ID);
                        }
                        
                    }) ;
                    
                    
                }

            }; 
            //_closetTab.UpdateShopItems(ShopItems.ShopItemsArray);

            _coinTab = new CoinTab(scale,tabHolderWidth,tabHeight);
            _coinTab.ItemAction = thisItem =>
            {
                var sgd = SerialGameData.LoadOrGenerate();
                sgd.coins += thisItem.Price;
                sgd.Save();
            };
            //_coinTab.UpdateShopItems(ShopItems.ShopItemsArray);
            
            _purchaseTab = new PurchaseTab(scale,tabHolderWidth,tabHeight);
            _purchaseTab.ItemAction = thisItem =>
            {
                var sgd = SerialGameData.LoadOrGenerate();
                if (sgd.coins > thisItem.Price)
                {

                    sgd.coins -= thisItem.Price;
                    sgd.purchased.Add(thisItem.ID);
                    if (thisItem.ShopItemType == ShopItemType.Furniture)
                    {
                        sgd.activeFurnitures.Add(thisItem.ID);
                        
                    }else if (thisItem.ShopItemType == ShopItemType.Cloth)
                    {
                        var type = thisItem.ID.Split("_")[0];
                        for (var i = 0; i < sgd.lineup.Length; i++)
                        {
                            if (type == sgd.lineup[i].Split("_")[0])
                            {
                                sgd.lineup[i] = thisItem.ID;
                                break;
                            }

                            if (i == sgd.lineup.Length)
                            {
                                Debug.LogWarning("cloth id invalid");
                            }
                        }
                    }

                }
                else
                {
                    Debug.Log("not enough coin");
                }

                sgd.Save();
                UpdateShopItems();
            };
            //_purchaseTab.UpdateShopItems(ShopItems.ShopItemsArray);
            UpdateShopItems();
        }


        void UpdateShopItems()
        {
            var sgd = SerialGameData.LoadOrGenerate();


            var bought = new List<ShopItem>();
            var notBought = new List<ShopItem>();
            foreach (var shopItem in ShopItems.GetNotCoinsArray())
            {
                if(sgd.purchased.Contains(shopItem.ID))
                {
                    bought.Add(shopItem);
                }
                else
                {
                    notBought.Add(shopItem);
                }
            }
            
            _closetTab.UpdateShopItems(bought.ToArray());
            _coinTab.UpdateShopItems(ShopItems.GetCoinsArray());
            _purchaseTab.UpdateShopItems(notBought.ToArray());
        }
        



        private void BgButtonFunction()
        {
            BgButtonAction();
        }
        
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_bgButton);
            ve.Add(_mainHolder);
        }
        
        public void RemoveFromVisualElement(VisualElement ve)
        {
            ve.Remove(_bgButton);
            ve.Remove(_mainHolder);
        }
    }
}