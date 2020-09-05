using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server;
using System.Collections.Generic;
using System.Collections;
using System;
using Server.Commands;
using Server.Commands.Generic;
using Server.Accounting;
using Server.Regions;

namespace Server.Gumps
{
	public class ResurrectCostGump : Gump
	{
		private int m_Price;
		private int m_Healer;
		private int m_Bank;
		private int m_ResurrectType;

		public ResurrectCostGump( Mobile owner, int healer ) : base( 150, 50 )
		{
			m_Healer = healer;
			m_Price = GetPlayerInfo.GetResurrectCost( owner );
			m_Bank = Banker.GetBalance( owner );
			m_ResurrectType = 0;

			string sText = "";

			string c1 = "5";
			string c2 = "10";
			string loss = "";

			if ( GetPlayerInfo.isFromSpace( owner ) )
			{
				loss = " If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
				c1 = "10";
				c2 = "20";
			}

			if ( m_Price > 0 )
			{
				if ( m_Price > m_Bank )
				{
					if ( m_Healer < 2 )
					{
						sText = "You currently do not have enough gold in the bank to provide an offering to the healer. Do you wish to plead to the healer for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
					}
					else
					{
						sText = "You currently do not have enough gold in the bank to provide an offering to the shrine. Do you wish to plead to the gods for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";

						if ( m_Healer == 3 )
						{
							sText = "You currently do not have enough gold in the bank to provide an offering to Azrael. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
						}
						else if ( m_Healer == 4 )
						{
							sText = "You currently do not have enough gold in the bank to provide an offering to the Reaper. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
						}
						else if ( m_Healer == 5 )
						{
							sText = "You currently do not have enough gold in the bank to provide an offering to the goddess of the sea. Do you wish to plead to Amphitrite for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
						}
						else if ( m_Healer == 6 )
						{
							sText = "You currently do not have enough gold in the bank to provide an offering to the Archmages. Do you wish to plead to the Archmages for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
						}
						else if ( m_Healer == 7 )
						{
							sText = "You currently do not have enough gold in the bank to provide an offering to Sin'Vraal. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
						}
					}
					m_ResurrectType = 1;
				}
				else
				{
					if ( m_Healer < 2 )
					{
						sText = "You currently have enough gold in the bank to provide an offering to the healer. Do you wish to offer the tribute to the healer for your life back?." + loss;
					}
					else
					{
						sText = "You currently have enough gold in the bank to provide an offering to the shrine. Do you wish to offer the tribute to the gods for your life back?." + loss;

						if ( m_Healer == 3 )
						{
							sText = "Azrael is not ready for your soul just yet, and you currently have enough gold in the bank to provide an offering to him. Do you wish to offer the tribute to Azrael for your life back?." + loss;
						}
						else if ( m_Healer == 4 )
						{
							sText = "Although the Reaper would gladly take your soul, he thinks your time has come to an end too soon. You currently have enough gold in the bank to provide an offering to the Reaper. Do you wish to offer the tribute to him for your life back?." + loss;
						}
						else if ( m_Healer == 5 )
						{
							sText = "You currently have enough gold in the bank to provide an offering to the goddess of the sea. Do you wish to offer the tribute to Amphitrite for your life back?." + loss;
						}
						else if ( m_Healer == 6 )
						{
							sText = "You currently have enough gold in the bank to provide an offering to the Archmages. Do you wish to offer the tribute to the Archmages for your life back?." + loss;
						}
						else if ( m_Healer == 7 )
						{
							sText = "You currently have enough gold in the bank to provide an offering to Sin'Vraal. Do you wish to offer the tribute to Sin'Vraal for your life back?." + loss;
						}
					}
					m_ResurrectType = 2;
				}
			}
			else
			{
				if ( m_Healer < 2 )
				{
					sText = "Do you wish to have the healer return you to life?.";
				}
				else
				{
					sText = "Do you wish to have the gods return you to life?.";

					if ( m_Healer == 3 )
					{
						sText = "Do you wish to have Azrael return you to life?.";
					}
					else if ( m_Healer == 4 )
					{
						sText = "Do you wish to have the Reaper return you to life?.";
					}
					else if ( m_Healer == 5 )
					{
						sText = "Do you wish to have Amphitrite return you to life?.";
					}
					else if ( m_Healer == 6 )
					{
						sText = "Do you wish to have the Archmages return you to life?.";
					}
					else if ( m_Healer == 7 )
					{
						sText = "Do you wish to have Sin'Vraal return you to life?.";
					}
				}
			}

			string sGrave = "RETURN TO THE LIVING";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sGrave = "YOUR LIFE BACK";			break;
				case 1:	sGrave = "YOUR RESURRECTION";		break;
				case 2:	sGrave = "RETURN TO THE LIVING";	break;
				case 3:	sGrave = "RETURN FROM THE DEAD";	break;
			}

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddImage(0, 0, 154);
			AddImage(300, 201, 154);
			AddImage(0, 201, 154);
			AddImage(300, 0, 154);
			AddImage(298, 199, 129);
			AddImage(2, 199, 129);
			AddImage(298, 2, 129);
			AddImage(2, 2, 129);
			AddImage(7, 6, 145);
			AddImage(8, 257, 142);
			AddImage(253, 285, 144);
			AddImage(171, 47, 132);
			AddImage(379, 8, 134);
			AddImage(167, 7, 156);
			AddImage(209, 11, 156);
			AddImage(189, 10, 156);
			AddImage(170, 44, 159);

