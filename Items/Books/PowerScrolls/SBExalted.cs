using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBExalted: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBExalted() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( DJ_SE_Alchemy ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Anatomy ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_AnimalLore ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_AnimalTaming ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Archery ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_ArmsLore ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Blacksmith ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Bushido ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Carpentry ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Cartography ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Chivalry ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Cooking ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_DetectHidden ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Discordance ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_EvalInt ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Fencing ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Fishing ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Fletching ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Focus ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Healing ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Hiding ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Inscribe ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Lockpicking ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Lumberjacking ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Macing ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Magery ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_MagicResist ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Meditation ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Mining ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Musicianship ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Necromancy ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Ninjitsu ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Parry ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Peacemaking ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Poisoning ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Provocation ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_RemoveTrap ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Snooping ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_SpiritSpeak ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Stealing ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Stealth ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Swords ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Tactics ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Tailoring ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Tinkering ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Tracking ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Veterinary ), 20000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SE_Wrestling ), 20000, 20, 0x14F0, 0x481 ) );
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