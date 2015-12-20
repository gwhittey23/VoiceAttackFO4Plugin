////////////////////////////////////////
// Title : WinampLib Class
//
// Author: Polis Pilavas, April 2005
//
// Description: A basic class for controlling Winamp via the classic Win32 Message API. A front-end 
//				to Winamp
//
// Comments: Adapted from Geekpedia http://www.geekpedia.com/tutorial27_Winamp-basic-controls.html
//           and Winamp 5 SDK. Constants were also taken from "wa_ipc.h" header file located in the
//           Winamp SDK and from http://svn.bolt.cx/svn/mircamp/tags/release-1.0/frontend.h header 
//			 file. Methods "GetCurrentSongTitle()" and "IsNumeric()" were adapted from CodeProject
//			 http://www.codeproject.com/vb/net/winampsongtitle.asp (Copyright Niels Penneman 2004)
////////////////////////////////////////

using System;
using System.Runtime.InteropServices;

namespace WinampFrontEndLib
{
	/// <summary>
	/// 
	/// </summary>
	
	public class WinampLib
	{

		#region DLL Imports

		[DllImport("user32.dll", CharSet = CharSet.Auto)] 
		public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, 
												[MarshalAs(UnmanagedType.LPTStr)] string lpWindowName); 

		[DllImport("user32.dll", CharSet = CharSet.Auto)] 
		public static extern int SendMessageA( 
			IntPtr hwnd, 
			int wMsg, 
			int wParam, 
			uint lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowText(
			IntPtr hwnd, 
			string lpString, 
			int cch);
		#endregion

		#region Command Type Constants
		// To tell Winamp that we are sending it a WM_COMMAND it needs the hex code 0x111
		const int WM_COMMAND = 0x111; 

		// To tell Winamp that we are sending it a WM_USER (WM_WA_IPC) it needs the hex code 0x0400
		const int WM_WA_IPC = 0x0400;
		#endregion

		#region Winamp-specific Constants
		// We have to define the Winamp class name
		private const string m_windowName = "Winamp v1.x"; 
		
		// Useful for GetSongTitle() Method
		private const string strTtlEnd = " - Winamp";
		#endregion

		#region WM_COMMAND Type Constants
		const int WA_NOTHING            = 0; 
		const int WINAMP_OPTIONS_PREFS  = 40012; // pops up the preferences
		const int WINAMP_OPTIONS_AOT    = 40019; // toggles always on top
		const int WINAMP_FILE_PLAY      = 40029; // pops up the load file(s) box
		const int WINAMP_OPTIONS_EQ     = 40036; // toggles the EQ window
		const int WINAMP_OPTIONS_PLEDIT = 40040; // toggles the playlist window
		const int WINAMP_HELP_ABOUT     = 40041; // pops up the about box
		const int WA_PREVTRACK          = 40044; // plays previous track
		const int WA_PLAY               = 40045; // plays selected track
		const int WA_PAUSE              = 40046; // pauses/unpauses currently playing track
		const int WA_STOP               = 40047; // stops currently playing track
		const int WA_NEXTTRACK          = 40048; // plays next track
		const int WA_VOLUMEUP           = 40058; // turns volume up
		const int WA_VOLUMEDOWN         = 40059; // turns volume down
		const int WINAMP_FFWD5S         = 40060; // fast forwards 5 seconds
		const int WINAMP_REW5S          = 40061; // rewinds 5 seconds
		const int WINAMP_BUTTON1_SHIFT  = 40144; // fast-rewind 5 seconds
		//const int WINAMP_BUTTON2_SHIFT  = 40145;
		//const int WINAMP_BUTTON3_SHIFT  = 40146;
		const int WINAMP_BUTTON4_SHIFT  = 40147; // stop after current track
		const int WINAMP_BUTTON5_SHIFT  = 40148; // fast-forward 5 seconds
		const int WINAMP_BUTTON1_CTRL   = 40154; // start of playlist
		const int WINAMP_BUTTON2_CTRL   = 40155; // open URL dialog
		//const int WINAMP_BUTTON3_CTRL   = 40156;
		const int WINAMP_BUTTON4_CTRL   = 40157; // fadeout and stop
		const int WINAMP_BUTTON5_CTRL   = 40158; // end of playlist
		const int WINAMP_FILE_DIR       = 40187; // pops up the load directory box
		const int ID_MAIN_PLAY_AUDIOCD1 = 40323; // starts playing the audio CD in the first CD reader
		const int ID_MAIN_PLAY_AUDIOCD2 = 40323; // plays the 2nd
		const int ID_MAIN_PLAY_AUDIOCD3 = 40323; // plays the 3rd
		const int ID_MAIN_PLAY_AUDIOCD4 = 40323; // plays the 4th
		#endregion

		#region WM_USER (WM_WA_IPC) Type Constants
		const int IPC_ISPLAYING      = 104;		 // Returns status of playback. Returns: 1 = playing, 3 = paused, 0 = not playing)
		const int IPC_GETVERSION     = 0;	     // Returns Winamp version (0x20yx for winamp 2.yx,  Versions previous to Winamp 2.0
												 // typically (but not always) use 0x1zyx for 1.zx versions
		const int IPC_DELETE         = 101;		 // Clears Winamp internal playlist;
		const int IPC_GETOUTPUTTIME  = 105;		 // Returns the position in milliseconds of the 
												 // current song (mode = 0), or the song length, in seconds (mode = 1). It 
												 // returns: -1 if not playing or if there is an error.
		const int IPC_JUMPTOTIME     = 106;		 // Sets the position in milliseconds of the current song (approximately). It
												 // returns -1 if not playing, 1 on eof, or 0 if successful. It requires Winamp v1.60+
		const int IPC_WRITEPLAYLIST  = 120;		 // Writes the current playlist to <winampdir>\\Winamp.m3u, and returns the current 
												 // playlist position. It requires Winamp v1.666+
		const int IPC_SETPLAYLISTPOS = 121;		 // Sets the playlist position
		const int IPC_SETVOLUME      = 122;		 // Sets the volume of Winamp (from 0-255)
		const int IPC_SETPANNING     = 123;		 // Sets the panning of Winamp (from 0 (left) to 255 (right))
		const int IPC_GETLISTLENGTH  = 124;		 // Returns the length of the current playlist in tracks
		const int IPC_GETLISTPOS     = 125;      // Returns the playlist position. A lot like IPC_WRITEPLAYLIST only faster since it 
												 // doesn't have to write out the list. It requires Winamp v2.05+
		const int IPC_GETINFO        = 126;		 // Returns info about the current playing song (about Kb rate). The value it returns 
												 // depends on the value of 'mode'. If mode == 0 then it returns the Samplerate (i.e. 44100), 
												 // if mode == 1 then it returns the Bitrate  (i.e. 128), if mode == 2 then it returns the 
												 // channels (i.e. 2)

		const int IPC_GETEQDATA		 = 127;      // Queries the status of the EQ. The value it returns depends on what 'position' is set to. It
												 // requires Winamp v2.05+
												 // Value      Meaning
												 // ------------------
												 // 0-9        The 10 bands of EQ data. 0-63 (+20db - -20db)
												 // 10         The preamp value. 0-63 (+20db - -20db)
												 // 11         Enabled. zero if disabled, nonzero if enabled.
												 // 12         Autoload. zero if disabled, nonzero if enabled.


		const int IPC_SETEQDATA      = 128;		 // Sets the value of the last position retrieved by IPC_GETEQDATA (integer eqPosition). It
												 // requires Winamp v2.05+
		#endregion

		private static int eqPosition = 0;

		public WinampLib()
		{
		}

		#region Other useful Winamp Methods
		public static string GetCurrentSongTitle() 
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 

			if (hwnd.Equals(IntPtr.Zero)) 
				return "N/A";
 
			string lpText = new string((char) 0, 100);
			int intLength = GetWindowText(hwnd, lpText, lpText.Length);
            
			if ((intLength <= 0) || (intLength > lpText.Length)) 
				return "N/A";
 
			string strTitle = lpText.Substring(0, intLength);
			int intName = strTitle.IndexOf(strTtlEnd);
			int intLeft = strTitle.IndexOf("[");
			int intRight = strTitle.IndexOf("]");
 
			if ((intName >= 0) && (intLeft >= 0) && (intName < intLeft) && (intRight >= 0) && (intLeft + 1 < intRight))
				return strTitle.Substring(intLeft + 1, intRight - intLeft - 1);
 
			if ((strTitle.EndsWith(strTtlEnd)) && (strTitle.Length > strTtlEnd.Length))
				strTitle = strTitle.Substring(0, strTitle.Length - strTtlEnd.Length);
 
			int intDot = strTitle.IndexOf(".");
			if ((intDot > 0) && IsNumeric(strTitle.Substring(0, intDot)))
				strTitle = strTitle.Remove(0, intDot + 1);
 
			return strTitle.Trim();
		}

