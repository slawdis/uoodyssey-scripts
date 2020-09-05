using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Misc;
using System.IO;
using Server.Commands;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Server.Accounting;

namespace Joeku.MOTD
{
	public class MOTD_Gump : Gump
	{
		public int m_Origin;
		public Mobile User;
		public bool Help;
		public int Index;

		public MOTD_Gump( Mobile user, bool help, int index, int origin ) : base( 25, 25 )
		{
			m_Origin = origin;

			this.User = user;
			this.Help = help;
			this.Index = index;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( user );

			int button = 4018;
			if( DB.CharacterMOTD == 1 )
				button = 3609;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 151);
			AddImage(300, 0, 151);
			AddImage(0, 300, 151);
			AddImage(300, 300, 151);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(7, 7, 145);
			AddImage(7, 354, 142);
			AddImage(167, 7, 140);
			AddImage(264, 564, 140);
			AddImage(274, 7, 140);
			AddImage(558, 566, 143);
			AddImage(558, 9, 143);
			AddImage(286, 540, 156);
			AddHtml( 177, 37, 230, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>MESSAGE OF THE DAY</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddBody( user );
			AddItem(38, 269, 7775);
			AddHtml( 419, 531, 154, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SHOW AT LOGIN</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(384, 531, button, button, 1, GumpButtonType.Reply, 0);
			AddImage(209, 100, 140);
			AddImage(367, 66, 134);
			AddItem(542, 36, 733);
			AddImage(181, 102, 159);
		}

		public void AddBody( Mobile m )
		{
			AddHtml(101, 152, 473, 353, MOTD_Main.Info[this.Index].Body, false, true); // Text - Main - Category - Body
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int button = info.ButtonID;

			if ( info.ButtonID == 1 )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

				if( DB.CharacterMOTD == 1 )
					DB.CharacterMOTD = 0;
				else
					DB.CharacterMOTD = 1;

				MOTD_Utility.SendGump( from, false, this.Index, m_Origin );

				from.SendSound( 0x4A ); 
			}
			else if ( m_Origin > 0 )
			{
				from.SendSound( 0x4A ); 
				from.SendGump( new Server.Engines.Help.HelpGump( from, 1 ) );
			}
		}
	}
}

namespace Joeku.MOTD
{
	public class MOTD_HelpInfo
	{
		public string Name;
		public int NameWidth;

		public MOTD_HelpInfo( string name )
		{
			Name = name;
			NameWidth = MOTD_Utility.StringWidth( ref Name );
		}
	}
}

namespace Joeku.MOTD
{
	public class MOTD_Info
	{
		public string Name;
		public int NameWidth;
		public string Body;
		public DateTime LastWriteTime;

		public MOTD_Info( string name )
		{
			Name = name;
			NameWidth = MOTD_Utility.StringWidth( ref Name );
		}
	}
}

namespace Joeku.MOTD
{
	public class MOTD_Main
	{
		public const int Version = 100;
		public const string ReleaseDate = "September 1st, 2012";

		public static readonly string FilePath = Path.Combine( Core.BaseDirectory, @"Info" );
		public static MOTD_Info[] Info = new MOTD_Info[]
		{
			new MOTD_Info( "News" ),
		};
		public static MOTD_HelpInfo[] HelpInfo = new MOTD_HelpInfo[]
		{
			new MOTD_HelpInfo( "Help" ),
			new MOTD_HelpInfo( "Preferences" )
		};

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( MOTD_Utility.EventSink_OnLogin );
			CommandSystem.Register( "MOTD", AccessLevel.Player, new CommandEventHandler( MOTD_Utility.EventSink_OnCommand ) );
			MOTD_Utility.CheckFiles( false );
		}
	}
}

namespace Joeku.MOTD
{
	public class MOTD_Utility
	{
		public static void EventSink_OnLogin( LoginEventArgs e )
		{
			if( CheckLogin( e.Mobile ) )
				SendGump( e.Mobile );
		}

		public static bool CheckLogin( Mobile m )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if( DB.CharacterMOTD == 1 )
				return false;

			return true;
		}

		[Usage( "MOTD" )]
		[Description( "Brings up the Message Of The Day menu." )]
		public static void EventSink_OnCommand( CommandEventArgs e )
		{
			SendGump( e.Mobile );
		}

		public static void SendGump( Mobile mob ){ SendGump( mob, false, 0, 0 ); }
		public static void SendGump( Mobile mob, bool help ){ SendGump( mob, help, 0, 0 ); }
		public static void SendGump( Mobile mob, bool help, int index, int origin )
		{
			if( !help )
				CheckFiles();

			mob.CloseGump( typeof( MOTD_Gump ) );
			mob.SendGump( new MOTD_Gump( mob, help, index, origin ) );
		}

