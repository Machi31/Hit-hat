using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToActive : MonoBehaviour
{
    public Score score;

    public GameObject[] gameObjectsActive;
    public GameObject[] gameObjectsInactive;

    private void Start(){
        for(int i = 0; i < score.idleLvl; i++){
            switch(i){
                case 5:
                    gameObjectsInactive[0].SetActive(false);
                    gameObjectsActive[i / 5 - 1].SetActive(true);
                    break;
                case 10:
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
