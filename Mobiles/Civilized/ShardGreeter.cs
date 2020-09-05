using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Mobiles
{
    public class ShardGreeter : BasePerson
	{
		public override bool ClickTitle{ get{ return false; } }
		public override bool IsInvulnerable{ get{ return true; } }

		private static bool m_Talked; 

		string[] kfcsay = new string[]  
		{ 
			"Welcome to Ultima Odyssey, your path to adventure.",
		};

		[Constructable]
		public ShardGreeter() : base( )
		{
			SpeechHue = Utility.RandomTalkHue();
			NameHue = -1;
			Body = 0x190;
			Hue = 0x430;

			Name = "the Time Lord";

			AddItem( new Sandals() );
			AddItem( new ClothCowl() );
			AddItem( new SorcererRobe() );

			Direction = Direction.South;
			CantWalk = true;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{                                                    
			if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 4 ) ) 
				{                
					m_Talked = true; 
					SayRandom( kfcsay, this ); 
					this.Move( GetDirectionTo( m.Location ) );
					chatTimer t = new chatTimer(); 
					t.Start(); 
				} 
			} 
		} 

		private class chatTimer : Timer 
		{ 
			public chatTimer() : base( TimeSpan.FromSeconds( 20 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
			} 

			protected override void OnTick() 
			{ 
				m_Talked = false; 
			} 
		} 

		private static void SayRandom( string[] say, Mobile m ) 
		{ 
			m.Say( say[Utility.Random( say.Length )] ); 
		}

		public ShardGreeter(Serial serial) : base(serial)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new ShardGreeterEntry( from, this ) ); 
		} 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class ShardGreeterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;

			public ShardGreeterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				if( !( m_Mobile is PlayerMobile ) )
					return;

				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
				{
					mobile.SendGump(new SpeechGump( "Welcome Brave Adventurer", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "ShardGreeter" ) ));
				}
			}
		}
	}
}