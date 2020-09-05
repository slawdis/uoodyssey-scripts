using System;

namespace Server.Items
{
	public class QuiverOfInfinity : BaseQuiver, IIslesDreadDyable
	{
		public override int LabelNumber { get { return 1075201; } } // Quiver of Infinity

		[Constructable]
		public QuiverOfInfinity() : base()
		{
			Weight = 8.0;
			WeightReduction = 80;
			LowerAmmoCost = 20;
			Attributes.DefendChance = 5;
		}

		public QuiverOfInfinity( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 1 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			if( version < 1 && DamageIncrease == 0 )
			{
				DamageIncrease = 10;
			}
		}
	}
}
