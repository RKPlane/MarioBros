using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemigosUI : MonoBehaviour
{
    public TMP_Text enemyText;

    void Update()
    {
        //Actualiza UI
        enemyText.text = "Enemigos: " + GameManager.instance.totalEnemies;
    }
}
