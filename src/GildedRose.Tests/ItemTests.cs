using GildedRose.Console.Inn;
using Xunit;

namespace GildedRose.Tests
{
    public enum TestItems
    {
        vest,
        brie,
        elixir,
        legendary,
        concert,
        conjured
    }
    public class ItemTests
    {
        #region Common test area
        InnInventoryUpdater updater;
        public ItemTests()
        {
            updater = new InnInventoryUpdater();
        }

        public Item GetItem(TestItems item)
        {

            switch (item)
            {
                case TestItems.vest:
                    return new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
                case TestItems.brie:
                    return new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
                case TestItems.elixir:
                    return new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 };
                case TestItems.legendary:
                    return new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
                case TestItems.concert:
                    return new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    };
                case TestItems.conjured:
                    return new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };
                default:
                    throw new System.ArgumentException("No requested item");
            }                    
        }

        public void AssertItemValues(Item item, int expectedSellIn, int expectedQuality)
        {
            Assert.Equal(expectedSellIn, item.SellIn);
            Assert.Equal(expectedQuality, item.Quality);
        }

        #endregion Common test area
        #region Vest item tests
        [Fact]
        public void VestTestSingleUpdate()
        {
            Item vest = GetItem(TestItems.vest);
            updater.StoreItem(vest);

            updater.UpdateAll();
            AssertItemValues(vest, 9, 19);                      
        }

        [Fact]
        public void VestTestTenUpdates()
        {
            Item vest = GetItem(TestItems.vest);
            updater.StoreItem(vest);

            for (int i = 0; i < 10; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(vest, 0, 10);
        }

        [Fact]
        public void VestTestOneUpdateAfterSellIn()
        {
            Item vest = GetItem(TestItems.vest);
            updater.StoreItem(vest);

            for (int i = 0; i < 11; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(vest, -1, 8);
        }

        [Fact]
        public void VestTestTenUpdatesAfterSellIn()
        {
            Item vest = GetItem(TestItems.vest);
            updater.StoreItem(vest);

            for (int i = 0; i < 20; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(vest, -10, 0);
        }
        #endregion Vest item tests
        #region Brie item tests
        [Fact]
        public void TestBrieSingleUpdate()
        {
            Item brie = GetItem(TestItems.brie);
            updater.StoreItem(brie);
            updater.UpdateAll();

            AssertItemValues(brie, 1, 1);
        }

        [Fact]
        public void TestBrieSellInZero()
        {
            Item brie = GetItem(TestItems.brie);
            updater.StoreItem(brie);

            for (int i = 0; i < 2; i++)
            {
                updater.UpdateAll();
            }

            AssertItemValues(brie, 0, 2);
        }

        [Fact]
        public void TestBrieOneUpdateAfterSellIn()
        {
            Item brie = GetItem(TestItems.brie);
            updater.StoreItem(brie);

            for (int i = 0; i < 3; i++)
            {
                updater.UpdateAll();
            }

            AssertItemValues(brie, -1, 4);
        }

        //Should reach maximum quality 
        [Fact]
        public void TestBrieThirtyUpdates()
        {
            Item brie = GetItem(TestItems.brie);
            updater.StoreItem(brie);

            for (int i = 0; i < 30; i++)
            {
                updater.UpdateAll();
            }

            AssertItemValues(brie, -28, 50);
        }
        #endregion Brie item tests
        #region Elixir item tests
        [Fact]
        public void ElixirTestSingleUpdate()
        {
            Item elixir = GetItem(TestItems.elixir);
            updater.StoreItem(elixir);

            updater.UpdateAll();
            AssertItemValues(elixir, 4, 6);
        }

        [Fact]
        public void ElixirTestSellInZero()
        {
            Item elixir = GetItem(TestItems.elixir);
            updater.StoreItem(elixir);

            for (int i = 0; i < 5; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(elixir, 0, 2);
        }

        [Fact]
        public void ElixirTestOneUpdateAfterSellIn()
        {
            Item elixir = GetItem(TestItems.elixir);
            updater.StoreItem(elixir);

            for (int i = 0; i < 6; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(elixir, -1, 0);
        }

        [Fact]
        public void ElixirTestTenUpdatesAfterSellIn()
        {
            Item elixir = GetItem(TestItems.elixir);
            updater.StoreItem(elixir);

            for (int i = 0; i < 15; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(elixir, -10, 0);
        }
        #endregion Elixir item tests
        #region Sulfuras item test
        // Only one test is necessary since no value of this item changes
        [Fact]
        public void SulfurasTestOneUpdate()
        {
            Item sulfuras = GetItem(TestItems.legendary);
            updater.StoreItem(sulfuras);
            updater.UpdateAll();
            AssertItemValues(sulfuras, 0, 80);
        }
        #endregion Sulfuras item test
        #region Concert item tests
        [Fact]
        public void ConcertTestSingleUpdate()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            updater.UpdateAll();
            AssertItemValues(concert, 14, 21);
        }
        [Fact]
        public void ConcertTestUntilFirstIncrease()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            for (int i = 0; i < 4; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(concert, 11, 24);
        }
        [Fact]
        public void ConcertTestOneUpdateAfterFirstIncrease()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            for (int i = 0; i < 6; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(concert, 9, 27);
        }
        [Fact]
        public void ConcertTestUntilSecondIncrease()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            for (int i = 0; i < 9; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(concert, 6, 33);
        }
        [Fact]
        public void ConcertTestOneUpdateAfterSecondIncrease()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            for (int i = 0; i < 11; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(concert, 4, 38);
        }
        [Fact]
        public void ConcertTestSellInZero()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            for (int i = 0; i < 15; i++)
            {
                updater.UpdateAll();
            }
            //Max value when selling on concert day.
            AssertItemValues(concert, 0, 50);
        }
        [Fact]
        public void ConcertTestOneUpdateAfterSellInZero()
        {
            Item concert = GetItem(TestItems.concert);
            updater.StoreItem(concert);
            for (int i = 0; i < 16; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(concert, -1, 0);
        }

        #endregion Concert item tests
        #region Conjured item tests

        [Fact]
        public void ConjuredTestOneUpdate()
        {
            Item conjured = GetItem(TestItems.conjured);
            updater.StoreItem(conjured);
            updater.UpdateAll();

            AssertItemValues(conjured, 2, 4);
        }

        [Fact]
        public void ConjuredTestSellInZero()
        {
            Item conjured = GetItem(TestItems.conjured);
            updater.StoreItem(conjured);
            for (int i = 0; i < 3; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(conjured, 0, 0);
        }

        [Fact]
        public void ConjuredTestOneUpdateAfterSellInZero()
        {
            Item conjured = GetItem(TestItems.conjured);
            updater.StoreItem(conjured);
            for (int i = 0; i < 4; i++)
            {
                updater.UpdateAll();
            }
            AssertItemValues(conjured, -1, 0);
        }

        #endregion Conjured item tests
    }
}
