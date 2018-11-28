using System;
using Terraria;

namespace DBZMOD
{
    public class PlayerProgressionHelper
    {
        public const int BASE_KI = 1000;
        // universally used in every place where ki is consumed or regenerated
        // filtering through this method lets the mod handle whether ki manipulation
        // is granting the player experience. Formulas up for debate.
        public static void AddKi(Player player, int kiAmount, bool isGivingExperience, bool isFlight = false, TransformationStates transformationState = TransformationStates.Untransformed)
        {
            MyPlayer modPlayer = MyPlayer.ModPlayer(player);
            modPlayer.KiCurrent += kiAmount;

            // experience will be rewarded for the ki change
            if (isGivingExperience)
            {
                // no change? no experience.
                if (kiAmount == 0)
                    return;

                // ki is being spent, improve ki maximum.
                if (kiAmount < 0)
                {
                    modPlayer.KiSpentInLifetime += -kiAmount;
                }
                else
                {
                    // ki is being regenerated, improve ki charge rates
                    modPlayer.KiCreatedInLifetime += kiAmount;
                }

                ProcessKiProgression(modPlayer);
            }
        }

        public static void ProcessKiProgression(MyPlayer player)
        {            
            float kiMultiplier = (float)Math.Floor(1 + Math.Log((double)player.KiSpentInLifetime / 1000d) / Math.Log(1.4d));
            player.KiMax = Math.Max(player.KiMax, (int)Math.Floor(BASE_KI * kiMultiplier));
        }
    }
}