			AddButton(162, 365, 4023, 4023, 1, GumpButtonType.Reply, 0);
			AddButton(389, 365, 4020, 4020, 2, GumpButtonType.Reply, 0);

			if ( m_Price > 0 )
			{
				AddHtml( 101, 271, 190, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Resurrection Tribute</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 271, 116, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + String.Format("{0} Gold", m_Price ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 101, 305, 190, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Gold in the Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 305, 116, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + String.Format("{0} Gold", m_Bank ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 177, 90, 400, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 100, 155, 477, 103, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		private static void ResurrectNow( object state )
		{
			Mobile m = state as Mobile;
			m.CloseGump( typeof( ResurrectNowGump ) );
			m.SendGump( new ResurrectNowGump( m ) );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.CloseGump( typeof( ResurrectCostGump ) );

			if( info.ButtonID == 1 )
			{
				if( from.Map == null || !from.Map.CanFit( from.Location, 16, false, false ) )
				{
					from.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					return;
				}

				if ( m_ResurrectType == 2 && Banker.Withdraw( from, m_Price ) )
				{
					from.SendLocalizedMessage( 1060398, m_Price.ToString() ); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
					from.SendLocalizedMessage( 1060022, Banker.GetBalance( from ).ToString() ); // You have ~1_AMOUNT~ gold in cash remaining in your bank box.
					Server.Misc.Death.Penalty( from, false );
				}
				else if ( m_ResurrectType == 1 && from.SkillsTotal > 200 && ( from.RawDex + from.RawInt + from.RawStr ) > 90 )
				{
					Server.Misc.Death.Penalty( from, true );
				}

				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				from.Resurrect();

				from.Hits = from.HitsMax;
				from.Stam = from.StamMax;
				from.Mana = from.ManaMax;
				from.Hidden = true;
			}
			else
			{
				from.SendMessage( "You decide to remain in the spirit realm." );
				Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), ResurrectNow, from );
				return;
			}
		}
	}
}

namespace Server 
{ 
	public class AutoRessurection
	{ 
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);
		}

		private static void EventSink_PlayerDeath(PlayerDeathEventArgs e)
		{
			Mobile m = e.Mobile;

			if ( ( m != null ) && ( !m.Alive ) )
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), ResurrectNow, m );
			}
		}

		private static void ResurrectNow( object state )
		{
			Mobile m = state as Mobile;
			m.CloseGump( typeof( ResurrectNowGump ) );
			m.SendGump( new ResurrectNowGump( m ) );
		}
	}
}

