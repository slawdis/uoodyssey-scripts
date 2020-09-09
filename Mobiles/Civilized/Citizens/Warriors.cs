using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class Warriors : Citizens
	{
		[Constructable]
		public Warriors()
		{
			Server.Misc.MorphingTime.RemoveMyClothes( this );
			Server.Misc.IntelligentAction.DressUpFighters( this, "", false );
			if ( Backpack != null ){ Backpack.Delete(); }
			CitizenRumor = "";
			CitizenType = 0;
			CitizenCost = 0;
			CitizenService = 0;
			CitizenPhrase = "";
			CantWalk = true;
			AI = AIType.AI_Melee;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
		}

		public override bool BleedImmune{ get{ return true; } }

		public override bool IsEnemy( Mobile m )
		{
			if ( m is Warriors && m != this )
				return true;

			return false;
		}

		public override void OnThink()
		{
			if ( Combatant == null )
			{
				foreach ( Mobile man in this.GetMobilesInRange( 1 ) )
				{
					if ( man is Warriors )
					{
						Combatant = man;
					}
				}
			}
			Hits = HitsMax;
			Criminal = false;
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.CryOut( this );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			string name = "";
				if ( Utility.RandomBool() ){ name = ", " + Combatant.Name + ""; }

			base.OnGaveMeleeAttack( defender );
			switch ( Utility.Random( 60 ))		   
			{
				case 0: Say("You have improved" + name + "."); break;
				case 1: Say("You will need to be quicker than that" + name + "."); break;
				case 2: Say("Where did you learn to fight like that" + name + "?"); break;
				case 3: Say("" + Utility.RandomMinMax(10,100) + " gold says I can best you" + name + "."); break;
				case 4: Say("You will have to do better than that" + name + "."); break;
				case 5: Say("En garde!"); break;
				case 6: Say("Eyes open and focus on me" + name + "."); break;
				case 7: Say("After this, I will buy you a drink" + name + "."); break;
				case 8: Say("This is good practice" + name + "."); break;
				case 9: Say("You need to learn to parry" + name + "."); break;
				case 10: Say("Did you have too much ale last night" + name + "?"); break;
			};
		}

		public Warriors( Serial serial ) : base( serial )
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

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			return false;
		}
	}
}