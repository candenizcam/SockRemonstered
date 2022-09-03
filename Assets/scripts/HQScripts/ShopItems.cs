using System.Linq;

namespace HQScripts
{
    public static class ShopItems
    {
        public static ShopItem[] ShopItemsArray =
        {
            new ShopItem("basket","ui/shop/items/Basket-s", 200,"Sock Basket","Best way to carry socks around.",ShopItemType.Furniture),
            new ShopItem("clock","ui/shop/items/Clock-s", 500, "Button Clock","Has an alarm and cufflinks to home wifi.",ShopItemType.Furniture),
            new ShopItem("LArm_S3","ui/shop/items/LArm-s3_shop", 500,"Striped Glove","Vertical lines make your arm look longer.",ShopItemType.Cloth),
            new ShopItem("lamp","ui/shop/items/Lamp-s", 1000, "Sock Lamp","No real socks were harmed while making this lamp shade.",ShopItemType.Furniture),
            new ShopItem("sleepper","ui/shop/items/Slipper-s", 1500,"Sleepper Bed","Sleeps right in.",ShopItemType.Furniture),
            new ShopItem("Body_S1","ui/shop/items/Body-s1_shop", 1500,"Starry Shirt","Stars were white when first bought.",ShopItemType.Cloth),
            new ShopItem("carpet","ui/shop/items/Carpet-s", 2500,"Fuzzy Carpet","Really brings the room together.",ShopItemType.Furniture),
            new ShopItem("1000","ui/shop/items/PileOfCoins", 1000,"Wool","44.99 TRY",ShopItemType.GameCoin),
            new ShopItem("3000","ui/shop/items/PileOfCoins-2", 3000,"Silk","109.99 TRY",ShopItemType.GameCoin),
            new ShopItem("55000","ui/shop/items/PileOfCoins-3", 55000,"Cashmere","1299.99 TRY",ShopItemType.GameCoin)

        };

        public static ShopItem[] GetCoinsArray()
        {
            return ShopItemsArray.Where(x => x.ShopItemType == ShopItemType.GameCoin).ToArray();
            //return ShopItemsArray.All((x) => x.ShopItemType == ShopItemType.GameCoin);
        }
        
        
        public static ShopItem[] GetNotCoinsArray()
        {
            return ShopItemsArray.Where(x => x.ShopItemType != ShopItemType.GameCoin).ToArray();
        }
        
    }


    public class ShopItem
    {
        public string ID;
        public string Location;
        public int Price;
        public string DisplayName;
        public string SmallText;
        public ShopItemType ShopItemType;
        public ShopItem(string id, string location, int price, string displayName, string smallText, ShopItemType shopItemType)
        {
            ID = id;
            Location = location;
            Price = price;
            SmallText = smallText;
            DisplayName = displayName;
            ShopItemType = shopItemType;
        }
    }

    public enum ShopItemType
    {
        Furniture, Cloth, GameCoin
    };
}