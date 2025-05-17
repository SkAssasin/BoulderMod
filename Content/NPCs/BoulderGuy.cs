using BoulderMod.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace BoulderMod.Content.NPCs
{
    [AutoloadHead]
    public class BoulderGuy : ModNPC
    {
        public override void Load()
        {
            base.Load();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 23;
            NPCID.Sets.DangerDetectRange[NPC.type] = 500; //this defines the npc danger detect range
            NPCID.Sets.AttackType[NPC.type] = 1; //this is the attack type,  0 (throwing), 1 (shooting), or 2 (magic). 3 (melee) 
            NPCID.Sets.AttackTime[NPC.type] = 3; //this defines the npc attack speed
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;//this defines the npc atack chance
            NPCID.Sets.ShimmerTownTransform[NPC.type] = false;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            if (Main.hardMode)
                NPC.damage = 15;

            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Clothier;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

				// Sets your NPC's flavor text in the bestiary. (use localization keys)
				//new FlavorTextBestiaryInfoElement("Mods.ExampleMod.Bestiary.ExamplePerson_1")
            ]);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (NPC.downedBoss3)
                return true;
            return false;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }
        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                    "Storner",
                    "Stoner",
                    "Stoey",
                    "Stoney",
                    "Steve"
            };
        }
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            if (NPC.downedGolemBoss && Main.rand.NextBool(5))
                chat.Add("You are using boulders by placing them? With your hands? Come on, this is a modern age. You rather have to buy my STAFF OF EARTH! JUST 10 GOLD FOR ONE!");
            chat.Add("I know, that you want my gun, but I'm not gonna sell it to you. IT'S MINE!");
            chat.Add("Did you hear about Terravid73? HE IS AWESOME!");
            chat.Add("Hello there...");
            /*if (Main.hardMode && Main.LocalPlayer.HasItem(Items.Placeables.BoulderBlock && Main.rand.NextBool(3))
            {
                Main.npcChatCornerItem = Items.Placeables.BoulderBlock;
               chat.Add("Found this in a Boulder biome. Maybe it has some use");
            }*/

            string chatFinal = chat; // this is here because the randomisation happens when the list gets converted to random
            return chatFinal;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
                shop = "Shop Main";
        }
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "Shop Main")
                .Add(ItemID.Boulder)
                // to add: hardened stone ore (if in hardmode)
                .Add<Items.Placeables.BoulderBlock>();
            if (NPC.downedGolemBoss)
                npcShop.Add(new Item(ItemID.StaffofEarth) { shopCustomPrice = Item.buyPrice(gold: 10) });
            npcShop.Register();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.CowboyHat));
        }
        public override bool CanGoToStatue(bool toKingStatue) => true;

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;  //npc damage
            knockback = 2f;   //npc knockback
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 1;
            randExtraCooldown = 2;
        }
        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            Main.GetItemDrawFrame(ItemID.Handgun, out item, out itemFrame);
            //item = ItemID.Handgun;
            //if (Main.hardMode)
            //    item = ItemID.PhoenixBlaster;

            scale = .75f;
            //horizontalHoldoutOffset = 20;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.Bullet;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 7f;
            // randomOffset = 4f;
        }
    }
}