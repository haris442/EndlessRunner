using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinCollect : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    private int collectedCoins;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            collectedCoins++;
            coinsText.text = collectedCoins.ToString();
            Destroy(other.gameObject);
        }
        
    }
    public int GetCollectedCoins()
    {
        return collectedCoins;
    }
}
