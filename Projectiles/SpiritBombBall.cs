﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace DBZMOD.Projectiles
{
    public class SpiritBombBall : KiProjectile
    {
        public int KiChargeDrain = -2;
        public bool Released = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Bomb");
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.light = 1f;
            projectile.aiStyle = 1;
            aiType = 14;
            projectile.friendly = true;
            projectile.extraUpdates = 2;
            projectile.ignoreWater = true;
            projectile.penetrate = 12;
            projectile.timeLeft = 400;
            projectile.tileCollide = false;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            KiDrainRate = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
			/*if (projectile.timeLeft < 85) 
			{
				byte b2 = (byte)(projectile.timeLeft * 3);
				byte a2 = (byte)(100f * ((float)b2 / 255f));
				return new Color((int)b2, (int)b2, (int)b2, (int)a2);
			}*/
			return new Color(255, 255, 255, 100);
        }

        public override void Kill(int timeLeft)
        {
            if (!projectile.active)
            {
                return;
            }

            Projectile proj = Projectile.NewProjectileDirect(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(0,0), mod.ProjectileType("SpiritBombExplosion"), projectile.damage, projectile.knockBack, projectile.owner);
            proj.width *= (int)projectile.scale;
            proj.height *= (int)projectile.scale;

            projectile.active = false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (Main.myPlayer == projectile.owner)
            {
                if (!Released)
                {
                    projectile.scale += 0.04f;
                    projectile.netUpdate = true;

                    projectile.position = player.position + new Vector2(0, -20 - (projectile.scale * 17));

                    for (int d = 0; d < 25; d++)
                    {
                        float angle = Main.rand.NextFloat(360);
                        float angleRad = MathHelper.ToRadians(angle);
                        Vector2 position = new Vector2((float)Math.Cos(angleRad), (float)Math.Sin(angleRad));

                        Dust tDust = Dust.NewDustDirect(projectile.position + (position * (20 + 12.5f * projectile.scale)), projectile.width, projectile.height, 15, 0f, 0f, 213, default(Color), 2.0f);
                        tDust.velocity = Vector2.Normalize((projectile.position + (projectile.Size / 2)) - tDust.position) * 2;
                        tDust.noGravity = true;
                    }

                    if (projectile.timeLeft < 399)
                    {
                        projectile.timeLeft = 400;
                    }
                    if (MyPlayer.ModPlayer(player).KiCurrent <= 0)
                    {
                        projectile.Kill();
                    }

                    PlayerProgressionHelper.AddKi(player, KiChargeDrain, false);
                    player.velocity = new Vector2(player.velocity.X / 3, player.velocity.Y);

                    //Rock effect
                    projectile.ai[1]++;
                    if (projectile.ai[1] % 7 == 0)
                        Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloat(-500, 600), projectile.Center.Y + 1000, 0, -10, mod.ProjectileType("StoneBlockDestruction"), projectile.damage, 0f, projectile.owner);
                    Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloat(-500, 600), projectile.Center.Y + 1000, 0, -10, mod.ProjectileType("DirtBlockDestruction"), projectile.damage, 0f, projectile.owner);
                }
            }

            //if button let go
            if (!player.channel || projectile.scale > 12)
            {
                if (!Released)
                {
                    Released = true;
                    projectile.velocity = Vector2.Normalize(Main.MouseWorld - projectile.position) * 6;
                    projectile.tileCollide = false;
                    projectile.damage *= (int)projectile.scale;
                    projectile.position.X = projectile.position.X + (projectile.width / 2);
                    projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                    projectile.width *= (int)projectile.scale;
                    projectile.height *= (int)projectile.scale;
                    projectile.position.X = projectile.position.X - (projectile.width / 2);
                    projectile.position.Y = projectile.position.Y - (projectile.height / 2);

                }
            }

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            DBZMOD.Circle.ApplyShader(-9001);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
        }
    }
}