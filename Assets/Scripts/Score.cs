using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int _score;
    public int scorePlus = 1;

    public TMP_Text scoreText;

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            _score += scorePlus;
            scoreText.text = _score.ToString();
        }
    }
}