namespace Server.Gumps
{
	public class ResurrectNowGump : Gump
	{
		public ResurrectNowGump( Mobile from ): base( 50, 20 )
		{
            int HealCost = GetPlayerInfo.GetResurrectCost( from );
			int BankGold = Banker.GetBalance( from );

			string sText = "Do you wish to plead to the gods for your life back now? You may also continue on in your spirit form and seek out a shrine or healer.";
			bool ResPenalty = false;

			string c1 = "5";
			string c2 = "10";
			string loss = "";

			if ( GetPlayerInfo.isFromSpace( from ) )
			{
				loss = " If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
				c1 = "10";
				c2 = "20";
			}

			if ( from.SkillsTotal > 200 && ( from.RawDex + from.RawInt + from.RawStr ) > 90 )
			{
				ResPenalty = true;

				if ( BankGold >= HealCost )
					sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills. You have enough gold in the bank to offer the resurrection tribute, so perhaps you may want to find a shrine or healer instead of suffering the penalties.";
				else
					sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills. You cannot afford the resurrection tribute due to the lack of gold in the bank, so perhaps you may want to do this.";
			}

			string sGrave = "YOU HAVE DIED!";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sGrave = "YOU HAVE DIED!";			break;
				case 1:	sGrave = "YOU HAVE PERISHED!";		break;
				case 2:	sGrave = "YOU MET YOUR END!";		break;
				case 3:	sGrave = "YOUR LIFE HAS ENDED!";	break;
			}

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddImage(0, 0, 154);
			AddImage(300, 201, 154);
			AddImage(0, 201, 154);
			AddImage(300, 0, 154);
			AddImage(298, 199, 129);
			AddImage(2, 199, 129);
			AddImage(298, 2, 129);
			AddImage(2, 2, 129);
			AddImage(7, 6, 145);
			AddImage(8, 257, 142);
			AddImage(253, 285, 144);
			AddImage(171, 47, 132);
			AddImage(379, 8, 134);
			AddImage(167, 7, 156);
			AddImage(209, 11, 156);
			AddImage(189, 10, 156);
			AddImage(170, 44, 159);

			AddItem(173, 64, 4455);
			AddItem(186, 85, 3810);
			AddItem(209, 102, 3808);

			AddButton(162, 365, 4023, 4023, 1, GumpButtonType.Reply, 0);
			AddButton(389, 365, 4020, 4020, 2, GumpButtonType.Reply, 0);

