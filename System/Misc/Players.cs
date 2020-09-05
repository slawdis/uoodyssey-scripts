using Server.Accounting;
using Server.Commands.Generic;
using Server.Commands;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server; 
using System.Collections.Generic;
using System.Collections;
using System;

namespace Server.Items
{
	public class CharacterDatabase : Item
	{
		public Mobile CharacterOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Character_Owner { get{ return CharacterOwner; } set{ CharacterOwner = value; } }

		public int CharacterMOTD;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_MOTD { get { return CharacterMOTD; } set { CharacterMOTD = value; InvalidateProperties(); } }

		public int CharacterSkill;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Skill { get { return CharacterSkill; } set { CharacterSkill = value; InvalidateProperties(); } }

		public string CharacterKeys;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Character_Keys { get { return CharacterKeys; } set { CharacterKeys = value; InvalidateProperties(); } }

		public string CharacterDiscovered;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Character_Discovered { get { return CharacterDiscovered; } set { CharacterDiscovered = value; InvalidateProperties(); } }

		public int CharacterSheath;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Sheath { get { return CharacterSheath; } set { CharacterSheath = value; InvalidateProperties(); } }

		public int CharacterGuilds;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Guilds { get { return CharacterGuilds; } set { CharacterGuilds = value; InvalidateProperties(); } }

		public string CharacterBoatDoor;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Character_BoatDoor { get { return CharacterBoatDoor; } set { CharacterBoatDoor = value; InvalidateProperties(); } }

		public string CharacterPublicDoor;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Character_PublicDoor { get { return CharacterPublicDoor; } set { CharacterPublicDoor = value; InvalidateProperties(); } }

		public int CharacterBegging;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Begging { get { return CharacterBegging; } set { CharacterBegging = value; InvalidateProperties(); } }

		public int CharacterWepAbNames;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_WepAbNames { get { return CharacterWepAbNames; } set { CharacterWepAbNames = value; InvalidateProperties(); } }

		public int CharHue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Char_Hue { get { return CharHue; } set { CharHue = value; InvalidateProperties(); } }

		public int CharHairHue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Char_HairHue { get { return CharHairHue; } set { CharHairHue = value; InvalidateProperties(); } }

		public string CharMusical;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Char_Musical { get{ return CharMusical; } set{ CharMusical = value; } }

		public string CharacterLoot;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Character_Loot { get{ return CharacterLoot; } set{ CharacterLoot = value; } }

		public string CharacterWanted;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Character_Wanted { get{ return CharacterWanted; } set{ CharacterWanted = value; } }

		public int CharacterOriental;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Oriental { get { return CharacterOriental; } set { CharacterOriental = value; InvalidateProperties(); } }

