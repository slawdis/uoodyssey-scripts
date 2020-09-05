using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBPower: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBPower() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( DJ_SP_Alchemy ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Anatomy ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_AnimalLore ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_AnimalTaming ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Archery ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_ArmsLore ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Blacksmith ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Bushido ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Carpentry ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Cartography ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Chivalry ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Cooking ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_DetectHidden ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Discordance ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_EvalInt ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Fencing ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Fishing ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Fletching ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Focus ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Healing ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Hiding ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Inscribe ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Lockpicking ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Lumberjacking ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Macing ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Magery ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_MagicResist ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Meditation ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Mining ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Musicianship ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Necromancy ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Ninjitsu ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Parry ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Peacemaking ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Poisoning ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Provocation ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_RemoveTrap ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Snooping ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_SpiritSpeak ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Stealing ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Stealth ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Swords ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Tactics ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Tailoring ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Tinkering ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Tracking ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Veterinary ), 160000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SP_Wrestling ), 160000, 20, 0x14F0, 0x481 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
			} 
		} 
	} 
}