using BoulderMod.Content.Tiles.Walls;
using BoulderMod.Content.Tiles;
using Terraria.WorldBuilding;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria;
using System;
using Terraria.ID;

namespace BoulderMod.Common.Systems.GenPasses
{
    internal class BoulderBiomeGenPass : GenPass
    {
        int biomeSize = 150;

        int oreClumpAmmount = 15;
        int oreClumpSizeMin = 3;
        int oreClumpSizeMax = 5;

        int fillPercent = 47;
        int[,] map;

        public BoulderBiomeGenPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Adding MORE boulders";

            // ----- Biome -----
            int x = WorldGen.genRand.Next(Main.maxTilesX / 3, (Main.maxTilesX / 3) * 2);
            int y = WorldGen.genRand.Next((int)GenVars.rockLayer + 300, Main.maxTilesY - 500);

            // Draw a circle
            WorldGen.TileRunner(x, y, 2 * biomeSize, Main.rand.Next(8, 8), ModContent.TileType<BoulderBlock>(), true, 0f, 0f, true, true);

            // Create walls
            for (int x_ = x - biomeSize; x_ < x + biomeSize; x_++)
                for (int y_ = y - biomeSize; y_ < y + biomeSize; y_++)
                {
                    //WorldGen.KillWall(x_, y_);
                    if (WorldGen.TileType(x_, y_) == ModContent.TileType<BoulderBlock>())
                        WorldGen.PlaceWall(x_, y_, ModContent.WallType<BoulderWall>());
                }

            // Create caves
            map = new int[biomeSize, biomeSize];
            RandomFillMap(x, y);

            // Place boulders
            for (int x_ = x - biomeSize / 2; x_ < x + biomeSize / 2; x_++)
                for (int y_ = y - biomeSize / 2; y_ < y + biomeSize / 2; y_++)
                {
                    if (WorldGen.genRand.Next(0, 100) < 75)
                        WorldGen.PlaceObject(x_, y_ - 1, TileID.Boulder, true);
                }

            // Generate Hardened Stone
            int tries = 200;
            while (tries > 0 && oreClumpAmmount > 0)
            {
                int oreX = Main.rand.Next(x - biomeSize, x + biomeSize);
                int oreY = Main.rand.Next(y - biomeSize, y + biomeSize);
                if (WorldGen.TileType(oreX, oreY) == ModContent.TileType<BoulderBlock>())
                {
                    WorldGen.TileRunner(oreX, oreY, Main.rand.Next(oreClumpSizeMin, oreClumpSizeMax), Main.rand.Next(16, 16), ModContent.TileType<HardenedStoneTile>(), true, 0f, 0f, true, true);
                    oreClumpAmmount--;
                }
                tries--;
            }
        }

        void RandomFillMap(int xx, int yy)
        {
            Random prng = new System.Random(WorldGen.currentWorldSeed.GetHashCode());

            for (int x = 0; x < biomeSize; x++)
                for (int y = 0; y < biomeSize; y++)
                    if (x == 0 || y == 0 || x == biomeSize - 1 || y == biomeSize - 1)
                        map[x, y] = 1;
                    else
                        map[x, y] = (prng.Next(0, 100) < fillPercent) ? 1 : 0;
            SmoothMap(xx, yy);
        }
        void SmoothMap(int xx, int yy)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int x = 0; x < biomeSize; x++)
                {
                    for (int y = 0; y < biomeSize; y++)
                    {
                        int neighbours = GetSurroundingWallCount(x, y);

                        if (neighbours > 4)
                            map[x, y] = 1;
                        else if (neighbours < 4)
                            map[x, y] = 0;
                    }
                }
            }
            CreateMap(xx, yy);
        }
        void CreateMap(int xx, int yy)
        {
            for (int x_ = 0; x_ < biomeSize; x_++)
                for (int y_ = 0; y_ < biomeSize; y_++)
                {
                    WorldGen.KillWall(xx + x_ - (biomeSize / 2), yy + y_ - (biomeSize / 2));
                    WorldGen.PlaceWall(xx + x_ - (biomeSize / 2), yy + y_ - (biomeSize / 2), ModContent.WallType<BoulderWall>());

                    WorldGen.KillTile(xx + x_ - (biomeSize / 2), yy + y_ - (biomeSize / 2));
                    if (map[x_, y_] == 1)
                        WorldGen.PlaceTile(xx + x_ - (biomeSize / 2), yy + y_ - (biomeSize / 2), ModContent.TileType<BoulderBlock>()); // Dia Gemspark Block = 267 (260 if off)
                }
        }
        int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int x = gridX - 1; x <= gridX + 1; x++)
            {
                for (int y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 && x < biomeSize && y >= 0 && y < biomeSize)
                    {
                        if (gridX != x || gridY != y)
                            wallCount += map[x, y];
                    }
                    else
                        wallCount++;
                }
            }
            return wallCount;
        }
    }
}