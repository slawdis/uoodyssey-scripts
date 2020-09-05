using System;

namespace Server.Items
{
	public class DeathBlow : WeaponAbility
	{
		public DeathBlow(){}

		public override int BaseMana { get { return 50; } }

		public override bool OnBeforeSwing(Mobile attacker, Mobile defender, bool validate)
		{
			if (validate && (!Validate(attacker) || !CheckMana(attacker, false))) return false;
			else return true;
		}

		public override bool OnBeforeDamage(Mobile attacker, Mobile defender)
		{
			if (!CheckMana(attacker, true)) return false;
			ClearCurrentAbility(attacker);
			attacker.SendMessage("You strike a deadly blow!");
			defender.SendMessage("You were struck with a deadly blow!");
			defender.PlaySound(0x213);
			defender.FixedParticles(0x377A, 1, 32, 9949, 1153, 0, EffectLayer.Head);
			Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(defender.X, defender.Y, defender.Z + 10), defender.Map), new Entity(Serial.Zero, new Point3D(defender.X, defender.Y, defender.Z + 20), defender.Map), 0x36FE, 1, 0, false, false, 1133, 3, 9501, 1, 0, EffectLayer.Waist, 0x100);
			int damage = 0;
			BaseWeapon weapon = attacker.Weapon as BaseWeapon;
			Skill skill = attacker.Skills[weapon.Skill];
			int skLevel = 1;
			if (skill != null) skLevel = (int)(skill.Value / 100) +1;
			int phys, fire, cold, pois, nrgy, chaos, direct;
			weapon.GetDamageTypes( attacker, out phys, out fire, out cold, out pois, out nrgy, out chaos, out direct );
			int maxd = (int)(weapon.MaxDamage);
			if (skill != null) skLevel = (int)(skill.Value / 50) +1;
			int resource = (int)(weapon.Resource);
			if (resource >=  400) resource -= 400;
			else if (resource >=  300) resource -= 300;
			else if (resource >=  200) resource -= 201;
			else if (resource >=  100) resource -= 101;
			if (resource >  20) resource = 20;
			if (resource <  1) resource = 1;
			damage = (int)((resource * skLevel * maxd) / 2);
			AOS.Damage( defender, attacker, damage, phys, fire, cold, pois, nrgy );
			return true;
		}
	}
}
