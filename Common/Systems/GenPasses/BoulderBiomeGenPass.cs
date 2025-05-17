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
        int size = 100;

        int fillPercent = 46;
        int[,] map;

        public BoulderBiomeGenPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Spawning a lot of boulders";

            // Biome
            int x = WorldGen.genRand.Next(Main.maxTilesX / 3, (Main.maxTilesX / 3) * 2);
            int y = WorldGen.genRand.Next((int)GenVars.rockLayer + 300, Main.maxTilesY - 500);

            // Draw a circle
            WorldGen.TileRunner(x, y, 2 * size, Main.rand.Next(16, 16), ModContent.TileType<BoulderBlock>(), true, 0f, 0f, true, true);

            // Create walls
            for (int x_ = x - size; x_ < x + size; x_++)
                for (int y_ = y - size; y_ < y + size; y_++)
                {
                    //WorldGen.KillWall(x_, y_);
                    if (WorldGen.TileType(x_, y_) == ModContent.TileType<BoulderBlock>())
                        WorldGen.PlaceWall(x_, y_, ModContent.WallType<BoulderWall>());
                }

            // Create caves
            map = new int[size, size];
            RandomFillMap(x, y);

            //place boulders
            for (int x_ = x - size / 2; x_ < x + size / 2; x_++)
                for (int y_ = y - size / 2; y_ < y + size / 2; y_++)
                {
                    if (WorldGen.genRand.Next(0, 100) < 50)
                        WorldGen.PlaceObject(x_, y_ - 1, TileID.Boulder, true);
                }
        }

        void RandomFillMap(int xx, int yy)
        {
            Random prng = new System.Random(WorldGen.currentWorldSeed.GetHashCode());

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                        map[x, y] = 1;
                    else
                        map[x, y] = (prng.Next(0, 100) < fillPercent) ? 1 : 0;
            SmoothMap(xx, yy);
        }
        void SmoothMap(int xx, int yy)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
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
            for (int x_ = 0; x_ < size; x_++)
                for (int y_ = 0; y_ < size; y_++)
                {
                    WorldGen.KillWall(xx + x_ - (size / 2), yy + y_ - (size / 2));
                    WorldGen.PlaceWall(xx + x_ - (size / 2), yy + y_ - (size / 2), ModContent.WallType<BoulderWall>());

                    WorldGen.KillTile(xx + x_ - (size / 2), yy + y_ - (size / 2));
                    if (map[x_, y_] == 1)
                        WorldGen.PlaceTile(xx + x_ - (size / 2), yy + y_ - (size / 2), ModContent.TileType<BoulderBlock>()); // Dia Gemspark Block = 267 (260 if off)
                }
        }
        int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int x = gridX - 1; x <= gridX + 1; x++)
            {
                for (int y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 && x < size && y >= 0 && y < size)
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