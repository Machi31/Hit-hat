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
                    gameObjectsInactive[i / 5 - 1].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 10:
                    gameObjectsInactive[i / 5 - 1].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 15:
                    gameObjectsInactive[i / 5 - 1].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 20:
                    gameObjectsInactive[i / 5 - 1].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 25:
                    gameObjectsInactive[i / 5 - 1].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 30:
                case 35:
                case 40:
                case 45:
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
    public void ActiveObjects(){
        int i = score.idleLvl;
        switch(i){
            case 5:
                gameObjectsInactive[i / 5 - 1].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 10:
                gameObjectsInactive[i / 5 - 1].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 15:
                gameObjectsInactive[i / 5 - 1].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 20:
                gameObjectsInactive[i / 5 - 1].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 25:
                gameObjectsInactive[i / 5 - 1].SetActive(false);
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            case 30:
            case 35:
            case 40:
            case 45:
                gameObjectsActive[i / 5 - 1].SetActive(true);
                break;
            default:
                break;
        }
    }
}
