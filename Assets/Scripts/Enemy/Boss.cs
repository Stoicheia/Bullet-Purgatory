using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Boss : MonoBehaviour
{
	public delegate void BossAction(Boss boss);
	public static event BossAction OnBossHit;
	public static event BossAction OnBossSpawn;
	public static event BossAction OnBossKilled;

	Enemy enemyInfo;
	string bossTag;
    public string BossTag { get => bossTag; }

	void Awake(){
		enemyInfo = GetComponent<Enemy>();
		bossTag = enemyInfo.enemyTag;
	}

    void OnEnable(){
    	OnBossSpawn?.Invoke(this);
    	enemyInfo.OnThisHit += Hit;
    	enemyInfo.OnThisDeath += Die;
    }

    void OnDisable(){
		enemyInfo.OnThisHit -= Hit;
    	enemyInfo.OnThisDeath -= Die;
    }

    void Hit()=>OnBossHit?.Invoke(this);
    void Die()=>OnBossKilled?.Invoke(this);

    public Enemy GetEnemyInfo(){
    	return enemyInfo;
    }
}
