﻿﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBZMOD.Buffs
{
    public class UIOmenBuff : TransBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ultra Instinct -Sign-");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            IsKaioken = false;
            IsSSJ = true;
            Description.SetDefault("The secret technique of the gods. Grants the ability to dodge any attack.");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            DamageMulti = 2f;
            SpeedMulti = 7f;
            KiDrainRate = 10;
            KiDrainBuffMulti = 1f;
            base.Update(player, ref buffIndex);
        }
    }
}

