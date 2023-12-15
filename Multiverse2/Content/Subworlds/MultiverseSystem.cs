﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.Xna.Framework;
using Multiverse2.Content.Configs;
using Multiverse2.Content.Generators;
using Multiverse2.Content.Tiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Multiverse2.Content.Subworlds
{
	public class MultiverseSystem : ModSystem
	{
		private GameTime _lastUpdateUiGameTime;

		internal UserInterface UI;
		internal PortalUI UIState;

		public override void OnModLoad()
		{
			if (ModContent.GetInstance<MultiverseConfig>() != null && ModContent.GetInstance<MultiverseConfig>().Worlds is
			{
				Count: > 0
			})
				foreach (var world in ModContent.GetInstance<MultiverseConfig>().Worlds)
					Mod.AddContent(new MultiverseWorld(world));

			InitializeLang();

			if (!Main.dedServ)
			{
				UI = new UserInterface();
				UIState = new PortalUI();
				UIState.Activate();
			}
		}

		private void InitializeLang()
		{
			if (ModContent.GetInstance<MultiverseConfig>() != null && ModContent.GetInstance<MultiverseConfig>().Worlds is
			{
				Count: > 0
			})
				foreach (var world in ModContent.GetInstance<MultiverseConfig>().Worlds)
				{
					Mod.GetLocalizationKey(world.Name);
				}
			if (ModContent.GetContent<ModGenerator>() != null && ModContent.GetContent<ModGenerator>().ToList() is
			{
				Count: > 0
			})
				foreach (var generator in ModContent.GetContent<ModGenerator>().ToList())
				{
                    Mod.GetLocalizationKey(generator.Name);
                }

            Mod.GetLocalizationKey("Save");
        }

		public override void Unload()
		{
			UIState = null;
			UI = null;
		}


		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUiGameTime = gameTime;
			if (UI?.CurrentState != null)
				UI.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"Multiverse2: PortalUI",
					delegate
					{
						if (_lastUpdateUiGameTime != null && UI?.CurrentState != null)
							UI.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						return true;
					},
					InterfaceScaleType.UI));
		}
	}
}