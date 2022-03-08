MetronomeUICreation.md

Goals

	Create a sprite for each beat of the metronome and move it to its apprpriate position

Inputs

	Transform StartPoint //Contains the point where the beat should begin
	Transform EndPoint  //Contains the point where beat should end

	Int NumberOfBeats // Contains the number of beats that should be created

	GameObject BeatTemplate //GameObject that will be duplicated   


Output
	Creates n number of Beat sprites and places them evenly
Steps

	Calculate X distance between StartPoint and EndPoint
	Set BeatTemplates Location To StartPoint
	
	For i in range (1, NumberOfBeats) (<)
		Instantiate(BeatTemplate) and move it right ( i * Distance)

			
