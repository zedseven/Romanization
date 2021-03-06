﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Romanization;

// ReSharper disable CheckNamespace
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace RomanizationTests.GreekTests.AncientTests
{
	/// <summary>
	/// For testing the Ancient Greek ALA-LC romanization system, <see cref="Greek.Ancient.AlaLc"/>.<br />
	/// The large test strings are modified versions of a passage from the Almagest.
	/// </summary>
	[TestClass]
	public class AlaLcTests
	{
		private readonly Greek.Ancient.AlaLc _systemOr = new Greek.Ancient.AlaLc(OutputNumeralType.Roman,  true);
		private readonly Greek.Ancient.AlaLc _systemOa = new Greek.Ancient.AlaLc(OutputNumeralType.Arabic, true);
		private readonly Greek.Ancient.AlaLc _systemNr = new Greek.Ancient.AlaLc(OutputNumeralType.Roman,  false);
		private readonly Greek.Ancient.AlaLc _systemNa = new Greek.Ancient.AlaLc(OutputNumeralType.Arabic, false);

		/// <summary>
		/// Aims to test use of the system with <see cref="OutputNumeralType.Roman"/> and older punctuation &amp; numerals.
		/// </summary>
		[TestMethod]
		public void ProcessOlderRomanTest()
		{
			Assert.AreEqual("Alexandros III o Makedōn", _systemOr.Process("Αλέξανδρος ΙΙΙ ο Μακεδών"));
			Assert.AreEqual("XI, ehndekatos esti parallēlos, kathh ohn an genoito ēh megistē ēhmera ōhrōn isēmerinōn XIV",
				_systemOr.Process("Δ̅Ι̅. ἑνδέκατός ἐστι παράλληλος, καθ’ ὃν ἂν γένοιτο ἡ μεγίστη ἡμέρα ὡρῶν ἰσημερινῶν Δ̅Ι̅Ι̅Ι̅Ι̅"));
		}

		/// <summary>
		/// Aims to test use of the system with <see cref="OutputNumeralType.Arabic"/> and older punctuation &amp; numerals.
		/// </summary>
		[TestMethod]
		public void ProcessOlderArabicTest()
		{
			Assert.AreEqual("Alexandros 3 o Makedōn", _systemOa.Process("Αλέξανδρος Ι̅Ι̅Ι̅ ο Μακεδών"));
			Assert.AreEqual("11, ehndekatos esti parallēlos, kathh ohn an genoito ēh megistē ēhmera ōhrōn isēmerinōn 14",
				_systemOa.Process("Δ̅Ι̅. ἑνδέκατός ἐστι παράλληλος, καθ’ ὃν ἂν γένοιτο ἡ μεγίστη ἡμέρα ὡρῶν ἰσημερινῶν Δ̅Ι̅Ι̅Ι̅Ι̅"));
		}

		/// <summary>
		/// Aims to test use of the system with <see cref="OutputNumeralType.Roman"/> and newer punctuation &amp; numerals.
		/// </summary>
		[TestMethod]
		public void ProcessNewerRomanTest()
		{
			Assert.AreEqual("Alexandros III o Makedōn", _systemNr.Process("Αλέξανδρος Γ' ο Μακεδών"));
			Assert.AreEqual("XI. ehndekatos esti parallēlos, kathh ohn an genoito ēh megistē ēhmera ōhrōn isēmerinōn " +
							"XIVS. apechei dh ouhtos tou isēmerinou moiras XXXVI kae graphetae dia ῾Rhodou. kae estin " +
							"entautha, oihōn oh gnōmōn LX, toioutōn ēh men therinē skia XIIS⁙, ēh de isēmerinē " +
							"XLIIIS∷, ēh de cheimerinē CIII∷",
				_systemNr.Process("ι̅α̅. ἑνδέκατός ἐστι παράλληλος, καθ’ ὃν ἂν γένοιτο ἡ μεγίστη ἡμέρα ὡρῶν ἰσημερινῶν ι̅δ̅∠ʹ. " +
								  "ἀπέχει δ’ οὗτος τοῦ ἰσημερινοῦ μοίρας λ̅ϛ̅ καὶ γράφεται διὰ ῾Ρόδου. καί ἐστιν ἐνταῦθα, οἵων " +
								  "ὁ γνώμων ξ̅, τοιούτων ἡ μὲν θερινὴ σκιὰ ι̅β̅∠ʹγʹιβʹ, ἡ δὲ ἰσημερινὴ μ̅γ̅∠ʹγʹ, ἡ δὲ χειμερινὴ " +
								  "ρ̅γ̅ γʹ"));
		}

		/// <summary>
		/// Aims to test use of the system with <see cref="OutputNumeralType.Arabic"/> and newer punctuation &amp; numerals.
		/// </summary>
		[TestMethod]
		public void ProcessNewerArabicTest()
		{
			Assert.AreEqual("Alexandros 3 o Makedōn", _systemNa.Process("Αλέξανδρος Γ' ο Μακεδών"));
			Assert.AreEqual("11. ehndekatos esti parallēlos, kathh ohn an genoito ēh megistē ēhmera ōhrōn isēmerinōn " +
							"14.5. apechei dh ouhtos tou isēmerinou moiras 36 kae graphetae dia ῾Rhodou. kae estin " +
							"entautha, oihōn oh gnōmōn 60, toioutōn ēh men therinē skia 12.92, ēh de isēmerinē " +
							"43.83, ēh de cheimerinē 103.33",
				_systemNa.Process("ι̅α̅. ἑνδέκατός ἐστι παράλληλος, καθ’ ὃν ἂν γένοιτο ἡ μεγίστη ἡμέρα ὡρῶν ἰσημερινῶν ι̅δ̅∠ʹ. " +
								  "ἀπέχει δ’ οὗτος τοῦ ἰσημερινοῦ μοίρας λ̅ϛ̅ καὶ γράφεται διὰ ῾Ρόδου. καί ἐστιν ἐνταῦθα, οἵων " +
								  "ὁ γνώμων ξ̅, τοιούτων ἡ μὲν θερινὴ σκιὰ ι̅β̅∠ʹγʹιβʹ, ἡ δὲ ἰσημερινὴ μ̅γ̅∠ʹγʹ, ἡ δὲ χειμερινὴ " +
								  "ρ̅γ̅ γʹ"));
		}
	}
}
