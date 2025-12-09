
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Image image;
    private bool haveItemImage;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void UpdateSlotImage(Sprite itemSprite)
    {
        if (itemSprite == null)
        {
            image.sprite = null;
            if (haveItemImage)
                Activated(false);
            return;
        }

        image.sprite = itemSprite;
        if (!haveItemImage)
            Activated(true);
        
            
    }

    void Activated(bool isActivated)
    {
        haveItemImage = isActivated;
        gameObject.SetActive(isActivated);
    }

    // void SlotMoveAnim(ItemInformation itemInformation, int direction)
    // {
    //     if(image.sprite == null)
    //         return;

    //     if (slotIndex == slotsNum / 2)
    //     {
    //         if (direction == -1)
    //             slotAnimation.Play($"{slotIndex}_Smaller");
    //         else
    //             slotAnimation.Play($"{slotIndex}_Bigger");
    //     }
    //     else if (slotIndex < slotsNum / 2)
    //     {
    //         if (direction == 1)
    //             slotAnimation.Play($"{slotIndex}_Bigger");
    //         else
    //             slotAnimation.Play($"{slotIndex}_Smaller");
    //     }
    //     else
    //     {
    //         if (direction == 1)
    //             slotAnimation.Play($"{slotIndex}_Smaller");
    //         else
    //             slotAnimation.Play($"{slotIndex}_Bigger");
    //     }
    //     StartCoroutine(WaitAnimFinish(itemInformation));
    // }
    // IEnumerator WaitAnimFinish(ItemInformation itemInformation)
    // {
    //     while (slotAnimation.isPlaying)
    //     {
    //         yield return null;
    //     }
    //     image.sprite = itemInformation.Sprite;
    //     if (!haveItemImage)
    //         Activated(true);
    // }
    // this is just move production. Not affect to indicator
}