		public int CharacterEvil;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Evil { get { return CharacterEvil; } set { CharacterEvil = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string MessageQuest;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Message_Quest { get { return MessageQuest; } set { MessageQuest = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string ArtifactQuestTime;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Artifact_QuestTime { get { return ArtifactQuestTime; } set { ArtifactQuestTime = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string StandardQuest;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Standard_Quest { get { return StandardQuest; } set { StandardQuest = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string FishingQuest;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Fishing_Quest { get { return FishingQuest; } set { FishingQuest = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string AssassinQuest;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Assassin_Quest { get { return AssassinQuest; } set { AssassinQuest = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string BardsTaleQuest;
		[CommandProperty( AccessLevel.GameMaster )]
		public string BardsTale_Quest { get { return BardsTaleQuest; } set { BardsTaleQuest = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string EpicQuestName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string EpicQuest_Name { get{ return EpicQuestName; } set{ EpicQuestName = value; } }

		public int EpicQuestNumber;
		[CommandProperty( AccessLevel.GameMaster )]
		public int EpicQuest_Number { get { return EpicQuestNumber; } set { EpicQuestNumber = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string SpellBarsMage1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Mage1 { get { return SpellBarsMage1; } set { SpellBarsMage1 = value; InvalidateProperties(); } }

		public string SpellBarsMage2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Mage2 { get { return SpellBarsMage2; } set { SpellBarsMage2 = value; InvalidateProperties(); } }

		public string SpellBarsMage3;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Mage3 { get { return SpellBarsMage3; } set { SpellBarsMage3 = value; InvalidateProperties(); } }

		public string SpellBarsMage4;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Mage4 { get { return SpellBarsMage4; } set { SpellBarsMage4 = value; InvalidateProperties(); } }

		public string SpellBarsNecro1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Necro1 { get { return SpellBarsNecro1; } set { SpellBarsNecro1 = value; InvalidateProperties(); } }

		public string SpellBarsNecro2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Necro2 { get { return SpellBarsNecro2; } set { SpellBarsNecro2 = value; InvalidateProperties(); } }

		public string SpellBarsChivalry1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Chivalry1 { get { return SpellBarsChivalry1; } set { SpellBarsChivalry1 = value; InvalidateProperties(); } }

		public string SpellBarsChivalry2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Chivalry2 { get { return SpellBarsChivalry2; } set { SpellBarsChivalry2 = value; InvalidateProperties(); } }

		public string SpellBarsDeath1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Death1 { get { return SpellBarsDeath1; } set { SpellBarsDeath1 = value; InvalidateProperties(); } }

		public string SpellBarsDeath2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Death2 { get { return SpellBarsDeath2; } set { SpellBarsDeath2 = value; InvalidateProperties(); } }

		public string SpellBarsBard1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Bard1 { get { return SpellBarsBard1; } set { SpellBarsBard1 = value; InvalidateProperties(); } }

		public string SpellBarsBard2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Bard2 { get { return SpellBarsBard2; } set { SpellBarsBard2 = value; InvalidateProperties(); } }

		public string SpellBarsPriest1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Priest1 { get { return SpellBarsPriest1; } set { SpellBarsPriest1 = value; InvalidateProperties(); } }

		public string SpellBarsPriest2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Priest2 { get { return SpellBarsPriest2; } set { SpellBarsPriest2 = value; InvalidateProperties(); } }

		public string SpellBarsMonk1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Monk1 { get{ return SpellBarsMonk1; } set{ SpellBarsMonk1 = value; } }

		public string SpellBarsMonk2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Monk2 { get{ return SpellBarsMonk2; } set{ SpellBarsMonk2 = value; } }

		public string SpellBarsWizard1;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Wizard1 { get { return SpellBarsWizard1; } set { SpellBarsWizard1 = value; InvalidateProperties(); } }

		public string SpellBarsWizard2;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Wizard2 { get { return SpellBarsWizard2; } set { SpellBarsWizard2 = value; InvalidateProperties(); } }

		public string SpellBarsWizard3;
		[CommandProperty( AccessLevel.GameMaster )]
		public string SpellBars_Wizard3 { get { return SpellBarsWizard3; } set { SpellBarsWizard3 = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public string ThiefQuest;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Thief_Quest { get{ return ThiefQuest; } set{ ThiefQuest = value; } }

		public string KilledSpecialMonsters;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Killed_SpecialMonsters { get{ return KilledSpecialMonsters; } set{ KilledSpecialMonsters = value; } }

		public string MusicPlaylist;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Music_Playlist { get{ return MusicPlaylist; } set{ MusicPlaylist = value; } }

		public int CharacterBarbaric;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Character_Conan { get { return CharacterBarbaric; } set { CharacterBarbaric = value; InvalidateProperties(); } }

		public int SkillDisplay;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Skill_Display { get { return SkillDisplay; } set { SkillDisplay = value; InvalidateProperties(); } }

		public int MagerySpellHue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Magery_SpellHue { get { return MagerySpellHue; } set { MagerySpellHue = value; InvalidateProperties(); } }

		public int ClassicPoisoning;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Classic_Poisoning { get { return ClassicPoisoning; } set { ClassicPoisoning = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		[Constructable]
		public CharacterDatabase() : base( 0x3F1A )
		{
			LootType = LootType.Blessed;
			Visible = false;
			Movable = false;
			Weight = 1.0;
			Name = "Character Statue";
		}

		public override bool DisplayLootType{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( CharacterOwner != null ){ list.Add( 1070722, "Statue of " + CharacterOwner.Name + "" ); }
        }

		public CharacterDatabase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
			writer.Write( (Mobile)CharacterOwner );
			writer.Write( CharacterMOTD );
			writer.Write( CharacterSkill );
			writer.Write( CharacterKeys );
			writer.Write( CharacterDiscovered );
			writer.Write( CharacterSheath );
			writer.Write( CharacterGuilds );
			writer.Write( CharacterBoatDoor );
			writer.Write( CharacterPublicDoor );
			writer.Write( CharacterBegging );
			writer.Write( CharacterWepAbNames );

			writer.Write( ArtifactQuestTime );

			writer.Write( StandardQuest );
			writer.Write( FishingQuest );
			writer.Write( AssassinQuest );
			writer.Write( MessageQuest );
			writer.Write( BardsTaleQuest );

			writer.Write( SpellBarsMage1 );
			writer.Write( SpellBarsMage2 );
			writer.Write( SpellBarsMage3 );
			writer.Write( SpellBarsMage4 );
			writer.Write( SpellBarsNecro1 );
			writer.Write( SpellBarsNecro2 );
			writer.Write( SpellBarsChivalry1 );
			writer.Write( SpellBarsChivalry2 );
			writer.Write( SpellBarsDeath1 );
			writer.Write( SpellBarsDeath2 );
			writer.Write( SpellBarsBard1 );
			writer.Write( SpellBarsBard2 );
			writer.Write( SpellBarsPriest1 );
			writer.Write( SpellBarsPriest2 );
			writer.Write( SpellBarsWizard1 );
			writer.Write( SpellBarsWizard2 );
			writer.Write( SpellBarsWizard3 );
			writer.Write( SpellBarsMonk1 );
			writer.Write( SpellBarsMonk2 );

			writer.Write( ThiefQuest );
			writer.Write( KilledSpecialMonsters );
			writer.Write( MusicPlaylist );
			writer.Write( CharacterWanted );
			writer.Write( CharacterLoot );
			writer.Write( CharMusical );
			writer.Write( EpicQuestName );
			writer.Write( CharacterBarbaric );
			writer.Write( SkillDisplay );
			writer.Write( MagerySpellHue );
			writer.Write( ClassicPoisoning );
			writer.Write( CharacterEvil );
			writer.Write( CharacterOriental );
			writer.Write( CharHue );
			writer.Write( CharHairHue );
			writer.Write( EpicQuestNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			CharacterOwner = reader.ReadMobile();
			CharacterMOTD = reader.ReadInt();
			CharacterSkill = reader.ReadInt();
			CharacterKeys = reader.ReadString();
			CharacterDiscovered = reader.ReadString();
			CharacterSheath = reader.ReadInt();
			CharacterGuilds = reader.ReadInt();
			CharacterBoatDoor = reader.ReadString();
			CharacterPublicDoor = reader.ReadString();
			CharacterBegging = reader.ReadInt();
			CharacterWepAbNames = reader.ReadInt();

			ArtifactQuestTime = reader.ReadString();

			StandardQuest = reader.ReadString();
			FishingQuest = reader.ReadString();
			AssassinQuest = reader.ReadString();
			MessageQuest = reader.ReadString();
			BardsTaleQuest = reader.ReadString();

			SpellBarsMage1 = reader.ReadString();
			SpellBarsMage2 = reader.ReadString();
			SpellBarsMage3 = reader.ReadString();
			SpellBarsMage4 = reader.ReadString();
			SpellBarsNecro1 = reader.ReadString();
			SpellBarsNecro2 = reader.ReadString();
			SpellBarsChivalry1 = reader.ReadString();
			SpellBarsChivalry2 = reader.ReadString();
			SpellBarsDeath1 = reader.ReadString();
			SpellBarsDeath2 = reader.ReadString();
			SpellBarsBard1 = reader.ReadString();
			SpellBarsBard2 = reader.ReadString();
			SpellBarsPriest1 = reader.ReadString();
			SpellBarsPriest2 = reader.ReadString();
			SpellBarsWizard1 = reader.ReadString();
			SpellBarsWizard2 = reader.ReadString();
			SpellBarsWizard3 = reader.ReadString();
			SpellBarsMonk1 = reader.ReadString();
			SpellBarsMonk2 = reader.ReadString();

			ThiefQuest = reader.ReadString();
			KilledSpecialMonsters = reader.ReadString();
			MusicPlaylist = reader.ReadString();
			CharacterWanted = reader.ReadString();
			CharacterLoot = reader.ReadString();
			CharMusical = reader.ReadString();
			EpicQuestName = reader.ReadString();
			CharacterBarbaric = reader.ReadInt();
			SkillDisplay = reader.ReadInt();
			MagerySpellHue = reader.ReadInt();
			ClassicPoisoning = reader.ReadInt();
			CharacterEvil = reader.ReadInt();
			CharacterOriental = reader.ReadInt();
			CharHue = reader.ReadInt();
			CharHairHue = reader.ReadInt();
			EpicQuestNumber = reader.ReadInt();
		}

		public static CharacterDatabase GetDB( Mobile m ) // ----------------------------------------------------------------------------------------
		{
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is CharacterDatabase )
				{
					CharacterDatabase db = (CharacterDatabase)item;
					if ( db.CharacterOwner == m )
					{
						return db;
					}
				}
			}

			return null;
		}

		public static int GetMySpellHue( Mobile m, int hue ) // ----------------------------------------------------------------------------------------
		{
			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
				int color = DB.MagerySpellHue;

				if ( color >= 0 ){ hue = color-1; } else { hue = -1; }
			}

			return hue;
		}

		public static bool GetWanted( Mobile m ) // -------------------------------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string wanted = DB.CharacterWanted;

			bool isWanted = true;

			if ( wanted == null ){ isWanted = false; }

			return isWanted;
		}

		public static void SetSavage( Mobile m ) // -------------------------------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			CharacterDatabase.SetDiscovered( m, "the Savaged Empire", true );
			m.Skills.Cap = 11000;
			Server.Misc.MorphingTime.RemoveMyClothes( m );

			if ( m.Female )
			{
				DB.CharacterEvil = 0;
				DB.CharacterOriental = 0;
				DB.CharacterBarbaric = 2;
			}
			else
			{
				DB.CharacterEvil = 0;
				DB.CharacterOriental = 0;
				DB.CharacterBarbaric = 1;
			}
			Server.Items.BarbaricSatchel.GivePack( m );

			BaseArmor hat = new LeatherCap();
			hat.Resource = CraftResource.DinosaurLeather;
			hat.Hue = 0xB61;
			hat.Name = "savage cap";
			hat.StrRequirement = 10;
			hat.ItemID = 0x5648;
			m.AddItem( hat );

			BaseArmor gloves = new LeatherGloves();
			gloves.Resource = CraftResource.DinosaurLeather;
			gloves.Hue = 0xB61;
			gloves.Name = "savage gloves";
			gloves.StrRequirement = 10;
			gloves.ItemID = 0x564E;
			m.AddItem( gloves );

			BaseArmor chest = new LeatherChest();
			chest.Resource = CraftResource.DinosaurLeather;
			chest.Hue = 0xB61;
			chest.Name = "savage tunic";
			chest.StrRequirement = 10;
			chest.ItemID = 0x5651;
			m.AddItem( chest );

			BaseArmor boots = new LeatherBoots();
			boots.Resource = CraftResource.DinosaurLeather;
			boots.Hue = 0xB61;
			boots.Name = "savage sandals";
			boots.StrRequirement = 10;
			boots.ItemID = 0x170d;
			m.AddItem( boots );

			BaseArmor pants = new LeatherLegs();
			pants.Resource = CraftResource.DinosaurLeather;
			pants.Hue = 0xB61;
			pants.Name = "savage skirt";
			pants.StrRequirement = 10;
			pants.ItemID = 0x1C08;
			m.AddItem( pants );

			BaseArmor bracers = new LeatherArms();
			bracers.Resource = CraftResource.DinosaurLeather;
			bracers.Hue = 0xB61;
			bracers.Name = "savage bracers";
			bracers.StrRequirement = 10;
			bracers.ItemID = 0x564D;
			m.AddItem( bracers );

			SavageTalisman talisman = new SavageTalisman();
			talisman.ItemOwner = m;
			m.AddItem( talisman );

			BaseWeapon dagger = new Dagger();
			dagger.Resource = CraftResource.Steel;
			m.AddItem( dagger );

			m.AddToBackpack( new Gold( 400 ) );
			m.AddToBackpack( new LambLeg( 15 ) );
			m.AddToBackpack( new Bandage( 100 ) );
			m.AddToBackpack( new Skillet() );

			CampersTent tent = new CampersTent();
			tent.Charges = 50;
			m.AddToBackpack( tent );
		}

		public static void SetSpaceMan( Mobile m ) // -------------------------------------------------------------------------------------------------
		{
			Point3D loc = new Point3D( 7000, 4000, 0 );
			m.MoveToWorld( loc, Map.Lodor );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			m.Skills.Cap = 40000;
			Server.Misc.MorphingTime.RemoveMyClothes( m );

			List<Item> contents = new List<Item>();
			foreach( Item i in m.Backpack.Items )
			{
				contents.Add(i);
			}
			foreach ( Item item in contents )
			{
				item.Delete();
			}

			for( int i = 0; i < m.Skills.Length; i++ )
			{
				Skill skill = (Skill)m.Skills[i];
				skill.Base = 0;
			}

			m.AddItem( new FancyShirt() );
			m.AddItem( new Boots() );
			m.AddItem( new LongPants() );

			BaseWeapon dagger = new Dagger();
			dagger.Name = "knife";
			m.AddItem( dagger );

			Item meat = new CookedBird( 10 );
			meat.Hue = 0xB64;
			meat.Name = "cooked alien meat";
			m.AddToBackpack( meat );

			Item water = new Waterskin();
			water.ItemID = 0x4971;
			water.Name = "empty canteen";
			m.AddToBackpack( water );

			MedicalRecord record = new MedicalRecord();
			record.DataPatient = m.Name;
			m.AddToBackpack( record );

			loc = new Point3D( 4109, 3775, 2 );
			m.MoveToWorld( loc, Map.Sosaria );
		}

		public static void SetWanted( Mobile m ) // -------------------------------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string wName = NameList.RandomName( "male" );
			string wTitle = "King";
			string wPron = "he";

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: wTitle = "Emperor"; break;
				case 1: wTitle = "Duke"; break;
				case 2: wTitle = "Earl"; break;
				case 3: wTitle = "Baron"; break;
				case 4: wTitle = "King"; break;
				case 5: wTitle = "Prince"; break;
			}

			if ( Utility.RandomMinMax( 0, 2 ) == 2 ) 
			{
				wName = NameList.RandomName( "female" );
				wPron = "she";

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: wTitle = "Empress"; break;
					case 1: wTitle = "Duchess"; break;
					case 2: wTitle = "Countess"; break;
					case 3: wTitle = "Baroness"; break;
					case 4: wTitle = "Queen"; break;
					case 5: wTitle = "Princess"; break;
				}
			} 

			DB.CharacterWanted = m.Name + " is wanted for the murder of " + wTitle + " " + wName + ". The " + wTitle + " was attacked while " + wPron + " was visting " + RandomThings.GetRandomCity() + ". Citizens stated they seen " + m.Name + " leaving the area with a blood covered " + Server.Misc.RandomThings.GetRandomWeapon() + ". The guard captain " + QuestCharacters.ParchmentWriter() + " warns all citizen to be on the lookout for " + m.Name + " as they escaped their jail cell in Britain.";
			int words = Utility.RandomMinMax( 1, 3 );
			if ( words == 2 ) 
			{
				DB.CharacterWanted = m.Name + " is wanted for the murder of " + wTitle + " " + wName + ". The " + wTitle + " was attacked while " + wPron + " was visting " + RandomThings.GetRandomCity() + ". " + m.Name + " also stole " + Server.Misc.QuestCharacters.QuestItems( true ) + " that the " + wTitle + " had with them. The guard captain " + QuestCharacters.ParchmentWriter() + " warns all citizen to be on the lookout for " + m.Name + " as they escaped their jail cell in Britain.";
			}
			else if ( words == 3 ) 
			{
				DB.CharacterWanted = m.Name + " is wanted for the murder of " + wTitle + " " + wName + ". The " + wTitle + " was assassinated by orders from a group calling themselves " + RandomThings.GetRandomSociety() + ". " + m.Name + " was hired by them to carry out the deed, but their motivations remain unclear. The guard captain " + QuestCharacters.ParchmentWriter() + " warns all citizen to be on the lookout for " + m.Name + " as they escaped their jail cell in Britain.";
			}

			m.Profile = DB.CharacterWanted;
			SetBardsTaleQuest( m, "BardsTaleWin", true );
			m.Skills.Cap = 13000;
			m.Kills = 1;
			m.Criminal = true;
			((PlayerMobile)m).Profession = 1;

			GuardNote note = new GuardNote();
			note.ScrollText = DB.CharacterWanted;
			m.AddToBackpack( note );
		}

		public static string GetWantedStory( Mobile m ) // -------------------------------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string wanted = DB.CharacterWanted;

			return wanted;
		}

		public static bool GetQuestState( Mobile m, string quest ) // -------------------------------------------------------------------------------
		{
			CharacterDatabase.MarkQuestInfo( m );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string goal = DB.StandardQuest;	

			if ( quest == "StandardQuest" ){ goal = DB.StandardQuest; }
			else if ( quest == "FishingQuest" ){ goal = DB.FishingQuest; }
			else if ( quest == "AssassinQuest" ){ goal = DB.AssassinQuest; }
			else if ( quest == "MessageQuest" ){ goal = DB.MessageQuest; }
			else if ( quest == "ThiefQuest" ){ goal = DB.ThiefQuest; }

			int nEntry = 1;

			if ( goal.Length > 0 )
			{
				string[] goals = goal.Split('#');
				foreach (string goalz in goals)
				{
					nEntry++;
				}
			}

			if ( nEntry > 3 ){ return true; }

			return false;
		}

		public static string GetQuestInfo( Mobile m, string quest ) // ------------------------------------------------------------------------------
		{
			CharacterDatabase.MarkQuestInfo( m );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string goal = DB.StandardQuest;	

			if ( quest == "StandardQuest" ){ goal = DB.StandardQuest; }
			else if ( quest == "FishingQuest" ){ goal = DB.FishingQuest; }
			else if ( quest == "AssassinQuest" ){ goal = DB.AssassinQuest; }
			else if ( quest == "MessageQuest" ){ goal = DB.MessageQuest; }
			else if ( quest == "ThiefQuest" ){ goal = DB.ThiefQuest; }

			return goal;
		}

		public static void SetQuestInfo( Mobile m, string quest, string setting ) // ----------------------------------------------------------------
		{
			CharacterDatabase.MarkQuestInfo( m );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( quest == "StandardQuest" ){ DB.StandardQuest = setting; }
			else if ( quest == "FishingQuest" ){ DB.FishingQuest = setting; }
			else if ( quest == "AssassinQuest" ){ DB.AssassinQuest = setting; }
			else if ( quest == "MessageQuest" ){ DB.MessageQuest = setting; }
			else if ( quest == "ThiefQuest" ){ DB.ThiefQuest = setting; }
		}

		public static void ClearQuestInfo( Mobile m, string quest ) // ------------------------------------------------------------------------------
		{
			CharacterDatabase.MarkQuestInfo( m );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( quest == "StandardQuest" ){ DB.StandardQuest = ""; }
			else if ( quest == "FishingQuest" ){ DB.FishingQuest = ""; }
			else if ( quest == "AssassinQuest" ){ DB.AssassinQuest = ""; }
			else if ( quest == "MessageQuest" ){ DB.MessageQuest = ""; }
			else if ( quest == "ThiefQuest" ){ DB.ThiefQuest = ""; }
		}

		public static void MarkQuestInfo( Mobile m ) // ---------------------------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB.StandardQuest == null ){ DB.StandardQuest = ""; }
			if ( DB.FishingQuest == null ){ DB.FishingQuest = ""; }
			if ( DB.AssassinQuest == null ){ DB.AssassinQuest = ""; }
			if ( DB.MessageQuest == null ){ DB.MessageQuest = ""; }
			if ( DB.ThiefQuest == null ){ DB.ThiefQuest = ""; }
		}

		public static bool GetDiscovered( Mobile m, string world ) // -------------------------------------------------------------------------------
		{
			SetDiscovered( m, "none", false );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string discovered = DB.CharacterDiscovered;

			bool BeenThere = false;

			if ( discovered.Length > 0 )
			{
				string[] discoveries = discovered.Split('#');
				int nEntry = 1;
				foreach (string found in discoveries)
				{
					if ( nEntry == 1 && found == "1" && world == "the Land of Lodoria" ){ BeenThere = true; }
					else if ( nEntry == 2 && found == "1" && world == "the Land of Sosaria" ){ BeenThere = true; }
					else if ( nEntry == 3 && found == "1" && world == "the Island of Umber Veil" ){ BeenThere = true; }
					else if ( nEntry == 4 && found == "1" && world == "the Land of Ambrosia" ){ BeenThere = true; }
					else if ( nEntry == 5 && found == "1" && world == "the Serpent Island" ){ BeenThere = true; }
					else if ( nEntry == 6 && found == "1" && world == "the Isles of Dread" ){ BeenThere = true; }
					else if ( nEntry == 7 && found == "1" && world == "the Savaged Empire" ){ BeenThere = true; }
					else if ( nEntry == 8 && found == "1" && world == "the Bottle World of Kuldar" ){ BeenThere = true; }
					else if ( nEntry == 9 && found == "1" && world == "the Underworld" ){ BeenThere = true; }

					nEntry++;
				}
			}

			return BeenThere;
		}

		public static void SetDiscovered( Mobile m, string world, bool repeat ) // ------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string discovered = DB.CharacterDiscovered;
			int records = 9; // TOTAL ENTRIES

			if ( discovered == null ){ discovered = "0#0#0#0#0#0#0#0#0#"; }

			if ( discovered.Length > 0 )
			{
				string[] discoveries = discovered.Split('#');
				string entry = "";
				int nEntry = 1;

				foreach ( string lands in discoveries )
				{
					if ( nEntry == 1 && world == "the Land of Lodoria" ){ entry = entry + "1#"; }
					else if ( nEntry == 2 && world == "the Land of Sosaria" ){ entry = entry + "1#"; }
					else if ( nEntry == 3 && world == "the Island of Umber Veil" ){ entry = entry + "1#"; }
					else if ( nEntry == 4 && world == "the Land of Ambrosia" ){ entry = entry + "1#"; }
					else if ( nEntry == 5 && world == "the Serpent Island" ){ entry = entry + "1#"; }
					else if ( nEntry == 6 && world == "the Isles of Dread" ){ entry = entry + "1#"; }
					else if ( nEntry == 7 && world == "the Savaged Empire" ){ entry = entry + "1#"; }
					else if ( nEntry == 8 && world == "the Bottle World of Kuldar" ){ entry = entry + "1#"; }
					else if ( nEntry == 9 && world == "the Underworld" ){ entry = entry + "1#"; }
					else if ( nEntry == 1 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 2 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 3 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 4 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 5 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 6 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 7 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 8 ){ entry = entry + lands + "#"; }
					else if ( nEntry == 9 ){ entry = entry + lands + "#"; }

					nEntry++;
				}

				while ( nEntry < records+1 )
				{
					entry = entry + "0#";
					nEntry++;
				}

				DB.CharacterDiscovered = entry;

				if ( repeat ){ SetDiscovered( m, world, false ); }
			}
		}

		public static bool GetKeys( Mobile m, string key ) // ---------------------------------------------------------------------------------------
		{
			SetKeys( m, "none", false );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string keys = DB.CharacterKeys;

			bool HaveIt = false;

			if ( keys.Length > 0 )
			{
				string[] discoveries = keys.Split('#');
				int nEntry = 1;
				foreach (string found in discoveries)
				{
					if ( nEntry == 1 && found == "1" && key == "UndermountainKey" ){ HaveIt = true; }
					else if ( nEntry == 2 && found == "1" && key == "BlackKnightKey" ){ HaveIt = true; }
					else if ( nEntry == 3 && found == "1" && key == "RangerOutpost" ){ HaveIt = true; }
					else if ( nEntry == 4 && found == "1" && key == "VordoKey" ){ HaveIt = true; }
					else if ( nEntry == 5 && found == "1" && key == "SkullGate" ){ HaveIt = true; }
					else if ( nEntry == 6 && found == "1" && key == "SerpentPillars" ){ HaveIt = true; }
					else if ( nEntry == 7 && found == "1" && key == "Antiques" ){ HaveIt = true; }
					else if ( nEntry == 8 && found == "1" && key == "Museums" ){ HaveIt = true; }
					else if ( nEntry == 9 && found == "1" && key == "Runes" ){ HaveIt = true; }
					else if ( nEntry == 10 && found == "1" && key == "Virtue" ){ HaveIt = true; }
					else if ( nEntry == 11 && found == "1" && key == "Corrupt" ){ HaveIt = true; }
					else if ( nEntry == 12 && found == "1" && key == "Gygax" ){ HaveIt = true; }
					else if ( nEntry == 13 && found == "1" && key == "DragonRiding" ){ HaveIt = true; }

					nEntry++;
				}
			}

			return HaveIt;
		}

		public static void SetKeys( Mobile m, string key, bool repeat ) // --------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string keys = DB.CharacterKeys;

			if ( keys == null ){ keys = "0#0#0#0#0#0#0#0#0#0#0#0#0#"; }

			if ( keys.Length > 0 )
			{
				string[] discoveries = keys.Split('#');
				string entry = "";
				int nEntry = 1;
				int records = 12; // TOTAL ENTRIES

				foreach ( string keyset in discoveries )
				{
					string sets = "1";
					if ( keyset != "1" ){ sets = "0"; } 
					if ( nEntry == 1 && key == "UndermountainKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 2 && key == "BlackKnightKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 3 && key == "RangerOutpost" ){ entry = entry + "1#"; }
					else if ( nEntry == 4 && key == "VordoKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 5 && key == "SkullGate" ){ entry = entry + "1#"; }
					else if ( nEntry == 6 && key == "SerpentPillars" ){ entry = entry + "1#"; }
					else if ( nEntry == 7 && key == "Antiques" ){ entry = entry + "1#"; }
					else if ( nEntry == 8 && key == "Museums" ){ entry = entry + "1#"; }
					else if ( nEntry == 9 && key == "Runes" ){ entry = entry + "1#"; }
					else if ( nEntry == 10 && key == "Virtue" ){ entry = entry + "1#"; }
					else if ( nEntry == 11 && key == "Corrupt" ){ entry = entry + "1#"; }
					else if ( nEntry == 12 && key == "Gygax" ){ entry = entry + "1#"; }
					else if ( nEntry == 13 && key == "DragonRiding" ){ entry = entry + "1#"; }

					else if ( nEntry == 1 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 2 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 3 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 4 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 5 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 6 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 7 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 8 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 9 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 10 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 11 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 12 ){ entry = entry + sets + "#"; }
					else if ( nEntry == 13 ){ entry = entry + sets + "#"; }

					nEntry++;
				}

				while ( nEntry < records+1 )
				{
					entry = entry + "0#";
					nEntry++;
				}

				DB.CharacterKeys = entry;

				if ( repeat ){ SetKeys( m, key, false ); }
			}
		}

		public static bool GetSpecialsKilled( Mobile m, string who ) // ---------------------------------------------------------------------------------------
		{
			SetSpecialsKilled( m, "none", false );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string whos = DB.KilledSpecialMonsters;

			bool AlreadyDid = false;

			if ( whos.Length > 0 )
			{
				string[] enemies = whos.Split('#');
				int nEntry = 1;
				foreach (string found in enemies)
				{
					if ( nEntry == 1 && found == "1" && who == "Arachnar" ){ AlreadyDid = true; }
					else if ( nEntry == 2 && found == "1" && who == "BlackGateDemon" ){ AlreadyDid = true; }
					else if ( nEntry == 3 && found == "1" && who == "BloodDemigod" ){ AlreadyDid = true; }
					else if ( nEntry == 4 && found == "1" && who == "Xurtzar" ){ AlreadyDid = true; }
					else if ( nEntry == 5 && found == "1" && who == "CaddelliteDragon" ){ AlreadyDid = true; }
					else if ( nEntry == 6 && found == "1" && who == "DragonKing" ){ AlreadyDid = true; }
					else if ( nEntry == 7 && found == "1" && who == "Vulcrum" ){ AlreadyDid = true; }
					else if ( nEntry == 8 && found == "1" && who == "OrkDemigod" ){ AlreadyDid = true; }
					else if ( nEntry == 9 && found == "1" && who == "Mangar" ){ AlreadyDid = true; }
					else if ( nEntry == 10 && found == "1" && who == "Astaroth" ){ AlreadyDid = true; }
					else if ( nEntry == 11 && found == "1" && who == "Faulinei" ){ AlreadyDid = true; }
					else if ( nEntry == 12 && found == "1" && who == "Nosfentor" ){ AlreadyDid = true; }
					else if ( nEntry == 13 && found == "1" && who == "Tarjan" ){ AlreadyDid = true; }
					else if ( nEntry == 14 && found == "1" && who == "Dracula" ){ AlreadyDid = true; }
					else if ( nEntry == 15 && found == "1" && who == "LichKing" ){ AlreadyDid = true; }
					else if ( nEntry == 16 && found == "1" && who == "Surtaz" ){ AlreadyDid = true; }
					else if ( nEntry == 17 && found == "1" && who == "TitanLithos" ){ AlreadyDid = true; }
					else if ( nEntry == 18 && found == "1" && who == "TitanPyros" ){ AlreadyDid = true; }
					else if ( nEntry == 19 && found == "1" && who == "TitanHydros" ){ AlreadyDid = true; }
					else if ( nEntry == 20 && found == "1" && who == "TitanStatos" ){ AlreadyDid = true; }
					else if ( nEntry == 21 && found == "1" && who == "Jormungandr" ){ AlreadyDid = true; }
					else if ( nEntry == 22 && found == "1" && who == "Exodus" ){ AlreadyDid = true; }

					nEntry++;
				}
			}

			return AlreadyDid;
		}

		public static void SetSpecialsKilled( Mobile m, string who, bool repeat ) // ----------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string whos = DB.KilledSpecialMonsters;
			int records = 22; // TOTAL ENTRIES

			if ( whos == null ){ whos = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }

			if ( whos.Length > 0 )
			{
				string[] enemies = whos.Split('#');
				string entry = "";
				int nEntry = 1;

				foreach ( string killed in enemies )
				{
					if ( nEntry == 1 && who == "Arachnar" ){ entry = entry + "1#"; }
					else if ( nEntry == 2 && who == "BlackGateDemon" ){ entry = entry + "1#"; }
					else if ( nEntry == 3 && who == "BloodDemigod" ){ entry = entry + "1#"; }
					else if ( nEntry == 4 && who == "Xurtzar" ){ entry = entry + "1#"; }
					else if ( nEntry == 5 && who == "CaddelliteDragon" ){ entry = entry + "1#"; }
					else if ( nEntry == 6 && who == "DragonKing" ){ entry = entry + "1#"; }
					else if ( nEntry == 7 && who == "Vulcrum" ){ entry = entry + "1#"; }
					else if ( nEntry == 8 && who == "OrkDemigod" ){ entry = entry + "1#"; }
					else if ( nEntry == 9 && who == "Mangar" ){ entry = entry + "1#"; }
					else if ( nEntry == 10 && who == "Astaroth" ){ entry = entry + "1#"; }
					else if ( nEntry == 11 && who == "Faulinei" ){ entry = entry + "1#"; }
					else if ( nEntry == 12 && who == "Nosfentor" ){ entry = entry + "1#"; }
					else if ( nEntry == 13 && who == "Tarjan" ){ entry = entry + "1#"; }
					else if ( nEntry == 14 && who == "Dracula" ){ entry = entry + "1#"; }
					else if ( nEntry == 15 && who == "LichKing" ){ entry = entry + "1#"; }
					else if ( nEntry == 16 && who == "Surtaz" ){ entry = entry + "1#"; }
					else if ( nEntry == 17 && who == "TitanLithos" ){ entry = entry + "1#"; }
					else if ( nEntry == 18 && who == "TitanPyros" ){ entry = entry + "1#"; }
					else if ( nEntry == 19 && who == "TitanHydros" ){ entry = entry + "1#"; }
					else if ( nEntry == 20 && who == "TitanStatos" ){ entry = entry + "1#"; }
					else if ( nEntry == 21 && who == "Jormungandr" ){ entry = entry + "1#"; }
					else if ( nEntry == 22 && who == "Exodus" ){ entry = entry + "1#"; }

					else if ( nEntry == 1 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 2 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 3 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 4 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 5 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 6 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 7 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 8 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 9 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 10 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 11 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 12 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 13 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 14 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 15 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 16 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 17 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 18 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 19 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 20 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 21 ){ entry = entry + killed + "#"; }
					else if ( nEntry == 22 ){ entry = entry + killed + "#"; }

					nEntry++;
				}

				while ( nEntry < records+1 )
				{
					entry = entry + "0#";
					nEntry++;
				}

				DB.KilledSpecialMonsters = entry;

				if ( repeat ){ SetSpecialsKilled( m, who, false ); }
			}
		}

		public static bool GetBardsTaleQuest( Mobile m, string part ) // -----------------------------------------------------------------------------
		{
			SetBardsTaleQuest( m, "none", false );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string quest = DB.BardsTaleQuest;

			string[] quests = quest.Split('#');
			int nEntry = 1;
			foreach (string goal in quests)
			{
				if ( nEntry == 1 && goal == "1" && part == "BardsTaleMadGodName" ){ return true; }
				else if ( nEntry == 2 && goal == "1" && part == "BardsTaleCatacombKey" ){ return true; }
				else if ( nEntry == 3 && goal == "1" && part == "BardsTaleEbonyKey" ){ return true; }
				else if ( nEntry == 4 && goal == "1" && part == "BardsTaleKylearanKey" ){ return true; }
				else if ( nEntry == 5 && goal == "1" && part == "BardsTaleHarkynKey" ){ return true; }
				else if ( nEntry == 6 && goal == "1" && part == "BardsTaleDragonKey" ){ return true; }
				else if ( nEntry == 7 && goal == "1" && part == "BardsTaleSpectreEye" ){ return true; }
				else if ( nEntry == 8 && goal == "1" && part == "BardsTaleCrystalSword" ){ return true; }
				else if ( nEntry == 9 && goal == "1" && part == "BardsTaleSilverSquare" ){ return true; }
				else if ( nEntry == 10 && goal == "1" && part == "BardsTaleBedroomKey" ){ return true; }
				else if ( nEntry == 11 && goal == "1" && part == "BardsTaleSilverTriangle" ){ return true; }
				else if ( nEntry == 12 && goal == "1" && part == "BardsTaleCrystalGolem" ){ return true; }
				else if ( nEntry == 13 && goal == "1" && part == "BardsTaleSilverCircle" ){ return true; }
				else if ( nEntry == 14 && goal == "1" && part == "BardsTaleMangarKey" ){ return true; }
				else if ( nEntry == 15 && goal == "1" && part == "BardsTaleWin" ){ return true; }

				nEntry++;
			}

			return false;
		}

		public static void SetBardsTaleQuest( Mobile m, string part, bool repeat ) // ---------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string quest = DB.BardsTaleQuest;
			int records = 15; // TOTAL ENTRIES

			if ( quest == null ){ quest = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }

			if ( quest.Length > 0 )
			{
				string[] quests = quest.Split('#');
				string entry = "";
				int nEntry = 1;
				int Finished = 0;

				foreach ( string goal in quests )
				{
					if ( nEntry == 1 && part == "BardsTaleMadGodName" ){ entry = entry + "1#"; }
					else if ( nEntry == 2 && part == "BardsTaleCatacombKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 3 && part == "BardsTaleEbonyKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 4 && part == "BardsTaleKylearanKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 5 && part == "BardsTaleHarkynKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 6 && part == "BardsTaleDragonKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 7 && part == "BardsTaleSpectreEye" ){ entry = entry + "1#"; }
					else if ( nEntry == 8 && part == "BardsTaleCrystalSword" ){ entry = entry + "1#"; }
					else if ( nEntry == 9 && part == "BardsTaleSilverSquare" ){ entry = entry + "1#"; }
					else if ( nEntry == 10 && part == "BardsTaleBedroomKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 11 && part == "BardsTaleSilverTriangle" ){ entry = entry + "1#"; }
					else if ( nEntry == 12 && part == "BardsTaleCrystalGolem" ){ entry = entry + "1#"; }
					else if ( nEntry == 13 && part == "BardsTaleSilverCircle" ){ entry = entry + "1#"; }
					else if ( nEntry == 14 && part == "BardsTaleMangarKey" ){ entry = entry + "1#"; }
					else if ( nEntry == 15 && part == "BardsTaleWin" ){ entry = entry + "1#"; Finished = 1; }

					else if ( nEntry == 1 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 2 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 3 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 4 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 5 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 6 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 7 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 8 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 9 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 10 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 11 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 12 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 13 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 14 ){ entry = entry + goal + "#"; }
					else if ( nEntry == 15 ){ entry = entry + goal + "#"; }

					nEntry++;
				}

				while ( nEntry < records+1 )
				{
					entry = entry + "0#";
					nEntry++;
				}

				DB.BardsTaleQuest = entry;

				if ( Finished > 0 ){ DB.BardsTaleQuest = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#1#"; }

				if ( repeat ){ SetBardsTaleQuest( m, part, false ); }
			}
		}

		public static void LootContainer( Mobile m, Container box ) // -------------------------------------------------------------------------------------
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			string looting = DB.CharacterLoot;

			if ( looting == null ){ Server.Misc.LootChoiceUpdates.InitializeLootChoice( m ); looting = DB.CharacterLoot; }

			if ( looting.Length > 0 )
			{
				int foundCoins = 0;
				int foundNuggets = 0;
				int foundGems = 0;
				int foundJewels = 0;
				int foundArrows = 0;
				int foundBolts = 0;
				int foundBandages = 0;
				int foundScrolls = 0;
				int foundReagents = 0;
				int foundPotions = 0;

				List<Item> belongings = new List<Item>();

				string[] discoveries = looting.Split('#');
				int nEntry = 1;
				foreach (string found in discoveries)
				{
					if ( nEntry == 1 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is Gold ){ belongings.Add(i); foundCoins = 1; }
							else if ( i is DDCopper ){ belongings.Add(i); foundCoins = 1; }
							else if ( i is DDSilver ){ belongings.Add(i); foundCoins = 1; }
							else if ( i is DDXormite ){ belongings.Add(i); foundCoins = 1; }
							else if ( i is DDGoldNuggets ){ belongings.Add(i); foundNuggets = 1; }
						}
					}
					else if ( nEntry == 2 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is StarSapphire ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Emerald ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Sapphire ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Ruby ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Citrine ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Amethyst ){ belongings.Add(i); foundGems = 1; }
							else if ( i is MysticalPearl ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Tourmaline ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Tourmaline ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Amber ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Crystals ){ belongings.Add(i); foundGems = 1; }
							else if ( i is Diamond ){ belongings.Add(i); foundGems = 1; }
							else if ( i is DDRelicGem ){ belongings.Add(i); foundGems = 1; }
							else if ( i is DDGemstones ){ belongings.Add(i); foundGems = 1; }
							else if ( i is DDJewels ){ belongings.Add(i); foundJewels = 1; }

							if ( i is DDRelicJewels ){ belongings.Add(i); foundJewels = 1; }
							else if ( i is MagicJewelryRing ){ belongings.Add(i); foundJewels = 1; }
							else if ( i is MagicJewelryNecklace ){ belongings.Add(i); foundJewels = 1; }
							else if ( i is MagicJewelryEarrings ){ belongings.Add(i); foundJewels = 1; }
							else if ( i is MagicJewelryBracelet ){ belongings.Add(i); foundJewels = 1; }
							else if ( i is MagicJewelryCirclet ){ belongings.Add(i); foundJewels = 1; }
						}
					}
					else if ( nEntry == 3 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is Arrow ){ belongings.Add(i); foundArrows = 1; }
							else if ( i is ManyArrows100 ){ belongings.Add(i); foundArrows = 1; }
							else if ( i is ManyArrows1000 ){ belongings.Add(i); foundArrows = 1; }
						}
					}
					else if ( nEntry == 4 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is Bolt ){ belongings.Add(i); foundBolts = 1; }
							else if ( i is ManyBolts100 ){ belongings.Add(i); foundBolts = 1; }
							else if ( i is ManyBolts1000 ){ belongings.Add(i); foundBolts = 1; }
						}
					}
					else if ( nEntry == 5 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is Bandage ){ belongings.Add(i); foundBandages = 1; }
						}
					}
					else if ( nEntry == 6 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is ReactiveArmorScroll || i is ClumsyScroll || i is CreateFoodScroll || i is FeeblemindScroll || 
							 i is HealScroll || i is MagicArrowScroll || i is NightSightScroll || i is WeakenScroll || 
							 i is AgilityScroll || i is CunningScroll || i is CureScroll || i is HarmScroll || 
							 i is MagicTrapScroll || i is MagicUnTrapScroll || i is ProtectionScroll || i is StrengthScroll || 
							 i is BlessScroll || i is FireballScroll || i is MagicLockScroll || i is PoisonScroll || 
							 i is TelekinisisScroll || i is TeleportScroll || i is UnlockScroll || i is WallOfStoneScroll || 
							 i is ArchCureScroll || i is ArchProtectionScroll || i is CurseScroll || i is FireFieldScroll || 
							 i is GreaterHealScroll || i is LightningScroll || i is ManaDrainScroll || i is RecallScroll || 
							 i is BladeSpiritsScroll || i is DispelFieldScroll || i is IncognitoScroll || i is MagicReflectScroll || 
							 i is MindBlastScroll || i is ParalyzeScroll || i is PoisonFieldScroll || i is SummonCreatureScroll || 
							 i is DispelScroll || i is EnergyBoltScroll || i is ExplosionScroll || i is InvisibilityScroll || 
							 i is MarkScroll || i is MassCurseScroll || i is ParalyzeFieldScroll || i is RevealScroll || 
							 i is ChainLightningScroll || i is EnergyFieldScroll || i is FlamestrikeScroll || i is GateTravelScroll || 
							 i is ManaVampireScroll || i is MassDispelScroll || i is MeteorSwarmScroll || i is PolymorphScroll || 
							 i is EarthquakeScroll || i is EnergyVortexScroll || i is ResurrectionScroll || i is SummonAirElementalScroll || 
							 i is SummonDaemonScroll || i is SummonEarthElementalScroll || i is SummonFireElementalScroll || i is SummonWaterElementalScroll )
							{ belongings.Add(i); foundScrolls = 1; }
						}
					}
					else if ( nEntry == 7 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is AnimateDeadScroll || i is BloodOathScroll || i is CorpseSkinScroll || i is CurseWeaponScroll || 
							 i is EvilOmenScroll || i is HorrificBeastScroll || i is LichFormScroll || i is MindRotScroll || 
							 i is PainSpikeScroll || i is PoisonStrikeScroll || i is StrangleScroll || i is SummonFamiliarScroll || 
							 i is VampiricEmbraceScroll || i is VengefulSpiritScroll || i is WitherScroll || i is WraithFormScroll || 
							 i is ExorcismScroll )
							{ belongings.Add(i); foundScrolls = 1; }
						}
					}
					else if ( nEntry == 8 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is BlackPearl ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is Bloodmoss ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is Garlic ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is Ginseng ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is MandrakeRoot ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is Nightshade ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is SpidersSilk ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is SulfurousAsh ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is reagents_magic_jar1 ){ belongings.Add(i); foundReagents = 1; }
						}
					}
					else if ( nEntry == 9 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is BatWing ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is DaemonBlood ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PigIron ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is NoxCrystal ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is GraveDust ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is reagents_magic_jar2 ){ belongings.Add(i); foundReagents = 1; }
						}
					}
					else if ( nEntry == 10 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is UnknownReagent ){ belongings.Add(i); foundReagents = 1; }
						}
					}
					else if ( nEntry == 11 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is BasePotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is AutoResPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is ShieldOfEarthPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is WoodlandProtectionPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is ProtectiveFairyPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is HerbalHealingPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is GraspingRootsPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is BlendWithForestPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is SwarmOfInsectsPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is VolcanicEruptionPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is TreefellowPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is StoneCirclePotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is DruidicRunePotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is LureStonePotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is NaturesPassagePotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is MushroomGatewayPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is RestorativeSoilPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is FireflyPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is HellsGateScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is ManaLeechScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is NecroCurePoisonScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is NecroPoisonScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is NecroUnlockScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is PhantasmScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is RetchedAirScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is SpectreShadowScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is UndeadEyesScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is VampireGiftScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is WallOfSpikesScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is BloodPactScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is GhostlyImagesScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is GhostPhaseScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is GraveyardGatewayScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is HellsBrandScroll ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is MagicalDyes ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is UnusualDyes ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is BottleOfAcid ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is CrystallineJar ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is NecroSkinPotion ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilWood ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilAmethyst ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilCaddellite ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilEmerald ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilGarnet ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilIce ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilJade ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilLeather ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilMarble ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilMetal ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilOnyx ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilQuartz ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilRuby ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilSapphire ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilSilver ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilSpinel ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilStarRuby ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilTopaz ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is OilWood ){ belongings.Add(i); foundPotions = 1; }
						}
					}
					else if ( nEntry == 12 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is UnknownKeg ){ belongings.Add(i); foundPotions = 1; }
							else if ( i is UnknownLiquid ){ belongings.Add(i); foundPotions = 1; }
						}
					}
					else if ( nEntry == 13 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is ArmysPaeonScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is EnchantingEtudeScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is EnergyCarolScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is EnergyThrenodyScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is FireCarolScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is FireThrenodyScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is FoeRequiemScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is IceCarolScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is IceThrenodyScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is KnightsMinneScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is MagesBalladScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is MagicFinaleScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is PoisonCarolScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is PoisonThrenodyScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is SheepfoeMamboScroll ){ belongings.Add(i); foundScrolls = 1; }
							else if ( i is SinewyEtudeScroll ){ belongings.Add(i); foundScrolls = 1; }
						}
					}
					else if ( nEntry == 14 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is UnknownScroll ){ belongings.Add(i); foundScrolls = 1; }
						}
					}
					else if ( nEntry == 15 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is EyeOfToad ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is FairyEgg ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is GargoyleEar ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is BeetleShell ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is MoonCrystal ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PixieSkull ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is RedLotus ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is SeaSalt ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is SilverWidow ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is SwampBerries ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is Brimstone ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is ButterflyWings ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is reagents_magic_jar3 ){ belongings.Add(i); foundReagents = 1; }
						}
					}
					else if ( nEntry == 16 && found == "1" )
					{
						foreach( Item i in box.Items )
						{
							if ( i is PlantHerbalism_Leaf ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PlantHerbalism_Flower ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PlantHerbalism_Mushroom ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PlantHerbalism_Lilly ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PlantHerbalism_Cactus ){ belongings.Add(i); foundReagents = 1; }
							else if ( i is PlantHerbalism_Grass ){ belongings.Add(i); foundReagents = 1; }
						}
					}

					nEntry++;
				}

				int sound = 0;
				foreach ( Item stuff in belongings )
				{
					sound = 1;
					m.AddToBackpack( stuff );
				}

				if ( sound > 0 )
				{
					m.PlaySound( 0x048 );

					string sMessage = "You take some ";

					if ( foundCoins > 0 ){ sMessage = sMessage + "coins, "; }
					if ( foundGems > 0 ){ sMessage = sMessage + "gems, "; }
					if ( foundNuggets > 0 ){ sMessage = sMessage + "nuggets, "; }
					if ( foundJewels > 0 ){ sMessage = sMessage + "jewels, "; }
					if ( foundArrows > 0 ){ sMessage = sMessage +" arrows, "; }
					if ( foundBolts > 0 ){ sMessage = sMessage + "bolts, "; }
					if ( foundBandages > 0 ){ sMessage = sMessage + "bandages, "; }
					if ( foundScrolls > 0 ){ sMessage = sMessage + "scrolls, "; }
					if ( foundReagents > 0 ){ sMessage = sMessage + "reagents, "; }
					if ( foundPotions > 0 ){ sMessage = sMessage + "potions, "; }

					sMessage = sMessage + "and put them in your pack.";

					m.SendMessage( sMessage );
				}
			}

			Server.Gumps.MReagentGump.XReagentGump( m );
			Server.Gumps.QuickBar.RefreshQuickBar( m );
			Server.Gumps.WealthBar.RefreshWealthBar( m );
		}
	}
}

