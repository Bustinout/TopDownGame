using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveTracker : MonoBehaviour
{
    public Text mainText;
    public Text bonusText;
    public Slider timer;

    public bool timerEnabled;

    void Start()
    {
        StaticManager.objectiveTracker = this;
    }

    public void refresh() //called by room manager
    {
        updateText();
        updateTimer();
    }

    /*
    1 - take x amount of damage
    2 - take no damage
    3 - kill streak (no long interval between kills)
    4 - kill all before timer
    5 - kill stationary mob before timer
    6 - kill special escaping mob
    7 - channel object before timer
    */

    public string bonusTextDescription(int x) //change wording for more immersion
    {
        switch (x)
        {
            case 1:
                return ("Bonus: Take " + StaticManager.currentRoom.BonusObjectiveRequred + " Damage");
            case 2:
                return ("Bonus: Take No Damage");
            case 3:
                return ("Bonus: Kill All Enemies Within " + StaticManager.currentRoom.killStreakWindow + " Seconds of Each Other");
            case 4:
                return ("Bonus: Kill All Enemies Within " + StaticManager.currentRoom.timeLeft + " Seconds");
            case 5:
                return ("Bonus: Destroy X Before Within " + StaticManager.currentRoom.timeLeft + " Seconds");
            case 6:
                return ("Bonus: Kill the X Before It Escapes");
            case 7:
                return ("Bonus: Collect/Purity the whatever before you know");
            default:
                return ("[BUG] - Invalid BonusObjective");
        }
    }


    public void updateText()
    {
        if (StaticManager.currentRoom.BonusCompleted)
        {
            bonusText.text = "Bonus Objective Completed";
            timer.value = 0;
            //spawn particle
        }
        else if (StaticManager.currentRoom.BonusFailed)
        {
            bonusText.text = "Bonus Objective Failed";
            timer.value = 0;
            //spawn particle
        }
        else
        {
            bonusText.text = bonusTextDescription(StaticManager.currentRoom.BonusObjective);
        }


        if (StaticManager.currentRoom.enemiesOnScreen > 0)
        {
            mainText.text = "Slay the Invaders";
        }
        else
        {
            mainText.text = "Enter the Portal";
        }
    }

    public void updateTimer()
    {
        if (timerEnabled)
        {
            timer.value = StaticManager.currentRoom.timeLeft / StaticManager.currentRoom.totalTime;
        }
        else
        {
            timer.value = 0;
        }
    }


}
