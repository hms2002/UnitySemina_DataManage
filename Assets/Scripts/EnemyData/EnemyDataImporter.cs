using UnityEngine;
using UnityEditor;
using System.IO;

public static class EnemyDataImporter
{
    private const string CsvPath = "Assets/Data/EnemyData.csv";
    private const string OutputFolder = "Assets/Data/EnemyAssets";

    [MenuItem("Tools/Import Enemy Data From CSV")]
    public static void Import()
    {
        if (!File.Exists(CsvPath))
        {
            Debug.LogError($"CSV 파일을 찾을 수 없습니다: {CsvPath}");
            return;
        }

        if (!AssetDatabase.IsValidFolder(OutputFolder))
        {
            AssetDatabase.CreateFolder("Assets/Data", "EnemyAssets");
        }

        string[] lines = File.ReadAllLines(CsvPath);

        // 0번 줄은 헤더라고 가정
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] cols = line.Split(',');

            string id = cols[0];
            string displayName = cols[1];
            int maxHp = int.Parse(cols[2]);
            float moveSpeed = float.Parse(cols[3]);
            int attackPower = int.Parse(cols[4]);

            string assetPath = $"{OutputFolder}/{id}.asset";

            EnemyData data = AssetDatabase.LoadAssetAtPath<EnemyData>(assetPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<EnemyData>();
                AssetDatabase.CreateAsset(data, assetPath);
            }

            data.id = id;
            data.displayName = displayName;
            data.maxHp = maxHp;
            data.moveSpeed = moveSpeed;
            data.attackPower = attackPower;

            EditorUtility.SetDirty(data);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("EnemyData Import 완료!");
    }

    [MenuItem("Tools/Import EnemyDataa")]
    public static void Importt()
    {

        // 출력 폴더 없으면 만들기
        if (!AssetDatabase.IsValidFolder(OutputFolder))
        {
            AssetDatabase.CreateFolder("Assets/Data", "EnemyAssets");
        }

        //  에셋 경로 (id 기준 이름)
        string assetPath = $"{OutputFolder}/Monster.asset";

        EnemyData data = ScriptableObject.CreateInstance<EnemyData>();
        AssetDatabase.CreateAsset(data, assetPath);
    

    // 저장
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("EnemyData Import 완료!");
    }
}