namespace Server.Commands
{
	public class CreateDB
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CreateDB", AccessLevel.Counselor, new CommandEventHandler( DBs_OnCommand ) );
		}

		private class DBsTarget : Target
		{
			public DBsTarget() : base( -1, true, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is PlayerMobile )
				{
					Mobile m = (Mobile)o;

					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

					if ( DB == null )
					{
						CharacterDatabase MyDB = new CharacterDatabase();
						MyDB.CharacterOwner = m;
						m.BankBox.DropItem( MyDB );
					}
				}
			}
		}

		[Usage( "CreateDB" )]
		[Description( "Creates a character statue in the character bank box, which acts as a database." )]
		private static void DBs_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new DBsTarget();
		}
	}
}

namespace Server.Misc
{
    class GetPlayerInfo
    {
		public static string GetSkillTitle( Mobile m )
		{
			bool isOriental = Server.Misc.GetPlayerInfo.OrientalPlay( m );
			bool isEvil = Server.Misc.GetPlayerInfo.EvilPlay( m );
			int isBarbaric = Server.Misc.GetPlayerInfo.BarbaricPlay( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB.CharacterSkill == 55 )
			{
				return "Titan of Ether";
			}
			else if ( m.SkillsTotal > 0 )
			{
				Skill highest = GetShowingSkill( m, DB );

				if ( highest != null )
				{
					if ( highest.Value < 0.1 )
					{
						return "Village Idiot";
					}
					else
					{
						string skillLevel = null;
						if ( highest.Value < 29.1 ){ skillLevel = "Aspiring"; }
						else { skillLevel = GetSkillLevel( highest ); }

						string skillTitle = highest.Info.Title;

						skillTitle = Skill.CharacterTitle( skillTitle, m.Female, m.Karma, m.Skills[SkillName.Chivalry].Value, m.Skills[SkillName.Fishing].Value, m.Skills[SkillName.Magery].Base, m.Skills[SkillName.Necromancy].Base, m.Skills[SkillName.Healing].Base, m.Skills[SkillName.SpiritSpeak].Base, isBarbaric, isOriental, isMonk(m), isSyth(m,false), isJedi(m,false), isJester(m), isEvil );

						return String.Concat( skillLevel, " ", skillTitle );
					}
				}
			}

			return "Village Idiot";
		}

