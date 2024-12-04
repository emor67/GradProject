using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coinsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            coinsCollected++;

            UpdateCoinText();

            other.gameObject.SetActive(false);

            //Debug.Log("Coins Collected: " + coinsCollected);
        }
    }

    private void UpdateCoinText(){
        coinText.text = "Coins: " + coinsCollected.ToString();
    }
}
