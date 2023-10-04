using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection == GameController.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            if(currentCharacterSelection < 0)
                currentCharacterSelection = GameController.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameController.instance.playerSprites[currentCharacterSelection];
        GameController.instance.player.SwapSprite(currentCharacterSelection);
    }

    public void OnUpgradeClick()
    {
        if(GameController.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    public void UpdateMenu()
    {

        weaponSprite.sprite = GameController.instance.weaponSprites[GameController.instance.weapon.weaponLevel];
        if(GameController.instance.weapon.weaponLevel == GameController.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameController.instance.weaponPrices[GameController.instance.weapon.weaponLevel].ToString();

        levelText.text = GameController.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameController.instance.player.hitpoint.ToString();
        pesosText.text = GameController.instance.pesos.ToString();

        int currLevel = GameController.instance.GetCurrentLevel();
        if(currLevel == GameController.instance.xpTable.Count)
        {
            xpText.text = GameController.instance.experience.ToString() + "total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameController.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameController.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameController.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
        
    }

}