		private static bool IsNumeric(string Value)
		{
			try 
			{
				double.Parse(Value);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region WM_COMMAND Type Methods
		public static void Stop()
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_STOP, WA_NOTHING); 
		} 

		public static void Play() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_PLAY, WA_NOTHING); 
		} 

		public static void Pause() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_PAUSE, WA_NOTHING); 
		} 

		public static void PrevTrack() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_PREVTRACK, WA_NOTHING); 
		} 

		public static void NextTrack() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_NEXTTRACK, WA_NOTHING); 
		} 

		public static void VolumeUp() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_VOLUMEUP, WA_NOTHING); 
		} 

		public static void VolumeDown() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WA_VOLUMEDOWN, WA_NOTHING); 
		} 

		public static void Forward5Sec() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WINAMP_FFWD5S, WA_NOTHING); 
		} 

		public static void Rewind5Sec() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_COMMAND, WINAMP_REW5S, WA_NOTHING); 
		}
		#endregion

		#region WM_USER (WM_WA_IPC) Type Methods
		public static int GetPlaybackStatus() 
		{ 
			IntPtr hwnd = FindWindow(m_windowName, null); 
			return SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_ISPLAYING);
		}

		public static int GetWinampVersion()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			return SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_GETVERSION);
		}

		public static void DeleteCurrentPlaylist()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_DELETE);
		}

		public static void SavePlaylist()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_WRITEPLAYLIST);
		}

		public static int GetPlaylistPosition()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			return SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_GETLISTPOS);
		}

		public static void SetPlaylistPosition(int position)
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, position, IPC_SETPLAYLISTPOS);
		}

		public static int GetTrackPosition()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			return SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_GETOUTPUTTIME);
		}

		public static int GetTrackCount()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			return SendMessageA(hwnd, WM_WA_IPC, WA_NOTHING, IPC_GETLISTLENGTH);
		}

		public static void JumpToTrackPosition(int position)
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, position, IPC_JUMPTOTIME);
		}

		public static void SetVolume(int position)
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, position, IPC_SETVOLUME);
		}

		public static void SetPanning(int position)
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, position, IPC_SETPANNING);
		}

		public static void GetTrackInfo(int mode)
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, mode, IPC_GETINFO);
		}

		public static void GetEqData(int position)
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			eqPosition = SendMessageA(hwnd, WM_WA_IPC, position, IPC_GETEQDATA);
		}

		public static int SetEqData()
		{
			IntPtr hwnd = FindWindow(m_windowName, null); 
			SendMessageA(hwnd, WM_WA_IPC, eqPosition, IPC_SETEQDATA);
			return eqPosition;
		}

		#endregion
	}
}
