using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;


namespace MoreMountains.TopDownEngine
{
    public class AbilityIcon : MonoBehaviour
    {
        public int abilityIndex;
        private Image spellIcon;
        public Image cooldownImage;
        public Text cooldownText;

        public GameObject tooltip;
        public Text tooltipName;
        public Text tooltipDescription;

        // Start is called before the first frame update
        void Start()
        {
            spellIcon = GetComponent<Image>();
            setSpellIcon();
            GetComponentInParent<SpellsPanel>().abilities[abilityIndex] = this;
            cooldownText.enabled = false;

            tooltip.SetActive(false);
            tooltipName.text = SaveLoad.current.currentHero.abilityNames[abilityIndex];
            tooltipDescription.text = SaveLoad.current.currentHero.abilityDescription[abilityIndex];
        }
        
        public void showTooltip()
        {
            if (StaticManager.cursorMode)
            {
                tooltip.SetActive(true);
            }
        }

        public void hideToolTip()
        {
            tooltip.SetActive(false);
        }

        public void setSpellIcon()
        {
            spellIcon.overrideSprite = Resources.Load<Sprite>(SaveLoad.current.currentHero.equippedAbilitiesIcons[abilityIndex]);
        }

        public void updateRadial(float x, float x1)
        {
            cooldownText.enabled = true;

            cooldownText.text = System.Math.Round(x, 1).ToString("0.0");
            cooldownImage.fillAmount = (x / x1);

            if (cooldownImage.fillAmount <= 0)
            {
                cooldownImage.fillAmount = 0;
                cooldownText.enabled = false;
            }

        }

    }


}

