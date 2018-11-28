﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;
using DBZMOD.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics;
using Microsoft.Xna.Framework;
using DBZMOD.Projectiles;
using Terraria.ModLoader.IO;
using Terraria.ID;
using DBZMOD;

namespace DBZMOD
{
    public class FlightSystem
    {
        //constants
        const int FLIGHT_KI_DRAIN = 4;
        public const float BURST_SPEED = 0.5f;
        const float FLIGHT_SPEED = 0.3f;

        bool m_FlightMode = false;
        Vector2 m_currentVel = new Vector2(0, 0);
        private int FLIGHT_KI_DRAIN_TIMER = 0;
        //float m_targetRotation = 0.0f;

        public void ToggleFlight(Player player, Mod mod)
        {
            m_FlightMode = !m_FlightMode;
            if(!m_FlightMode && MyPlayer.ModPlayer(player).flightDampeningUnlocked)
            {
                player.AddBuff(mod.BuffType("KatchinFeet"), 600);
            }
        }
       
        public void Update(TriggersSet triggersSet, Player player)
        {
            if (m_FlightMode)
            {
                //check for ki
                if (MyPlayer.ModPlayer(player).KiCurrent <= 0)
                {
                    m_FlightMode = false;
                    player.fullRotation = MathHelper.ToRadians(0);
                    return;
                }

                //prepare vals
                player.fullRotationOrigin = new Vector2(11, 22);
                MyPlayer.ModPlayer(player).IsFlying = true;
                Vector2 m_rotationDir = Vector2.Zero;

                //m_targetRotation = 0;

                //Input checks
                float boostSpeed = (BURST_SPEED) * (MyPlayer.EnergyCharge.Current? 1 : 0);
                int totalFlightUsage = FLIGHT_KI_DRAIN - MyPlayer.ModPlayer(player).FlightUsageAdd;
                float totalFlightSpeed = FLIGHT_SPEED + boostSpeed + (player.moveSpeed / 3) + MyPlayer.ModPlayer(player).FlightSpeedAdd;

                if (triggersSet.Up)
                {
                    m_currentVel.Y -= totalFlightSpeed;
                    m_rotationDir = Vector2.UnitY;
                }
                else if (triggersSet.Down)
                {
                    m_currentVel.Y += totalFlightSpeed;
                    m_rotationDir = -Vector2.UnitY;
                }

                if (triggersSet.Right)
                {
                    m_currentVel.X += totalFlightSpeed;
                    m_rotationDir += Vector2.UnitX;
                }
                else if (triggersSet.Left)
                {
                    m_currentVel.X -= totalFlightSpeed;
                    m_rotationDir -= Vector2.UnitX;
                }

                if (m_currentVel.Length() > 0.5f)
                {
                    if(boostSpeed == 0) //not boosting?
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Dust tdust = Terraria.Dust.NewDustPerfect(Main.LocalPlayer.Center, 91, new Vector2(0f, 0f), 0, new Color(255,255,255), 1.75f);
							tdust.noGravity = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
							Dust tdust = Terraria.Dust.NewDustPerfect(Main.LocalPlayer.Center, 91, new Vector2(0f, 0f), 0, new Color(255,255,255), 2.25f);
							tdust.noGravity = true;
                        }
                    }
                }

                //caluclate velocity
                player.velocity = m_currentVel - (Vector2.UnitY * 0.4f);
                m_currentVel.X = MathHelper.Lerp(m_currentVel.X, 0, 0.1f);
                m_currentVel.Y = MathHelper.Lerp(m_currentVel.Y, 0, 0.1f);

                //calculate rotation
                float radRot = 0;
                if (m_rotationDir != Vector2.Zero)
                {
                    m_rotationDir.Normalize();
                    radRot = (float)Math.Atan((m_rotationDir.X / m_rotationDir.Y));

                    if (m_rotationDir.Y < 0)
                    {
                        if (m_rotationDir.X > 0)
                            radRot += MathHelper.ToRadians(180);
                        else if (m_rotationDir.X < 0)
                            radRot -= MathHelper.ToRadians(180);
                        else
                        {
                            if (m_currentVel.X > 0)
                                radRot = MathHelper.ToRadians(180);
                            else if (m_currentVel.X < 0)
                                radRot = MathHelper.ToRadians(-180);
                        }

                    }
                }
                player.fullRotation = MathHelper.Lerp(player.fullRotation, radRot, 0.1f);
                FLIGHT_KI_DRAIN_TIMER++;
                //drain ki
                if (!MyPlayer.ModPlayer(player).flightUpgraded)
                {
                    if (FLIGHT_KI_DRAIN_TIMER >= 1)
                    {
                        int flightKiCost = totalFlightUsage + (totalFlightUsage * (int)boostSpeed);
                        PlayerProgressionHelper.AddKi(player, flightKiCost, true, true);
                        FLIGHT_KI_DRAIN_TIMER = 0;
                    }
                }
                else if (MyPlayer.ModPlayer(player).flightUpgraded)
                {
                    if (FLIGHT_KI_DRAIN_TIMER >= 3)
                    {
                        int flightKiCost = totalFlightUsage + (totalFlightUsage * (int)boostSpeed);
                        PlayerProgressionHelper.AddKi(player, flightKiCost, true, true, TransformationStates.Untransformed);
                        FLIGHT_KI_DRAIN_TIMER = 0;
                    }
                }
                if (totalFlightUsage < 1)
                {
                    totalFlightUsage = 1;
                }
            }
            else //no longer flying cuz of mode change or ki ran out
            {
                Mod mod = ModLoader.GetMod("DBZMOD");
                player.fullRotation = MathHelper.ToRadians(0);
                MyPlayer.ModPlayer(player).IsFlying = false;
                if (MyPlayer.ModPlayer(player).KiCurrent <= 0 && MyPlayer.ModPlayer(player).flightDampeningUnlocked)
                {
                    player.AddBuff(mod.BuffType("KatchinFeet"), 600);
                }
            }
        }

    }
}

