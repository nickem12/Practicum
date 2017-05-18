using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState { Waiting, Pickup, Staple, NeedToReload, FinishStaple, MeetBoss, MeetingBoss, BacktoWork, WifeCallState, Finish}

public class OpeningQuestData : MonoBehaviour {

    public QuestState questState;
    bool lockSittingOption;
    bool finishedStapling;
    bool pickUpStaple;
    public bool instructions;

	// Use this for initialization
	void Start () {
        questState = QuestState.Waiting;
	}
	
	// Update is called once per frame
	void Update () {
		
        switch(questState)
        {
            case QuestState.Waiting:
                if (instructions) questState = QuestState.Pickup;
                break;
            case QuestState.Pickup:
                break;
            case QuestState.Staple:
                break;
            case QuestState.NeedToReload:
                break;
            case QuestState.FinishStaple:
                break;
            case QuestState.MeetBoss:
                break;
            case QuestState.MeetingBoss:
                break;
            case QuestState.BacktoWork:
                break;
            case QuestState.WifeCallState:
                break;
            case QuestState.Finish:
                break;
        }
	}
}
