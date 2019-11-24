using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
    public string[] dialogue;

    public Text text;
    public GameObject soundEffect;
    //change to contain index to a soundeffect stored in a prefab script, then just set index in dialogue setter

    public bool demoEnd;

    //private Image bgImage;
    private bool playing;

    public GameObject inputNameWindow;
    public string inputName;
    public bool inputtingName; //prevents spacebar from moving on

    void Start()
    {
        StaticManager.dialogueBox = this;
        text.text = "";
        testDialogue();
    }

    public string[] randomDialogue()
    {
        if (demoEnd)
        {
            return new string[] { "There's supposed to be a boss stage here.", "But, I haven't gotten to that yet.", "If you've made it this far, I'm quite impressed.", "Anyways, thanks for checkin out the demo!" };
        }

        int roll = Random.Range(0, 4);
        if (roll <= 2)
        {
            return new string[] { "Hello there.", "This is a super early version of the game.", "Use WASD to move, LMB and RMB to attack, SHIFT to dodge.", "Use TAB to open the inventory and ESC to pause.", "Enjoy." };
        }
        else
        {
            loadMusic("Sounds/temp/Skyrim_Far_Horizons");
            return new string[] { "Hey, you.", "You're finally awake.", "You were trying to cross the border, right?", "Walked right into that Imperial ambush, same as us, and that thief over there.", "*CRASH*", "By the nine divines!" };
        }
    }

    private void loadMusic(string x)
    {
        StaticManager.musicmanager.AS.clip = (AudioClip)Resources.Load(x);
        StaticManager.musicmanager.AS.Play();
    }

    public void testDialogue()
    {
        dialogue = randomDialogue();
        playDialogue(0);
    }

    public void confirmName()
    {
        //set the name

        //play some postname dialogue
    }


    public void playDialogue(int x)
    {
        text.text = "";
        arrayIndex = x;
        sentenceIndex = 0;
        //set bgImage to whatever [x]
        StartCoroutine("playText");
        playing = true;
    }

    public void inputNameStart()
    {
        inputNameWindow.SetActive(true);
        inputtingName = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inputtingName)
            {
                return;
            }

            if (playing)
            {
                StopCoroutine("playText");
                text.text = dialogue[arrayIndex];
                playing = false;
            }
            else
            {
                if (arrayIndex < dialogue.Length-1)
                {
                    playDialogue(arrayIndex + 1);
                }
                else
                {
                    if (demoEnd)
                    {
                        SceneManager.LoadScene("Titlescreen");
                    }
                    SceneManager.LoadScene("PlainsLevel");
                }
            }
        }
    }


    private int sentenceIndex;
    private int arrayIndex;
    IEnumerator playText()
    {
        while (sentenceIndex < dialogue[arrayIndex].Length)
        {
            text.text += dialogue[arrayIndex].ToCharArray()[sentenceIndex];

            if (sentenceIndex%3 == 1)
            {
                Instantiate(soundEffect);
            }
            

            sentenceIndex++;
            yield return new WaitForSecondsRealtime(0.025f);
        }
        playing = false;
    }

}
