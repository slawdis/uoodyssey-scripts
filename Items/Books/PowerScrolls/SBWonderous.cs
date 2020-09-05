using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBWonderous: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWonderous() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( DJ_SW_Alchemy ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Anatomy ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_AnimalLore ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_AnimalTaming ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Archery ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_ArmsLore ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Blacksmith ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Bushido ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Carpentry ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Cartography ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Chivalry ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Cooking ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_DetectHidden ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Discordance ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_EvalInt ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Fencing ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Fishing ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Fletching ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Focus ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Healing ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Hiding ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Inscribe ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Lockpicking ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Lumberjacking ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Macing ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Magery ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_MagicResist ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Meditation ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Mining ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Musicianship ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Necromancy ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Ninjitsu ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Parry ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Peacemaking ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Poisoning ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Provocation ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_RemoveTrap ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Snooping ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_SpiritSpeak ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Stealing ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Stealth ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Swords ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Tactics ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Tailoring ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Tinkering ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Tracking ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Veterinary ), 10000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SW_Wrestling ), 10000, 20, 0x14F0, 0x481 ) );
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