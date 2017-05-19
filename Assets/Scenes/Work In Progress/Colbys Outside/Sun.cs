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
	// Use this for initialization
	void Start () {

        Morning = new Color(253f, 90f, 52f);
        Noon = new Color(255f, 255f, 255f);
        Evening = new Color(255f, 101f, 162f);
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, 10f * Time.deltaTime);
        transform.LookAt(Vector3.zero);

        GetComponent<Light>().intensity = transform.position.y / 25000;

        if(transform.position.y >= 495f)
        {
            eveningTrigger = true;
        }
        if(transform.position.y <= -495)
        {
            eveningTrigger = false;
        }
        if(transform.position.y > -200 && transform.position.y < 250 && !eveningTrigger)
        {
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, Morning, Time.deltaTime * smooth);
            
        }
        if(transform.position.y > 250)
        {
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, Noon, Time.deltaTime * smooth);
        }

        if (transform.position.y > 0 && transform.position.y < 350 && eveningTrigger)
        {
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, Evening, Time.deltaTime * smooth);
        }
        Debug.Log(GetComponent<Light>().color);

	}

    

}
