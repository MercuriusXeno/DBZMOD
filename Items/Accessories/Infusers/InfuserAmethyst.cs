﻿﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBZMOD.Items.Accessories.Infusers
{
    public class InfuserAmethyst : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hitting enemies with ki attacks inflicts shadowflame.");
            DisplayName.SetDefault("Amethyst Ki Infuser");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 30;
            item.value = 3200;
            item.rare = 5;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            {
                player.GetModPlayer<MyPlayer>(mod).infuserAmethyst = true;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AngerKiCrystal", 25);
            recipe.AddIngredient(null, "ScrapMetal", 12);
            recipe.AddIngredient(ItemID.Amethyst, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}