		public static bool isMonk ( Mobile m )
		{
			int points = 0;

			Spellbook book = Spellbook.FindMystic( m );
			if ( book is MysticSpellbook )
			{
				MysticSpellbook tome = (MysticSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Mystic.MysticSpell.MonkNotIllegal( m ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isFromSpace( Mobile m )
		{
			if ( m.Skills.Cap >= 40000 )
				return true;

			return false;
		}

		public static bool isSyth ( Mobile m, bool checkSword )
		{
			int points = 0;

			Spellbook book = Spellbook.FindSyth( m );
			if ( book is SythSpellbook )
			{
				SythSpellbook tome = (SythSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Syth.SythSpell.SythNotIllegal( m, checkSword ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isJedi ( Mobile m, bool checkSword )
		{
			int points = 0;

			Spellbook book = Spellbook.FindJedi( m );
			if ( book is JediSpellbook )
			{
				JediSpellbook tome = (JediSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Jedi.JediSpell.JediNotIllegal( m, checkSword ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isJester ( Mobile from )
		{
			int points = 0;

			if ( from is PlayerMobile && from != null )
			{
				foreach( Item i in from.Backpack.FindItemsByType( typeof( BagOfTricks ), true ) )
				{
					if ( i != null ){ points = 1; }
				}

				if ( from.Skills[SkillName.Begging].Value > 10 || from.Skills[SkillName.EvalInt].Value > 10 )
				{
					points++;
				}

				if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
				{
					Item robe = from.FindItemOnLayer( Layer.OuterTorso );
					if ( robe.ItemID == 0x1f9f || robe.ItemID == 0x1fa0 || robe.ItemID == 0x4C16 || robe.ItemID == 0x4C17 || robe.ItemID == 0x2B6B || robe.ItemID == 0x3162 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.MiddleTorso ) != null )
				{
					Item shirt = from.FindItemOnLayer( Layer.MiddleTorso );
					if ( shirt.ItemID == 0x1f9f || shirt.ItemID == 0x1fa0 || shirt.ItemID == 0x4C16 || shirt.ItemID == 0x4C17 || shirt.ItemID == 0x2B6B || shirt.ItemID == 0x3162 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.Helm ) != null )
				{
					Item hat = from.FindItemOnLayer( Layer.Helm );
					if ( hat.ItemID == 0x171C || hat.ItemID == 0x4C15 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.Shoes ) != null )
				{
					Item feet = from.FindItemOnLayer( Layer.Shoes );
					if ( feet.ItemID == 0x4C27 )
						points++;
				}
			}

			if ( points > 2 )
				return true;

			return false;
		}

		private static Skill GetHighestSkill( Mobile m )
		{
			Skills skills = m.Skills;

			if ( !Core.AOS )
				return skills.Highest;

			Skill highest = m.Skills[SkillName.Wrestling];

			for ( int i = 0; i < m.Skills.Length; ++i )
			{
				Skill check = m.Skills[i];

				if ( highest == null || check.Value > highest.Value )
					highest = check;
				else if ( highest != null && highest.Lock != SkillLock.Up && check.Lock == SkillLock.Up && check.Value == highest.Value )
					highest = check;
			}

			return highest;
		}

		private static string[,] m_Levels = new string[,]
			{
				{ "Neophyte",		"Neophyte",		"Neophyte"		},
				{ "Novice",			"Novice",		"Novice"		},
				{ "Apprentice",		"Apprentice",	"Apprentice"	},
				{ "Journeyman",		"Journeyman",	"Journeyman"	},
				{ "Expert",			"Expert",		"Expert"		},
				{ "Adept",			"Adept",		"Adept"			},
				{ "Master",			"Master",		"Master"		},
				{ "Grandmaster",	"Grandmaster",	"Grandmaster"	},
				{ "Elder",			"Tatsujin",		"Shinobi"		},
				{ "Legendary",		"Kengo",		"Ka-ge"			}
			};

		private static string GetSkillLevel( Skill skill )
		{
			return m_Levels[GetTableIndex( skill ), GetTableType( skill )];
		}

		private static int GetTableType( Skill skill )
		{
			switch ( skill.SkillName )
			{
				default: return 0;
				case SkillName.Bushido: return 1;
				case SkillName.Ninjitsu: return 2;
			}
		}

		private static int GetTableIndex( Skill skill )
		{
			int fp = 0; // Math.Min( skill.BaseFixedPoint, 1200 );

			if ( skill.Value >= 120 ){ fp = 9; }
			else if ( skill.Value >= 110 ){ fp = 8; }
			else if ( skill.Value >= 100 ){ fp = 7; }
			else if ( skill.Value >= 90 ){ fp = 6; }
			else if ( skill.Value >= 80 ){ fp = 5; }
			else if ( skill.Value >= 70 ){ fp = 4; }
			else if ( skill.Value >= 60 ){ fp = 3; }
			else if ( skill.Value >= 50 ){ fp = 2; }
			else if ( skill.Value >= 40 ){ fp = 1; }
			else { fp = 0; }

			return fp;

			// return (fp - 300) / 100;
		}

		private static Skill GetShowingSkill( Mobile m, CharacterDatabase DB )
		{
			Skill skill = GetHighestSkill( m );

			if ( DB != null )
			{
				int NskillShow = DB.CharacterSkill;

				if ( NskillShow > 0 )
				{
					if ( NskillShow == 1 ){ skill = m.Skills[SkillName.Alchemy]; }
					else if ( NskillShow == 2 ){ skill = m.Skills[SkillName.Anatomy]; }
					else if ( NskillShow == 3 ){ skill = m.Skills[SkillName.AnimalLore]; }
					else if ( NskillShow == 4 ){ skill = m.Skills[SkillName.AnimalTaming]; }
					else if ( NskillShow == 5 ){ skill = m.Skills[SkillName.Archery]; }
					else if ( NskillShow == 6 ){ skill = m.Skills[SkillName.ArmsLore]; }
					else if ( NskillShow == 7 ){ skill = m.Skills[SkillName.Begging]; }
					else if ( NskillShow == 8 ){ skill = m.Skills[SkillName.Blacksmith]; }
					else if ( NskillShow == 9 ){ skill = m.Skills[SkillName.Bushido]; }
					else if ( NskillShow == 10 ){ skill = m.Skills[SkillName.Camping]; }
					else if ( NskillShow == 11 ){ skill = m.Skills[SkillName.Carpentry]; }
					else if ( NskillShow == 12 ){ skill = m.Skills[SkillName.Cartography]; }
					else if ( NskillShow == 13 ){ skill = m.Skills[SkillName.Chivalry]; }
					else if ( NskillShow == 14 ){ skill = m.Skills[SkillName.Cooking]; }
					else if ( NskillShow == 15 ){ skill = m.Skills[SkillName.DetectHidden]; }
					else if ( NskillShow == 16 ){ skill = m.Skills[SkillName.Discordance]; }
					else if ( NskillShow == 17 ){ skill = m.Skills[SkillName.EvalInt]; }
					else if ( NskillShow == 18 ){ skill = m.Skills[SkillName.Fencing]; }
					else if ( NskillShow == 19 ){ skill = m.Skills[SkillName.Fishing]; }
					else if ( NskillShow == 20 ){ skill = m.Skills[SkillName.Fletching]; }
					else if ( NskillShow == 21 ){ skill = m.Skills[SkillName.Focus]; }
					else if ( NskillShow == 22 ){ skill = m.Skills[SkillName.Forensics]; }
					else if ( NskillShow == 23 ){ skill = m.Skills[SkillName.Healing]; }
					else if ( NskillShow == 24 ){ skill = m.Skills[SkillName.Herding]; }
					else if ( NskillShow == 25 ){ skill = m.Skills[SkillName.Hiding]; }
					else if ( NskillShow == 26 ){ skill = m.Skills[SkillName.Inscribe]; }
					else if ( NskillShow == 27 ){ skill = m.Skills[SkillName.ItemID]; }
					else if ( NskillShow == 28 ){ skill = m.Skills[SkillName.Lockpicking]; }
					else if ( NskillShow == 29 ){ skill = m.Skills[SkillName.Lumberjacking]; }
					else if ( NskillShow == 30 ){ skill = m.Skills[SkillName.Macing]; }
					else if ( NskillShow == 31 ){ skill = m.Skills[SkillName.Magery]; }
					else if ( NskillShow == 32 ){ skill = m.Skills[SkillName.MagicResist]; }
					else if ( NskillShow == 33 ){ skill = m.Skills[SkillName.Meditation]; }
					else if ( NskillShow == 34 ){ skill = m.Skills[SkillName.Mining]; }
					else if ( NskillShow == 35 ){ skill = m.Skills[SkillName.Musicianship]; }
					else if ( NskillShow == 36 ){ skill = m.Skills[SkillName.Necromancy]; }
					else if ( NskillShow == 37 ){ skill = m.Skills[SkillName.Ninjitsu]; }
					else if ( NskillShow == 38 ){ skill = m.Skills[SkillName.Parry]; }
					else if ( NskillShow == 39 ){ skill = m.Skills[SkillName.Peacemaking]; }
					else if ( NskillShow == 40 ){ skill = m.Skills[SkillName.Poisoning]; }
					else if ( NskillShow == 41 ){ skill = m.Skills[SkillName.Provocation]; }
					else if ( NskillShow == 42 ){ skill = m.Skills[SkillName.RemoveTrap]; }
					else if ( NskillShow == 43 ){ skill = m.Skills[SkillName.Snooping]; }
					else if ( NskillShow == 44 ){ skill = m.Skills[SkillName.SpiritSpeak]; }
					else if ( NskillShow == 45 ){ skill = m.Skills[SkillName.Stealing]; }
					else if ( NskillShow == 46 ){ skill = m.Skills[SkillName.Stealth]; }
					else if ( NskillShow == 47 ){ skill = m.Skills[SkillName.Swords]; }
					else if ( NskillShow == 48 ){ skill = m.Skills[SkillName.Tactics]; }
					else if ( NskillShow == 49 ){ skill = m.Skills[SkillName.Tailoring]; }
					else if ( NskillShow == 50 ){ skill = m.Skills[SkillName.TasteID]; }
					else if ( NskillShow == 51 ){ skill = m.Skills[SkillName.Tinkering]; }
					else if ( NskillShow == 52 ){ skill = m.Skills[SkillName.Tracking]; }
					else if ( NskillShow == 53 ){ skill = m.Skills[SkillName.Veterinary]; }
					else if ( NskillShow == 54 ){ skill = m.Skills[SkillName.Wrestling]; }
					else { skill = GetHighestSkill( m ); }
				}
			}

			return skill;
		}

		public static string GetNPCGuild( Mobile m )
		{
			string GuildTitle = "";

			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Profession == 1 ){ GuildTitle = " & Fugitive"; }
				else if ( pm.NpcGuild == NpcGuild.MagesGuild ){ GuildTitle = " of the Wizards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.WarriorsGuild ){ GuildTitle = " of the Warriors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ThievesGuild ){ GuildTitle = " of the Thieves Guild"; }
				else if ( pm.NpcGuild == NpcGuild.RangersGuild ){ GuildTitle = " of the Rangers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.HealersGuild ){ GuildTitle = " of the Healers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MinersGuild ){ GuildTitle = " of the Miners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ GuildTitle = " of the Merchants Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TinkersGuild ){ GuildTitle = " of the Tinkers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TailorsGuild ){ GuildTitle = " of the Tailors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.FishermensGuild ){ GuildTitle = " of the Mariners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BardsGuild ){ GuildTitle = " of the Bards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildTitle = " of the Blacksmiths Guild"; }
				else if ( pm.NpcGuild == NpcGuild.NecromancersGuild ){ GuildTitle = " of the Black Magic Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildTitle = " of the Alchemists Guild"; }
				else if ( pm.NpcGuild == NpcGuild.DruidsGuild ){ GuildTitle = " of the Druids Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ArchersGuild ){ GuildTitle = " of the Archers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CarpentersGuild ){ GuildTitle = " of the Carpenters Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CartographersGuild ){ GuildTitle = " of the Cartographers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.LibrariansGuild ){ GuildTitle = " of the Librarians Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CulinariansGuild ){ GuildTitle = " of the Culinary Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AssassinsGuild ){ GuildTitle = " of the Assassins Guild"; }
			}
			else if ( m is BaseVendor )
			{
				BaseVendor pm = (BaseVendor)m;

				if ( pm.NpcGuild == NpcGuild.MagesGuild ){ GuildTitle = "Wizards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.WarriorsGuild ){ GuildTitle = "Warriors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ThievesGuild ){ GuildTitle = "Thieves Guild"; }
				else if ( pm.NpcGuild == NpcGuild.RangersGuild ){ GuildTitle = "Rangers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.HealersGuild ){ GuildTitle = "Healers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MinersGuild ){ GuildTitle = "Miners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ GuildTitle = "Merchants Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TinkersGuild ){ GuildTitle = "Tinkers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TailorsGuild ){ GuildTitle = "Tailors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.FishermensGuild ){ GuildTitle = "Mariners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BardsGuild ){ GuildTitle = "Bards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildTitle = "Blacksmiths Guild"; }
				else if ( pm.NpcGuild == NpcGuild.NecromancersGuild ){ GuildTitle = "Black Magic Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildTitle = "Alchemists Guild"; }
				else if ( pm.NpcGuild == NpcGuild.DruidsGuild ){ GuildTitle = "Druids Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ArchersGuild ){ GuildTitle = "Archers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CarpentersGuild ){ GuildTitle = "Carpenters Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CartographersGuild ){ GuildTitle = "Cartographers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.LibrariansGuild ){ GuildTitle = "Librarians Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CulinariansGuild ){ GuildTitle = "Culinary Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AssassinsGuild ){ GuildTitle = "Assassins Guild"; }
			}
			return GuildTitle;
		}

		public static string GetStatusGuild( Mobile m )
		{
			string GuildTitle = "";

			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Profession == 1 ){ GuildTitle = "The Fugitive"; }
				else if ( pm.NpcGuild == NpcGuild.MagesGuild ){ GuildTitle = "The Wizards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.WarriorsGuild ){ GuildTitle = "The Warriors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ThievesGuild ){ GuildTitle = "The Thieves Guild"; }
				else if ( pm.NpcGuild == NpcGuild.RangersGuild ){ GuildTitle = "The Rangers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.HealersGuild ){ GuildTitle = "The Healers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MinersGuild ){ GuildTitle = "The Miners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ GuildTitle = "The Merchants Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TinkersGuild ){ GuildTitle = "The Tinkers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.TailorsGuild ){ GuildTitle = "The Tailors Guild"; }
				else if ( pm.NpcGuild == NpcGuild.FishermensGuild ){ GuildTitle = "The Mariners Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BardsGuild ){ GuildTitle = "The Bards Guild"; }
				else if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildTitle = "The Blacksmiths Guild"; }
				else if ( pm.NpcGuild == NpcGuild.NecromancersGuild ){ GuildTitle = "The Black Magic Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildTitle = "The Alchemists Guild"; }
				else if ( pm.NpcGuild == NpcGuild.DruidsGuild ){ GuildTitle = "The Druids Guild"; }
				else if ( pm.NpcGuild == NpcGuild.ArchersGuild ){ GuildTitle = "The Archers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CarpentersGuild ){ GuildTitle = "The Carpenters Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CartographersGuild ){ GuildTitle = "The Cartographers Guild"; }
				else if ( pm.NpcGuild == NpcGuild.LibrariansGuild ){ GuildTitle = "The Librarians Guild"; }
				else if ( pm.NpcGuild == NpcGuild.CulinariansGuild ){ GuildTitle = "The Culinary Guild"; }
				else if ( pm.NpcGuild == NpcGuild.AssassinsGuild ){ GuildTitle = "The Assassins Guild"; }
			}
			return GuildTitle;
		}

		public static int GetPlayerLevel( Mobile m )
		{
			int fame = m.Fame;
				if ( fame > 15000){ fame = 15000; }

			int karma = m.Karma;
				if ( karma < 0 ){ karma = m.Karma * -1; }
				if ( karma > 15000){ karma = 15000; }

			int skills = m.Skills.Total;
				if ( skills > 10000){ skills = 10000; }
				skills = (int)( 1.5 * skills );			// UP TO 15,000

			int stats = m.RawStr + m.RawDex + m.RawInt;
				if ( stats > 250){ stats = 250; }
				stats = 60 * stats;						// UP TO 15,000

			int level = (int)( ( fame + karma + skills + stats ) / 600 );
				level = (int)( ( level - 10 ) * 1.12 );

			if ( level < 1 ){ level = 1; }
			if ( level > 100 ){ level = 100; }

			return level;
		}

		public static int GetPlayerDifficulty( Mobile m )
		{
			int difficulty = 0;
			int level = GetPlayerLevel( m );

			if ( level >=95 ){ difficulty = 4; }
			else if ( level >=75 ){ difficulty = 3; }
			else if ( level >=50 ){ difficulty = 2; }
			else if ( level >=25 ){ difficulty = 1; }

			return difficulty;
		}

		public static int GetResurrectCost( Mobile m )
		{
			int fame = m.Fame;
				if ( fame > 15000){ fame = 15000; }
			int karma = m.Karma * -1;
				if ( karma > 15000){ karma = 15000; }

			int skills = m.Skills.Total;
				if ( skills > 10000){ skills = 10000; }
				skills = (int)( 1.5 * skills );			// UP TO 15,000

			int stats = m.RawStr + m.RawDex + m.RawInt;
				if ( stats > 250){ stats = 250; }
				stats = 60 * stats;						// UP TO 15,000

			int level = (int)( ( fame + karma + skills + stats ) / 600 );
				level = (int)( ( level - 10 ) * 1.12 );

			if ( level < 1 ){ level = 1; }
			if ( level > 100 ){ level = 100; }

			level = ( level * 20 );

			if ( m.Skills.Total <= 2000 ){ level = 0; }
			if ( ( m.RawStr + m.RawDex + m.RawInt ) <= 90 ){ level = 0; }

			if ( ((PlayerMobile)m).Profession == 1 ){ level = level * 2; }
			else if ( GetPlayerInfo.isFromSpace( m ) ){ level = level * 3; }

			return level;
		}

		public static string GetTodaysDate()
		{
			string sYear = DateTime.Now.Year.ToString();
			string sMonth = DateTime.Now.Month.ToString();
				string sMonthName = "January";
				if ( sMonth == "2" ){ sMonthName = "February"; }
				else if ( sMonth == "3" ){ sMonthName = "March"; }
				else if ( sMonth == "4" ){ sMonthName = "April"; }
				else if ( sMonth == "5" ){ sMonthName = "May"; }
				else if ( sMonth == "6" ){ sMonthName = "June"; }
				else if ( sMonth == "7" ){ sMonthName = "July"; }
				else if ( sMonth == "8" ){ sMonthName = "August"; }
				else if ( sMonth == "9" ){ sMonthName = "September"; }
				else if ( sMonth == "10" ){ sMonthName = "October"; }
				else if ( sMonth == "11" ){ sMonthName = "November"; }
				else if ( sMonth == "12" ){ sMonthName = "December"; }
			string sDay = DateTime.Now.Day.ToString();
			string sHour = DateTime.Now.Hour.ToString();
			string sMinute = DateTime.Now.Minute.ToString();
			string sSecond = DateTime.Now.Second.ToString();

			if ( sHour.Length == 1 ){ sHour = "0" + sHour; }
			if ( sMinute.Length == 1 ){ sMinute = "0" + sMinute; }
			if ( sSecond.Length == 1 ){ sSecond = "0" + sSecond; }

			string sDateString = sMonthName + " " + sDay + ", " + sYear + " at " + sHour + ":" + sMinute;

			return sDateString;
		}

		public static bool LuckyPlayer( int luck )
		{
			if ( luck <= 0 )
				return false;

			if ( luck > 2000 )
				luck = 2000;

			int clover = (int)(luck * 0.04); // RETURNS A MAX OF 80%

			if ( clover >= Utility.RandomMinMax( 1, 100 ) )
				return true;

			return false;
		}

		public static bool LuckyKiller( int luck )
		{
			if ( luck <= 0 )
				return false;

			if ( luck > 2000 )
				luck = 2000;

			int clover = (int)(luck * 0.02) + 10; // RETURNS A MAX OF 50%

			if ( clover >= Utility.RandomMinMax( 1, 100 ) )
				return true;

			return false;
		}

		public static bool EvilPlayer( Mobile m )
		{
			if ( m is BaseCreature )
				m = ((BaseCreature)m).GetMaster();

			if ( m is PlayerMobile )
			{
				if ( m.AccessLevel > AccessLevel.Player )
					return true;

				if ( m.Skills[SkillName.Necromancy].Base >= 50.0 && m.Karma < 0 ) // NECROMANCERS
					return true;

				if ( m.Skills[SkillName.Forensics].Base >= 80.0 && m.Karma < 0 ) // UNDERTAKERS
					return true;

				if ( m.Skills[SkillName.Chivalry].Base >= 50.0 && m.Karma <= -5000 ) // DEATH KNIGHTS
					return true;

				if ( m.Skills[SkillName.EvalInt].Base >= 50.0 && m.Skills[SkillName.Swords].Base >= 50.0 && m.Karma <= -5000 && Server.Misc.GetPlayerInfo.isSyth(m,false) ) // SYTH
					return true;
			}

			return false;
		}

		public static int LuckyPlayerArtifacts( int luck )
		{
			if ( luck > 2000 )
				luck = 2000;

			int clover = (int)(luck * 0.005); // RETURNS A MAX OF 10

			return clover;
		}

		public static bool OrientalPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterOriental == 1 )
					return true;
			}
			else if ( m != null && m is BaseCreature )
			{
				Mobile killer = m.LastKiller;
				if (killer is BaseCreature)
				{
					BaseCreature bc_killer = (BaseCreature)killer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							killer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							killer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							killer=bc_killer.BardMaster;
					}
				}

				if ( killer != null && killer is PlayerMobile )
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( killer );

					if ( DB.CharacterOriental == 1 )
						return true;
				}
				else
				{
					Mobile hitter = m.FindMostRecentDamager(true);
					if (hitter is BaseCreature)
					{
						BaseCreature bc_killer = (BaseCreature)hitter;
						if(bc_killer.Summoned)
						{
							if(bc_killer.SummonMaster != null)
								hitter = bc_killer.SummonMaster;
						}
						else if(bc_killer.Controlled)
						{
							if(bc_killer.ControlMaster != null)
								hitter=bc_killer.ControlMaster;
						}
						else if(bc_killer.BardProvoked)
						{
							if(bc_killer.BardMaster != null)
								hitter=bc_killer.BardMaster;
						}
					}

					if ( hitter != null && hitter is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( hitter );

						if ( DB.CharacterOriental == 1 )
							return true;
					}
				}
			}

			return false;
		}

		public static int BarbaricPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterBarbaric > 0 )
					return DB.CharacterBarbaric;
			}

			return 0;
		}

		public static bool EvilPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterEvil == 1 )
					return true;
			}
			else if ( m != null && m is BaseCreature )
			{
				Mobile killer = m.LastKiller;
				if (killer is BaseCreature)
				{
					BaseCreature bc_killer = (BaseCreature)killer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							killer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							killer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							killer=bc_killer.BardMaster;
					}
				}

				if ( killer != null && killer is PlayerMobile )
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( killer );

					if ( DB.CharacterEvil == 1 )
						return true;
				}
				else
				{
					Mobile hitter = m.FindMostRecentDamager(true);
					if (hitter is BaseCreature)
					{
						BaseCreature bc_killer = (BaseCreature)hitter;
						if(bc_killer.Summoned)
						{
							if(bc_killer.SummonMaster != null)
								hitter = bc_killer.SummonMaster;
						}
						else if(bc_killer.Controlled)
						{
							if(bc_killer.ControlMaster != null)
								hitter=bc_killer.ControlMaster;
						}
						else if(bc_killer.BardProvoked)
						{
							if(bc_killer.BardMaster != null)
								hitter=bc_killer.BardMaster;
						}
					}

					if ( hitter != null && hitter is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( hitter );

						if ( DB.CharacterEvil == 1 )
							return true;
					}
				}
			}

