﻿﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DBZMOD.Items
{
    public class KiOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ki Orb");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 3));
            Lighting.AddLight(item.Center, 0.1f, 0.3f, 0.9f);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 66;
        }

        public override bool ItemSpace(Player player)
        {
            return true;
        }

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= MyPlayer.ModPlayer(player).OrbGrabRange;
        }

        public override bool OnPickup(Player player)
        {
            Main.PlaySound(SoundID.NPCDeath7, player.position);
            //Mine makes a sound
            //modplayer resource increase goes here++
            PlayerProgressionHelper.AddKi(player, MyPlayer.ModPlayer(player).OrbHealAmount, false);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), 50, false, false);
            //(165, 255, 185) is the color of the text
            //(1) is the displayed text
            return false;
        }
			public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
 }