using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // * Данные для очков
    public double score;
    public TMP_Text scoreText;
    
    // * Данные для очков за удар
    public double scorePlus = 1;
    public int scorePlusLvl;
    public double scorePlusCost = 10;
    public TMP_Text scorePlusText;
    public TMP_Text plusCostText;

    // * Данные для очков за ческунду
    public double idlePlus;
    public int idleLvl;
    public double idleCost = 250;
    public TMP_Text idleText;
    public TMP_Text idleCostText;

    private void Start(){
        scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score)}";
        scorePlusText.text = $"Очков за удар: {NumberFormatter.FormatNumber(scorePlus)}";
        plusCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(scorePlusCost)}";
        idleCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(idleCost)}";
        if (idleLvl == 0)
            idleText.text = $"Очков в секунду: 0";
        else if (idleLvl > 0){
            idleText.text = $"Очков в секунду: {NumberFormatter.FormatNumber(idlePlus)}";
            StartCoroutine(IdleScore());
        }
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            score += scorePlus;
            scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score)}";
        }
    }

    public void StartAllCoroutine(){
        StartCoroutine(IdleScore());
    }

    public IEnumerator IdleScore(){
        while (true){
            score += idlePlus;
            scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score)}";
            yield return new WaitForSeconds(1);
        }
    }
}

public class NumberFormatter
{
    public static string FormatNumber(double number)
    {
        string[] suffixes = { "", "k", "m", "b", "t", "q", "aa", "ab", "ac", "ad", "ae" }; // Допустимые суффиксы
        int suffixIndex = 0;

        // Находим подходящий суффикс
        while (number >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            number /= 1000;
            suffixIndex++;
        }

        // Форматируем число с учетом суффикса и одним десятичным знаком
        string formattedNumber = $"{number:N2}{suffixes[suffixIndex]}";
        return formattedNumber;
    }
}
