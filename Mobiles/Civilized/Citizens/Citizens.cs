using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Text;
using Server.Commands;
using Server.Commands.Generic;
using System.IO;
using Server.Mobiles;
using System.Threading;
using Server.Gumps;
using Server.Accounting;
using Server.Regions;
using System.Globalization;

namespace Server.Mobiles
{
	public class Citizens : BaseCreature
	{
		public override bool PlayerRangeSensitive { get { return true; } }

		public int CitizenService;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Service { get { return CitizenService; } set { CitizenService = value; InvalidateProperties(); } }

		public int CitizenType;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Type { get { return CitizenType; } set { CitizenType = value; InvalidateProperties(); } }

		public int CitizenCost;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Cost { get { return CitizenCost; } set { CitizenCost = value; InvalidateProperties(); } }

		public string CitizenPhrase;
		[CommandProperty(AccessLevel.Owner)]
		public string Citizen_Phrase { get { return CitizenPhrase; } set { CitizenPhrase = value; InvalidateProperties(); } }

		public string CitizenRumor;
		[CommandProperty(AccessLevel.Owner)]
		public string Citizen_Rumor { get { return CitizenRumor; } set { CitizenRumor = value; InvalidateProperties(); } }

		public override bool InitialInnocent{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		[Constructable]
		public Citizens() : base( AIType.AI_Citizen, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 
				FacialHairItemID = Utility.RandomList( 0, 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0: Server.Misc.IntelligentAction.DressUpWizards( this ); 				CitizenType = 1;	break;
				case 1: Server.Misc.IntelligentAction.DressUpFighters( this, "", false );	CitizenType = 2;	break;
				case 2: Server.Misc.IntelligentAction.DressUpRogues( this, "", false );		CitizenType = 3;	break;
			}

			if ( Backpack != null ){ Backpack.Delete(); }
			Container pack = new Backpack();
			pack.Movable = false;
			AddItem( pack );

			if ( this.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				Item myOneHand = this.FindItemOnLayer( Layer.OneHanded );

				if ( myOneHand.ItemID == 0x26BC || myOneHand.ItemID == 0x26C6 || myOneHand.ItemID == 0x269D || myOneHand.ItemID == 0x269E || myOneHand.ItemID == 0xDF2 || myOneHand.ItemID == 0xDF3 || myOneHand.ItemID == 0xDF4 || myOneHand.ItemID == 0xDF5 )
				{
					Server.Misc.MaterialInfo.ColorMetal( myOneHand, 0 );
				}
				else
				{
					Server.Misc.MorphingTime.ChangeMaterialType( myOneHand, this );
				}
			}

			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				Item myTwoHand = this.FindItemOnLayer( Layer.TwoHanded );

				if ( myTwoHand.ItemID == 0x26BC || myTwoHand.ItemID == 0x26C6 || myTwoHand.ItemID == 0x269D || myTwoHand.ItemID == 0x269E || myTwoHand.ItemID == 0xDF2 || myTwoHand.ItemID == 0xDF3 || myTwoHand.ItemID == 0xDF4 || myTwoHand.ItemID == 0xDF5 )
				{
					Server.Misc.MaterialInfo.ColorMetal( myTwoHand, 0 );
				}
				else
				{
					Server.Misc.MorphingTime.ChangeMaterialType( myTwoHand, this );
				}
			}

			string dungeon = QuestCharacters.SomePlace( "tavern" );
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ dungeon = RandomThings.MadeUpDungeon(); }

			string Clues = QuestCharacters.SomePlace( "tavern" );
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ Clues = RandomThings.MadeUpDungeon(); }

