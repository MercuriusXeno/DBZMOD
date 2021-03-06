﻿using System;
using System.Linq;
using DBZMOD.Buffs;
using DBZMOD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBZMOD.UI
{
    internal class TransMenu : EasyMenu
    {
        public static bool menuvisible = false;
        private UIText titleText;
        public UIImage backPanelImage;
        private UIImageButton ssjButtonTexture;
        private UIImageButton ssj2ButtonTexture;
        private UIImageButton ssj3ButtonTexture;
        private UIImageButton lssjButtonTexture;
        private UIImageButton lssj2ButtonTexture;
        private UIImageButton ssjgButtonTexture;
        private UIImage lockedImage1;
        private UIImage lockedImage2;
        private UIImage lockedImage3;
        private UIImage lockedImageG;
        private UIImage lockedImageL1;
        private UIImage lockedImageL2;
        private UIImage unknownImage2;
        private UIImage unknownImage3;
        private UIImage unknownImageG;
        private UIImage unknownImageL1;
        private UIImage unknownImageL2;


        public static int MenuSelection;
        public static bool SSJ1On;
        public static bool SSJ2On;
        public static bool SSJ3On;
        public static bool LSSJOn;
        private Player player;
        public const float PADDINGX = 10f;
        public const float PADDINGY = 30f;

        public override void OnInitialize()
        {
            base.OnInitialize();
            //backPanel.BackgroundColor = new Color(100, 100, 100);
            
            backPanelImage = new UIImage(GFX.BackPanel);
            backPanelImage.Width.Set(GFX.BackPanel.Width, 0f);
            backPanelImage.Height.Set(GFX.BackPanel.Height, 0f);
            backPanelImage.Left.Set(-12, 0f);
            backPanelImage.Top.Set(-12, 0f);
            backPanel.Append(backPanelImage);
            float Row1_OffsetX = 0.0f;

            InitText(ref titleText, "Transformation Tree", 55, -32, Color.White);

            Row1_OffsetX = PADDINGX;
            InitButton(ref ssjButtonTexture, GFX.SSJ1ButtonImage, new MouseEvent(TrySelectingSSJ1),
                Row1_OffsetX - 2, 
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref lockedImage1, GFX.LockedImage,
                0,
                0,
                ssjButtonTexture);

            Row1_OffsetX = PADDINGX + GFX.SSJ1ButtonImage.Width;
            InitButton(ref ssj2ButtonTexture, GFX.SSJ2ButtonImage, new MouseEvent(TrySelectingSSJ2),
                Row1_OffsetX + 14, 
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref lockedImage2, GFX.LockedImage,
                0,
                0,
                ssj2ButtonTexture);

            InitImage(ref unknownImage2, GFX.UnknownImage,
                0,
                0,
                ssj2ButtonTexture);

            Row1_OffsetX = PADDINGX + GFX.SSJ2ButtonImage.Width * 2;
            InitButton(ref ssj3ButtonTexture, GFX.SSJ3ButtonImage, new MouseEvent(TrySelectingSSJ3),
                Row1_OffsetX + 22, 
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref lockedImage3, GFX.LockedImage,
                0,
                0,
                ssj3ButtonTexture);

            InitImage(ref unknownImage3, GFX.UnknownImage,
                0,
                0,
                ssj3ButtonTexture);

            InitButton(ref lssjButtonTexture, GFX.LSSJButtonImage, new MouseEvent(TrySelectingLSSJ), 
                PADDINGX + 14 + GFX.SSJ1ButtonImage.Width, 
                GFX.SSJ1ButtonImage.Height + PADDINGY - 10,
                backPanelImage);
            
            InitImage(ref lockedImageL1, GFX.LockedImage,
                0, 
                0,
                lssjButtonTexture);

            InitImage(ref unknownImageL1, GFX.UnknownImage,
                0, 
                0,
                lssjButtonTexture);

            Row1_OffsetX = PADDINGX + GFX.SSJ3ButtonImage.Width * 3;
            InitButton(ref ssjgButtonTexture, GFX.SSJGButtonImage, new MouseEvent(TrySelectingSSJG),
                Row1_OffsetX + 30, 
                PADDINGY - 20,
                backPanelImage);
                            
            InitImage(ref lockedImageG, GFX.LockedImage,
                0,
                0,
                ssjgButtonTexture);

            InitImage(ref unknownImageG, GFX.UnknownImage,
                0,
                0,
                ssjgButtonTexture);
            
            InitButton(ref lssj2ButtonTexture, GFX.LSSJ2ButtonImage, new MouseEvent(TrySelectingLSSJ2), 
                PADDINGX + 22 + GFX.SSJ1ButtonImage.Width * 2, 
                GFX.SSJ1ButtonImage.Height + PADDINGY - 10,
                backPanelImage);

            InitImage(ref lockedImageL2, GFX.LockedImage,
                0, 
                0,
                lssj2ButtonTexture);

            InitImage(ref unknownImageL2, GFX.UnknownImage,
                0, 
                0,
                lssj2ButtonTexture);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            lockedImage1.ImageScale = !player.SSJ1Achieved ? 1.0f : 0.0f;

            if(player.playerTrait == "Legendary")
            {
                lockedImageL1.ImageScale = !player.LSSJAchieved ? 1.0f : 0.0f;

                lockedImageL2.ImageScale = !player.LSSJ2Achieved ? 1.0f : 0.0f;

                lockedImage2.ImageScale = 1.0f;

                unknownImage2.ImageScale = 0.0f;

                lockedImage3.ImageScale = 0.0f;

                unknownImage3.ImageScale = 1.0f;

                lockedImageG.ImageScale = 0.0f;

                unknownImageG.ImageScale = 1.0f;

                unknownImageL1.ImageScale = 0.0f;

                unknownImageL2.ImageScale = !player.LSSJAchieved ? 1.0f : 0.0f;
            }
            else
            {
                unknownImageL1.ImageScale = 0.0f;
                unknownImageL2.ImageScale = 1.0f;
                unknownImage2.ImageScale = 0.0f;
                unknownImage3.ImageScale = 0.0f;
                unknownImageG.ImageScale = 0.0f;
                lockedImage2.ImageScale = !player.SSJ2Achieved ? 1.0f : 0.0f;

                lockedImage3.ImageScale = !player.SSJ3Achieved ? 1.0f : 0.0f;

                lockedImageG.ImageScale = !player.SSJGAchieved ? 1.0f : 0.0f;

                lockedImageL1.ImageScale = 1.0f;
                lockedImageL2.ImageScale = 0.0f;
            }

        }

        /*
            Menu Selection ID's
            1 = SSJ1
            2 = SSJ2
            3 = SSJ3
            4 = LSSJ1
            5 = SSJG
            6 = LSSJ2
            7 = SSJB/SSJBE
            8 = LSSJG
            9 = UI/MUI
         */
        private void TrySelectingSSJ1(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if(player.SSJ1Achieved)
            {
                MenuSelection = 1;
                Main.PlaySound(SoundID.MenuTick);
                Main.NewText("SSJ1 Mastery = " + player.MasteryLevel1 + "/" + player.MasteryMax1);
            }
            else
            {
                Main.PlaySound(SoundID.MenuClose);
                Main.NewText("Only through failure with a powerful foe will true power awaken.");
            }
        }

        private void TrySelectingSSJ2(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if(player.SSJ2Achieved && !(player.playerTrait == "Legendary"))
            {
                MenuSelection = 2;
                Main.PlaySound(SoundID.MenuTick);
                Main.NewText("SSJ2 Mastery = " + player.MasteryLevel2 + "/" + player.MasteryMax2);
            }
            else if (!player.LSSJAchieved)
            {
                Main.PlaySound(SoundID.MenuClose);
                Main.NewText("One may awaken their true power through extreme pressure while ascended.");
            }
        }
        private void TrySelectingSSJ3(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if(player.SSJ3Achieved && !(player.playerTrait == "Legendary"))
            {
                MenuSelection = 3;
                Main.PlaySound(SoundID.MenuTick);
                Main.NewText("SSJ3 Mastery = " + player.MasteryLevel3 + "/" + player.MasteryMax3);
            }
            else if (!player.LSSJAchieved)
            {
                Main.PlaySound(SoundID.MenuClose);
                Main.NewText("The power of an ancient foe may be the key to unlocking greater power.");
            }
        }
        private void TrySelectingLSSJ(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.LSSJAchieved)
            {
                MenuSelection = 4;
                Main.PlaySound(SoundID.MenuTick);
            }
            else if (!player.SSJ2Achieved)
            {
                Main.PlaySound(SoundID.MenuClose);
                Main.NewText("The rarest saiyans may be able to achieve a form beyond anything a normal saiyan could obtain.");
            }
        }

        private void TrySelectingLSSJ2(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.LSSJ2Achieved)
            {
                MenuSelection = 6;
                Main.PlaySound(SoundID.MenuTick);
            }
            else if (!player.LSSJ2Achieved)
            {
                Main.PlaySound(SoundID.MenuClose);
                Main.NewText("A legendary saiyan sometimes may lose complete control upon being pushed into a critical state.");
            }
        }
        private void TrySelectingSSJG(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if(player.SSJGAchieved && !(player.playerTrait == "Legendary"))
            {
                MenuSelection = 5;
                Main.PlaySound(SoundID.MenuTick);
            }
            else if (!player.LSSJAchieved)
            {
                Main.PlaySound(SoundID.MenuClose);
                Main.NewText("The godlike power of the lunar star could awaken something beyond mortal comprehension.");
            }
        }

    }
}