using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI goldText;

    private void Awake() {
        SetInstance();
        
    }

    private void Start() {
        GameManager.Instance.OnGoldUpdate.AddListener(UpdateGold);
        UpdateGold(GameManager.Instance.PlayerGold);
    }


    public void UpdateGold(int playerGold) {
        goldText.text = "Oro: " + playerGold;
    }



    private void SetInstance() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