		public static void CheckFiles(){ CheckFiles( true ); }
		public static void CheckFiles( bool checkTime )
		{
			CheckPaths();

			string path = String.Empty;
			for( int i = 0; i < MOTD_Main.Info.Length; i++ )
			{
				path = Path.Combine( MOTD_Main.FilePath, String.Format("{0}.txt", MOTD_Main.Info[i].Name) );
				if( !checkTime || (checkTime && File.GetLastWriteTime( path ) > MOTD_Main.Info[i].LastWriteTime) )
				{
					MOTD_Main.Info[i].Body = ReadFile( path );
					MOTD_Main.Info[i].LastWriteTime = File.GetLastWriteTime( path );
				}
			}
		}
		private static void CheckPaths()
		{
			string path = MOTD_Main.FilePath;
			if( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
			
			for( int i = 0; i < MOTD_Main.Info.Length; i++ )
			{
				path = Path.Combine( MOTD_Main.FilePath, String.Format("{0}.txt", MOTD_Main.Info[i].Name) );
				if ( !File.Exists( path ) )
					using (StreamWriter writer = new StreamWriter(path)) 
						GenerateExampleCode( writer );
			}
		}
		private static string ReadFile( string path )
		{
			string file = String.Empty;
			List<string> lines = new List<string>();
			string line = String.Empty;
			bool started = false;

			using( StreamReader reader = new StreamReader( path ) )
			{
				while( (line = reader.ReadLine()) != null )
				{
					if( line != String.Empty && !line.StartsWith( "//" ) )
					{
						if( line.StartsWith( "[*]" ) )
						{
							started = true;
							file += ParseLines( lines );
							lines.Clear();

							line = line.Remove( 0, 3 );
							if( line != String.Empty )
								lines.Add( line );
						}
						else if( started )
							lines.Add( line );
					}
				}
			}

			file += ParseLines( lines );

			return TrimFile( file );
		}
		private static string ParseLines( List<string> list )
		{
			if( list.Count < 3 )
				return String.Empty;

			string lines = String.Empty;
			for( int i = 0; i < list.Count; i++ )
			{
				switch( i )
				{
					case 0:
						lines += String.Format( "<CENTER><BIG>{0} ", list[i] );
						break;
					case 1:
						lines += String.Format( "by {0}<BR>----------------------------------</BIG></CENTER>", list[i] );
						break;
					default:
						lines += list[i] + "<BR>";
						break;
				}
			}

			return lines;
		}
		private static string TrimFile( string file )
		{
			if( file.EndsWith("<BR>") )
				return TrimFile( file.Remove( file.Length-4, 4 ) );

			return file;
		}

		public static void GenerateExampleCode( StreamWriter writer )
		{
			for( int i = 0; i < ExampleCode.Length; i++ )
				writer.WriteLine( "// {0}", ExampleCode[i] );

			writer.WriteLine();
			writer.WriteLine( "[*]{0}", DateTime.Now.ToShortDateString() );
			writer.WriteLine( "System" );
			writer.WriteLine( "   This script does not contain any entries. Contact the shard administrators for more information." );
		}
		private static string[] ExampleCode = new string[]
		{
			String.Format( "MOTD v{0}", ((double)MOTD_Main.Version)/100 ),
			"Author: Joeku",
			MOTD_Main.ReleaseDate,
			"",
			"To create an entry for the MOTD, it must start",
			"with \"[*]\" and be at least three lines long.",
			"",
				"Example:",
				"  [*]12/2/2007",
				"  Joeku",
				"  This is an example entry.",
			"",
			"The first line is the date, the second",
			"line is the author of the entry, and all lines",
			"afterward make up the body of the entry.",
			"",
			"Blank and commented (starting with \"//\") lines",
			"will not be displayed in-game. Entries with fewer",
			"than three lines will not be displayed in-game."
		};

		public static int StringWidth( ref string text )
		{
			int size = 1;

			for( int i = 0; i < text.Length; i++ )
			{
				try
				{
					if (CharLibrary[(int)text[i]] < 1)
					{
						text = text.Remove(i, 1);
						text = text.Insert(i, ConstChar.ToString());
					}
				}
				catch
				{
					text = text.Remove(i, 1);
					text = text.Insert(i, ConstChar.ToString());
				}

				size += CharLibrary[(int)text[i]];
			}

			return size;
		}
		private static char ConstChar = '_';
		private static int[] CharLibrary = new int[127]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			8, 3, 4, 12, 9, 10, 11, 3, 4, 4, 10, 7, 3, 6, 3,
			9, 8, 4, 8, 8, 8, 8, 8, 8, 8, 8, 3, 3, 8, 6, 8,
			7, 12, 8, 8, 8, 8, 7, 7, 8, 8, 3, 8, 8, 7, 10,
			8, 8, 8, 9, 8, 8, 7, 8, 8, 12, 8, 9, 8, 4, 9, 5,
			10, 8, 3, 6, 6, 6, 6, 6, 6, 6, 6, 3, 6, 6, 3, 9,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 8, 6, 6, 6, 5, 2, 5, 6
		};
	}
}