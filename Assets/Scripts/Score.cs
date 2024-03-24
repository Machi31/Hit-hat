using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public ObjectsToActive obj;
    // * Данные для очков
    public float score;
    public TMP_Text scoreText;
    
    // * Данные для очков за удар
    public float scorePlus = 1;
    public int scorePlusLvl;
    public float scorePlusCost = 10;
    public TMP_Text scorePlusText;
    public TMP_Text plusCostText;

    // * Данные для очков за ческунду
    public float idlePlus;
    public int idleLvl;
    public float idleCost = 250;
    public TMP_Text idleText;
    public TMP_Text idleCostText;

    // * Данные для очков за рекламу
    public TMP_Text adText;

    [SerializeField] bool _isFirst;

    private void Start(){
        // _isFirst = PlayerPrefsX.GetBool("isFirst");
        // if (_isFirst){
        //     score = PlayerPrefs.GetFloat("score");
        //     scorePlus = PlayerPrefs.GetFloat("scorePlus");
        //     scorePlusCost = PlayerPrefs.GetFloat("scorePlusCost");
        //     scorePlusLvl = PlayerPrefs.GetInt("scorePlusLvl");
        //     idlePlus = PlayerPrefs.GetFloat("idlePlus");
        //     idleLvl = PlayerPrefs.GetInt("idleLvl");
        //     idleCost = PlayerPrefs.GetFloat("idleCost");
        // }
        // else{
        //     scorePlus = 1;
        //     scorePlusCost = 10;
        //     idlePlus = 4;
        //     idleCost = 250;
        //     PlayerPrefs.SetFloat("scorePlus", scorePlus);
        //     PlayerPrefs.SetFloat("scorePlusCost", scorePlusCost);
        //     PlayerPrefs.SetFloat("idlePlus", idlePlus);
        //     PlayerPrefs.SetFloat("idleCost", idleCost);
        //     _isFirst = true;
        //     PlayerPrefsX.SetBool("isFirst", _isFirst);
        // }
        scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score)}";
        scorePlusText.text = $"Очков за удар: {NumberFormatter.FormatNumber(scorePlus)}";
        plusCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(scorePlusCost)}";
        idleCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(idleCost)}";
        if (idleLvl == 0){
            idleText.text = $"Очков в секунду: 0";
            adText.text = $"За просмотр рекламы: 0 =)";
        }
        else if (idleLvl > 0){
            idleText.text = $"Очков в секунду: {NumberFormatter.FormatNumber(idlePlus)}";
            adText.text = $"За просмотр рекламы: {NumberFormatter.FormatNumber(idlePlus * 120)}";
            StartCoroutine(IdleScore());
        }
        obj.StartActive();
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            score += scorePlus;
            PlayerPrefs.SetFloat("score", score);
            scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score)}";
        }
    }

    public void StartAllCoroutine(){
        StartCoroutine(IdleScore());
    }

    public IEnumerator IdleScore(){
        while (true){
            score += idlePlus;
            PlayerPrefs.SetFloat("score", score);
            scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score)}";
            adText.text = $"За просмотр рекламы: {NumberFormatter.FormatNumber(idlePlus * 120)}";
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