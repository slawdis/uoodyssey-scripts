using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBMythical: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBMythical() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( DJ_SM_Alchemy ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Anatomy ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_AnimalLore ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_AnimalTaming ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Archery ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_ArmsLore ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Blacksmith ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Bushido ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Carpentry ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Cartography ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Chivalry ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Cooking ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_DetectHidden ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Discordance ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_EvalInt ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Fencing ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Focus ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Fishing ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Fletching ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Healing ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Hiding ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Inscribe ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Lockpicking ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Lumberjacking ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Macing ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Magery ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_MagicResist ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Meditation ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Mining ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Musicianship ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Necromancy ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Ninjitsu ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Parry ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Peacemaking ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Poisoning ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Provocation ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_RemoveTrap ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Snooping ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_SpiritSpeak ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Stealing ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Stealth ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Swords ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Tactics ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Tailoring ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Tinkering ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Tracking ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Veterinary ), 40000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SM_Wrestling ), 40000, 20, 0x14F0, 0x481 ) );
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