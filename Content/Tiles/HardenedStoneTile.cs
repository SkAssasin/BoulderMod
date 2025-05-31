using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;

namespace BoulderMod.Content.Tiles
{
    internal class HardenedStoneTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true; // affected by light
            Main.tileShine[Type] = 900; // how often dust particles spawn (higher number = less particles)

            AddMapEntry(new Color(128, 128, 128));
            RegisterItemDrop(ModContent.ItemType<Items.Placeables.HardenedStoneTile>());

            DustType = DustID.Stone;
            HitSound = SoundID.Tink;

            MineResist = 1.5f;
            MinPick = 50;
        }
    }
}
