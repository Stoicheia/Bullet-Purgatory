using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScorePopupUI : MonoBehaviour
{
    public StatsManager levelStats;
    [SerializeField] private ScorePopup popup;
    private float grazeTimer;
    private float grazeScoreAccum;
    private bool firstGraze;
    
    [SerializeField] float grazePopupCooldown;

    [SerializeField] private Color grazeScoreColor;
    [SerializeField] private Color coinScoreColor;
    [SerializeField] private Color killScoreColor;
    [SerializeField] private Color comboPopupColor;
    public Transform comboPopupLocation;

    private int prevCombo;
    private void Awake()
    {
        grazeTimer = grazePopupCooldown;
        grazeScoreAccum = 0;
        firstGraze = true;
    }

    private void OnEnable()
    {
        Grazebox.OnGraze += GrazePopupIncrement;
        ItemPicker.OnCoinPickup += SpawnCoinPopup;
        levelStats.OnEnemyKilledAfterCombo += SpawnEnemyPopup;
        levelStats.OnComboIncrease += SpawnCombo;
    }
    
    private void OnDisable()
    {
        Grazebox.OnGraze -= GrazePopupIncrement;
        ItemPicker.OnCoinPickup -= SpawnCoinPopup;
        levelStats.OnEnemyKilledAfterCombo -= SpawnEnemyPopup;
        levelStats.OnComboIncrease -= SpawnCombo;
    }

    void SpawnGrazePopup(float f, Transform t)
    {
        ScorePopup myPopup = Instantiate(popup, t.position, quaternion.identity) as ScorePopup;
        myPopup.SetText(Mathf.RoundToInt(f).ToString());
        myPopup.SetColor(grazeScoreColor);
    }

    void GrazePopupIncrement(float f, Transform t)
    {
        grazeTimer += Time.deltaTime;
        grazeScoreAccum += Score.Graze2Score(f)*(1+levelStats.killCombo);
        if (grazeTimer >= grazePopupCooldown)
        {
            if (firstGraze)
            {
                grazeScoreAccum = 1;
                firstGraze = false;
            }
            SpawnGrazePopup(grazeScoreAccum, t);
            ResetGraze();
        }
    }

    void ResetGraze()
    {
        grazeTimer = 0;
        grazeScoreAccum = 0;
    }

    void SpawnCoinPopup(int c, Player p)
    {
        ScorePopup myPopup = Instantiate(popup, p.transform.position, Quaternion.identity) as ScorePopup;
        myPopup.SetText(c.ToString());
        myPopup.SetColor(coinScoreColor);
    }

    void SpawnEnemyPopup(string dead, Enemy e)
    {
        if (!e.WasKilled()) return;
        ScorePopup myPopup = Instantiate(popup, e.transform.position, Quaternion.identity) as ScorePopup;
        myPopup.SetText(((int)e.maxHP*10*levelStats.killCombo).ToString());
        myPopup.SetColor(killScoreColor);
    }

    void SpawnCombo()
    {
        if (levelStats.killCombo%10<=prevCombo%10 && levelStats.killCombo >= 10)
        {
            ScorePopup myPopup = Instantiate(popup, comboPopupLocation) as ScorePopup;
            myPopup.SetText(levelStats.killCombo+" Combo!");
            myPopup.SetColor(Color.Lerp(comboPopupColor, new Color(1,1,0,1), 2/Mathf.PI*Mathf.Atan(levelStats.killCombo/200f)));
        }

        prevCombo = levelStats.killCombo;
    }
}
