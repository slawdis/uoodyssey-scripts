using System;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;

namespace Server.Items
{
	public abstract class BaseContainer : Container
	{
		public override int DefaultMaxWeight
		{
			get
			{
				if ( IsSecure )
					return 0;

				return base.DefaultMaxWeight;
			}
		}

		public BaseContainer( int itemID ) : base( itemID )
		{
		}

		public override bool IsAccessibleTo( Mobile m )
		{
			if ( !BaseHouse.CheckAccessible( m, this ) )
				return false;

			return base.IsAccessibleTo( m );
		}

		public override void OnAfterSpawn()
		{
			Server.Mobiles.PremiumSpawner.SpreadItems( this );
			base.OnAfterSpawn();
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( this.IsSecure && !BaseHouse.CheckHold( m, this, item, message, checkItems, plusItems, plusWeight ) )
				return false;

			return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );
		}

		public override bool CheckItemUse( Mobile from, Item item )
		{
			if ( IsDecoContainer && item is BaseBook )
				return true;

			return base.CheckItemUse( from, item );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{
			if ( !CheckHold( from, dropped, sendFullMessage, true ) )
				return false;

			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsLockedDown( this ) )
			{
				if ( dropped is VendorRentalContract || ( dropped is Container && ((Container)dropped).FindItemByType( typeof( VendorRentalContract ) ) != null ) )
				{
					from.SendLocalizedMessage( 1062492 ); // You cannot place a rental contract in a locked down container.
					return false;
				}

				if ( !house.LockDown( from, dropped, false ) )
					return false;
			}

			List<Item> list = this.Items;

			for ( int i = 0; i < list.Count; ++i )
			{
				Item item = list[i];

				if ( !(item is Container) && item.StackWith( from, dropped, false ) )
					return true;
			}

			DropItem( dropped );
			DropItemFix( dropped, from, ItemID, GumpID );

			return true;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( TryDropItem( from, dropped, true ) )
			{
				from.SendSound( GetDroppedSound( dropped ), GetWorldLocation() );

				DropItemFix( dropped, from, ItemID, GumpID );

				return true;
			}
			else
			{
				return false;
			}
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if ( !CheckHold( from, item, true, true ) )
				return false;

			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsLockedDown( this ) )
			{
				if ( item is VendorRentalContract || ( item is Container && ((Container)item).FindItemByType( typeof( VendorRentalContract ) ) != null ) )
				{
					from.SendLocalizedMessage( 1062492 ); // You cannot place a rental contract in a locked down container.
					return false;
				}

				if ( !house.LockDown( from, item, false ) )
					return false;
			}

			item.Location = new Point3D( p.X, p.Y, 0 );
			AddItem( item );

			from.SendSound( GetDroppedSound( item ), GetWorldLocation() );

			Server.Gumps.MReagentGump.XReagentGump( from );
			Server.Gumps.QuickBar.RefreshQuickBar( from );
			Server.Gumps.WealthBar.RefreshWealthBar( from );

