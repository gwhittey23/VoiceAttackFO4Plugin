﻿	I have change most of the commands in profile so that now all have to do is speak the name inorder to have action. This is for most actions except for fast travel,home and direction commands 
	because the location list is so long having multiple list can make editing sluggish. 
	
	
	I have provided some premade commands in profile for you to use.
		-"Fast Travel to" or "Teleport to" + name of the location and it will send you to that location if it is unlocked ingame
			ex: "Fast Travel to Bunker Hill" will fast travel to bunker hill
		-"Set home to" + name of a location will set a home fast travel option
		-"Go Home" Will fast travel to the home point you sent(Vault 111 if have not set one)
		-"Get Directions to" + name of location will say back the compass heading that location is towards.
		-"Heading" or "Directions" Will reuse last Directions you got.
		-"Setup" is used to set Host ip/port of server different from default. Change two text vaules in this command to what you want.
		
	To make your own VoiceAttack commands you need to send following values to the plugin.

	The host ip/port is coded to default of 127.0.0.1(localhost) and port of 8089. If want to change this send text vaules with your commands 
to the plugin HOST and PORT.
	
	-To issue a fast travel command you must set text value called FT_Location to the name of location you want to goto and text value called SentCommand 
	must be set to FastTravel

	-To get direction to a location even if not on your map, sned text value called Direction_Location with SentCommand set to LocationDirections
	-To get directions to a Quest that you have active on your map send text value with name of quest and SentCommand set to QuestDirections
	

	
	Once server completes the command it will send back ServerResponse which VoiceAttack can access via {TXT:ServerResponse}

	
	List of Text Variables use in VoiceAtacck commands to send to the Plugin.
-------------------------------------------------------------------------------------------

	*Changed way plugin interacts with VoiceAttack so all need to now send to the plugin is 
	HOST,PORT,SentCommand,SentName
	
	HOST - Used to set HOST ip defaults to 127.0.0.1 in plugin
	PORT - Used to set HOST Port defaults to 8089 in Plugin
	
	SentName - Can be any valid name for location,food,weapon,grenade etc. 
	SentCommand - Used to control how the plugin will process you commands. Set it to following to get effect
		FastTravel - Fast travel to {TXT:FT_Location}
		LocationDirections - Set to get Directions to {TXT:Direction_Location}
		ToggleRadio - Turns current radion staion on/off
		ChangeStation - Change radio to {TXT:StationName}
		NextStation - Cycles to next in range radio station
		QuestDirections - Will get directions to the spoke quest step. *This command not working that well due to having to use dictation 
						  engine to get the text which it does not work well. I am planning on make this better somehow.
		GrenadeEquip - Will equip grenade/mine/moltov of {TXT:GrenadeName}
		NextGrenade - Will cycle to next grenade in your invetory(right now it is alpha sorting I am going to be making it also dmg based and other sort )
			New SentCommand items added 
		EquipWeapon - Will Equip a sent weapon name. 
		EatFood - Will eat what ever food name you send.


	These items are TXT values are decreapted but still can use them, SentName replaces all these as I went in a different direction on server side that I was planning.
	FT_Location - Used to set fast travel location that want sent to Fallout 4. Must be a vailid location on map and one you can FT too.
	Home_Location - USed to send home location that can set.
	Direction_Location - Used to get directions to a valid location on map. Can be any location even un discovered ones.
	StationName - USed to set valid radio station name

	
	
	
	
	-To come:
			
			-Add Invetory equiping.useing via name of item.(On hold as inventory system data that fallout 4 is sending is hoocky and it sendign false items that can will crash the game.)
			-Add a next mine and next grenade commands as well as current one that does both.
			-Add in status monitoring that will monitor your stats and give verbal feedback if the go below/over a set value.
				 
				    
					    