using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    private void Awake() {
        SetInstance();
    }



    public void AddGold(int goldToAdd) {

    }



    private void SetInstance() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
