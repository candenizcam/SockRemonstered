using System.Linq;

namespace HQScripts
{
    public static class ShopItems
    {
        public static ShopItem[] ShopItemsArray =
        {
            new ShopItem("clock","ui/shop/ClosetIcon", 500, "Button Clock","Has an alarm and cufflinks to home wifi.",ShopItemType.Furniture),
            new ShopItem("lamp","ui/shop/ClosetIcon", 500, "Sock Lamp","No real socks were harmed while making this lamp shade.",ShopItemType.Furniture),
            new ShopItem("sleepper","ui/shop/ClosetIcon", 500,"Sleepper Bed","Sleeps right in.",ShopItemType.Furniture),
            new ShopItem("thousand","ui/shop/PileOfCoins", 1000,"Wool","44.99 TRY",ShopItemType.GameCoin),
            new ShopItem("thousand","ui/shop/PileOfCoins", 3000,"Silk","109.99 TRY",ShopItemType.GameCoin),
            new ShopItem("thousand","ui/shop/PileOfCoins", 55000,"Cashmere","1299.99 TRY",ShopItemType.GameCoin)

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