			return false;
		}

		public static int GetWealth( Mobile from, int pack )
		{
			int wealth = 0;

			Container bank = from.FindBankNoCreate();
				if ( pack > 0 ){ bank = from.Backpack; }

			if ( bank != null )
			{
				Item[] gold = bank.FindItemsByType( typeof( Gold ) );
				Item[] checks = bank.FindItemsByType( typeof( BankCheck ) );
				Item[] silver = bank.FindItemsByType( typeof( DDSilver ) );
				Item[] copper = bank.FindItemsByType( typeof( DDCopper ) );
				Item[] xormite = bank.FindItemsByType( typeof( DDXormite ) );
				Item[] jewels = bank.FindItemsByType( typeof( DDJewels ) );
				Item[] crystals = bank.FindItemsByType( typeof( Crystals ) );
				Item[] gems = bank.FindItemsByType( typeof( DDGemstones ) );
				Item[] nuggets = bank.FindItemsByType( typeof( DDGoldNuggets ) );

				for ( int i = 0; i < gold.Length; ++i )
					wealth += gold[i].Amount;

				for ( int i = 0; i < checks.Length; ++i )
					wealth += ((BankCheck)checks[i]).Worth;

				for ( int i = 0; i < silver.Length; ++i )
					wealth += (int)Math.Floor((decimal)(silver[i].Amount / 5));

				for ( int i = 0; i < copper.Length; ++i )
					wealth += (int)Math.Floor((decimal)(copper[i].Amount / 10));

				for ( int i = 0; i < xormite.Length; ++i )
					wealth += (xormite[i].Amount)*3;

				for ( int i = 0; i < crystals.Length; ++i )
					wealth += (crystals[i].Amount)*5;

				for ( int i = 0; i < jewels.Length; ++i )
					wealth += (jewels[i].Amount)*2;

				for ( int i = 0; i < gems.Length; ++i )
					wealth += (gems[i].Amount)*2;

				for ( int i = 0; i < nuggets.Length; ++i )
					wealth += (nuggets[i].Amount);
			}

			return wealth;
		}
	}
}

