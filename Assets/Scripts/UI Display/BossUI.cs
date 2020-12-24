using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossUI : MonoBehaviour
{
    public RectTransform bossHpBar;
    public RectTransform bossHp;
    public TextMeshProUGUI bossText;

    float maxHP;
    Boss activeBoss;

    Vector2 fullBarSize;

    private void Awake()
    {
        Hide(null);
    }

    private void Start()
    {
        fullBarSize = bossHpBar.sizeDelta;
    }
    private void OnEnable()
    {
        Boss.OnBossSpawn += Show;
        Boss.OnBossHit += Show;
        Boss.OnBossKilled += Hide;
    }

    private void OnDisable()
    {
        Boss.OnBossSpawn -= Show;
        Boss.OnBossHit -= Show;
        Boss.OnBossKilled -= Hide;
    }

    private void Update()
    {

    }

    void Show(Boss boss)
    {
        bossHpBar.gameObject.SetActive(true);
        bossHp.gameObject.SetActive(true);
        bossText.gameObject.SetActive(true);
        maxHP = boss.GetEnemyInfo().maxHP;
        activeBoss = boss;
        bossText.text = boss.BossTag;
        UpdateBarSize(boss);
    }

    void Hide(Boss boss)
    {
        bossHpBar.gameObject.SetActive(false);
        bossHp.gameObject.SetActive(false);
        bossText.gameObject.SetActive(false);
        activeBoss = null;
    }

    void UpdateBarSize(Boss boss)
    {
        bossHp.sizeDelta = new Vector2(fullBarSize.x * boss.GetEnemyInfo().HP / maxHP, fullBarSize.y);
    }
}
