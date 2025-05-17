using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.Utilities;
using BoulderMod.Content.NPCs;
using Terraria.GameContent;
using ReLogic.Content;

namespace BoulderMod.Content.NPCs
{
    internal class BombSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 32;
            NPC.damage = 12;
            NPC.defense = 4;
            NPC.lifeMax = 35;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 8f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.alpha = 75;

            AnimationType = NPCID.BlueSlime; 
            Banner = Item.NPCtoBanner(NPCID.BlueSlime); 
            BannerItem = Item.BannerToItem(Banner); // Makes kills of this NPC go towards dropping the banner it's associated with.
                                                    //SpawnModBiomes = new int[] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // Associates this NPC with the ExampleSurfaceBiome in Bestiary
        }
        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> bombSprite = Mod.Assets.Request<Texture2D>("Content/Items/Tools/ExampleHookChain");
            Rectangle frame = bombSprite.Value.Frame(1, 2, 0, /* is the slime animation on frame 0? *//*  ? 0 : 1);

            spriteBatch.Draw(bombSprite.Value, new Vector2(0, 0), frame, Color.White);

            return true;
        }*/
    }
}
