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

	HOST - Used to set HOST ip defaults to 127.0.0.1 in plugin
	PORT - Used to set HOST Port defaults to 8089 in Plugin
	FT_Location - Used to set fast travel location that want sent to Fallout 4. Must be a vailid location on map and one you can FT too.
	Home_Location - USed to send home location that can set.
	Direction_Location - Used to get directions to a valid location on map. Can be any location even un discovered ones.
	StationName - USed to set valid radio station name
	SentCommand - Used to control how the plugin will process you commands. Set it to following to get effect
		FastTravel - Fast travel to {TXT:FT_Location}
		LocationDirections - Set to get Directions to {TXT:Direction_Location}
		ToggleRadio - Turns current radion staion on/off
		ChangeStation - Change radio to {TXT:StationName}
		NextStation - Cycles to next in range radio station
		QuestDirections - Will get directions to the spoke quest step. *This command not working that well due to having to use dictation 
						  engine to get the text which it does not work well. I am planning on make this better somehow.

	
	
	
	
	
	
	-To come:
			
			-Add Invetory equiping.useing via name of item.
			-Add in status monitoring that will monitor your stats and give verbal feedback if the go below/over a set value.
				 
