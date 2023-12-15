using System;
using System.ComponentModel;
using MultiverseRel.Content.Configs.UI;
using MultiverseRel.Content.Generators;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace MultiverseRel.Content.Configs
{
	[TypeConverter(typeof(ToFromStringConverter<GeneratorDefinition>))]
	[CustomModConfigItem(typeof(GeneratorDefinitionElement))]
	public class GeneratorDefinition : EntityDefinition
	{
		public static readonly Func<TagCompound, GeneratorDefinition> DESERIALIZER = Load;

		public GeneratorDefinition()
			: this(-1)
		{
		}

		public GeneratorDefinition(int type)
			: base(type >= 0? GeneratorLoader.Get(type).FullName : "Terraria/None")
		{
		}

		public GeneratorDefinition(string key)
			: base(key)
		{
		}

		public GeneratorDefinition(string mod, string name)
			: base(mod, name)
		{
		}

		public override int Type =>
			!ModContent.TryFind<ModGenerator>(Mod != "Terraria" ? Mod + "/" + Name : Name, out var gen) ? -1 : gen.Type;

		public static GeneratorDefinition FromString(string s)
		{
			return new GeneratorDefinition(s);
		}

		public static GeneratorDefinition Load(TagCompound tag)
		{
			return new GeneratorDefinition(tag.GetString("mod"), tag.GetString("name"));
		}
	}
}