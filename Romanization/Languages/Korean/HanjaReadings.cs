﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Romanization
{
	public static partial class Korean
	{
		public static readonly Lazy<HanjaReadingsSystem> HanjaReadings = new Lazy<HanjaReadingsSystem>(() => new HanjaReadingsSystem());

		/// <summary>
		/// A system for converting Hanja to Hangeul characters, or for romanizing Hanja directly.
		/// </summary>
		public sealed class HanjaReadingsSystem : IReadingsRomanizationSystem<HanjaReadingsSystem.ReadingTypes>
		{
			/// <summary>
			/// The supported reading types for Hanja. In this case, Hangeul is the only supported one.
			/// </summary>
			[Flags]
			public enum ReadingTypes
			{
				/// <summary>
				/// Hangeul, the Korean alphabet.
				/// </summary>
				Hangeul = 1
			}

			private const string HangeulFileName = "HanjaHangeul.csv";

			private static readonly Dictionary<string, char[]> HangeulReadings = new Dictionary<string, char[]>();

			internal HanjaReadingsSystem()
			{
				Utilities.LoadCharacterMap(HangeulFileName, HangeulReadings, k => k, v => v.Split(' ').Select(c => c[0]).ToArray());
			}

			/// <summary>
			/// Performs romanization of all Hanja in the given text, first to Hangeul, then to proper romanization according to <paramref name="system"/>.
			/// </summary>
			/// <param name="text">The text to romanize.</param>
			/// <param name="system">The romanization system to use for the Hangeul characters the Hanja is converted to.</param>
			/// <returns>A romanized version of the text, leaving unrecognized characters untouched. Note that all romanized text will be lowercase.</returns>
			[Pure]
			public string Process(string text, IRomanizationSystem system)
			{
				system ??= RevisedRomanization.Value;
				return system.Process(
					string.Join("",
						ProcessWithReadings(text).Characters
							.Select(c => c.Readings.Length > 0 ? c.Readings[0].Value : c.Character)));
			}

			/// <summary>
			/// Performs romanization of all Hanja in the given text, first to Hangeul, then to proper romanization according to the Revised Romanization of Korea system.
			/// </summary>
			/// <param name="text">The text to romanize.</param>
			/// <returns>A romanized version of the text, leaving unrecognized characters untouched. Note that all romanized text will be lowercase.</returns>
			[Pure]
			public string Process(string text)
				=> Process(text, RevisedRomanization.Value);

			/// <summary>
			/// Converts all Hanja in the given text to their first Hangeul character in the list of possible readings.<br />
			/// Note that this will not yield a romanized string, but rather a standard Korean one. See <see cref="Process(string, IRomanizationSystem)"/> for a romanized output.
			/// </summary>
			/// <param name="text">The text to romanize.</param>
			/// <returns>A version of the text with all Hanja characters replaced with their first Hangeul readings.</returns>
			[Pure]
			public string ProcessToHangeul(string text)
				=> string.Join("", ProcessWithReadings(text).Characters
					.Select(c => c.Readings.Length > 0 ? c.Readings[0].Value : c.Character));

			/// <summary>
			/// Performs romanization of all Hanja in the given text.<br />
			/// Returns a collection of all the characters in <paramref name="text"/>, but with all readings (pronunciations) of each in Hangeul (the Korean alphabet).<br />
			/// </summary>
			/// <param name="text">The text to romanize.</param>
			/// <returns>A <see cref="LanguageAgnostic.ReadingsString{ReadingTypes}"/> with all readings for each character in <paramref name="text"/>.</returns>
			[Pure]
			public LanguageAgnostic.ReadingsString<ReadingTypes> ProcessWithReadings(string text)
				=> new LanguageAgnostic.ReadingsString<ReadingTypes>(text.SplitIntoSurrogatePairs()
					.Select(c =>
					{
						List<LanguageAgnostic.Reading<ReadingTypes>> readings = new List<LanguageAgnostic.Reading<ReadingTypes>>(text.Length);

						if (HangeulReadings.TryGetValue(c, out char[] rawHanjaHangeulReadings))
							readings.AddRange(rawHanjaHangeulReadings.Select(r => new LanguageAgnostic.Reading<ReadingTypes>(ReadingTypes.Hangeul, r.ToString())));

						return new LanguageAgnostic.ReadingCharacter<ReadingTypes>(c, readings);
					})
					.ToArray());
		}
	}
}