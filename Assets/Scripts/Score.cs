using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // * Данные для очков
    public int score;
    public TMP_Text scoreText;
    
    // * Данные для очков за удар
    public int scorePlus = 1;
    public int scorePlusLvl;
    public int[] scorePlusCost;
    public TMP_Text scorePlusText;
    public TMP_Text plusCostText;

    // * Данные для очков за ческунду
    public int idlePlus;
    public int idleLvl;
    public int[] idleCost;
    public TMP_Text idleText;
    public TMP_Text idleCostText;

    private void Start() {
        scoreText.text = $"Очков: {score}";
        scorePlusText.text = $"Очков за удар: {scorePlus}";
        plusCostText.text = $"Стоимость: {scorePlusCost[scorePlusLvl]}";
        idleText.text = $"Очков в секунду: {idlePlus}";
        idleCostText.text = $"Стоимость: {idleCost[idleLvl]}";
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            score += scorePlus;
            scoreText.text = $"Очков: {score}";
        }
    }

    public void StartAllCoroutine(){
        StartCoroutine(IdleScore());
    }

    public IEnumerator IdleScore(){
        while (true){
            score += idlePlus;
            scoreText.text = $"Очков: {score}";
            yield return new WaitForSeconds(1);
        }
    }
}