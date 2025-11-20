using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<BallData> data = new List<BallData>();
}

[System.Serializable]
public class BallData
{
    public Vector3 pos;
    public Vector3 vel;
    public BallData(Vector3 pos, Vector3 vel)
    {
        this.pos = pos;
        this.vel = vel;
    }
}
public class SaveManager : MonoBehaviour
{
    public List<BallMove> balls;
    private static string SavePath =>
    Path.Combine(Application.persistentDataPath, "save.json");
    private void Start()
    {
        SaveData? data = Load();
        SetLoadData(data);
    }

    // 종료 시 실행되는 함수
    private void OnApplicationQuit()
    {
        Save();
    }
    private SaveData Load()
    {
        if (!File.Exists(SavePath))
            return null;

        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
    private void SetLoadData(SaveData data)
    {
        List<BallData> saveBalls = data.data;

        // 각 ball에 저장되어있던 데이터 넣기
        for (int i = 0; i < balls.Count; i++)
        {
            if (saveBalls.Count <= i) break;
            balls[i].SetBallData(saveBalls[i]);
        }

    }
    private void Save()
    {
        SaveData saveData = new SaveData();
        
        // ball 순회하며 저장 데이터 가져오기
        foreach (var ball in balls) 
        {
            saveData.data.Add(ball.GetBallData());
        }
        // 파일 저장하기
        string jsonedFile = JsonUtility.ToJson(saveData,true);
        Debug.Log(jsonedFile);
        File.WriteAllText(SavePath, jsonedFile);
    }
}
