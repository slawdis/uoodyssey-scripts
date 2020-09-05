using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBLegendary: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBLegendary() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( DJ_SL_Alchemy ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Anatomy ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_AnimalLore ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_AnimalTaming ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Archery ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_ArmsLore ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Blacksmith ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Bushido ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Carpentry ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Cartography ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Chivalry ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Cooking ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_DetectHidden ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Discordance ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_EvalInt ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Fencing ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Fishing ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Fletching ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Focus ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Healing ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Hiding ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Inscribe ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Lockpicking ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Lumberjacking ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Macing ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Magery ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_MagicResist ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Meditation ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Mining ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Musicianship ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Necromancy ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Ninjitsu ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Parry ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Peacemaking ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Poisoning ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Provocation ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_RemoveTrap ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Snooping ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_SpiritSpeak ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Stealing ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Stealth ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Swords ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Tactics ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Tailoring ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Tinkering ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Tracking ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Veterinary ), 80000, 20, 0x14F0, 0x481 ) );
				Add( new GenericBuyInfo( typeof( DJ_SL_Wrestling ), 80000, 20, 0x14F0, 0x481 ) );
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