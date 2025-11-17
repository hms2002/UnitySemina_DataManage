using UnityEngine;

[CreateAssetMenu(menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string id;
    public string displayName;
    public int maxHp;
    public float moveSpeed;
    public int attackPower;
}
