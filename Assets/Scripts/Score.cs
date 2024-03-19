using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public ObjectsToActive objToAct;

    [SerializeField] private bool _first = true;

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

    private void Start(){
        _first = PlayerPrefsX.GetBool("first");
        if (_first){
            score = PlayerPrefs.GetFloat("score");
            scorePlus = PlayerPrefs.GetFloat("scorePlus");
            scorePlusCost = PlayerPrefs.GetFloat("scorePlusCost");
            scorePlusLvl = PlayerPrefs.GetInt("scorePlusLvl");
            idlePlus = PlayerPrefs.GetFloat("idlePlus");
            idleLvl = PlayerPrefs.GetInt("idleLvl");
            idleCost = PlayerPrefs.GetFloat("idleCost");
        }
        else{
            score = 0;
            scorePlus = 1;
            scorePlusCost = 10;
            scorePlusLvl = 0;
            idlePlus = 4;
            idleLvl = 0;
            idleCost = 250;
            PlayerPrefs.SetFloat("score", score);
            PlayerPrefs.SetFloat("scorePlus", scorePlus);
            PlayerPrefs.SetFloat("scorePlusCost", scorePlusCost);
            PlayerPrefs.SetInt("scorePlusLvl", scorePlusLvl);
            PlayerPrefs.SetFloat("idlePlus", idlePlus);
            PlayerPrefs.SetInt("idleLvl", idleLvl);
            PlayerPrefs.SetFloat("idleCost", idleCost);

            _first = true;
            PlayerPrefsX.SetBool("first", _first);
        }

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

        objToAct.StartObjToAct();
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