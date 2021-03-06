using Terraria;
using Terraria.ModLoader;
using DBZMOD.Items;

namespace DBZMOD.Prefixes
{
    public class CondensedPrefix : ModPrefix
    {
        public override float RollChance(Item item)
        {
            return 3f;
        }
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Condensed");
        }
        public override bool CanRoll(Item item)
        {
            if (item.modItem is KiItem)
            {
                return true;
            }
            return false;
        }
        public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }

        public override void Apply(Item item)
        {
            //((KiItem)item.modItem)
            item.damage = (int)(item.damage * 1.10f);
            item.shootSpeed *= 0.85f;
        }
    }
}