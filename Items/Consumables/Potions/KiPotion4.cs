﻿﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBZMOD.Items.Consumables.Potions
{
    public class KiPotion4 : KiPotion
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 34;
            item.consumable = true;
            item.maxStack = 30;
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 12;
            item.useTime = 12;
            item.value = 800;
            item.rare = 3;
            item.potion = false;
            IsKiPotion = true;
            KiHeal = 1500;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Massive Ki Potion");
            Tooltip.SetDefault("Restores 1500 Ki.");
        }
		 public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AngerKiCrystal", 2);
            recipe.AddIngredient(null, "KiPotion3", 1);
            recipe.AddTile(null, "ZTable");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}