			return true;
		}

		public static void OrderContainer( Container c, Mobile from )
		{
			if ( from == null || c == null )
				return;

			if ( !isOriginalContainer( c.ItemID ) )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
				if ( DB.Hue == 2 )
				{
					List<Item> contents = new List<Item>();
					foreach( Item i in c.Items )
					{
						contents.Add(i);
					}
					foreach ( Item item in contents )
					{
						int x = Utility.RandomMinMax( 20, 80 );
						int y = Utility.RandomMinMax( 85, 170 );
						item.Location = new Point3D( x, y, 0 );
					}
				}
			}
		}

		public override void UpdateTotal( Item sender, TotalType type, int delta )
		{
			base.UpdateTotal( sender, type, delta );

			if ( type == TotalType.Weight && RootParent is Mobile )
			{
				Server.Gumps.MReagentGump.XReagentGump( ((Mobile) RootParent) );
				Server.Gumps.QuickBar.RefreshQuickBar( ((Mobile) RootParent) );
				Server.Gumps.WealthBar.RefreshWealthBar( ((Mobile) RootParent) );
				((Mobile) RootParent).InvalidateProperties();
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			AlignContainerGumpSound( this );
			if ( from.AccessLevel > AccessLevel.Player || from.InRange( this.GetWorldLocation(), 2 ) || this.RootParent is PlayerVendor )
			{
				Open( from );
			}
			else
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
		}

		public virtual void Open( Mobile from )
		{
			GumpSound( from, this.GumpID, this.ItemID );
			DisplayTo( from );
		}

        public override void DisplayTo( Mobile from )
        {
			int item = this.ItemID;
			int gump = this.GumpID;

			int interfaces = 0;
				if ( from is PlayerMobile ){ CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from ); interfaces = DB.Hue; }

			if ( ( !isOriginalContainer( item ) && interfaces == 2 && !(this is WaterChest) && !(this is LandChest) && !(this is DungeonChest) && !((this.GetType()).IsAssignableFrom(typeof(Corpse))) && !(this is SunkenShip) ) || interfaces == 3 )
			{
				if ( interfaces == 3 )
				{
					this.GumpID = GetLargeGump( gump );
				}
				else
				{
					this.GumpID = GetAlternateGump( item, gump );
				}
				base.DisplayTo( from );
				this.GumpID = gump;
			}
			else
			{
				base.DisplayTo( from );
			}
        }

		public static int BankGump( Mobile from, BankBox box )
		{
			if ( from is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
				int interfaces = DB.Hue;

				if ( interfaces == 3 )
				{
					return 10910;
				}
			}
			return 74;
		}

		public BaseContainer( Serial serial ) : base( serial )
		{
		}

		/* Note: base class insertion; we cannot serialize anything here */
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			AlignContainerGumpSound( this );
			if ( ItemID == 0x9B2 ){ ItemID = 0x53D5; }
		}

		public static void DropItemFix( Item dropped, Mobile from, int containerID, int gumpID )
		{
			int interfaces = 0;
				if ( from is PlayerMobile ){ CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from ); interfaces = DB.Hue; }

			DropItems( dropped, containerID, gumpID, interfaces );
		}
	}

	public class CreatureBackpack : Backpack	//Used on BaseCreature
	{
		[Constructable]
		public CreatureBackpack( string name )
		{
			Name = name;
			Layer = Layer.Backpack;
			Hue = 5;
			Weight = 3.0;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Name != null )
				list.Add( 1075257, Name ); // Contents of ~1_PETNAME~'s pack.
			else
				base.AddNameProperty( list );
		}

		public override void OnItemRemoved( Item item )
		{
			if ( Items.Count == 0 )
				this.Delete();

			base.OnItemRemoved( item );
		}

		public override bool OnDragLift( Mobile from )
		{
			if ( from.AccessLevel > AccessLevel.Player )
				return true;

			from.SendLocalizedMessage( 500169 ); // You cannot pick that up.
			return false;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			return false;
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{
			return false;
		}

		public CreatureBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 )
				Weight = 13.0;
		}
	}

	public class StrongBackpack : Backpack	//Used on Pack animals
	{
		[Constructable]
		public StrongBackpack()
		{
			Layer = Layer.Backpack;
			Weight = 13.0;
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			return base.CheckHold( m, item, false, checkItems, plusItems, plusWeight );
		}

		public override int DefaultMaxWeight{ get{ return 1600; } }

		public override bool CheckContentDisplay( Mobile from )
		{
			object root = this.RootParent;

			if ( root is BaseCreature && ((BaseCreature)root).Controlled && ((BaseCreature)root).ControlMaster == from )
				return true;

			return base.CheckContentDisplay( from );
		}

		public StrongBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 )
				Weight = 13.0;
		}
	}

	public class Backpack : BaseContainer, IDyable
	{
		[Constructable]
		public Backpack() : base( 0xE75 )
		{
			Layer = Layer.Backpack;
			Weight = 3.0;
		}

		public override int DefaultMaxWeight {
			get {
				if ( Core.ML ) {
					Mobile m = ParentEntity as Mobile;
					if ( m != null && m.Player && m.Backpack == this ) {
						return 550;
					} else {
						return base.DefaultMaxWeight;
					}
				} else {
					return base.DefaultMaxWeight;
				}
			}
		}

		public Backpack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && ItemID == 0x9B2 )
				ItemID = 0xE75;
		}
	}

	public class Pouch : TrapableContainer
	{
		[Constructable]
		public Pouch() : base( 0xE79 )
		{
			Weight = 1.0;
		}

		public Pouch( Serial serial ) : base( serial )
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

	public abstract class BaseBagBall : BaseContainer, IDyable
	{
		public BaseBagBall( int itemID ) : base( itemID )
		{
			Weight = 1.0;
		}

		public BaseBagBall( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

	public class SmallBagBall : BaseBagBall
	{
		[Constructable]
		public SmallBagBall() : base( 0x2256 )
		{
		}

		public SmallBagBall( Serial serial ) : base( serial )
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

	public class LargeBagBall : BaseBagBall
	{
		[Constructable]
		public LargeBagBall() : base( 0x2257 )
		{
		}

		public LargeBagBall( Serial serial ) : base( serial )
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

	public class Bag : BaseContainer, IDyable
	{
		[Constructable]
		public Bag() : base( 0xE76 )
		{
			Weight = 2.0;
			Hue = 0xABE;
		}

		public Bag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			if ( Hue == 0 ){ Hue = 0xABE; }

			return true;
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

			if ( Hue == 0 ){ Hue = 0xABE; }
		}
	}

	[Flipable( 0x1E3F, 0x1E52 )]
	public class LargeBag : BaseContainer, IDyable
	{
		[Constructable]
		public LargeBag() : base( 0x1E3F )
		{
			Name = "large bag";
			Weight = 2.0;
			GumpID = 0x62;
		}

		public LargeBag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
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

	[Flipable( 0x1248, 0x1264 )]
	public class GiantBag : BaseContainer, IDyable
	{
		[Constructable]
		public GiantBag() : base( 0x1248 )
		{
			Name = "giant bag";
			Weight = 4.0;
			GumpID = 0x62;
		}

		public GiantBag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
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

	[Flipable( 0x1C10, 0x1CC6 )]
	public class LargeSack : BaseContainer, IDyable
	{
		[Constructable]
		public LargeSack() : base( 0x1C10 )
		{
			Name = "large rucksack";
			Weight = 3.0;
			GumpID = 0x2A74;
		}

		public LargeSack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x27BE, 0x27D7 )]
	public class RuggedBackpack : BaseContainer, IDyable
	{
		[Constructable]
		public RuggedBackpack() : base( 0x27BE )
		{
			Name = "rugged backpack";
			Weight = 3.0;
			GumpID = 0x2A74;
		}

		public RuggedBackpack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class Barrel : BaseContainer
	{
		[Constructable]
		public Barrel() : base( 0xE77 )
		{
			Weight = 25.0;
		}

		public Barrel( Serial serial ) : base( serial )
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

			if ( Weight == 0.0 )
				Weight = 25.0;
		}
	}

	public class Keg : BaseContainer
	{
		[Constructable]
		public Keg() : base( 0xE7F )
		{
			Weight = 15.0;
		}

		public Keg( Serial serial ) : base( serial )
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

	public class PicnicBasket : BaseContainer
	{
		[Constructable]
		public PicnicBasket() : base( 0xE7A )
		{
			Weight = 2.0; // Stratics doesn't know weight
		}

		public PicnicBasket( Serial serial ) : base( serial )
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

	public class Basket : BaseContainer
	{
		[Constructable]
		public Basket() : base( 0x990 )
		{
			Weight = 1.0; // Stratics doesn't know weight
		}

		public Basket( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x24D9, 0x24DA )]
	public class OrientBasket1 : BaseContainer
	{
		[Constructable]
		public OrientBasket1() : base( 0x24D9 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket1( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x24D5, 0x24D6 )]
	public class OrientBasket2 : BaseContainer
	{
		[Constructable]
		public OrientBasket2() : base( 0x24D5 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket2( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x24DB, 0x24DC )]
	public class OrientBasket3 : BaseContainer
	{
		[Constructable]
		public OrientBasket3() : base( 0x24DB )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket3( Serial serial ) : base( serial )
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

	public class OrientBasket4 : BaseContainer
	{
		[Constructable]
		public OrientBasket4() : base( 0x24D7 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket4( Serial serial ) : base( serial )
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

	public class OrientBasket5 : BaseContainer
	{
		[Constructable]
		public OrientBasket5() : base( 0x24D8 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket5( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x9AA, 0xE7D )]
	public class WoodenBox : LockableContainer
	{
		[Constructable]
		public WoodenBox() : base( 0x9AA )
		{
			Weight = 4.0;
		}

		public WoodenBox( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x9A9, 0xE7E )]
	public class SmallCrate : LockableContainer
	{
		[Constructable]
		public SmallCrate() : base( 0x9A9 )
		{
			Weight = 2.0;
		}

		public SmallCrate( Serial serial ) : base( serial )
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

			if ( Weight == 4.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0xE3F, 0xE3E )]
	public class MediumCrate : LockableContainer
	{
		[Constructable]
		public MediumCrate() : base( 0xE3F )
		{
			Weight = 2.0;
		}

		public MediumCrate( Serial serial ) : base( serial )
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

			if ( Weight == 6.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0xE3D, 0xE3C )]
	public class LargeCrate : LockableContainer
	{
		[Constructable]
		public LargeCrate() : base( 0xE3D )
		{
			Weight = 1.0;
		}

		public LargeCrate( Serial serial ) : base( serial )
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

			if ( Weight == 8.0 )
				Weight = 1.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x436, 0x437 )]
	public class MetalSafe : LockableContainer
	{
		[Constructable]
		public MetalSafe() : base( 0x436 )
		{
			Name = "metal safe";
			GumpID = 0x975;
			Weight = 25.0;
		}

		public MetalSafe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x5329, 0x532A )]
	public class IronSafe : LockableContainer
	{
		[Constructable]
		public IronSafe() : base( 0x5329 )
		{
			Name = "iron safe";
			GumpID = 0x975;
			Weight = 20.0;
		}

		public IronSafe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x4FE3, 0x4FE4 )]
	public class MetalVault : LockableContainer
	{
		[Constructable]
		public MetalVault() : base( 0x4FE3 )
		{
			Name = "metal safe";
			GumpID = 0x975;
			Weight = 25.0;
		}

		public MetalVault( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x4D05, 0x4D06 )]
	public class ArmsBarrel : LockableContainer
	{
		[Constructable]
		public ArmsBarrel() : base( 0x4D05 )
		{
			Name = "arms barrel";
			GumpID = 0x2A78;
			Server.Misc.MaterialInfo.ColorPlainMetal( this );
			Weight = 25.0;
		}

		public ArmsBarrel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x0C0F, 0x0DB6 )]
	public class NecromancerBarrel : LockableContainer
	{
		[Constructable]
		public NecromancerBarrel() : base( 0x0C0F )
		{
			Name = "necromancer barrel";
			GumpID = 0x2A78;
			Weight = 25.0;
		}

		public NecromancerBarrel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x9A8, 0xE80 )]
	public class MetalBox : LockableContainer
	{
		[Constructable]
		public MetalBox() : base( 0x9A8 )
		{
		}

		public MetalBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 3 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x9AB, 0xE7C )]
	public class MetalChest : LockableContainer
	{
		[Constructable]
		public MetalChest() : base( 0x9AB )
		{
		}

		public MetalChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x281D, 0x281E )]
	public class StoneCoffer : LockableContainer
	{
		[Constructable]
		public StoneCoffer() : base( 0x281D )
		{
			Name = "stone coffer";
			GumpID = 0x2810;
		}

		public StoneCoffer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x52FB, 0x52FD )]
	public class VirtueStoneChest : LockableContainer
	{
		[Constructable]
		public VirtueStoneChest() : base( 0x52FB )
		{
			Name = "chest of virtue";
			GumpID = 0x2810;
			Light = LightType.Circle225;
		}

		public VirtueStoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x281F, 0x2820 )]
	public class GildedStoneChest : LockableContainer
	{
		[Constructable]
		public GildedStoneChest() : base( 0x281F )
		{
			Name = "gilded stone chest";
			GumpID = 0x2810;
		}

		public GildedStoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2821, 0x2822 )]
	public class FancyStoneChest : LockableContainer
	{
		[Constructable]
		public FancyStoneChest() : base( 0x2821 )
		{
			Name = "fancy stone chest";
			GumpID = 0x2810;
		}

		public FancyStoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2825, 0x2826 )]
	public class StoneStrongbox : LockableContainer
	{
		[Constructable]
		public StoneStrongbox() : base( 0x2825 )
		{
			Name = "stone strongbox";
			GumpID = 0x2810;
		}

		public StoneStrongbox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2823, 0x2824 )]
	public class StoneChest : LockableContainer
	{
		[Constructable]
		public StoneChest() : base( 0x2823 )
		{
			Name = "stone chest";
			GumpID = 0x2810;
		}

		public StoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3330, 0x3331 )]
	public class SilverChest : LockableContainer
	{
		[Constructable]
		public SilverChest() : base( 0x3330 )
		{
			Name = "silver chest";
		}

		public SilverChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3332, 0x3333 )]
	public class RustyChest : LockableContainer
	{
		[Constructable]
		public RustyChest() : base( 0x3332 )
		{
			Name = "rusty chest";
		}

		public RustyChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3334, 0x3335 )]
	public class BronzeChest : LockableContainer
	{
		[Constructable]
		public BronzeChest() : base( 0x3334 )
		{
			Name = "bronze chest";
		}

		public BronzeChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3336, 0x3337 )]
	public class IronChest : LockableContainer
	{
		[Constructable]
		public IronChest() : base( 0x3336 )
		{
			Name = "iron chest";
		}

		public IronChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x10EC, 0x10ED )]
	public class SpaceChest : LockableContainer
	{
		[Constructable]
		public SpaceChest() : base( 0x10EC )
		{
			Name = "metal trunk";
			GumpID = 0x976;
		}

		public SpaceChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	public class SpaceCrate : LockableContainer
	{
		[Constructable]
		public SpaceCrate() : base( 0x10EA )
		{
			Name = "metal crate";
			GumpID = 0x976;
		}

		public SpaceCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	public class HazardCrate : LockableContainer
	{
		[Constructable]
		public HazardCrate() : base( 0x10EB )
		{
			Name = "containment crate";
			GumpID = 0x976;
		}

		public HazardCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0xE41, 0xE40 )]
	public class MetalGoldenChest : LockableContainer
	{
		[Constructable]
		public MetalGoldenChest() : base( 0xE41 )
		{
		}

		public MetalGoldenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0xe43, 0xe42 )]
	public class WoodenChest : LockableContainer
	{
		[Constructable]
		public WoodenChest() : base( 0xe43 )
		{
			Weight = 2.0;
			Hue = 0x724;
		}

		public WoodenChest( Serial serial ) : base( serial )
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

			if ( Weight == 15.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0x280B, 0x280C )]
	public class PlainWoodenChest : LockableContainer
	{
		[Constructable]
		public PlainWoodenChest() : base( 0x280B )
		{
		}

		public PlainWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x280D, 0x280E )]
	public class OrnateWoodenChest : LockableContainer
	{
		[Constructable]
		public OrnateWoodenChest() : base( 0x280D )
		{
		}

		public OrnateWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x280F, 0x2810 )]
	public class GildedWoodenChest : LockableContainer
	{
		[Constructable]
		public GildedWoodenChest() : base( 0x280F )
		{
		}

		public GildedWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x2811, 0x2812 )]
	public class WoodenFootLocker : LockableContainer
	{
		[Constructable]
		public WoodenFootLocker() : base( 0x2811 )
		{
			GumpID = 0x10C;
		}

		public WoodenFootLocker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
			
			GumpID = 0x10C;
		}
	}

	[Furniture]
	[Flipable( 0x2800, 0x2801 )]
	public class WoodenCoffin : LockableContainer
	{
		[Constructable]
		public WoodenCoffin() : base( 0x2800 )
		{
			Name = "coffin";
			GumpID = 0x41D;
		}

		public WoodenCoffin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x27E9, 0x27EA )]
	public class WoodenCasket : LockableContainer
	{
		[Constructable]
		public WoodenCasket() : base( 0x27E9 )
		{
			Name = "coffin";
			GumpID = 0x41D;
		}

		public WoodenCasket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x27E0, 0x280A )]
	public class StoneCoffin : LockableContainer
	{
		[Constructable]
		public StoneCoffin() : base( 0x27E0 )
		{
			Name = "sarcophagus";
			Weight = 100.0;
			GumpID = 0x1D;
		}

		public StoneCoffin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x2802, 0x2803 )]
	public class StoneCasket : LockableContainer
	{
		[Constructable]
		public StoneCasket() : base( 0x2802 )
		{
			Name = "sarcophagus";
			Weight = 100.0;
			GumpID = 0x1D;
		}

		public StoneCasket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x1AFC, 0x1AFD )]
	public class RockUrn : LockableContainer
	{
		[Constructable]
		public RockUrn() : base( 0x1AFC )
		{
			Name = "urn";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public RockUrn( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x1AFE, 0x1AFF )]
	public class RockVase : LockableContainer
	{
		[Constructable]
		public RockVase() : base( 0x1AFE )
		{
			Name = "vase";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public RockVase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	public class StoneOrnateUrn : LockableContainer
	{
		[Constructable]
		public StoneOrnateUrn() : base( 0x39A2 )
		{
			Name = "ornate urn";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public StoneOrnateUrn( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	public class StoneOrnateTallVase : LockableContainer
	{
		[Constructable]
		public StoneOrnateTallVase() : base( 0x398B )
		{
			Name = "ornate vase";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public StoneOrnateTallVase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x2813, 0x2814 )]
	public class FinishedWoodenChest : LockableContainer
	{
		[Constructable]
		public FinishedWoodenChest() : base( 0x2813 )
		{
		}

		public FinishedWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	public class HugeCrate : LockableContainer
	{
		[Constructable]
		public HugeCrate() : base( 0x4F86 )
		{
			Name = "huge crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public HugeCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class StableCrate : LockableContainer
	{
		[Constructable]
		public StableCrate() : base( 0x4F87 )
		{
			Name = "stable crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public StableCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class FletcherCrate : LockableContainer
	{
		[Constructable]
		public FletcherCrate() : base( 0x4F88 )
		{
			Name = "fletcher crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public FletcherCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class ButcherCrate : LockableContainer
	{
		[Constructable]
		public ButcherCrate() : base( 0x4F89 )
		{
			Name = "butcher crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public ButcherCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class CarpenterCrate : LockableContainer
	{
		[Constructable]
		public CarpenterCrate() : base( 0x4F8A )
		{
			Name = "carpenter crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public CarpenterCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class JewelerCrate : LockableContainer
	{
		[Constructable]
		public JewelerCrate() : base( 0x4F8B )
		{
			Name = "jeweler crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public JewelerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class WizardryCrate : LockableContainer
	{
		[Constructable]
		public WizardryCrate() : base( 0x4F8C )
		{
			Name = "wizardry crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public WizardryCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BlacksmithCrate : LockableContainer
	{
		[Constructable]
		public BlacksmithCrate() : base( 0x4F8D )
		{
			Name = "blacksmith crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BlacksmithCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class ProvisionerCrate : LockableContainer
	{
		[Constructable]
		public ProvisionerCrate() : base( 0x4F8E )
		{
			Name = "provisioner crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public ProvisionerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TailorCrate : LockableContainer
	{
		[Constructable]
		public TailorCrate() : base( 0x4F8F )
		{
			Name = "tailor crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TailorCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TinkerCrate : LockableContainer
	{
		[Constructable]
		public TinkerCrate() : base( 0x4F90 )
		{
			Name = "tinker crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TinkerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class AlchemyCrate : LockableContainer
	{
		[Constructable]
		public AlchemyCrate() : base( 0x4F91 )
		{
			Name = "alchemy crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public AlchemyCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BakerCrate : LockableContainer
	{
		[Constructable]
		public BakerCrate() : base( 0x4F92 )
		{
			Name = "baker crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BakerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TreasureCrate : LockableContainer
	{
		[Constructable]
		public TreasureCrate() : base( 0x4F93 )
		{
			Name = "treasure crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TreasureCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class MusicianCrate : LockableContainer
	{
		[Constructable]
		public MusicianCrate() : base( 0x4F94 )
		{
			Name = "musician crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public MusicianCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BeekeeperCrate : LockableContainer
	{
		[Constructable]
		public BeekeeperCrate() : base( 0x4F95 )
		{
			Name = "beekeeper crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BeekeeperCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class LibrarianCrate : LockableContainer
	{
		[Constructable]
		public LibrarianCrate() : base( 0x4F96 )
		{
			Name = "librarian crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public LibrarianCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BowyerCrate : LockableContainer
	{
		[Constructable]
		public BowyerCrate() : base( 0x4F97 )
		{
			Name = "bowyer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BowyerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class HealerCrate : LockableContainer
	{
		[Constructable]
		public HealerCrate() : base( 0x4F98 )
		{
			Name = "healer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public HealerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TavernCrate : LockableContainer
	{
		[Constructable]
		public TavernCrate() : base( 0x4F99 )
		{
			Name = "tavern crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TavernCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class NecromancerCrate : LockableContainer
	{
		[Constructable]
		public NecromancerCrate() : base( 0x4F9A )
		{
			Name = "necromancer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public NecromancerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class AdventurerCrate : LockableContainer
	{
		[Constructable]
		public AdventurerCrate() : base( 0x4F9B )
		{
			Name = "adventurer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public AdventurerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class SailorCrate : LockableContainer
	{
		[Constructable]
		public SailorCrate() : base( 0x4F9C )
		{
			Name = "sailor crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public SailorCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class SupplyCrate : LockableContainer
	{
		[Constructable]
		public SupplyCrate() : base( 0x4F9D )
		{
			Name = "supply crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public SupplyCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class ArmsCrate : LockableContainer
	{
		[Constructable]
		public ArmsCrate() : base( 0x4F9E )
		{
			Name = "arms crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public ArmsCrate( Serial serial ) : base( serial )
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