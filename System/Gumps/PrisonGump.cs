using System;
using Server;
using Server.Misc;
using Server.Gumps;

namespace Server.Gumps 
{
    public class PrisonGump : Gump
    {
        public PrisonGump () : base ( 25,25 )
        {
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 154);
			AddImage(300, 0, 154);
			AddImage(2, 2, 163);
			AddImage(302, 2, 163);
			AddImage(182, 2, 163);
			AddImage(6, 7, 145);
			AddImage(167, 13, 132);
			AddImage(264, 13, 132);
			AddImage(559, 10, 143);
			AddItem(87, 153, 1670);
			AddHtml( 179, 37, 404, 25, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>SENT TO PRISON</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 178, 73, 404, 209, @"<BODY><BASEFONT Color=#FCFF00><BIG>For your deeds you have been sent to prison! Although the guards intended for you to rot forever in your cell, they have been careless. Not only did they forget to lock your cell, but they left you alone for a brief time. You decided to use this opportunity to make your escape, but the doorway out is locked. You gather your belongings from the chest the guards put them in, only to discover they confiscated some of your things. You will surely never see them again. You have heard rumors of others escaping this prison through a tunnel they dug out of one of the cells. Perhaps you can do the same.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
        }
    }
}