using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int scorePlus = 1;
    public int scorePlusLvl;
    public int[] scorePlusCost;

    public TMP_Text scoreText;
    public TMP_Text scorePlusText;
    public TMP_Text plusCostText;

    private void Start() {
        scoreText.text = $"Очков: {score}";
        scorePlusText.text = $"Очков за удар: {scorePlus}";
        plusCostText.text = $"Стоимость: {scorePlusCost[scorePlusLvl]}";
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            score += scorePlus;
            scoreText.text = $"Очков: {score}";
        }
    }
}