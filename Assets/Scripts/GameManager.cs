using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource[] audioSources; // Массив аудиоисточников для проигрывания аудиофайлов
    private int currentAudioIndex = 0; // Индекс текущего проигрываемого аудиофайла

    void Start()
    {
        // Начинаем проигрывание первого аудиофайла
        PlayNextAudio();

        Cursor.lockState = CursorLockMode.Locked; // Захватывает курсор в центре экрана.
        Cursor.visible = false; // Скрывает курсор.
    }

    void Update()
    {
        // Проверяем, завершилось ли проигрывание текущего аудиофайла
        if (!audioSources[currentAudioIndex].isPlaying)
            // Если да, запускаем следующий аудиофайл
            PlayNextAudio();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Освобождаем захват курсора и делаем его видимым
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void PlayNextAudio()
    {
        // Переходим к следующему аудиофайлу в массиве
        currentAudioIndex = Random.Range(0, audioSources.Length);
        // Запускаем проигрывание нового аудиофайла
        audioSources[currentAudioIndex].Play();
    }
}