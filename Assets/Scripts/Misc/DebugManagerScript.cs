using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManagerScript : MonoBehaviour {

    static bool created = false;
    private void Start()
    {
        if(!created)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
        
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
