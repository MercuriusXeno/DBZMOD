using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DBZMOD.Items
{
	public class RadiantFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radiant Fragment");
			Tooltip.SetDefault("'The endurance of the cosmos crackles around this fragment'");
		    ItemID.Sets.ItemNoGravity[item.type] = true;
	        Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(1, 1));
            ItemID.Sets.ItemIconPulse[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 10000;
			item.rare = 9;
		}
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
		}
	}
}