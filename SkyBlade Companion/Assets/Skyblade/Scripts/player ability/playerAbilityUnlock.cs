using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playerAbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockDropBomb;
    

    public GameObject pickupEffect;
    public string unlockMessage;
    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            playerAbilityTracker player=collision.GetComponentInParent<playerAbilityTracker>();
            if(unlockDoubleJump)
            {
                player.canDoubleJump = true;
            }

            if (unlockDash)
            {
                player.canDash = true;
            }

            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
            }

            if (unlockDropBomb)
            {
                player.canDropBomb = true;
            }
            Instantiate(pickupEffect,transform.position,transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;
            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);
            audioManager.instance.PlayAFX(6);

        }
    }
}
