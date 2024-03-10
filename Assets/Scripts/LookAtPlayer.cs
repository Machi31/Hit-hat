using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Перетащите объект игрока в это поле в инспекторе

    void Update()
    {
        // Проверяем, что у нас есть ссылка на игрока
        if (player != null)
            // Направляем объект так, чтобы его лицевая сторона была направлена к игроку
            transform.LookAt(transform.position + transform.position - player.position);
        else
            Debug.LogWarning("Ссылка на игрока отсутствует!");
    }
}
