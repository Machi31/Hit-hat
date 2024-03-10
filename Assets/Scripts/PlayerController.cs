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
            if (score.score >= score.scorePlusCost[score.scorePlusLvl]){
                score.score -= score.scorePlusCost[score.scorePlusLvl];
                score.scorePlusLvl++;
                score.plusCostText.text = $"Стоимость: {score.scorePlusCost[score.scorePlusLvl]}";
                score.scoreText.text = $"Очков: {score.score}";
                score.scorePlus++;
                score.scorePlusText.text = $"Очков за удар: {score.scorePlus}";
            }
            else
                StartCoroutine(HighlightText());
        }
    }

    private IEnumerator HighlightText()
    {
        Color originalColor = score.plusCostText.color;
        score.plusCostText.color = highlightColor;

        yield return new WaitForSeconds(0.5f); // Измените этот параметр на ваш выбор

        score.plusCostText.color = originalColor;
    }
}