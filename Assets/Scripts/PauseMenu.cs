using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenuCanvas;
    [SerializeField]private GameObject playerDeadCan;
    private float currentTimeScale;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenuCanvas.activeInHierarchy)
        {
            currentTimeScale = Time.timeScale;
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenuCanvas.activeInHierarchy)
        {
            Resume();
        }
    }


    public void PlayerDead()
    {
        playerDeadCan.SetActive(true);
        Time.timeScale = 0;
        enabled = false;
    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = currentTimeScale;
    }

    


    
}
