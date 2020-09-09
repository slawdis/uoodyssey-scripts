using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;

namespace Server.Mobiles
{
	public class WanderingHealer : BaseHealer
	{
		public override bool CanTeach{ get{ return true; } }

		public override bool CheckTeach( SkillName skill, Mobile from )
		{
			if ( !base.CheckTeach( skill, from ) )
				return false;

			return ( skill == SkillName.Anatomy )
				|| ( skill == SkillName.Camping )
				|| ( skill == SkillName.Forensics )
				|| ( skill == SkillName.Healing )
				|| ( skill == SkillName.SpiritSpeak );
		}

		[Constructable]
		public WanderingHealer()
		{
			Title = "the wandering healer";

			AddItem( new GnarledStaff() );

			SetSkill( SkillName.Camping, 80.0, 100.0 );
			SetSkill( SkillName.Forensics, 80.0, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 80.0, 100.0 );
		}

		public virtual int GetRobeColor()
		{
			return Utility.RandomYellowHue();
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( GetRobeColor() ) );
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
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Thou Art Going To Get Hurt", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Healer" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public override void OnThink()
		{
			bool needsPlacing = false;

			if ( this.X >= 0 && this.Y >= 0 && this.X <= 6 && this.Y <= 6 && this.Map == Map.Lodor ){ needsPlacing = true; }
			else if ( this.X >= 0 && this.Y >= 0 && this.X <= 6 && this.Y <= 6 && this.Map == Map.Sosaria ){ needsPlacing = true; }
			else if ( this.X >= 0 && this.Y >= 0 && this.X <= 6 && this.Y <= 6 && this.Map == Map.SerpentIsland ){ needsPlacing = true; }
			else if ( this.X >= 0 && this.Y >= 0 && this.X <= 6 && this.Y <= 6 && this.Map == Map.IslesDread ){ needsPlacing = true; }
			else if ( this.X >= 1125 && this.Y >= 298 && this.X <= 1131 && this.Y <= 305 && this.Map == Map.SavagedEmpire ){ needsPlacing = true; }
			else if ( this.X >= 5457 && this.Y >= 3300 && this.X <= 5459 && this.Y <= 3302 && this.Map == Map.Sosaria ){ needsPlacing = true; }
			else if ( this.X >= 608 && this.Y >= 4090 && this.X <= 704 && this.Y <= 4096 && this.Map == Map.Sosaria ){ needsPlacing = true; }
			else if ( this.X >= 6126 && this.Y >= 827 && this.X <= 6132 && this.Y <= 833 && this.Map == Map.Sosaria ){ needsPlacing = true; }
			else if ( this.X == 4 && this.Y == 4 && this.Map == Map.Underworld ){ needsPlacing = true; }

			if ( needsPlacing ){ PremiumSpawner.SpreadOut( this ); }

			base.OnThink();
		}

		public WanderingHealer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}