namespace Server.Gumps 
{
    public class StatsGump : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "status", AccessLevel.Player, new CommandEventHandler( MyStats_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "status" )]
		[Description( "Opens Stats Gump." )]
		public static void MyStats_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( StatsGump ) );
			from.SendGump( new StatsGump( from, 0 ) );
        }
   
        public StatsGump ( Mobile from, int origin ) : base ( 25,25 )
        {
			m_Origin = origin;

            int LRCCap = 100;
            int LMCCap = 40;
            double BandageSpeedCap = 5.0;
            int SwingSpeedCap = 100;
            int HCICap = 45;
            int DCICap = 45;
            int FCCap = 4; // FC 4 For Paladin, otherwise FC 2 for Mage
            int FCRCap = 4;
            int DamageIncreaseCap = 100;
            int SDICap = 1000000;
				if ( SDICap > Server.Misc.MyServerSettings.SpellDamageIncreaseVsMonsters() && Server.Misc.MyServerSettings.SpellDamageIncreaseVsMonsters() > 0 ){ SDICap = Server.Misc.MyServerSettings.SpellDamageIncreaseVsMonsters(); }
            int ReflectDamageCap = 100;
            int SSICap = 100;
            
            int LRC = AosAttributes.GetValue( from, AosAttribute.LowerRegCost ) > LRCCap ? LRCCap : AosAttributes.GetValue( from, AosAttribute.LowerRegCost );
            int LMC = AosAttributes.GetValue( from, AosAttribute.LowerManaCost ) > LMCCap ? LMCCap : AosAttributes.GetValue( from, AosAttribute.LowerManaCost );
            double BandageSpeed = ( 5.0 + (0.5 * ((double)(120 - from.Dex) / 10)) ) < BandageSpeedCap ? BandageSpeedCap : ( 5.0 + (0.5 * ((double)(120 - from.Dex) / 10)) );
            TimeSpan SwingSpeed = (from.Weapon as BaseWeapon).GetDelay(from) > TimeSpan.FromSeconds(SwingSpeedCap) ? TimeSpan.FromSeconds(SwingSpeedCap) : (from.Weapon as BaseWeapon).GetDelay(from);
            int HCI = AosAttributes.GetValue( from, AosAttribute.AttackChance ) > HCICap ? HCICap : AosAttributes.GetValue( from, AosAttribute.AttackChance );
            int DCI = AosAttributes.GetValue( from, AosAttribute.DefendChance ) > DCICap ? DCICap : AosAttributes.GetValue( from, AosAttribute.DefendChance );
            int FC = AosAttributes.GetValue( from, AosAttribute.CastSpeed ) > FCCap ? FCCap : AosAttributes.GetValue( from, AosAttribute.CastSpeed );
            int FCR = AosAttributes.GetValue( from, AosAttribute.CastRecovery ) > FCRCap ? FCRCap : AosAttributes.GetValue( from, AosAttribute.CastRecovery );
            int DamageIncrease = AosAttributes.GetValue( from, AosAttribute.WeaponDamage ) > DamageIncreaseCap ? DamageIncreaseCap : AosAttributes.GetValue( from, AosAttribute.WeaponDamage );
            int SDI = AosAttributes.GetValue( from, AosAttribute.SpellDamage ) > SDICap ? SDICap : AosAttributes.GetValue( from, AosAttribute.SpellDamage );
            int ReflectDamage = AosAttributes.GetValue( from, AosAttribute.ReflectPhysical ) > ReflectDamageCap ? ReflectDamageCap : AosAttributes.GetValue( from, AosAttribute.ReflectPhysical );
            int SSI = AosAttributes.GetValue( from, AosAttribute.WeaponSpeed ) > SSICap ? SSICap : AosAttributes.GetValue( from, AosAttribute.WeaponSpeed );
            int HealCost = GetPlayerInfo.GetResurrectCost( from );
			int CharacterLevel = GetPlayerInfo.GetPlayerLevel( from );
            int EP = BasePotion.EnhancePotions( from );

			AddPage(0);
			AddImage(300, 300, 155);
			AddImage(0, 300, 155);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(600, 0, 155);
			AddImage(600, 300, 155);
			AddImage(794, 0, 155);
			AddImage(794, 300, 155);

			AddImage(2, 2, 129);
			AddImage(302, 2, 129);
			AddImage(2, 298, 129);
			AddImage(598, 2, 129);
			AddImage(598, 298, 129);
			AddImage(301, 298, 129);
			AddImage(792, 2, 129);
			AddImage(792, 298, 129);

			AddImage(5, 8, 145);
			AddImage(7, 354, 142);
			AddImage(324, 563, 140);
			AddImage(433, 29, 140);
			AddImage(175, 29, 140);
			AddImage(165, 7, 156);
			AddImage(191, 7, 156);
			AddImage(219, 5, 156);
			AddImage(698, 7, 156);
			AddImage(519, 563, 140);
			AddImage(748, 381, 144);
			AddImage(893, 7, 146);
			AddImage(630, 29, 140);
			AddImage(893, 6, 156);

			string title = "CHARACTER SHEET";
			if ( m_Origin > 0 ){ title = "PLAYERS HANDBOOK"; }

			AddHtml( 166, 60, 203, 29, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 472, 60, 432, 29, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + from.Name + " the " + GetPlayerInfo.GetSkillTitle( from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(151, 113, 3823);
			AddHtml( 190, 116, 57, 19, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 116, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + Banker.GetBalance( from ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 96, 156, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Strength</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 196, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Dexterity</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 236, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Intelligence</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 276, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Fame</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 316, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Karma</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 356, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Tithing Points</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 396, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hunger</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 436, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Thirst</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 476, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Enhance Potions</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 236, 156, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.RawStr, from.Str - from.RawStr ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 196, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.RawDex, from.Dex - from.RawDex ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 236, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.RawInt, from.Int - from.RawInt ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 276, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Fame ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 316, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Karma ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 356, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.TithingPoints ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 396, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Hunger ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 436, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Thirst ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 476, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", EP ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 406, 116, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Level</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 156, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hits</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 196, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Stamina</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 236, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Mana</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 276, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hits Regen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 316, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Stamina Regen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 356, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Mana Regen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 396, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Low Reagent</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 436, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Low Mana</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 476, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Resurrect Cost</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 516, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Murders</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 546, 116, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", CharacterLevel ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 156, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.Hits - AosAttributes.GetValue( from, AosAttribute.BonusHits ), AosAttributes.GetValue( from, AosAttribute.BonusHits ) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 196, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.Stam - AosAttributes.GetValue( from, AosAttribute.BonusStam ), AosAttributes.GetValue( from, AosAttribute.BonusStam ) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 236, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.Mana - AosAttributes.GetValue( from, AosAttribute.BonusMana ), AosAttributes.GetValue( from, AosAttribute.BonusMana ) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 276, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", AosAttributes.GetValue( from, AosAttribute.RegenHits ) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 316, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", AosAttributes.GetValue( from, AosAttribute.RegenStam ) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 356, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", AosAttributes.GetValue( from, AosAttribute.RegenMana ) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 396, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", LRC ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 436, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", LMC ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 476, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} Gold", HealCost ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 516, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Kills) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 723, 116, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hit Chance</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 156, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Defend Chance</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 196, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Swing Speed</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 236, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Swing Speed +</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 276, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Bandage Speed</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 316, 169, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Damage Increase</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 356, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Reflect Damage</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 396, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Fast Cast</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 436, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Cast Recovery</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 476, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Spell Damage +</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 863, 116, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", HCI ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 156, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", DCI ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 196, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}s", new DateTime(SwingSpeed.Ticks).ToString("s.ff") ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 236, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", SSI ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 276, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0:0.0}s", new DateTime(TimeSpan.FromSeconds( BandageSpeed ).Ticks).ToString("s.ff") ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 316, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", DamageIncrease ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 356, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", ReflectDamage ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 396, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", FC ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 436, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", FCR ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 476, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", SDI ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( m_Origin != 1 )
			{
				AddButton(1015, 233, 236, 236, 1, GumpButtonType.Reply, 0);
				AddHtml( 1007, 307, 71, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>Refresh</CENTER></BIG></BASEFONT></BODY></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else
			{
				AddImage(1000, 240, 11424);
			}
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			switch ( info.ButtonID )
			{
				case 1:
				{
					from.SendSound( 0x4A );
					from.CloseGump( typeof( StatsGump ) );
					from.SendGump( new StatsGump( from, m_Origin ) );
					break;
				}
			}
		}
    }
}

namespace Server.Engines.Quests
{
	public class QuestButton
	{
		public static void Initialize()
		{
			EventSink.QuestGumpRequest += new QuestGumpRequestHandler( EventSink_QuestGumpRequest );
		}

		public static void EventSink_QuestGumpRequest( QuestGumpRequestArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( StatsGump ) );
			from.SendGump( new StatsGump( from, 0 ) );
        }
	}
}