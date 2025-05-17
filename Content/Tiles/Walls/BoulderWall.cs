using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoulderMod.Content.Tiles.Walls
{
    internal class BoulderWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = false;
            DustType = DustID.DirtSpray;

            AddMapEntry(new Color(89, 57, 40));
        }
    }
}
