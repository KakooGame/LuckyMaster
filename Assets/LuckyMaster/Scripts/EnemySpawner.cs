using UnityEngine;
using ImprovedTimers;  // 确保包含 Timer 库的命名空间
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 敌人预制体数组
    public float spawnInterval = 5f;  // 生成间隔时间
    public Collider spawnArea;  // 用于控制生成范围的碰撞体
    public float minSpawnDistance = 2f;  // 最小生成距离，防止敌人重叠
    public Transform playerTransform;  // 玩家 Transform
    public float minDistanceFromPlayer = 5f;  // 敌人与玩家之间的最小距离

    [System.Serializable]
    public class EnemySettings
    {
        public string enemyName;
        public float fixedYPosition = 0f;  // 固定的 Y 轴位置（默认值可在检视器修改）
        public float spawnProbability = 1f;  // 生成概率（默认值可在检视器修改）
    }

    public List<EnemySettings> enemySettingsList = new List<EnemySettings>();  // 敌人设置列表

    private CountdownTimer spawnTimer;
    private List<Vector3> recentSpawnPositions = new List<Vector3>();  // 记录最近生成的位置

    void Start()
    {
        // 初始化倒计时定时器
        spawnTimer = new CountdownTimer(spawnInterval);
        spawnTimer.OnTimerStart += () => Debug.Log("Enemy spawn timer started");  // 定时器开始时打印日志
        spawnTimer.OnTimerStop += () => Debug.Log("Enemy spawn timer stopped");  // 定时器停止时打印日志
        spawnTimer.OnTimerStop += SpawnEnemy;  // 定时器停止时调用 SpawnEnemy 方法生成敌人

        spawnTimer.Start();  // 开始计时器

        // 初始化敌人生成概率
        InitializeEnemySpawnProbabilities();
    }

    /// <summary>
    /// 生成敌人实例并确定其位置。
    /// </summary>
    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;  // 检查是否有敌人预制体

        // 获取随机敌人索引
        int enemyIndex = GetRandomEnemyIndex();
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length) return;  // 检查敌人索引是否有效

        // 获取有效的生成位置
        Vector3 spawnPosition = GetValidSpawnPosition(enemyIndex);
        if (spawnPosition != Vector3.zero)  // 如果找到有效位置则生成敌人
        {
            Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
            recentSpawnPositions.Add(spawnPosition);  // 记录生成的位置

            // 保持记录列表的大小合理，防止内存溢出
            if (recentSpawnPositions.Count > 50)
            {
                recentSpawnPositions.RemoveAt(0);
            }
        }

        // 重置计时器以继续下一次生成
        spawnTimer.Reset(spawnInterval);
        spawnTimer.Start();
    }

    /// <summary>
    /// 初始化敌人生成概率，如果敌人没有设置对应的概率，则添加默认设置。
    /// </summary>
    void InitializeEnemySpawnProbabilities()
    {
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            string enemyName = enemyPrefab.name;
            if (!enemySettingsList.Exists(e => e.enemyName == enemyName))
            {
                // 如果敌人设置列表中没有该敌人，则添加默认设置
                EnemySettings defaultSettings = new EnemySettings { enemyName = enemyName };
                enemySettingsList.Add(defaultSettings);
            }
        }
    }

    /// <summary>
    /// 获取有效的生成位置，确保位置不会与其他敌人或玩家重叠。
    /// </summary>
    /// <param name="enemyIndex">敌人索引</param>
    /// <returns>返回有效的生成位置，如果没有找到有效位置则返回 Vector3.zero</returns>
    Vector3 GetValidSpawnPosition(int enemyIndex)
    {
        string enemyName = enemyPrefabs[enemyIndex].name;
        // 查找该敌人的固定 Y 轴位置
        EnemySettings settings = enemySettingsList.Find(e => e.enemyName == enemyName);
        float yPosition = settings != null ? settings.fixedYPosition : 0f;

        // 尝试最多 10 次找到有效的位置
        for (int attempt = 0; attempt < 10; attempt++)
        {
            if (spawnArea != null)
            {
                // 在生成区域内随机生成一个点，使用固定的 Y 轴位置
                Vector3 randomPoint = new Vector3(
                    Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    yPosition,
                    Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                );

                // 检查该位置是否有效
                if (IsPositionValid(randomPoint))
                {
                    return randomPoint;
                }
            }
        }

        Debug.LogWarning("Failed to find a valid spawn position");  // 如果未找到有效位置，打印警告
        return Vector3.zero;
    }

    /// <summary>
    /// 检查生成位置是否有效，确保与其他敌人或玩家的距离足够远。
    /// </summary>
    /// <param name="position">待检查的位置</param>
    /// <returns>如果位置有效则返回 true，否则返回 false</returns>
    bool IsPositionValid(Vector3 position)
    {
        // 检查与最近生成位置的距离，确保敌人不会重叠
        foreach (Vector3 recentPosition in recentSpawnPositions)
        {
            if (Vector3.Distance(position, recentPosition) < minSpawnDistance)
            {
                return false;  // 如果距离太近，则位置无效
            }
        }

        // 检查与玩家的距离，确保敌人不会生成在玩家附近
        if (playerTransform != null && Vector3.Distance(position, playerTransform.position) < minDistanceFromPlayer)
        {
            return false;  // 如果距离玩家太近，则位置无效
        }

        return true;  // 如果所有检查都通过，则位置有效
    }

    /// <summary>
    /// 根据生成概率获取随机敌人的索引。
    /// </summary>
    /// <returns>返回选中的敌人索引</returns>
    int GetRandomEnemyIndex()
    {
        if (enemySettingsList.Count == 0) return 0;  // 检查是否有敌人设置

        float totalProbability = 0f;
        
        // 计算所有敌人的总生成概率
        foreach (EnemySettings settings in enemySettingsList)
        {
            totalProbability += settings.spawnProbability;
        }

        // 生成一个 0 到 totalProbability 之间的随机值
        float randomValue = Random.Range(0f, totalProbability);
        // 累积概率
        float cumulativeProbability = 0f;

        // 遍历敌人设置列表，找到对应的敌人索引
        for (int i = 0; i < enemySettingsList.Count; i++)
        {
            // 累积概率
            cumulativeProbability += enemySettingsList[i].spawnProbability;
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        return 0;  // 默认返回第一个敌人
    }

    void OnDestroy()
    {
        // 释放定时器资源，防止内存泄漏
        spawnTimer.Dispose();
    }
}