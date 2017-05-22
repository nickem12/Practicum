using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    private Color Morning;
    private Color Noon;
    private Color Evening;
    private Color colorToReturn;
    public float smooth = 2;
    public bool eveningTrigger = true;
    public float speedOfSun = 5f;
    public Light[] spotlight;
    private int counter = 0;
    private GameObject thePlayer;
    private Color nightSky;
    private Color daySky;
    private Color morningSky;
    private Color eveningSky;
    public GameObject[] allStars;
    public GameObject shootingStar;
    private bool shootingStarCreated = false;

	// Use this for initialization
	void Start () {

        Morning = new Color(253f, 90f, 52f);
        Noon = new Color(255f, 255f, 255f);
        Evening = new Color(255f, 101f, 162f);
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        nightSky = new Color(0, 0, 1);
        daySky = new Color(1, 1, 3);
        morningSky = new Color(5, 2, 1);
        eveningSky = new Color(4, 2, 1);

	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, speedOfSun * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        for(counter = 0; counter < spotlight.Length; counter++)
        {
            spotlight[counter].transform.LookAt(transform.position);
        }

       

        GetComponent<Light>().intensity = transform.position.y / 25000;

        if(transform.position.y >= 495f)
        {
            eveningTrigger = true;
        }
        if(transform.position.y <= -495)
        {
            eveningTrigger = false;
        }


        //Morning
        if(transform.position.y > -100 && transform.position.y < 250 && !eveningTrigger)
        {
            thePlayer.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(thePlayer.GetComponentInChildren<Camera>().backgroundColor, morningSky, Time.deltaTime * smooth);
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, Morning, Time.deltaTime * smooth);

            for (counter = 0; counter < allStars.Length; counter++)
            {
                allStars[counter].SetActive(false);
            }
            shootingStarCreated = false;
        }

        //Noon
        if(transform.position.y > 250)
        {
            thePlayer.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(thePlayer.GetComponentInChildren<Camera>().backgroundColor, daySky, Time.deltaTime * smooth);
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, Noon, Time.deltaTime * smooth);
        }

        //Evening
        if (transform.position.y > 0 && transform.position.y < 350 && eveningTrigger)
        {
            thePlayer.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(thePlayer.GetComponentInChildren<Camera>().backgroundColor, eveningSky, Time.deltaTime * smooth);
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, Evening, Time.deltaTime * smooth);

           
        }
          
        //Night
      if(transform.position.y < 0)
        {
            thePlayer.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(thePlayer.GetComponentInChildren<Camera>().backgroundColor, nightSky, Time.deltaTime * smooth);
            for (counter = 0; counter < allStars.Length; counter++)
            {
                allStars[counter].SetActive(true);
            }
            if (!shootingStarCreated)
            {
                //shootingStar.GetComponent<MeshRenderer>().enabled = true;
                shootingStar.GetComponentInChildren<ParticleSystem>().Play();
                shootingStarCreated = true;
            }
        }

      if(shootingStarCreated)
        {
            shootingStar.transform.Translate(Vector3.left * Time.deltaTime * 700f);
        }

	}

    

}
