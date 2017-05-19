using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState { Waiting, Pickup, Staple, NeedToReload, FinishStaple, MeetBoss, MeetingBoss, BacktoWork, WifeCallState, Finish}

public class OpeningQuestData : MonoBehaviour {

    public QuestState questState;
    public GameObject[] NarrativeLines;


	void Start () {
        questState = QuestState.Waiting;
	}


	public void NextState(){
		switch(questState)
        {
            case QuestState.Waiting:
                questState = QuestState.Pickup;
                break;
            case QuestState.Pickup:
				questState = QuestState.Staple;
                break;
            case QuestState.Staple:
				questState = QuestState.NeedToReload;
                break;
            case QuestState.NeedToReload:
				questState = QuestState.FinishStaple;
                break;
            case QuestState.FinishStaple:
				questState = QuestState.MeetBoss;
                break;
            case QuestState.MeetBoss:
				questState = QuestState.MeetingBoss;
                break;
            case QuestState.MeetingBoss:
				questState = QuestState.BacktoWork;
                break;
            case QuestState.BacktoWork:
				questState = QuestState.WifeCallState;
                break;
            case QuestState.WifeCallState:
				questState = QuestState.Finish;
                break;
        }
	}

}
