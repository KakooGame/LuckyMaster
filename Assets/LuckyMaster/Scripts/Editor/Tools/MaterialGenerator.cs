using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

public class MaterialGenerator : MonoBehaviour
{
    // 定义20种颜色
    private Color[] colors = new Color[]
    {
        new Color(0.976f, 0.255f, 0.267f), // 红
        new Color(0.953f, 0.447f, 0.173f), // 橙
        new Color(0.976f, 0.588f, 0.118f), // 金黄
        new Color(0.976f, 0.518f, 0.290f), // 桃色
        new Color(0.976f, 0.780f, 0.310f), // 黄色
        new Color(0.565f, 0.745f, 0.427f), // 绿色
        new Color(0.263f, 0.667f, 0.545f), // 青绿
        new Color(0.341f, 0.459f, 0.565f), // 蓝色
        new Color(0.302f, 0.565f, 0.557f), // 靛蓝
        new Color(0.153f, 0.490f, 0.631f), // 深蓝
        new Color(0.447f, 0.055f, 0.718f), // 紫
        new Color(0.227f, 0.047f, 0.639f), // 深紫
        new Color(0.263f, 0.380f, 0.933f), // 靛青
        new Color(0.282f, 0.584f, 0.937f), // 天蓝
        new Color(0.298f, 0.788f, 0.941f), // 浅蓝
        new Color(0.969f, 0.145f, 0.522f), // 深粉
        new Color(0.710f, 0.090f, 0.620f), // 粉紫
        new Color(0.337f, 0.043f, 0.678f), // 深靛蓝
        new Color(0.282f, 0.047f, 0.659f), // 蓝紫
        new Color(0.235f, 0.039f, 0.424f)  // 紫红
    };

    // 材质生成路径
    private string materialPath = "Assets/GeneratedMaterials/";

    [Button("Generate Materials")]
    public void GenerateMaterials()
    {
#if UNITY_EDITOR
        // 检查文件夹是否存在，不存在则创建
        if (!AssetDatabase.IsValidFolder(materialPath))
        {
            AssetDatabase.CreateFolder("Assets", "GeneratedMaterials");
        }

        // 遍历每种颜色，生成对应材质
        for (int i = 0; i < colors.Length; i++)
        {
            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.color = colors[i];

            // 设置材质属性（如启用光照、调节光泽等）
            material.SetFloat("_Smoothness", 0.5f); // 设置光滑度

            // 保存材质到指定路径
            string fileName = materialPath + "ColorMaterial_" + i + ".mat";
            AssetDatabase.CreateAsset(material, fileName);
        }

        // 确保更新项目资源
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Materials generated successfully at: " + materialPath);
#endif
    }
}

public class MaterialGeneratorEditor 
{
    [MenuItem("Tools/Generate Materials")]
    public static void GenerateMaterialsFromMenu()
    {
        MaterialGenerator generator = new MaterialGenerator();
        generator.GenerateMaterials();
    }
}