			string city = RandomThings.GetRandomCity();
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ city = RandomThings.MadeUpCity(); }

			string adventurer = Server.Misc.TavernPatrons.Adventurer();

			int relic = Utility.RandomMinMax( 1, 59 );
			string item = Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 );
				item = "the '" + cultInfo.ToTitleCase(item) + "'";

			string locale = Server.Items.SomeRandomNote.GetSpecialItem( relic, 0 );

			if ( Utility.RandomMinMax( 1, 3 ) > 1 )
			{
				item = QuestCharacters.QuestItems( true );
				locale = dungeon;
			}

			string preface = "I found";

			int topic = Utility.RandomMinMax( 0, 40 );
				if ( this is HouseVisitor ){ topic = 100; }

			switch ( topic )
			{
				case 0:	CitizenRumor = "I heard that " + item + " can be obtained in " + locale + "."; break;
				case 1:	CitizenRumor = "I heard something about " + item + " and " + locale + "."; break;
				case 2:	CitizenRumor = "Someone told me that " + locale + " is where you would look for " + item + "."; break;
				case 3:	CitizenRumor = "I heard many tales of adventurers going to " + locale + " and seeing " + item + "."; break;
				case 4:	CitizenRumor = QuestCharacters.RandomWords() + " was in the tavern talking about " + item + " and " + locale + "."; break;
				case 5:	CitizenRumor = "I was talking with the local " + RandomThings.GetRandomJob() + ", and they mentioned " + item + " and " + locale + "."; break;
				case 6:	CitizenRumor = "I met with " + QuestCharacters.RandomWords() + " and they told me to bring back " + item + " from " + locale + "."; break;
				case 7:	CitizenRumor = "I heard that " + item + " can be found in " + locale + "."; break;
				case 8:	CitizenRumor = "Someone from " + RandomThings.GetRandomCity() + " died in " + locale + " searching for " + item + "."; break;
				case 9:	CitizenRumor = Server.Misc.TavernPatrons.GetRareLocation( this, true, false );		break;
			}

			switch( Utility.RandomMinMax( 0, 13 ) )
			{
				case 0: preface = "I found"; 											break;
				case 1: preface = "I heard rumours about"; 								break;
				case 2: preface = "I heard a story about"; 								break;
				case 3: preface = "I overheard someone tell of"; 						break;
				case 4: preface = "Some " + adventurer + " found"; 						break;
				case 5: preface = "Some " + adventurer + " heard rumours about"; 		break;
				case 6: preface = "Some " + adventurer + " heard a story about"; 		break;
				case 7: preface = "Some " + adventurer + " overheard another tell of"; 	break;
				case 8: preface = "Some " + adventurer + " is spreading rumors about"; 	break;
				case 9: preface = "Some " + adventurer + " is telling tales about"; 	break;
				case 10: preface = "We found"; 											break;
				case 11: preface = "We heard rumours about"; 							break;
				case 12: preface = "We heard a story about"; 							break;
				case 13: preface = "We overheard someone tell of"; 						break;
			}

			if ( CitizenRumor == null ){ CitizenRumor = preface + " " + Server.Misc.TavernPatrons.CommonTalk( "", city, dungeon, this, adventurer, true ) + "."; }

			CitizenCost = 0;
			CitizenService = 0;

			if ( this is HouseVisitor )
			{
				CitizenService = 0;
			}
			else if ( CitizenType == 1 )
			{
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ CitizenService = Utility.RandomMinMax( 1, 8 ); }
			}
			else
			{
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 1;		break;
					case 2: CitizenService = 2;		break;
					case 3: CitizenService = 3;		break;
					case 4: CitizenService = 4;		break;
					case 5: CitizenService = 5;		break;
				}
			}

			string phrase = "";

			int initPhrase = Utility.RandomMinMax( 0, 6 );
				if ( this is TavernPatronNorth || this is TavernPatronSouth || this is TavernPatronEast || this is TavernPatronWest ){ initPhrase = Utility.RandomMinMax( 0, 4 ); }

			switch ( initPhrase )
			{
				case 0:	phrase = "Greetings, Z~Z~Z~Z~Z."; break;
				case 1:	phrase = "Hail, Z~Z~Z~Z~Z."; break;
				case 2:	phrase = "Good day to you, Z~Z~Z~Z~Z."; break;
				case 3:	phrase = "Hello, Z~Z~Z~Z~Z."; break;
				case 4:	phrase = "We are just here to rest after exploring " + dungeon + "."; break;
				case 5:	phrase = "This is the first time I have been to Y~Y~Y~Y~Y."; break;
				case 6:	phrase = "Hail, Z~Z~Z~Z~Z. Welcome to Y~Y~Y~Y~Y."; break;
			}

			if ( CitizenService == 1 )
			{
				if ( CitizenType == 1 ){ CitizenPhrase = phrase + " I can recharge any wands you may have with you, but only up to a certain amount. If you want my help, then simply hand me your wand so I can perform the ritual needed."; }
				else if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled blacksmith, so if you need any metal armor repaired I can do it for you. Just hand me the armor and I will see what I can do."; }
				else { CitizenPhrase = phrase + " If you need a chest or box unlocked, I can help you with that. Just hand me the container and I will see what I can do. I promise to give it back."; }
			}
			else if ( CitizenService == 2 )
			{
				if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled blacksmith, so if you need any metal weapons repaired I can do it for you. Just hand me the weapon and I will see what I can do."; }
				else { CitizenPhrase = phrase + " I am quite a skilled leather worker, so if you need any leather item repaired I can do it for you. Just hand me the item and I will see what I can do."; }
			}
			else if ( CitizenService == 3 )
			{
				if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden weapons repaired I can do it for you. Just hand me the weapon and I will see what I can do."; }
				else { CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden weapons repaired I can do it for you. Just hand me the weapon and I will see what I can do."; }
			}
			else if ( CitizenService == 4 )
			{
				if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden armor repaired I can do it for you. Just hand me the armor and I will see what I can do."; }
				else { CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden armor repaired I can do it for you. Just hand me the armor and I will see what I can do."; }
			}
			else if ( CitizenService == 5 )
			{
				string aty1 = "a magic item"; if (Utility.RandomBool() ){ aty1 = "an enchanted item"; } else if (Utility.RandomBool() ){ aty1 = "a special item"; }
				string aty2 = "found"; if (Utility.RandomBool() ){ aty2 = "discovered"; }
				string aty3 = "willing to part with"; if (Utility.RandomBool() ){ aty3 = "willing to trade"; } else if (Utility.RandomBool() ){ aty3 = "willing to sell"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " while exploring " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I won " + aty1 + " from a card game in " + city + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " on the remains of some " + adventurer + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " from a chest in " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " on a beast I killed in " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " on some " + adventurer + " in " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the item if you wish. If you want to trade, then hand me the gold and I will give you the item.";
			}

			if ( CitizenType == 1 && CitizenService == 2 ){ PackItem( new reagents_magic_jar1() ); CitizenCost = Utility.RandomMinMax( 70, 150 )*10; }
			else if ( CitizenType == 1 && CitizenService == 3 ){ PackItem( new reagents_magic_jar2() ); CitizenCost = Utility.RandomMinMax( 50, 90 )*10; }
			else if ( CitizenType == 1 && CitizenService == 4 ){ PackItem( new reagents_magic_jar3() ); CitizenCost = Utility.RandomMinMax( 180, 300 )*10; }
			else if ( CitizenType == 1 && CitizenService == 6 )
			{
				if ( Utility.RandomBool() )
				{
					if ( Utility.RandomBool() )
					{
						Spellbook tome = new MySpellbook();
						CitizenCost = Utility.RandomMinMax( ((tome.SpellCount+1)*500), ((tome.SpellCount+1)*800) );
						PackItem( tome ); 
					}
					else
					{
						Spellbook tome = new MyNecromancerSpellbook();
						CitizenCost = Utility.RandomMinMax( ((tome.SpellCount+1)*800), ((tome.SpellCount+1)*1000) );
						PackItem( tome ); 
					}
				}
				else
				{
					PackItem( new Runebook() ); CitizenCost = Utility.RandomMinMax( 150, 230 )*10;
				}
			}
			else if ( CitizenType == 1 && CitizenService == 7 )
			{
				Item scroll = Server.Items.DungeonLoot.RandomHighLevelScroll();

				int mult = 1;

				if ( scroll is PainSpikeScroll || scroll is EvilOmenScroll || scroll is WraithFormScroll || scroll is ArchCureScroll || scroll is ArchProtectionScroll || scroll is CurseScroll || scroll is FireFieldScroll || scroll is GreaterHealScroll || scroll is LightningScroll || scroll is ManaDrainScroll || scroll is RecallScroll ){ mult = 10; }

				else if ( scroll is MindRotScroll || scroll is SummonFamiliarScroll || scroll is HorrificBeastScroll || scroll is AnimateDeadScroll || scroll is BladeSpiritsScroll || scroll is DispelFieldScroll || scroll is IncognitoScroll || scroll is MagicReflectScroll || scroll is MindBlastScroll || scroll is ParalyzeScroll || scroll is PoisonFieldScroll || scroll is SummonCreatureScroll ){ mult = 20; }

				else if ( scroll is DispelScroll || scroll is EnergyBoltScroll || scroll is ExplosionScroll || scroll is InvisibilityScroll || scroll is MarkScroll || scroll is MassCurseScroll || scroll is ParalyzeFieldScroll || scroll is RevealScroll ){ mult = 40; }

				else if ( scroll is PoisonStrikeScroll || scroll is WitherScroll || scroll is StrangleScroll || scroll is LichFormScroll || scroll is ChainLightningScroll || scroll is EnergyFieldScroll || scroll is FlamestrikeScroll || scroll is GateTravelScroll || scroll is ManaVampireScroll || scroll is MassDispelScroll || scroll is MeteorSwarmScroll || scroll is PolymorphScroll ){ mult = 60; }

				else if ( scroll is ExorcismScroll || scroll is VampiricEmbraceScroll || scroll is VengefulSpiritScroll || scroll is EarthquakeScroll || scroll is EnergyVortexScroll || scroll is ResurrectionScroll || scroll is SummonAirElementalScroll || scroll is SummonDaemonScroll || scroll is SummonEarthElementalScroll || scroll is SummonFireElementalScroll || scroll is  SummonWaterElementalScroll ){ mult = 80; }

				PackItem( scroll );
				CitizenCost = Utility.RandomMinMax( 8, 12 )*mult;
			}
			else if ( CitizenType == 1 && CitizenService == 8 )
			{
				Item wand = Loot.RandomWand();
				Server.Misc.MaterialInfo.ColorMetal( wand, 0 );
				string wandOwner = Server.LootPackEntry.MagicWandOwner() + " ";
				wand.Name = wandOwner + wand.Name;
				BaseWeapon bw = (BaseWeapon)wand;
				if ( bw.IntRequirement == 10 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*5; }
				else if ( bw.IntRequirement == 15 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*10; }
				else if ( bw.IntRequirement == 20 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*15; }
				else if ( bw.IntRequirement == 25 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*20; }
				else if ( bw.IntRequirement == 30 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*25; }
				else if ( bw.IntRequirement == 35 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*30; }
				else if ( bw.IntRequirement == 40 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*35; }
				else if ( bw.IntRequirement == 45 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*40; }
				PackItem( wand );
			}
			else if ( CitizenService == 5 )
			{
				int val = Utility.RandomMinMax( 25, 100 );
				int props = 5 + Utility.RandomMinMax( 0, 5 );
				int luck = Utility.RandomMinMax( 0, 200 );
				int chance = Utility.RandomMinMax( 1, 100 );

				if ( chance < 80 )
				{
					Item arty = Loot.RandomArmorOrShieldOrWeaponOrJewelry();
					if ( arty is BaseWeapon ){ BaseRunicTool.ApplyAttributesTo( (BaseWeapon)arty, false, luck, props, val, val ); }
					else if ( arty is BaseArmor ){ BaseRunicTool.ApplyAttributesTo( (BaseArmor)arty, false, luck, props, val, val ); }
					else if ( arty is BaseJewel ){ BaseRunicTool.ApplyAttributesTo( (BaseJewel)arty, false, luck, props, val, val ); }
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*20;
				}
				else if ( chance < 90 )
				{
					Item arty = Loot.RandomClothing();
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					BaseRunicTool.ApplyAttributesTo( (BaseClothing)arty, false, luck, props, val, val );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*20;
				}
				else if ( chance < 95 )
				{
					Item arty = Loot.RandomInstrument();
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					SlayerName slayer = BaseRunicTool.GetRandomSlayer();
					BaseInstrument instr = (BaseInstrument)arty;

					int cHue = 0;
					int cUse = 0;

					switch ( instr.Resource )
					{
						case CraftResource.AshTree: cHue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); cUse = 20; break;
						case CraftResource.CherryTree: cHue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); cUse = 40; break;
						case CraftResource.EbonyTree: cHue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); cUse = 60; break;
						case CraftResource.GoldenOakTree: cHue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); cUse = 80; break;
						case CraftResource.HickoryTree: cHue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); cUse = 100; break;
						case CraftResource.MahoganyTree: cHue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); cUse = 120; break;
						case CraftResource.DriftwoodTree: cHue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); cUse = 120; break;
						case CraftResource.OakTree: cHue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); cUse = 140; break;
						case CraftResource.PineTree: cHue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); cUse = 160; break;
						case CraftResource.GhostTree: cHue = MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ); cUse = 160; break;
						case CraftResource.RosewoodTree: cHue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); cUse = 180; break;
						case CraftResource.WalnutTree: cHue = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); cUse = 200; break;
						case CraftResource.PetrifiedTree: cHue = MaterialInfo.GetMaterialColor( "petrified", "", 0 ); cUse = 250; break;
						case CraftResource.ElvenTree: cHue = MaterialInfo.GetMaterialColor( "elven", "", 0 ); cUse = 400; break;
					}

					instr.UsesRemaining = instr.UsesRemaining + cUse;
					if ( cHue > 0 ){ arty.Hue = cHue; }
					else if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ arty.Hue = Utility.RandomColor(0); }
					instr.Quality = InstrumentQuality.Regular;
					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Quality = InstrumentQuality.Exceptional; }
					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Slayer = slayer; }

					BaseRunicTool.ApplyAttributesTo( (BaseInstrument)arty, false, luck, props, val, val );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*20;
				}
				else
				{
					Item arty = Loot.RandomArty();
					arty.Movable = false;
					PackItem( arty );
					CitizenCost = Utility.RandomMinMax( 250, 750 )*10;
				}
			}

			if ( CitizenType == 1 && ( CitizenService == 2 || CitizenService == 3 || CitizenService == 4 || CitizenService == 6 || CitizenService == 7 || CitizenService == 8 ) )
			{
				string aty1 = "a jar of wizard reagents";
					if ( CitizenService == 3 ){ aty1 = "a jar of necromancer reagents"; }
					else if ( CitizenService == 4 ){ aty1 = "a jar of alchemical reagents"; }
					else if ( CitizenService == 6 ){ aty1 = "a book"; }
					else if ( CitizenService == 7 ){ aty1 = "a scroll"; }
					else if ( CitizenService == 8 ){ aty1 = "a wand"; }

				string aty3 = "willing to part with"; if (Utility.RandomBool() ){ aty3 = "willing to trade"; } else if (Utility.RandomBool() ){ aty3 = "willing to sell"; }

				CitizenPhrase = phrase + " I have " + aty1 + " that I am " + aty3 + " for G~G~G~G~G gold.";
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the item if you wish. If you want to trade, then hand me the gold and I will give you the item.";
			}

			string holding = "";
			List<Item> belongings = new List<Item>();
			foreach( Item i in this.Backpack.Items )
			{
				i.Movable = false;
				holding = i.Name;
				if ( i.Name != null && i.Name != "" ){} else { holding = MorphingItem.AddSpacesToSentence( (i.GetType()).Name ); }
				if ( Server.Misc.MaterialInfo.GetMaterialName( i ) != "" ){ holding = Server.Misc.MaterialInfo.GetMaterialName( i ) + " " + i.Name; }
				holding = cultInfo.ToTitleCase(holding);
			}

			if ( holding != "" ){ CitizenPhrase = CitizenPhrase + "<br><br>" + holding; } 
			else if ( CitizenService == 5 ){ CitizenPhrase = null; }
			else if ( ( CitizenService >= 2 && CitizenService <= 8 ) && CitizenType == 1 ){ CitizenPhrase = null; }

			CantWalk = true;
			Title = TavernPatrons.GetTitle();
			Hue = Utility.RandomSkinColor();
			Utility.AssignRandomHair( this );
			SpeechHue = Utility.RandomTalkHue();
			NameHue = Utility.RandomOrangeHue();
			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 300, 400 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );
			SetSkill( SkillName.Wrestling, 100 );

			Fame = 0;
			Karma = 0;
			VirtualArmor = 30;

			int HairColor = Utility.RandomHairHue();
			HairHue = HairColor;
			FacialHairHue = HairColor;

			if ( this is HouseVisitor && Backpack != null ){ Backpack.Delete(); }
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !(this is HouseVisitor) )
			{
				Region reg = Region.Find( this.Location, this.Map );
				if ( DateTime.Now >= m_NextTalk && InRange( m, 30 ) )
				{
					if ( Utility.RandomBool() ){ TavernPatrons.GetChatter( this ); }
					Server.Misc.MaterialInfo.IsNoHairHat( 0, this );
					m_NextTalk = (DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 45 ) ));
				}
			}
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				Citizens citizen = (Citizens)m_Giver;

				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					string speak = "";

					if ( m_Giver.Fame == 0 && m_Mobile.Backpack.FindItemByType( typeof ( MuseumBook ) ) != null && !(m_Giver is HouseVisitor) )
					{
						speak = MuseumBook.TellRumor( m_Mobile, m_Giver );
					}
					if ( speak == "" && m_Giver.Fame == 0 && m_Mobile.Backpack.FindItemByType( typeof ( QuestTome ) ) != null && !(m_Giver is HouseVisitor) )
					{
						speak = QuestTome.TellRumor( m_Mobile, m_Giver );
					}

					if ( speak != "" )
					{
						m_Mobile.PlaySound( 0x5B6 );
						m_Giver.Say( speak );
					}
					else if ( citizen.CitizenService == 0 )
					{
						speak = citizen.CitizenRumor;
						if ( speak.Contains("Z~Z~Z~Z~Z") ){ speak = speak.Replace("Z~Z~Z~Z~Z", m_Mobile.Name); }
						if ( speak.Contains("Y~Y~Y~Y~Y") ){ speak = speak.Replace("Y~Y~Y~Y~Y", m_Mobile.Region.Name); }
						m_Giver.Say( speak );
					}
					else
					{
						mobile.CloseGump( typeof( CitizenGump ) );
						mobile.SendGump(new CitizenGump( m_Giver, m_Mobile ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public override bool OnBeforeDeath()
		{
			Say("In Vas Mani");
			this.Hits = this.HitsMax;
			this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
			this.PlaySound( 0x202 );
			return false;
		}

		public override bool IsEnemy( Mobile m )
		{
			return false;
		}

		public static void PopulateCities()
		{
			ArrayList wanderers = new ArrayList();
			foreach ( Mobile wanderer in World.Mobiles.Values )
			{
				if ( wanderer is Citizens && !( wanderer is HouseVisitor || wanderer is AdventurerWest || wanderer is AdventurerSouth || wanderer is AdventurerNorth || wanderer is AdventurerEast || wanderer is TavernPatronWest || wanderer is TavernPatronSouth || wanderer is TavernPatronNorth || wanderer is TavernPatronEast ) )
				{
					wanderers.Add( wanderer );
				}
			}
			for ( int i = 0; i < wanderers.Count; ++i )
			{
				Mobile person = ( Mobile )wanderers[ i ];
				Effects.SendLocationParticles( EffectItem.Create( person.Location, person.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				person.PlaySound( 0x1FE );
				person.Delete();
			}

			ArrayList meetingSpots = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is MeetingSpots )
				{
					meetingSpots.Add( item );
				}
			}
			for ( int i = 0; i < meetingSpots.Count; ++i )
			{
				Item spot = ( Item )meetingSpots[ i ];
				if ( PeopleMeetingHere() ){ CreateCitizenss( spot ); }
			}
			CreateDragonRiders();
		}

		public static void CreateCitizenss ( Item spot )
		{
			Region reg = Region.Find( spot.Location, spot.Map );

			int total = 0;
			int mod = 2;

			bool mount = false;

			if (!( reg.IsPartOf( "Anchor Rock Docks" ) || reg.IsPartOf( "Kraken Reef Docks" ) || reg.IsPartOf( "Savage Sea Docks" ) || reg.IsPartOf( "Serpent Sail Docks" ) || reg.IsPartOf( "the Forgotten Lighthouse" ) ))
			{
				if ( Utility.RandomBool() ){ mount = true; mod = 3; }
			}

			Point3D cit1 = new Point3D( ( spot.X-mod ), ( spot.Y ),   	spot.Z );	Direction dir1 = Direction.East;
			Point3D cit2 = new Point3D( ( spot.X   ), ( spot.Y-mod ),   spot.Z );	Direction dir2 = Direction.South;
			Point3D cit3 = new Point3D( ( spot.X+mod ), ( spot.Y ),   	spot.Z );	Direction dir3 = Direction.West;
			Point3D cit4 = new Point3D( ( spot.X   ), ( spot.Y+mod ),	spot.Z );	Direction dir4 = Direction.North;

			Mobile citizen = null;

			if ( Utility.RandomBool() )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit1, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir1;
						((BaseCreature)citizen).ControlSlots = 2;
						Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit2, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir2;
						((BaseCreature)citizen).ControlSlots = 3;
						Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
			if ( Utility.RandomMinMax( 1, 4 ) == 1 || total == 0 )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit3, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir3;
						((BaseCreature)citizen).ControlSlots = 4;
						Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
			if ( Utility.RandomMinMax( 1, 4 ) == 1 || total < 2 )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit4, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir4;
						((BaseCreature)citizen).ControlSlots = 5;
						Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
		}

		public static void CreateDragonRiders()
		{
			if ( Server.Misc.MyServerSettings.ClientVersion() )
			{
				Point3D loc; Map map; Direction direction;

				if ( Utility.RandomBool() ){ loc = new Point3D( 3022, 969, 70 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Britain
				if ( Utility.RandomBool() ){ loc = new Point3D( 2985, 1042, 45 ); map = Map.Sosaria; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Britain
				if ( Utility.RandomBool() ){ loc = new Point3D( 6728, 1797, 30 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Kuldara
				if ( Utility.RandomBool() ){ loc = new Point3D( 6752, 1665, 80 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Kuldara
				if ( Utility.RandomBool() ){ loc = new Point3D( 355, 1071, 65 ); map = Map.IslesDread; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 385, 1044, 99 ); map = Map.IslesDread; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 392, 1096, 59 ); map = Map.IslesDread; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 734, 367, 40 ); map = Map.Underworld; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Fort of Tenebrae
				if ( Utility.RandomBool() ){ loc = new Point3D( 1441, 3779, 30 ); map = Map.Sosaria; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Town of Renika
				if ( Utility.RandomBool() ){ loc = new Point3D( 1395, 3668, 115 ); map = Map.Sosaria; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // the Island of Umber Veil
				if ( Utility.RandomBool() ){ loc = new Point3D( 2278, 1667, 30 ); map = Map.SerpentIsland; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Furnace
				if ( Utility.RandomBool() ){ loc = new Point3D( 2176, 1680, 75 ); map = Map.SerpentIsland; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Furnace
				if ( Utility.RandomBool() ){ loc = new Point3D( 291, 1736, 60 ); map = Map.SavagedEmpire; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Barako
				if ( Utility.RandomBool() ){ loc = new Point3D( 282, 1631, 110 ); map = Map.SavagedEmpire; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the Savaged Empire
				if ( Utility.RandomBool() ){ loc = new Point3D( 786, 875, 55 ); map = Map.SavagedEmpire; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Kurak
				if ( Utility.RandomBool() ){ loc = new Point3D( 821, 982, 80 ); map = Map.SavagedEmpire; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Kurak
				if ( Utility.RandomBool() ){ loc = new Point3D( 2687, 3165, 60 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port of Dusk
				if ( Utility.RandomBool() ){ loc = new Point3D( 2956, 1248, 70 ); map = Map.Lodor; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 2970, 1319, 45 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 2902, 1399, 55 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3737, 397, 44 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Town of Glacial Hills
				if ( Utility.RandomBool() ){ loc = new Point3D( 3660, 470, 44 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Town of Glacial Hills
				if ( Utility.RandomBool() ){ loc = new Point3D( 4215, 2993, 60 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // Greensky Village
				if ( Utility.RandomBool() ){ loc = new Point3D( 2827, 2258, 35 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Islegem
				if ( Utility.RandomBool() ){ loc = new Point3D( 4842, 3266, 50 ); map = Map.Lodor; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 4815, 3112, 73 ); map = Map.Lodor; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 4712, 3194, 84 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 1809, 2224, 70 ); map = Map.Lodor; direction = Direction.Right; CreateDragonRider ( loc, map, direction ); } // the City of Lodoria
				if ( Utility.RandomBool() ){ loc = new Point3D( 1942, 2185, 57 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Lodoria
				if ( Utility.RandomBool() ){ loc = new Point3D( 2084, 2195, 32 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Lodoria
				if ( Utility.RandomBool() ){ loc = new Point3D( 841, 2019, 55 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Portshine
				if ( Utility.RandomBool() ){ loc = new Point3D( 6763, 3649, 122 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Ravendark
				if ( Utility.RandomBool() ){ loc = new Point3D( 6759, 3756, 76 ); map = Map.Lodor; direction = Direction.Right; CreateDragonRider ( loc, map, direction ); } // the Village of Ravendark
				if ( Utility.RandomBool() ){ loc = new Point3D( 4232, 1454, 48 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 4293, 1492, 45 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 4172, 1489, 45 ); map = Map.Lodor; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 2381, 3155, 28 ); map = Map.Lodor; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Port of Starguide
				if ( Utility.RandomBool() ){ loc = new Point3D( 2302, 3154, 52 ); map = Map.Lodor; direction = Direction.West; CreateDragonRider ( loc, map, direction ); } // the Port of Starguide
				if ( Utility.RandomBool() ){ loc = new Point3D( 876, 904, 30 ); map = Map.Lodor; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // the Village of Whisper
				if ( Utility.RandomBool() ){ loc = new Point3D( 1101, 321, 66 ); map = Map.SavagedEmpire; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Savage Sea Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 952, 1801, 50 ); map = Map.SerpentIsland; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Serpent Sail Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 315, 1407, 17 ); map = Map.Sosaria; direction = Direction.Left; CreateDragonRider ( loc, map, direction ); } // Anchor Rock Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 415, 1292, 67 ); map = Map.Sosaria; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // Anchor Rock Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 5932, 2868, 45 ); map = Map.Sosaria; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Lunar City of Dawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 3705, 1486, 55 ); map = Map.Sosaria; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Death Gulch
				if ( Utility.RandomBool() ){ loc = new Point3D( 1608, 1507, 48 ); map = Map.Sosaria; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // The Town of Devil Guard
				if ( Utility.RandomBool() ){ loc = new Point3D( 2084, 258, 60 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Fawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 2168, 305, 60 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Fawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 4781, 1185, 50 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Glacial Coast Village
				if ( Utility.RandomBool() ){ loc = new Point3D( 869, 2068, 40 ); map = Map.Sosaria; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the Village of Grey
				if ( Utility.RandomBool() ){ loc = new Point3D( 3070, 2615, 60 ); map = Map.Sosaria; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3180, 2613, 66 ); map = Map.Sosaria; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3322, 2638, 70 ); map = Map.Sosaria; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 838, 692, 70 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Town of Moon
				if ( Utility.RandomBool() ){ loc = new Point3D( 4565, 1253, 82 ); map = Map.Sosaria; direction = Direction.Left; CreateDragonRider ( loc, map, direction ); } // the Town of Mountain Crest
				if ( Utility.RandomBool() ){ loc = new Point3D( 1823, 758, 70 ); map = Map.Sosaria; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // the Land of Sosaria
				if ( Utility.RandomBool() ){ loc = new Point3D( 7089, 610, 100 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port
				if ( Utility.RandomBool() ){ loc = new Point3D( 7025, 680, 120 ); map = Map.Sosaria; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port
			}
		}

		public static void CreateDragonRider ( Point3D loc, Map map, Direction direction )
		{
			DragonRider citizen = new DragonRider();
			citizen.MoveToWorld( loc, map );
			MountCitizens ( citizen, true );
			citizen.Direction = direction;
			((BaseCreature)citizen).ControlSlots = 2;
			Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			citizen.PlaySound( 0x1FE );
		}

		public static void MountCitizens ( Mobile m, bool includeDragyns )
		{
			if ( m is DragonRider )
			{
				BaseMount dragon = new RidingDragon(); dragon.Body = Utility.RandomList( 59, 61 ); dragon.Blessed = true; dragon.Location = m.Location; dragon.OnAfterSpawn(); Server.Mobiles.BaseMount.Ride( dragon, m );
			}
			else if ( m.Map == Map.Sosaria && m.X >= 2954 && m.Y >= 893 && m.X <= 3026 && m.Y <= 967 ){ /* DO NOTHING IN CASTLE BRITISH */ }
			else if ( m.Map == Map.Lodor && m.X >= 1759 && m.Y >= 2195 && m.X <= 1821 && m.Y <= 2241 ){ /* DO NOTHING IN CASTLE OF KNOWLEDGE */ }
			else if ( m.Map == Map.SavagedEmpire && m.X >= 309 && m.Y >= 1738 && m.X <= 323 && m.Y <= 1751 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.SavagedEmpire && m.X >= 284 && m.Y >= 1642 && m.X <= 298 && m.Y <= 1655 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.SavagedEmpire && m.X >= 785 && m.Y >= 896 && m.X <= 805 && m.Y <= 879 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.SavagedEmpire && m.X >= 706 && m.Y >= 953 && m.X <= 726 && m.Y <= 963 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.IslesDread && m.X >= 364 && m.Y >= 1027 && m.X <= 415 && m.Y <= 1057 ){ /* DO NOTHING IN THE CIMMERIAN CASTLE */ }
			else if ( m.Region.IsPartOf( "Kraken Reef Docks" ) || m.Region.IsPartOf( "Anchor Rock Docks" ) || m.Region.IsPartOf( "Serpent Sail Docks" ) || m.Region.IsPartOf( "Savage Sea Docks" ) || m.Region.IsPartOf( "the Forgotten Lighthouse" ) ){ /* DO NOTHING ON THE PORTS */ }
			else if ( Server.Mobiles.AnimalTrainer.IsNoMountRegion( m, m.Region ) && Server.Misc.MyServerSettings.NoMountsInCertainRegions() ){ /* DO NOTHING IN NO MOUNT REGIONS */ }
			else if ( Server.Misc.MyServerSettings.NoMountBuilding() && Server.Misc.Worlds.InBuilding( m ) ){ /* DO NOTHING IN NO MOUNT REGIONS */ }
			else if ( !(m is HouseVisitor ) )
			{
				BaseMount mount = new Horse();

				int roll = 0;

				switch ( Utility.Random( 30 ) )
				{
					case 0: roll = Utility.RandomMinMax( 1, 9 ); if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = Utility.RandomMinMax( 1, 10 ); }
						switch ( roll )
						{
							case 1: mount = new CaveBearRiding();				break;
							case 2: mount = new DireBear();						break;
							case 3: mount = new ElderBlackBearRiding();			break;
							case 4: mount = new ElderBrownBearRiding();			break;
							case 5: mount = new ElderPolarBearRiding();			break;
							case 6: mount = new GreatBear();					break;
							case 7: mount = new GrizzlyBearRiding();			break;
							case 8: mount = new KodiakBear();					break;
							case 9: mount = new SabretoothBearRiding();			break;
							case 10: mount = new PandaRiding();					break;
						}
						break;
					case 1: roll = Utility.RandomMinMax( 1, 3 ); if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = Utility.RandomMinMax( 1, 4 ); }
						switch ( roll )
						{
							case 1: mount = new BullradonRiding();				break;
							case 2: mount = new GorceratopsRiding();			break;
							case 3: mount = new GorgonRiding();					break;
							case 4: mount = new BasiliskRiding();				break;
						}
						break;
					case 2:
						roll = Utility.RandomMinMax( 1, 4 );
						if ( Server.Misc.MorphingTime.CheckNecro( m ) ){ roll = Utility.RandomMinMax( 3, 4 ); }
						switch ( roll )
						{
							case 1: mount = new WhiteWolf();		break;
							case 2: mount = new WinterWolf();		break;
							case 3: mount = new BlackWolf();		break;
							case 4: mount = new DemonDog();			Server.Misc.MorphingTime.TurnToNecromancer( m );	break;
						}
						break;
					case 3: roll = Utility.RandomMinMax( 1, 2 ); if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = Utility.RandomMinMax( 1, 6 ); }
						switch ( roll )
						{
							case 1: mount = new LionRiding();				break;
							case 2: mount = new SnowLion();					break;
							case 3: mount = new TigerRiding();				break;
							case 4: mount = new WhiteTigerRiding();			break;
							case 5: mount = new PredatorHellCatRiding();	break;
							case 6: mount = new SabretoothTigerRiding();	break;
						}
						break;
					case 4:
						switch ( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: mount = new DesertOstard();		break;
							case 2: mount = new ForestOstard();		break;
							case 3: mount = new FrenziedOstard();	break;
							case 4: mount = new SnowOstard();		break;
						}
						break;
					case 5: roll = Utility.RandomMinMax( 1, 4 ); if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = Utility.RandomMinMax( 1, 5 ); }
						switch ( roll )
						{
							case 1: mount = new GiantHawk();		break;
							case 2: mount = new GiantRaven();		break;
							case 3: mount = new Roc();				break;
							case 4: mount = new Phoenix();			break;
							case 5: mount = new AxeBeakRiding();	break;
						}
						break;
					case 6:
						switch ( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: mount = new SwampDrakeRiding();																															break;
							case 2: mount = new Wyverns();																																	break;
							case 3: mount = new Teradactyl();																																break;
							case 4: mount = new GemDragon();		if ( Server.Misc.MyServerSettings.ClientVersion() ){ mount.Hue = 0; mount.ItemID = Utility.RandomMinMax( 595, 596 ); }		break;
						}
						break;
					case 7:
						switch ( Utility.RandomMinMax( 1, 6 ) )
						{
							case 1: mount = new Beetle();					break;
							case 2: mount = new FireBeetle();				break;
							case 3: mount = new GlowBeetleRiding();			break;
							case 4: mount = new PoisonBeetleRiding();		break;
							case 5: mount = new TigerBeetleRiding();		break;
							case 6: mount = new WaterBeetleRiding();		break;
						}
						break;
					case 8: roll = Utility.RandomMinMax( 1, 2 ); if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = Utility.RandomMinMax( 1, 5 ); }
						switch ( roll )
						{
							case 1: mount = new RaptorRiding();													break;
							case 2: mount = new RavenousRiding();												break;
							case 3: mount = new RaptorRiding();			mount.Body = 116;	mount.ItemID = 116;	break;
							case 4: mount = new RaptorRiding();			mount.Body = 117;	mount.ItemID = 117;	break;
							case 5: mount = new RaptorRiding();			mount.Body = 219;	mount.ItemID = 219;	break;
						}
						break;
					case 9:
						roll = 1; if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = 0; } if ( !Server.Misc.MyServerSettings.AllowZebras() ){ roll = 1; }
						roll = Utility.RandomMinMax( roll, 8 );
						if ( Server.Misc.MorphingTime.CheckNecro( m ) ){ roll = Utility.RandomMinMax( 3, 8 ); }
						switch ( roll )
						{
							case 0: mount = new ZebraRiding();					break;
							case 1: mount = new Unicorn();						break;
							case 2: mount = new IceSteed();						break;
							case 3: mount = new FireSteed();					break;
							case 4: mount = new Nightmare();					break;
							case 5: mount = new AncientNightmareRiding();		break;
							case 6: mount = new DarkUnicornRiding();			Server.Misc.MorphingTime.TurnToNecromancer( m );	break;
							case 7: mount = new HellSteed();					Server.Misc.MorphingTime.TurnToNecromancer( m );	break;
							case 8: mount = new Dreadhorn();					break;
						}
						break;
					case 10: roll = Utility.RandomMinMax( 1, 5 ); if ( Server.Misc.MyServerSettings.ClientVersion() ){ roll = Utility.RandomMinMax( 1, 6 ); }
						switch ( roll )
						{
							case 1: mount = new Ramadon();				break;
							case 2: mount = new RidableLlama();			break;
							case 3: mount = new GriffonRiding();		break;
							case 4: mount = new HippogriffRiding();		break;
							case 5: mount = new Kirin();				break;
							case 6: mount = new ManticoreRiding();		break;
						}
						break;
				}

				if ( mount is Horse && Utility.RandomMinMax(1,50) == 1 && Server.Misc.MyServerSettings.ClientVersion() )
				{
					mount.Body = 587;
					mount.ItemID = 587;
					switch ( Utility.RandomMinMax( 1, 16 ) )
					{
						case 1: mount.Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 );	break;
						case 2: mount.Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 );	break;
						case 3: mount.Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 );		break;
						case 4: mount.Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 );		break;
						case 5: mount.Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 );			break;
						case 6: mount.Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 );		break;
						case 7: mount.Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 );		break;
						case 8: mount.Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 );		break;
						case 9: mount.Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 );		break;
						case 10: mount.Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 );		break;
						case 11: mount.Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 );		break;
						case 12: mount.Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 );		break;
						case 13: mount.Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 );		break;
						case 14: mount.Hue = MaterialInfo.GetMaterialColor( "xormite", "classic", 0 );		break;
						case 15: mount.Hue = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 );		break;
						case 16: mount.Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 );		break;
					}
				}

				Server.Mobiles.BaseMount.Ride( mount, m );
			}
		}

		public static bool PeopleMeetingHere()
		{
			if ( Utility.RandomBool() )
				return true;

			return false;
		}

		public Citizens( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( CitizenService );
			writer.Write( CitizenType );
			writer.Write( CitizenCost );
			writer.Write( CitizenPhrase );
			writer.Write( CitizenRumor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			CitizenService = reader.ReadInt();
			CitizenType = reader.ReadInt();
			CitizenCost = reader.ReadInt();
			CitizenPhrase = reader.ReadString();
			CitizenRumor = reader.ReadString();
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.MorphingTime.CheckNecromancer( this );

			if ( this.Home.X > 0 && this.Home.Y > 0 && ( Math.Abs( this.X-this.Home.X ) > 2 || Math.Abs( this.Y-this.Home.Y ) > 2 || Math.Abs( this.Z-this.Home.Z ) > 2 ) )
			{
				this.Location = this.Home;
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( this, this.Map, 0x201 );
			}
		}

		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );
			Server.Misc.MorphingTime.CheckNecromancer( this );
		}

		public class CitizenGump : Gump
		{
			private Mobile c_Citizen;
			private Mobile c_Player;

			public CitizenGump( Mobile citizen, Mobile player ) : base( 25, 25 )
			{
				c_Citizen = citizen;
				Citizens b_Citizen = (Citizens)citizen;
				c_Player = player;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				string speak = b_Citizen.CitizenPhrase;
				if ( speak.Contains("Z~Z~Z~Z~Z") ){ speak = speak.Replace("Z~Z~Z~Z~Z", c_Player.Name); }
				if ( speak.Contains("Y~Y~Y~Y~Y") ){ speak = speak.Replace("Y~Y~Y~Y~Y", c_Player.Region.Name); }
				if ( speak.Contains("G~G~G~G~G") ){ speak = speak.Replace("G~G~G~G~G", (b_Citizen.CitizenCost).ToString()); }

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(269, 0, 153);
				AddImage(2, 2, 163);
				AddImage(271, 2, 163);
				AddImage(6, 6, 145);
				AddImage(167, 7, 140);
				AddImage(244, 7, 140);
				AddImage(530, 9, 143);

				AddHtml( 177, 45, 371, 204, @"<BODY><BASEFONT Color=#FFA200><BIG>" + speak + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			from.CloseGump( typeof( CitizenGump ) );
			int sound = 0;
			string say = "";
			bool isArmor = false; if ( dropped is BaseArmor ){ isArmor = true; }
			bool isWeapon = false; if ( dropped is BaseWeapon ){ isWeapon = true; }
			bool isMetal = false; if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( dropped ) ){ isMetal = true; }
			bool isWood = false; if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( dropped ) ){ isWood = true; }
			bool isLeather = false; if ( Server.Misc.MaterialInfo.IsAnyKindOfClothItem( dropped ) ){ isLeather = true; }
			bool fixArmor = false;
			bool fixWeapon = false;

			if ( dropped is Cargo )
			{
				Server.Items.Cargo.GiveCargo( (Cargo)dropped, this, from );
			}
			else if ( dropped is Gold )
			{
				if ( CitizenCost > 0 && CitizenCost == dropped.Amount )
				{
					dropped.Delete();
					sound = 0x2E6;
					say = "That is a fair trade.";
					Item give = null;
					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						give = i;
					}
					give.Movable = true;
					from.AddToBackpack( give );
					CitizenService = 0;
				}
			}
			else if ( CitizenType == 1 )
			{
				if ( CitizenType == 1 && dropped is BaseMagicStaff )
				{
                    BaseMagicStaff ba = (BaseMagicStaff)dropped;
                    BaseWeapon bw = (BaseWeapon)dropped;

					int myCharges = 0;

					if ( bw.IntRequirement == 10 ) { myCharges = 30; }
					else if ( bw.IntRequirement == 15 ) { myCharges = 23; }
					else if ( bw.IntRequirement == 20 ) { myCharges = 18; }
					else if ( bw.IntRequirement == 25 ) { myCharges = 15; }
					else if ( bw.IntRequirement == 30 ) { myCharges = 12; }
					else if ( bw.IntRequirement == 35 ) { myCharges = 9; }
					else if ( bw.IntRequirement == 40 ) { myCharges = 6; }
					else if ( bw.IntRequirement == 45 ) { myCharges = 3; }

					if ( bw.IntRequirement < 1 ){ say = "That does not need to be recharged."; }
                    else if ( ba.Charges <= myCharges )
                    {
                        say = "Your wand is charged.";
                        sound = 0x5C1;
						ba.Charges = myCharges;
                    }
                    else { say = "That wand has too many charges already."; }
				}
			}
			else if ( CitizenService == 1 )
			{
				if ( CitizenType == 2 && isArmor && isMetal ){ fixArmor = true; sound = 0x541; }
				else if ( CitizenType == 3 && dropped is LockableContainer )
				{
					LockableContainer box = (LockableContainer)dropped;
					say = "I unlocked it for you.";
					sound = 0x241;
					box.Locked = false;
					box.TrapPower = 0;
					box.TrapLevel = 0;
					box.LockLevel = 0;
					box.MaxLockLevel = 0;
					box.RequiredSkill = 0;
					box.TrapType = TrapType.None;
				}
			}
			else if ( CitizenService == 2 )
			{
				if ( CitizenType == 2 && isWeapon && isMetal ){ fixWeapon = true; sound = 0x541; }
				else if ( CitizenType == 3 && isArmor && isLeather ){ fixArmor = true; sound = 0x248; }
				else if ( CitizenType == 3 && isWeapon && isLeather ){ fixWeapon = true; sound = 0x248; }
			}
			else if ( CitizenService == 3 )
			{
				if ( CitizenType == 2 && isWeapon && isWood ){ fixWeapon = true; sound = 0x23D; }
				else if ( CitizenType == 3 && isWeapon && isWood ){ fixWeapon = true; sound = 0x23D; }
			}
			else if ( CitizenService == 4 )
			{
				if ( CitizenType == 2 && isArmor && isWood ){ fixArmor = true; sound = 0x23D; }
				else if ( CitizenType == 3 && isArmor && isWood ){ fixArmor = true; sound = 0x23D; }
			}

			if ( fixArmor && dropped is BaseArmor )
			{
				say = "This is repaired and ready for battle.";
				BaseArmor ba = (BaseArmor)dropped;
				ba.MaxHitPoints -= 1;
				ba.HitPoints = ba.MaxHitPoints;
			}
			else if ( fixWeapon && dropped is BaseWeapon )
			{
				say = "This is repaired and is ready for battle.";
				BaseWeapon bw = (BaseWeapon)dropped;
				bw.MaxHitPoints -= 1;
				bw.HitPoints = bw.MaxHitPoints;
			}

			SayTo(from, say);
			if ( sound > 0 ){ from.PlaySound( sound ); }

			return false;
		}
	}
}