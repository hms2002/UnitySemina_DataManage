using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;   // 데이터 참조
    private int currentHp;

    private void Start()
    {
        currentHp = data.maxHp;
        Debug.Log($"Enemy 생성: {data.id}, HP = {currentHp}, ATK = {data.attackPower}");
    }
}
