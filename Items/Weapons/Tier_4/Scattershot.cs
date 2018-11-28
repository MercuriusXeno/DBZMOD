﻿﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBZMOD.Items.Weapons.Tier_4
{
	public class Scattershot : KiItem
	{
		public override void SetDefaults()
		{
			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			item.shoot = mod.ProjectileType("ScattershotBlast");
			item.shootSpeed = 20f;
			item.damage = 12;
			item.knockBack = 2f;
			item.useStyle = 5;
			item.UseSound = SoundID.Item12;
			item.useAnimation = 15;
			item.useTime = 7;
			item.width = 40;
			item.noUseGraphic = true;
			item.height = 40;
			item.autoReuse = true;
			if(!Main.dedServ)
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Kiblast1").WithPitchVariance(.3f);
            }
			item.value = 13000;
			item.rare = 4;
            item.channel = true;
            KiDrain = 120;
			WeaponType = "Barrage";
	    }
	    public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Scatter Shot");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			{
                int numberProjectiles = 1 + (int)Math.Floor(Main.rand.Next(4) / 4d); // 1 or rarely 2 shots
			    for (int i = 0; i < numberProjectiles; i++)
			    {
				    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)); // 30 degree spread.
				    // If you want to randomize the speed to stagger the projectiles
			         float scale = 1f - (Main.rand.NextFloat() * .4f);
			         perturbedSpeed = perturbedSpeed * scale; 
				    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			    }
			    return false;
			}
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 12f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "AngerKiCrystal", 30);
			recipe.AddIngredient(null, "HellzoneGrenade");
            recipe.AddIngredient(null, "SoulofEntity", 10);
            recipe.AddTile(null, "ZTable");
            recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
