using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseM : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Reference;  //Drag Camera here
    public GameObject Reference_1; //Drag Player_1 here

    public GameObject pauseMenuUI;


    //Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //If ESC is pressed
        {
            if (GameIsPaused) 
            {
                Resume();
                Debug.Log("Resumed"); //just for debugging
            } else
            {
                Pause();
                Debug.Log("Paused"); //just for debugging
            }
        }
    }
    public void Resume() //function for pressing "resume"
    {
        pauseMenuUI.SetActive(false); //RESUMED //
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameIsPaused = !GameIsPaused; //resets

         Reference.GetComponent<Camera_FPS>().enabled = true; //some odd glitch?
         Reference_1.GetComponent<Camera_FPS>().enabled = true; //some odd glitch?
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true); //PAUSED //
        Time.timeScale = 0f;
        GameIsPaused = true;

        Reference.GetComponent<Camera_FPS>().enabled = false; //^^
        Reference_1.GetComponent<Camera_FPS>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;


    }
}