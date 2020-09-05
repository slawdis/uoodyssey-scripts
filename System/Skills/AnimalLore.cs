using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.SkillHandlers
{
	public class AnimalLore
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.AnimalLore].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse(Mobile m)
		{
			m.Target = new InternalTarget();

			m.SendLocalizedMessage( 500328 ); // What animal should I look at?

			return TimeSpan.FromSeconds( 1.0 );
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( !from.Alive )
				{
					from.SendLocalizedMessage( 500331 ); // The spirits of the dead are not the province of animal lore.
				}
				else if ( targeted is HenchmanMonster || targeted is HenchmanWizard || targeted is HenchmanFighter || targeted is HenchmanArcher )
				{
					from.SendLocalizedMessage( 500329 ); // That's not an animal!
				}
				else if ( targeted is BaseCreature )
				{
					BaseCreature c = (BaseCreature)targeted;

					SlayerEntry skipTypeA = SlayerGroup.GetEntryByName( SlayerName.SlimyScourge );
					SlayerEntry skipTypeB = SlayerGroup.GetEntryByName( SlayerName.ElementalBan );
					SlayerEntry skipTypeC = SlayerGroup.GetEntryByName( SlayerName.Repond );
					SlayerEntry skipTypeD = SlayerGroup.GetEntryByName( SlayerName.Silver );
					SlayerEntry skipTypeE = SlayerGroup.GetEntryByName( SlayerName.GiantKiller );
					SlayerEntry skipTypeF = SlayerGroup.GetEntryByName( SlayerName.GolemDestruction );

					if ( !c.IsDeadPet )
					{
						if ( !skipTypeA.Slays( c ) && !skipTypeB.Slays( c ) && !skipTypeC.Slays( c ) && !skipTypeD.Slays( c ) && !skipTypeE.Slays( c ) && !skipTypeF.Slays( c ) )
						{
							if ( c.ControlMaster == from )
							{
								from.CloseGump( typeof( AnimalLoreGump ) );
								from.SendGump( new AnimalLoreGump( c, 0 ) );
							}
							else if ( (!c.Controlled || !c.Tamable) && from.Skills[SkillName.AnimalLore].Value < 100.0 )
							{
								from.SendLocalizedMessage( 1049674 ); // At your skill level, you can only lore tamed creatures.
							}
							else if ( !c.Tamable && from.Skills[SkillName.AnimalLore].Value < 110.0 )
							{
								from.SendLocalizedMessage( 1049675 ); // At your skill level, you can only lore tamed or tameable creatures.
							}
							else if ( !from.CheckTargetSkill( SkillName.AnimalLore, c, 0.0, 120.0 ) )
							{
								from.SendLocalizedMessage( 500334 ); // You can't think of anything you know offhand.
							}
							else
							{
								from.CloseGump( typeof( AnimalLoreGump ) );
								from.SendGump( new AnimalLoreGump( c, 0 ) );
							}
						}
						else
						{
							from.SendLocalizedMessage( 500329 ); // That's not an animal!
						}
					}
					else
					{
						from.SendLocalizedMessage( 500331 ); // The spirits of the dead are not the province of animal lore.
					}
				}
				else
				{
					from.SendLocalizedMessage( 500329 ); // That's not an animal!
				}
			}
		}
	}

	public class AnimalLoreGump : Gump
	{
		private static string FormatSkill( BaseCreature c, SkillName name )
		{
			Skill skill = c.Skills[name];

			if ( skill.Base < 10.0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0:F1}</div>", skill.Value );
		}

		private static string FormatCombat( BaseCreature from )
		{
			int c = 0;
			double skills = 0.0;

			double skill1 = from.Skills[SkillName.Archery].Value;
				if ( skill1 > 10.0 ){ c++; skills = skills + skill1; }
			double skill2 = from.Skills[SkillName.Fencing].Value;
				if ( skill2 > 10.0 ){ c++; skills = skills + skill2; }
			double skill3 = from.Skills[SkillName.Macing].Value;
				if ( skill3 > 10.0 ){ c++; skills = skills + skill3; }
			double skill4 = from.Skills[SkillName.Swords].Value;
				if ( skill4 > 10.0 ){ c++; skills = skills + skill4; }
			double skill5 = from.Skills[SkillName.Wrestling].Value;
				if ( skill5 > 10.0 ){ c++; skills = skills + skill5; }

			if ( c == 0 )
			{
				return "<div align=right>---</div>";
			}
			else
			{
				skills = skills / c;
			}

			if ( skills > 125.0 )
				skills = 125.0;

			return String.Format( "<div align=right>{0:F1}</div>", skills );
		}

		private static string FormatAttributes( int cur, int max )
		{
			if ( max == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}/{1}</div>", cur, max );
		}

		private static string FormatStat( int val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}</div>", val );
		}

		private static string FormatDouble( double val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0:F1}</div>", val );
		}

		private static string FormatElement( int val )
		{
			if ( val <= 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}%</div>", val );
		}

		private static string FormatDamage( int min, int max )
		{
			if ( min <= 0 || max <= 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}-{1}</div>", min, max );
		}

		public AnimalLoreGump( BaseCreature c, int source ) : base( 25, 25 )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 152);
			AddImage(300, 0, 152);
			AddImage(600, 0, 152);
			AddImage(0, 300, 152);
			AddImage(300, 300, 152);
			AddImage(900, 0, 152);
			AddImage(600, 300, 152);
			AddImage(900, 300, 152);
			AddImage(2, 2, 129);
			AddImage(898, 298, 129);
			AddImage(600, 2, 129);
			AddImage(898, 2, 129);
			AddImage(598, 298, 129);
			AddImage(300, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(7, 7, 150);
			AddImage(998, 7, 146);
			AddImage(554, 559, 140);
			AddImage(7, 377, 148);
			AddImage(164, 19, 141);
			AddImage(737, 19, 141);
			AddImage(442, 19, 141);
			AddImage(853, 377, 144);
			AddImage(345, 559, 140);

			AddImage(73, 130, 157);
			AddImage(393, 132, 157);
			AddImage(718, 132, 157);
			AddImage(394, 343, 157);
			AddImage(719, 343, 157);
			if ( source == 0 && c.MinTameSkill > 0 )
			{
				AddImage(109, 415, 157);
				AddImage(110, 473, 157);
				AddImage(719, 437, 157);
			}

			AddHtml( 199, 68, 803, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>" + c.Name + " " + c.Title + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 105, 134, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Attributes</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 164, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Hits</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 194, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Stamina</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 224, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Mana</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 254, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Strength</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 284, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Dexterity</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 314, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Intelligence</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 105, 344, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Barding Difficulty</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			if ( source == 0 && c.MinTameSkill > 0 ){ AddHtml( 105, 374, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Taming Needed</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }

			AddHtml( 265, 164, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatAttributes( c.Hits, c.HitsMax ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 265, 194, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatAttributes( c.Stam, c.StamMax ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 265, 224, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatAttributes( c.Mana, c.ManaMax ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 265, 254, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatStat( c.Str ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 265, 284, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatStat( c.Dex ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 265, 314, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatStat( c.Int ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				double bd = Items.BaseInstrument.GetBaseDifficulty( c );
				if ( c.Uncalmable )
					bd = 0;
			AddHtml( 265, 344, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatDouble( bd ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( source == 0 && c.MinTameSkill > 0 ){ AddHtml( 265, 374, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatDouble( c.MinTameSkill ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }

			if ( source == 0 && c.MinTameSkill > 0 )
			{
				AddHtml( 142, 415, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Loyalty Rating</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					string loyalty = "Wild";
					int loyal = 1 + (c.Loyalty / 10);
					switch ( loyal ) 
					{
						case 1: loyalty = "Confused"; break;
						case 2: loyalty = "Extremely Unhappy"; break;
						case 3: loyalty = "Rather Unhappy"; break;
						case 4: loyalty = "Unhappy"; break;
						case 5: loyalty = "Somewhat Content"; break;
						case 6: loyalty = "Content"; break;
						case 7: loyalty = "Happy"; break;
						case 8: loyalty = "Rather Happy"; break;
						case 9: loyalty = "Very Happy"; break;
						case 10: loyalty = "Extremely Happy"; break;
						case 11: loyalty = "Wonderfully Happy"; break;
						case 12: loyalty = "Euphoric"; break;
					}
				AddHtml( 142, 445, 172, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + loyalty + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 142, 473, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Pack Instinct</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					string packInstinct = "None";
					if ( (c.PackInstinct & PackInstinct.Canine) != 0 )
						packInstinct = "Canine";
					else if ( (c.PackInstinct & PackInstinct.Ostard) != 0 )
						packInstinct = "Ostard";
					else if ( (c.PackInstinct & PackInstinct.Feline) != 0 )
						packInstinct = "Feline";
					else if ( (c.PackInstinct & PackInstinct.Arachnid) != 0 )
						packInstinct = "Arachnid";
					else if ( (c.PackInstinct & PackInstinct.Daemon) != 0 )
						packInstinct = "Daemon";
					else if ( (c.PackInstinct & PackInstinct.Bear) != 0 )
						packInstinct = "Bear";
					else if ( (c.PackInstinct & PackInstinct.Equine) != 0 )
						packInstinct = "Equine";
					else if ( (c.PackInstinct & PackInstinct.Bull) != 0 )
						packInstinct = "Bull";
				AddHtml( 142, 503, 172, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + packInstinct + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 425, 136, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Resistance</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 425, 166, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Physical</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 425, 196, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Fire</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 425, 226, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Cold</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 425, 256, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Poison</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 425, 286, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Energy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 585, 166, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.PhysicalResistance ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 585, 196, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.FireResistance ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 585, 226, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.ColdResistance ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 585, 256, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.PoisonResistance ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 585, 286, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.EnergyResistance ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 426, 347, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Damage</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 426, 377, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Physical</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 426, 407, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Fire</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 426, 437, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Cold</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 426, 467, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Poison</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 426, 497, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Energy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 426, 527, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Base Damage</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 586, 377, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.PhysicalDamage ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 586, 407, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.FireDamage ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 586, 437, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.ColdDamage ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 586, 467, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.PoisonDamage ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 586, 497, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatElement( c.EnergyDamage ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 586, 527, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatDamage( c.DamageMin, c.DamageMax ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 750, 136, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Combat Ratings</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 750, 196, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Tactics</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 750, 226, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Resist Spells</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 750, 256, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Anatomy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 750, 286, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Poisoning</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 909, 196, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.Tactics ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 909, 226, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.MagicResist ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 909, 256, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.Anatomy ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 909, 286, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.Poisoning ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( source < 2 || source == 3 )
			{
				AddHtml( 750, 166, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Wrestling</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 909, 166, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.Wrestling ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else
			{
				AddHtml( 750, 166, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Combat Skill</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 909, 166, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatCombat( c ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 751, 317, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Lore & Knowledge</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 751, 347, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Magery</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 751, 377, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Evaulate Intellect</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 751, 407, 144, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Meditation</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 910, 347, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.Magery ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 910, 377, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.EvalInt ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 910, 407, 100, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + FormatSkill( c, SkillName.Meditation ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( source == 0 && c.MinTameSkill > 0 )
			{
				AddHtml( 751, 441, 144, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Preferred Foods</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					string foodPref = "";

					if ( (c.FavoriteFood & FoodType.None) != 0 )
						foodPref = foodPref + "None<br>";
					if ( (c.FavoriteFood & FoodType.FruitsAndVegies) != 0 )
						foodPref = foodPref + "Fruits & Vegetables<br>";
					if ( (c.FavoriteFood & FoodType.GrainsAndHay) != 0 )
						foodPref = foodPref + "Grains & Hay<br>";
					if ( (c.FavoriteFood & FoodType.Fish) != 0 )
						foodPref = foodPref + "Fish<br>";
					if ( (c.FavoriteFood & FoodType.Meat) != 0 )
						foodPref = foodPref + "Meat<br>";
					if ( (c.FavoriteFood & FoodType.Eggs) != 0 )
						foodPref = foodPref + "Eggs<br>";
					if ( (c.FavoriteFood & FoodType.Gold) != 0 )
						foodPref = foodPref + "Gold<br>";
					if ( (c.FavoriteFood & FoodType.Fire) != 0 )
						foodPref = foodPref + "Brimstone & Sulfurous Ash<br>";
					if ( (c.FavoriteFood & FoodType.Gems) != 0 )
						foodPref = foodPref + "Gems<br>";
					if ( (c.FavoriteFood & FoodType.Nox) != 0 )
						foodPref = foodPref + "Swamp Berries, Nox Crystals & Nightshade<br>";
					if ( (c.FavoriteFood & FoodType.Sea) != 0 )
						foodPref = foodPref + "Seaweed & Sea Salt<br>";
					if ( (c.FavoriteFood & FoodType.Moon) != 0 )
						foodPref = foodPref + "Moon Crystals<br>";

				AddHtml( 751, 471, 400, 85, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + foodPref + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			if ( source == 0 )
			{
				AddItem(1068, 137, 2999);
				AddItem(1067, 300, 1793);
				AddItem(1076, 228, 3713);
				AddItem(1072, 432, 5352);
			}
			else if ( source == 1 ) // MM
			{
				AddItem(1068, 137, 12318);
				AddImage(1036, 211, 11416);
			}
			else if ( source == 2 ) // PH
			{
				AddItem(1068, 137, 12319);
				AddImage(1036, 211, 11417);
			}
		}
	}
}