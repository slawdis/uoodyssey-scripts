using System;
using Server;

namespace Server.Items
{
	public class GwennosHarp : LapHarp
	{
		public override int LabelNumber{ get{ return 1063480; } }

		public override int InitMinUses{ get{ return 1600; } }
		public override int InitMaxUses{ get{ return 1600; } }

		[Constructable]
		public GwennosHarp()
		{
			Hue = 0x47E;
			Slayer = SlayerName.Repond;
			Slayer2 = SlayerName.ReptilianDeath;
			SkillBonuses.SetValues( 0, SkillName.Discordance, 10 );
			SkillBonuses.SetValues( 1, SkillName.Musicianship, 10 );
			SkillBonuses.SetValues( 2, SkillName.Peacemaking, 10 );
			SkillBonuses.SetValues( 3, SkillName.Provocation, 10 );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public GwennosHarp( Serial serial ) : base( serial )
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