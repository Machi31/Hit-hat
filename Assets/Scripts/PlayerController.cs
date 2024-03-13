using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Score score;

    public Camera playerCamera;

    public Color highlightColor = Color.red; // Цвет подсветки

    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _isJumping = false;

    private Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    private void Update(){
        float horizontal = Input.GetAxis("Horizontal"); // Получение горизонтального движения
        float vertical = Input.GetAxis("Vertical"); // Получение вертикального движения

        Vector3 moveDirection = new(horizontal, 0, vertical);

        if (moveDirection != Vector3.zero){
            // Получаем угол поворота камеры
            float targetAngle = Camera.main.transform.eulerAngles.y;
            // Создаем кватернион поворота вокруг оси Y
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            // Поворачиваем игрока в нужном направлении
            transform.rotation = targetRotation;
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.Self); // Перемещение относительно себя
        
        if (Input.GetKey(KeyCode.Space) && _isGrounded && !_isJumping){
            Jump();
            _isJumping = true;
        }
    }

    private void Jump(){
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Ground")){
            _isGrounded = true;
            _isJumping = false;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Ground"))
            _isGrounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LevelUp")){
            if (score.score >= score.scorePlusCost){
                score.score -= score.scorePlusCost;
                score.scorePlusLvl++;
                score.scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score.score)}";
                score.scorePlus *= 1.5f;
                score.scorePlusCost *= 1.75f;
                score.scorePlusText.text = $"Очков за удар: {NumberFormatter.FormatNumber(score.scorePlus)}";
                score.plusCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(score.scorePlusCost)}";
            }
            else
                StartCoroutine(RedPlusText());
        }
        else if (collision.gameObject.CompareTag("IdleLvlUp")){
            if (score.score >= score.idleCost){
                score.score -= score.idleCost;
                score.idleLvl++;
                if (score.idleLvl == 1)
                    score.StartAllCoroutine();
                score.scoreText.text = $"Очков: {NumberFormatter.FormatNumber(score.score)}";
                score.idlePlus *= 2.5f;
                score.idleCost *= 2.7f;
                score.idleText.text = $"Очков в секунду: {NumberFormatter.FormatNumber(score.idlePlus)}";
                score.idleCostText.text = $"Стоимость: {NumberFormatter.FormatNumber(score.scorePlusCost)}";
            }
            else
                StartCoroutine(RedIdleText());
        }
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
}