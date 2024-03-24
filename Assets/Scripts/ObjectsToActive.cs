using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToActive : MonoBehaviour
{
    public PlayerController player;
    public Score score;

    public GameObject[] gameObjectsActive;
    public GameObject[] gameObjectsInactive;

    public void StartActive(){
        for(int i = 0; i < score.idleLvl; i++){
            switch(i){
                case 5:
                    gameObjectsInactive[0].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 10:
                    gameObjectsInactive[1].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 15:
                case 20:
                case 25:
                case 30:
                case 35:
                case 40:
                case 45:
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
            }
        }
    }
    public void ActiveObjects(){
        int i = score.idleLvl;
        switch(i){
            case 5:
                gameObjectsInactive[0].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 10:
                gameObjectsInactive[1].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                CutsceneManager.Instance.StartCutscene("World");
                break;
            case 25:
                gameObjectsActive[i / 5 - 1].SetActive(true);
                CutsceneManager.Instance.StartCutscene("Buildings");
                break;
            case 15:
            case 20:
            case 30:
            case 35:
            case 40:
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 45:
                gameObjectsActive[i / 5 - 1].SetActive(true);
                CutsceneManager.Instance.StartCutscene("All");
                break;
        }
    }
}
