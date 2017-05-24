using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum QuestState { Waiting, Pickup, Staple, NeedToReload, FinishStaple, MeetBoss, MeetingBoss, BacktoWork, WifeCallState, Finish}

[RequireComponent(typeof(PlayerStartRotation))]

public class OpeningQuestData : MonoBehaviour {

    public QuestState questState;
    public GameObject[] NarrativeLines;

    public GameObject StaplerLine;
    public GameObject StaplerAmmo;
    public GameObject JustAFewMore;
    public GameObject BossChair;
    public AudioClip EndMusic;

    private bool FadingOut = false;

    private bool MusicFlag = false;
    private GameObject PlayerObject;

    private PlayerStartRotation SittingComp;
    private bool hasSat = false;

    private float MaxVolume = 0.45f;
    private float VolumePerc = 0.0f;
    private float VolumeIncrease = 0.2f;

	void Start () {
        questState = QuestState.Waiting;
        SittingComp = this.GetComponent<PlayerStartRotation>();
        BossChair.GetComponent<PlayerStartRotation>().ableToSit = false;
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
	}

	void Update(){

		if(questState == QuestState.Pickup && !SittingComp.sitting){
			SittingComp.ableToSit = false;
		}
		else if (questState == QuestState.Staple && !SittingComp.sitting){
			SittingComp.ableToSit = true;
		}
		else if (questState == QuestState.Staple && SittingComp.sitting){
			SittingComp.ableToSit = false;
		}
		else if (questState == QuestState.NeedToReload && SittingComp.sitting){
			SittingComp.ableToSit = true;
		}
		else if (questState == QuestState.NeedToReload && !SittingComp.sitting){
			SittingComp.ableToSit = false;
		}
		else if (questState == QuestState.FinishStaple && !SittingComp.sitting){
			SittingComp.ableToSit = true;
		}
		else if (questState == QuestState.FinishStaple && SittingComp.sitting){
			SittingComp.ableToSit = false;
		}
		else if (questState == QuestState.MeetBoss && SittingComp.sitting){
			SittingComp.ableToSit = true;
		}
		else if (questState == QuestState.MeetBoss && !SittingComp.sitting){
			SittingComp.ableToSit = false;
		}
		else if(questState == QuestState.MeetingBoss && !hasSat){
			if(BossChair.GetComponent<PlayerStartRotation>().sitting){
				hasSat = true;
				NarrativeLines[5].SetActive(false);
                NarrativeLines[6].SetActive(true);
			}
		}
		else if (questState == QuestState.MeetingBoss && BossChair.GetComponent<PlayerStartRotation>().sitting){
			BossChair.GetComponent<PlayerStartRotation>().ableToSit = false;
		}
		else if (questState == QuestState.BacktoWork && !BossChair.GetComponent<PlayerStartRotation>().sitting){
			BossChair.GetComponent<PlayerStartRotation>().ableToSit = false;
			SittingComp.ableToSit = true;
		}
		else if (questState == QuestState.BacktoWork && BossChair.GetComponent<PlayerStartRotation>().sitting){
			BossChair.GetComponent<PlayerStartRotation>().ableToSit = true;
			SittingComp.ableToSit = true;
		}
		else if (questState == QuestState.WifeCallState && SittingComp.sitting){
			SittingComp.ableToSit = false;
			NarrativeLines[8].SetActive(true);

			if(MusicFlag){
				VolumePerc += VolumeIncrease * Time.deltaTime;
				PlayerObject.GetComponent<AudioSource>().volume = VolumePerc * MaxVolume;
				if(PlayerObject.GetComponent<AudioSource>().volume > MaxVolume) PlayerObject.GetComponent<AudioSource>().volume = MaxVolume;
			}
			else if(!MusicFlag){
				PlayerObject.GetComponent<AudioSource>().clip = EndMusic;
				PlayerObject.GetComponent<AudioSource>().volume = 0.0f;
				PlayerObject.GetComponent<AudioSource>().Play();
				MusicFlag = true;
			}
		}
		else if(FadingOut){
			float alpha = this.GetComponent<Fading>().GetAlpha();
			if(alpha <= 0.01f){
				Destroy(PlayerObject);
				SceneManager.LoadScene("Outside");
			}
		}
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
				BossChair.GetComponent<PlayerStartRotation>().ableToSit = true;
				if(hasSat){
					NarrativeLines[5].SetActive(false);
                	NarrativeLines[6].SetActive(true);
                }
                break;
            case QuestState.MeetingBoss:
				questState = QuestState.BacktoWork;
				NarrativeLines[6].SetActive(false);
                NarrativeLines[7].SetActive(true);

                break;
            case QuestState.BacktoWork:
				questState = QuestState.WifeCallState;
				NarrativeLines[7].SetActive(false);
                

                break;
            case QuestState.WifeCallState:
				questState = QuestState.Finish;
                break;
        }
	}


	private void EndReact(string ID){

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
		else if (ID == "9_19_Janice"){
			this.GetComponent<Fading>().enabled = true;
			FadingOut = true;

			Destroy(PlayerObject);
			SceneManager.LoadScene("Outside");
		}
	}


}
