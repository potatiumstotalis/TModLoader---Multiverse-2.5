using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using MultiverseRel.Content.Configs.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace MultiverseRel.Content.Configs
{
	public class MultiverseConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool PortalRecipe;

		[ReloadRequired]
		public List<MultiverseWorldConfiguration> Worlds;

		[ReloadRequired]
		public List<MultiverseTpFilterConfiguration> TpFilter;
	}

	public class MultiverseWorldConfiguration
	{
		[DefaultValue("World")]
		public string Name;

		[DefaultValue(0)]
		public int Seed;

		[DefaultValue(true)]
		public bool Saving;

		[Range(4200, 8400)] [DefaultValue(4200)] [Slider]
		public int Width;

		[Range(1200, 2400)] [DefaultValue(1200)] [Slider]
		public int Height;

		public GeneratorDefinition Generator;

		protected bool Equals(MultiverseWorldConfiguration other)
		{
			return Name == other.Name && Seed == other.Seed && Saving == other.Saving && Width == other.Width && Height == other.Height && Equals(Generator, other.Generator);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MultiverseWorldConfiguration)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Seed, Saving, Width, Height, Generator);
		}
	}

	public class MultiverseTpFilterConfiguration
	{
		[DefaultValue(true)]
		public bool Command;

		[DefaultValue(true)]
		public bool Portal;

		public SubworldDefinition Subworld;

		protected bool Equals(MultiverseTpFilterConfiguration other)
		{
			return Command == other.Command && Portal == other.Portal && Equals(Subworld, other.Subworld);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MultiverseTpFilterConfiguration)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Command, Portal, Subworld);
		}
	}
}