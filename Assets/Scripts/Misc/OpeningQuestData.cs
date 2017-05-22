using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState { Waiting, Pickup, Staple, NeedToReload, FinishStaple, MeetBoss, MeetingBoss, BacktoWork, WifeCallState, Finish}

public class OpeningQuestData : MonoBehaviour {

    public QuestState questState;
    public GameObject[] NarrativeLines;

    public GameObject StaplerLine;
    public GameObject StaplerAmmo;
    public GameObject JustAFewMore;

	void Start () {
        questState = QuestState.Waiting;
	}

	public void StaplerFlag(){
		StaplerLine.SetActive(true);
	}

	public void NextState(){
		switch(questState)
        {
            case QuestState.Waiting:
                questState = QuestState.Pickup;
                NarrativeLines[0].SetActive(false);
                NarrativeLines[1].SetActive(true);
                break;
            case QuestState.Pickup:
				questState = QuestState.Staple;
				NarrativeLines[1].SetActive(false);
                NarrativeLines[2].SetActive(true);
                break;
            case QuestState.Staple:
				questState = QuestState.NeedToReload;
				NarrativeLines[2].SetActive(false);
                NarrativeLines[3].SetActive(true);
               	StaplerAmmo.SetActive(true);
                break;
            case QuestState.NeedToReload:
				questState = QuestState.FinishStaple;
				NarrativeLines[3].SetActive(false);
                NarrativeLines[4].SetActive(true);
                break;
            case QuestState.FinishStaple:
				questState = QuestState.MeetBoss;
				NarrativeLines[4].SetActive(false);
                NarrativeLines[5].SetActive(true);
                break;
            case QuestState.MeetBoss:
				questState = QuestState.MeetingBoss;
				NarrativeLines[5].SetActive(false);
                NarrativeLines[6].SetActive(true);
                break;
            case QuestState.MeetingBoss:
				questState = QuestState.BacktoWork;
				NarrativeLines[6].SetActive(false);
                NarrativeLines[7].SetActive(true);

                break;
            case QuestState.BacktoWork:
				questState = QuestState.WifeCallState;
				NarrativeLines[7].SetActive(false);
                NarrativeLines[8].SetActive(true);

                break;
            case QuestState.WifeCallState:
				questState = QuestState.Finish;
                break;
        }
	}


	private void EndReact(string ID){

		Debug.Log(ID);

		if(ID == "2_4B"){
			NextState();
		}
		else if (ID == "5_3_Right_Over"){
			GameObject Stapler = GameObject.FindGameObjectWithTag("Stapler");
			Destroy(Stapler);
			NextState();
		}
		else if (ID == "6_2_Take_a_seat"){
			NextState();
		}
		else if (ID == "7_10_Six_sides"){
			NextState();
		}
		else if (ID == "8_1_How"){
			NextState();
		}
	}


}
