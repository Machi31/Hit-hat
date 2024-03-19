using System.Collections;
using UnityEngine;
using YG;

public class PlayerController : MonoBehaviour
{
    public ObjectsToActive objToAct;
    public Score score;

    public Camera playerCamera;

    public Color highlightColor = Color.red; // Цвет подсветки

    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _stepPlayed = false;
    public bool _canMove = true;

    private Rigidbody rb;
    public Animator anim;
    public GameObject activePlayer;
    public Transform startPos;
    public AudioSource[] steps;

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    private void Update(){
        if (_canMove){
            float horizontal = Input.GetAxis("Horizontal"); // Получение горизонтального движения
            float vertical = Input.GetAxis("Vertical"); // Получение вертикального движения

            if ((horizontal!= 0 || vertical!= 0) && activePlayer.activeSelf){
                anim.SetBool("Run", true);
                if (!_stepPlayed){
                    StartCoroutine(StepsPlay());
                    _stepPlayed = true;
                }

                if (Cursor.lockState == CursorLockMode.None){
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            else if (activePlayer.activeSelf && horizontal == 0 && vertical == 0)
                anim.SetBool("Run", false);

            Vector3 moveDirection = new(vertical, 0, -horizontal);

            if (moveDirection != Vector3.zero){
                // Получаем угол поворота камеры
                float targetAngle = Camera.main.transform.eulerAngles.y;
                // Создаем кватернион поворота вокруг оси Y
                Quaternion targetRotation = Quaternion.Euler(0, targetAngle - 90f, 0);
                // Поворачиваем игрока в нужном направлении
                transform.rotation = targetRotation;
            }

            transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.Self); // Перемещение относительно себя
            
            if (Input.GetKey(KeyCode.Space) && _isGrounded && !_isJumping){
                Jump();
                _isGrounded = false;
                _isJumping = true;
            }
        }
    }

    private void Jump(){
        if (activePlayer.activeSelf)
            anim.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            if (activePlayer.activeSelf)
                anim.SetTrigger("Land");
            _isGrounded = true;
            _isJumping = false;
        }

        if (collision.gameObject.CompareTag("LevelUp")){
            if (score.score >= score.scorePlusCost){
                score.score -= score.scorePlusCost;
                PlayerPrefs.SetFloat("score", score.score);

                score.scorePlusLvl++;
                PlayerPrefs.SetInt("scorePlusLvl", score.scorePlusLvl);

                score.scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score.score)}";
                if (score.scorePlusLvl % 10 == 0)
                    score.scorePlus *= 10;
                else
                    score.scorePlus *= 1.5f;
                PlayerPrefs.SetFloat("scorePlus", score.scorePlus);

                if (score.scorePlusLvl % 25 == 0)
                    score.scorePlusCost *= 5f;
                else
                    score.scorePlusCost *= 1.75f;
                PlayerPrefs.SetFloat("scorePlusCost", score.scorePlusCost);

                score.scorePlusText.text = $"Очков за удар: {NumberFormatter.FormatNumber(score.scorePlus)}";
                if (score.scorePlusLvl + 1 % 10 == 0)
                    score.plusCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(score.scorePlusCost)} /n Следующий уровень: {NumberFormatter.FormatNumber(score.scorePlus)} x 5!";
                else
                    score.plusCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(score.scorePlusCost)}";
            }
            else
                StartCoroutine(RedPlusText());
        }
        else if (collision.gameObject.CompareTag("IdleLvlUp")){
            if (score.score >= score.idleCost){
                score.score -= score.idleCost;
                PlayerPrefs.SetFloat("score", score.score);

                score.idleLvl++;
                PlayerPrefs.SetInt("idleLvl", score.idleLvl);

                if (score.idleLvl == 1)
                    score.StartAllCoroutine();
                score.scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score.score)}";
                if (score.idleLvl % 5 == 0)
                    objToAct.ActiveObjects();

                if (score.idleLvl % 10 == 0)
                    score.idlePlus *= 7;
                else
                    score.idlePlus *= 2.5f;
                PlayerPrefs.SetFloat("idlePlus", score.idlePlus);

                if (score.idleLvl % 25 == 0)
                    score.idleCost *= 10f;
                else
                    score.idleCost *= 3.35f;
                PlayerPrefs.SetFloat("idleCost", score.idleCost);
                score.idleText.text = $"Очков в секунду: {NumberFormatter.FormatNumber(score.idlePlus)}";
                if (score.idleLvl + 1 % 10 == 0)
                    score.idleCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(score.idleCost)} /n Следующий уровень: {NumberFormatter.FormatNumber(score.idlePlus)} x 7!";
                else
                    score.idleCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(score.idleCost)}";
            }
            else
                StartCoroutine(RedIdleText());
        }
        else if (collision.gameObject.CompareTag("Ad")){
            YandexGame.RewVideoShow(0);
            Rewarded();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Reset"))
            transform.position = startPos.position;
    }

    public void Rewarded(){
        score.score += score.idlePlus * 120;
        PlayerPrefs.SetFloat("score", score.score);
    }

    private IEnumerator RedPlusText()
    {
        Color originalColor = score.plusCostText.color;
        score.plusCostText.color = highlightColor;

        yield return new WaitForSeconds(0.5f);

        score.plusCostText.color = originalColor;
    }

    private IEnumerator RedIdleText()
    {
        Color originalColor = score.plusCostText.color;
        score.idleCostText.color = highlightColor;

        yield return new WaitForSeconds(0.5f);

        score.idleCostText.color = originalColor;
    }

    private IEnumerator StepsPlay(){
        steps[Random.Range(0, steps.Length)].Play();
        yield return new WaitForSeconds(0.75f);
        _stepPlayed = false;
    }
}