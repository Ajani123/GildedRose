using GildedRose.Console.Loggers;
using GildedRose.Console.Utils;
using System;
using System.Collections.Generic;

namespace GildedRose.Console.Inn
{
    public class InnInventoryUpdater
    {
        private Dictionary<Item, Action<Item>> itemAction = new Dictionary<Item, Action<Item>>();

        public void StoreItems(IList<Item> items)
        {
            Logger.Log(string.Format("Storing {0} items", items.Count));
            foreach (Item item in items)
            {
                StoreItem(item);
            }
        }

        public void StoreItem(Item item)
        {
            Logger.Log(string.Format("Storing Name = {0}, quality = {1}, sellin = {2} ", item.Name,item.Quality,item.SellIn));
            switch (item.Name)
            {
                case "Aged Brie":
                    itemAction.Add(item, itemToUpdate=>UpdateByValue(itemToUpdate, 1));
                    break;
                case "Elixir of the Mongoose":
                case "+5 Dexterity Vest":
                    itemAction.Add(item, itemToUpdate => UpdateByValue(itemToUpdate, -1));
                    break;
                case "Sulfuras, Hand of Ragnaros":
                    itemAction.Add(item, itemToUpdate => { Logger.Log("No action needed for Legendary items");});
                    break;
                case "Backstage passes to a TAFKAL80ETC concert":
                    itemAction.Add(item, UpdateConcert);
                    break;
                case "Conjured Mana Cake":
                    itemAction.Add(item, itemToUpdate => UpdateByValue(itemToUpdate, -2));
                    break;
                default:
                    break;
            }
        }
        
        public void UpdateAll()
        {
            Logger.Log(string.Format("Updating {0} items", itemAction.Count));
            foreach (Item key in itemAction.Keys)
            {
                Logger.Log(string.Format("Updating: {0}", key.Name));
                itemAction[key](key);
            }
        }

        private void UpdateByValue(Item item, int value)
        {
            Logger.Log(string.Format("Updating by value={0}: name={1}, sellin={2}, startQuality={3}", value, item.Name, item.SellIn, item.Quality));
            int calculatedQuality = item.Quality + value;
            if (item.SellIn <= 0)
            {
                calculatedQuality = item.Quality + 2 * value;
            }

            item.Quality = ExtraMath.Clamp(0, calculatedQuality, 50);
            item.SellIn--;
            Logger.Log(string.Format("Updated by value={0} parameters: name={1}, sellin={2}, startQuality={3}", value, item.Name, item.SellIn, item.Quality));
        }

        private void UpdateConcert(Item item)
        {
            Logger.Log(string.Format("Updating concert: name={0}, sellin={1}, startQuality={2}", item.Name, item.SellIn, item.Quality));
            //Every day quality increases
            item.Quality++;

            // 10 days or less +1 
            if (item.SellIn <= 10) item.Quality++;

            // 5 days or less +1
            if (item.SellIn <= 5) item.Quality++;

            item.Quality = Math.Min(item.Quality, 50);

            if (item.SellIn <= 0) item.Quality = 0;

            item.SellIn--;
            Logger.Log(string.Format("Updated concert parameters: name={0}, sellin={1}, startQuality={2}", item.Name, item.SellIn, item.Quality));
        } 

    }
}
