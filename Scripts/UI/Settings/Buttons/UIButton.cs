using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIButton : MonoBehaviour
{
    public enum buttonType { CloseGame, OpenWindow, CloseWindow, Fullscreen, Start };
    public buttonType type;

    public float lowOpacity = 0.5f;
    public float highOpacity = 1f;

    public GameObject window;
    public GameObject checkMark;
    public bool menuButton;

    private GameObject clickedSFX;
    private GameObject mouseOverSFX;

    void Start()
    {
        if (menuButton)
        {
            mouseOverSFX = Resources.Load<GameObject>("SFX/UI/titleSelectedSFX") as GameObject;
        }

        switch (type)
        {
            case buttonType.CloseGame:
                decreaseOpacity();
                clickedSFX = Resources.Load<GameObject>("SFX/UI/clickedSFX") as GameObject;
                break;
            case buttonType.OpenWindow:
                decreaseOpacity();
                clickedSFX = Resources.Load<GameObject>("SFX/UI/clickedSFX") as GameObject;
                break;
            case buttonType.CloseWindow:
                decreaseOpacity();
                clickedSFX = Resources.Load<GameObject>("SFX/UI/clickedSFX") as GameObject;
                break;
            case buttonType.Fullscreen:
                setCheckMark(SaveLoad.current.fullscreen);
                clickedSFX = Resources.Load<GameObject>("SFX/UI/clickedSFX") as GameObject;
                break;
            case buttonType.Start:
                decreaseOpacity();
                setStartButtonText();
                clickedSFX = Resources.Load<GameObject>("SFX/UI/startSFX") as GameObject;
                break;
            default:
                Debug.Log("Invalid ButtonType Case");
                break;
        }
    }

    private void setStartButtonText()
    {
        if (!SaveLoad.current.newGame)
        {
            GetComponentInChildren<Text>().text = "Continue";
        }
    }

    private void increaseOpacity()
    {
        Color temp = GetComponent<Image>().color;
        temp.a = highOpacity;
        GetComponent<Image>().color = temp;
    }

    private void decreaseOpacity()
    {
        Color temp = GetComponent<Image>().color;
        temp.a = lowOpacity;
        GetComponent<Image>().color = temp;
    }

    private void setCheckMark(bool x)
    {
        checkMark.SetActive(x);
    }

    private void toggleFullScreen()
    {
        Library.toggleFullScreen();
        setCheckMark(SaveLoad.current.fullscreen);
    }

    private void closeWindow()
    {
        window.SetActive(false);
    }

    private void openWindow()
    {
        window.SetActive(true);
    }


    private void playMouseOverSound()
    {
        if (mouseOverSFX != null)
        {
            Instantiate(mouseOverSFX);
        }
    }

    private void playClickedSound()
    {
        if (clickedSFX != null)
        {
            Instantiate(clickedSFX);
        }
    }

    private void startGame()
    {
        if (SaveLoad.current.newGame)
        {
            //start fade
            SceneManager.LoadScene("DialogueScene");
        }
        else
        {
            //load hub
        }
        
    }

    public void buttonPressed()
    {
        playClickedSound();
        switch (type)
        {
            case buttonType.CloseGame:
                Application.Quit();
                Debug.Log(Time.time + " - Game Closed");
                break;
            case buttonType.OpenWindow:
                openWindow();
                break;
            case buttonType.CloseWindow:
                closeWindow();
                break;
            case buttonType.Fullscreen:
                toggleFullScreen();
                break;
            case buttonType.Start:
                startGame();
                break;
            default:
                Debug.Log("Invalid ButtonType Case");
                break;
        }
    }

    public void pointerEnter()
    {
        playMouseOverSound();
        switch (type)
        {
            case buttonType.CloseGame:
                increaseOpacity();
                break;
            case buttonType.OpenWindow:
                increaseOpacity();
                break;
            case buttonType.CloseWindow:
                increaseOpacity();
                break;
            case buttonType.Fullscreen:
                //
                break;
            case buttonType.Start:
                increaseOpacity();
                break;
            default:
                Debug.Log("Invalid ButtonType Case");
                break;
        }
    }
    
    public void pointerExit()
    {
        switch (type)
        {
            case buttonType.CloseGame:
                decreaseOpacity();
                break;
            case buttonType.OpenWindow:
                decreaseOpacity();
                break;
            case buttonType.CloseWindow:
                decreaseOpacity();
                break;
            case buttonType.Fullscreen:
                //
                break;
            case buttonType.Start:
                decreaseOpacity();
                break;
            default:
                Debug.Log("Invalid ButtonType Case");
                break;
        }
    }

}
