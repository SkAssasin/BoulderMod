using Terraria.ModLoader;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using BoulderMod.Common.Systems.GenPasses;

namespace BoulderMod.Common.Systems
{
    internal class WorldSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int biomeIdex = tasks.FindIndex(t => t.Name.Equals("Hives"));
            if (biomeIdex != 1)
                tasks.Insert(biomeIdex + 1, new BoulderBiomeGenPass("Boulder Biome Pass", 300));
        }
    }
}
