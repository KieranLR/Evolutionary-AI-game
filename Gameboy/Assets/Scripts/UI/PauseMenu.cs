using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public static bool timeStopped;
    public GameObject pauseMenu;

    public void Start() {
        DontDestroyOnLoad(this);
    }
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
	}

    public void Resume() {
        pauseMenu.SetActive(false);
        ResumeTime();
        isPaused = false;
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        StopTime();
        isPaused = true;

    }

    public void StopTime() {
        Time.timeScale = 0f;
        timeStopped = true;
    }

    public void ResumeTime() {
        Time.timeScale = 1f;
        timeStopped = false;
    }

    public void Exit() {
        Application.Quit();
    }
}
