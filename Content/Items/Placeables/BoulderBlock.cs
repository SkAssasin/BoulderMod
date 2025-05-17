//using ExampleMod.Content.Biomes;
//using ExampleMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace BoulderMod.Content.Items.Placeables
{
    internal class BoulderBlock:ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            ItemID.Sets.SortingPriorityExtractibles[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.consumable = true;
            //Item.value = Item.buyPrice(copper: 50);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<Tiles.BoulderBlock>();
        }
    }
}