			if ( ResPenalty )
			{
				AddHtml( 101, 271, 190, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Resurrection Tribute</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 271, 116, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>" + String.Format("{0} Gold", HealCost ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 101, 305, 190, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gold in the Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 305, 116, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>" + Banker.GetBalance( from ).ToString() + " Gold</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 267, 95, 306, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 100, 155, 477, 103, @"<BODY><BASEFONT Color=#FF0000><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.CloseGump( typeof( ResurrectNowGump ) );

			if ( info.ButtonID == 1 && !from.Alive )
			{
				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				from.Resurrect();

				if ( from.SkillsTotal > 200 && ( from.RawDex + from.RawInt + from.RawStr ) > 90 )
				{
					Server.Misc.Death.Penalty( from, true );
				}

				from.Hits = from.HitsMax;
				from.Stam = from.StamMax;
				from.Mana = from.ManaMax;
				from.Hidden = true;
			}
			else { return; }
		}
	}
}

namespace Server.Misc
{
    class Death
    {
        public static void Penalty( Mobile from, bool allPenalty )
        {
			if ( from is PlayerMobile && ( ( GetPlayerInfo.isFromSpace( from ) && !allPenalty ) || allPenalty ) )
			{
				double val1 = 0.10;
				double val2 = 0.95;

				if ( GetPlayerInfo.isFromSpace( from ) && allPenalty )
				{
					val1 = 0.20;
					val2 = 0.90;
				}

				if( from.Fame > 0 ) // 10% FAME LOSS
				{
					int amount = (int)(from.Fame * val1);
					if ( from.Fame - amount < 0 ){ amount = from.Fame; }
						if ( from.Fame < 1 ){ from.Fame = 0; }
					Misc.Titles.AwardFame( from, -amount, true );
				}

				if( from.Karma > 0 ) // 10% KARMA LOSS
				{
					int amount = (int)(from.Karma * val1);
					if ( from.Karma - amount < 0 ){ amount = from.Karma; }
						if ( from.Karma < 1 ){ from.Karma = 0; }
					Misc.Titles.AwardKarma( from, -amount, true );
				}

				double loss = val2;

				if( from.RawStr * loss > 10 )
					from.RawStr = (int)(from.RawStr * loss);
						if ( from.RawStr < 10 ){ from.RawStr = 10; }
				if( from.RawInt * loss > 10 )
					from.RawInt = (int)(from.RawInt * loss);
						if ( from.RawInt < 10 ){ from.RawInt = 10; }
				if( from.RawDex * loss > 10 )
					from.RawDex = (int)(from.RawDex * loss);
						if ( from.RawDex < 10 ){ from.RawDex = 10; }

				for( int s = 0; s < from.Skills.Length; s++ )
				{
					if( from.Skills[s].Base * loss > 35 )
						from.Skills[s].Base *= loss;
				}
			}
		}
	}
}

namespace Server.Misc
{
    class GhostHelper
    {
        public static void OnGhostWalking( Mobile from )
        {
			Map map = from.Map;

			if ( map == null )
				return;

			int range = 1000; // 1000 TILES AWAY
			int HowFarAway = 0;
			int TheClosest = 1000000;
			int IsClosest = 0;
			int distchk = 0;
			int distpck = 0;

			ArrayList healers = new ArrayList();
			foreach ( Mobile healer in from.GetMobilesInRange( range ) )
			if ( healer is BaseHealer )
			{
				bool WillResurrectMe = true;

				Region reg = Region.Find( healer.Location, healer.Map );

				if ( healer is WanderingHealer || healer is EvilHealer )
				{
					WillResurrectMe = true;
				}
				else if ( ( reg.IsPartOf( "Xardok's Castle" ) || reg.IsPartOf( "the Undercity of Umbra" ) ) && ( from.Karma < 0 || from.Kills > 0 || from.Criminal ) )
				{
					WillResurrectMe = true;
				}
				else if ( from.Criminal || from.Kills > 0 || from.Karma < 0 )
				{
					WillResurrectMe = false;
				}

				if ( SameArea( from, healer ) == true && WillResurrectMe == true )
				{
					distchk++;
					healers.Add( healer ); 
					if ( HowFar( from.X, from.Y, healer.X, healer.Y ) < TheClosest ){ TheClosest = HowFar( from.X, from.Y, healer.X, healer.Y ); IsClosest = distchk; }
				}
			}

			int crim = 0;

			foreach ( Item shrine in from.GetItemsInRange( range ) )
			if ( shrine is AnkhWest || shrine is AnkhNorth || /* shrine is AnkhOfSacrificeAddon || */ shrine is AltarEvil || shrine is AltarWizard || shrine is AltarGargoyle || shrine is AltarDaemon || shrine is AltarSea || shrine is AltarStatue || shrine is AltarShrineSouth || shrine is AltarShrineEast || shrine is AltarGodsSouth || shrine is AltarGodsEast )
			{
				Region spot = Region.Find( shrine.Location, shrine.Map );

				crim = 0;

				if ( spot.IsPartOf( typeof( VillageRegion ) ) && from.Criminal == true ){ crim = 1; }

				if ( crim == 0 )
				{
					Mobile mSp = new ShrineCritter();
					mSp.MoveToWorld(new Point3D(shrine.X, shrine.Y, shrine.Z), shrine.Map);
					if ( SameArea( from, mSp ) == true )
					{
						distchk++;
						healers.Add( mSp ); 
						if ( HowFar( from.X, from.Y, mSp.X, mSp.Y ) < TheClosest ){ TheClosest = HowFar( from.X, from.Y, mSp.X, mSp.Y ); IsClosest = distchk; }
					}
				}
			}

			for ( int h = 0; h < healers.Count; ++h )
			{
				distpck++;
				if ( distpck == IsClosest )
				{
					Mobile theHealer = ( Mobile )healers[ h ];
					HowFarAway = HowFar( from.X, from.Y, theHealer.X, theHealer.Y );
					from.QuestArrow = new GhostArrow( from, theHealer, HowFarAway*2 );
				}
			}
		}

