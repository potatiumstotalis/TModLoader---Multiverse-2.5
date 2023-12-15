﻿using System;
using System.Collections.Generic;
using MultiverseRel.Content.Generators;

namespace MultiverseRel.Content.Configs.UI
{
	public class GeneratorDefinitionElement : DefinitionElement<GeneratorDefinition>
	{
		protected override DefinitionOptionElement<GeneratorDefinition> CreateDefinitionOptionElement()
		{
			return new GeneratorDefinitionOptionElement(Value, 0.8f);
		}

		protected override void TweakDefinitionOptionElement(
			DefinitionOptionElement<GeneratorDefinition> optionElement)
		{
			optionElement.Top.Set(0.0f, 0.0f);
			optionElement.Left.Set(-124f, 1f);
		}

		protected override List<DefinitionOptionElement<GeneratorDefinition>> CreateDefinitionOptionElementList()
		{
			optionScale = 0.8f;
			var definitionOptionElementList = new List<DefinitionOptionElement<GeneratorDefinition>>();
			for (var type = 0; type < GeneratorLoader.GeneratorCount; ++type)
			{
				var optionElement = type != -1
					? new GeneratorDefinitionOptionElement(new GeneratorDefinition(type), optionScale)
					: new GeneratorDefinitionOptionElement(new GeneratorDefinition("Terraria", "None"), optionScale);
				optionElement.OnLeftClick += (a, b) =>
				{
					Value = optionElement.definition;
					updateNeeded = true;
					selectionExpanded = false;
				};
				definitionOptionElementList.Add(optionElement);
			}

			return definitionOptionElementList;
		}

		protected override List<DefinitionOptionElement<GeneratorDefinition>> GetPassedOptionElements()
		{
			var definitionOptionElementList = new List<DefinitionOptionElement<GeneratorDefinition>>();
			foreach (var option in options)
				if (GeneratorLoader.Get(option.type).Name
					    .IndexOf(chooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase) !=
				    -1)
				{
					var str = option.definition.Mod;
					if (option.type >= 0)
						str = GeneratorLoader.Get(option.type).Mod.DisplayName;
					if (str.IndexOf(chooserFilterMod.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
						definitionOptionElementList.Add(option);
				}

			return definitionOptionElementList;
		}
	}
}