		public static int HowFar( int x1, int y1, int x2, int y2 )
		{
            int xDelta = Math.Abs(x1 - x2);
            int yDelta = Math.Abs(y1 - y2);
            return (int)(Math.Sqrt(Math.Pow(xDelta, 2) + Math.Pow(yDelta, 2)));
		}

		public static bool SameArea( Mobile from, Mobile healer )
		{
			Map map = from.Map;
			Map mup = Map.Internal;

			int x = 9000;
			int y = 9000;
			string region = "";

			if ( healer != null ){ x = healer.X; y = healer.Y; region = Server.Misc.Worlds.GetRegionName( healer.Map, healer.Location ); mup = healer.Map; }

			Point3D location = new Point3D( from.X, from.Y, from.Z );
			Point3D loc = new Point3D( x, y, 0 );

			if ( Worlds.IsPlayerInTheLand( map, location, from.X, from.Y ) == true && Worlds.IsPlayerInTheLand( mup, loc, loc.X, loc.Y ) == true && map == mup ) // THEY ARE IN THE SAME LAND
				return true;

			else if ( region == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) ) // THEY ARE IN THE SAME REGION
				return true;

            return false;
		}
	}

	public class GhostArrow : QuestArrow
	{
		private Mobile m_From;
		private Timer m_Timer;
		private Mobile m_Target;

		public GhostArrow( Mobile from, Mobile target, int range ) : base( from, target )
		{
			m_From = from;
			m_Target = target;
			m_Timer = new GhostTimer( from, target, range, this );
			m_Timer.Start();
		}

		public override void OnClick( bool rightClick )
		{
			if ( rightClick )
			{
				m_From = null;
				Stop();
			}
		}

		public override void OnStop()
		{
			m_Timer.Stop();
		}
	}

	public class GhostTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_Range;
		private int m_LastX, m_LastY;
		private QuestArrow m_Arrow;

		public GhostTimer( Mobile from, Mobile target, int range, QuestArrow arrow ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 2.5 ) )
		{
			m_From = from;
			m_Target = target;
			m_Range = range;

			m_Arrow = arrow;
		}

		protected override void OnTick()
		{
			if ( !m_Arrow.Running )
			{
				Stop();
				return;
			}
			else if ( m_From.NetState == null || m_From.Alive || m_From.Deleted || m_Target.Deleted || !m_From.InRange( m_Target, m_Range ) || GhostHelper.SameArea( m_From, m_Target ) == false )
			{
				m_Arrow.Stop();
				Stop();
				if ( !m_From.Alive ){ GhostHelper.OnGhostWalking( m_From ); }
				return;
			}

			if ( m_LastX != m_Target.X || m_LastY != m_Target.Y )
			{
				m_LastX = m_Target.X;
				m_LastY = m_Target.Y;

				m_Arrow.Update();
			}
		}
	}
}

namespace Server.Mobiles
{
	[CorpseName( "a mouse corpse" )]
	public class ShrineCritter : BaseCreature
	{
		[Constructable]
		public ShrineCritter() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mouse";
			Body = 0;
			BaseSoundID = 0;
			Hidden = true;
			CantWalk = true;
			Timer.DelayCall( TimeSpan.FromMinutes( 10.0 ), new TimerCallback( Delete ) );

			SetSkill( SkillName.Hiding, 500.0 );
			SetSkill( SkillName.Stealth, 500.0 );
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public ShrineCritter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( Delete ) );
		}
	}
}