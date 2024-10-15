using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "LuckyMaster/CharacterAttributes", order = 1)]
public class CharacterAttributes : ScriptableObject
{
    // 基础属性（DND 六维）
    [Header("基础属性 (DND 六维)")] public int strength; // 力量，影响物理攻击力和近战伤害
    public int dexterity; // 敏捷，影响闪避、移动速度、远程攻击准确性和暴击几率
    public int constitution; // 体质，影响最大生命值和生命恢复速度
    public int intelligence; // 智力，影响法术强度和与魔法相关的技能
    public int wisdom; // 感知，影响法术命中率和魔法抵抗
    public int charisma; // 魅力，影响召唤生物的效果和与 NPC 的交互

    // 核心资源属性
    [Header("核心资源属性")] public int healthPoints; // 生命值 (HP)，角色的生存值
    public int manaPoints; // 魔法值 (MP)，施放技能或法术所需的能量
    public int stamina; // 体力值，进行闪避或特殊攻击时需要消耗的体力值
    public int shieldPoints; // 护盾值 (SP)，用于吸收部分伤害

    // 攻击属性
    [Header("攻击属性")] public int physicalAttackPower; // 物理攻击力，基础物理攻击的伤害值
    public int magicAttackPower; // 魔法攻击力，基础魔法攻击的伤害值
    public float attackSpeed; // 攻击速度，影响角色的攻击频率
    public float criticalChance; // 暴击几率，攻击时触发暴击的概率
    public float criticalDamage; // 暴击伤害，暴击攻击的伤害倍率
    public int rangedAttackPower; // 远程攻击力，影响远程攻击的伤害值
    public float attackRange; // 攻击范围，影响角色攻击的有效范围

    // 防御属性
    [Header("防御属性")] public int armor; // 护甲，减少所受的物理伤害
    public int magicResistance; // 魔法抗性，减少所受的魔法伤害
    public float elementalResistance; // 元素抗性，针对特定元素的减伤能力
    public float crowdControlResistance; // 控制抗性，减少控制效果的持续时间

    // 恢复与持续属性
    [Header("恢复与持续属性")] public float healthRegeneration; // 生命恢复，每秒恢复的生命值
    public float manaRegeneration; // 魔法恢复，每秒恢复的魔法值
    public float staminaRegeneration; // 体力恢复，每秒恢复的体力值

    // 吸血与特殊效果属性
    [Header("吸血与特殊效果属性")] public float lifeSteal; // 生命窃取，按伤害的一定比例恢复生命值
    public float manaSteal; // 魔法窃取，按伤害的一定比例恢复魔法值
    public int shieldAbsorption; // 吸收护盾，对部分伤害提供护盾吸收的效果
    public float percentageDamage; // 百分比伤害，基于敌人最大生命值或当前生命值造成的额外伤害

    // 回避与生存属性
    [Header("回避与生存属性")] public float dodgeChance; // 闪避率，角色躲避敌人攻击的概率
    public float blockChance; // 格挡几率，通过格挡减少或免除部分伤害的概率
    public float luck; // 幸运，影响掉落稀有物品、暴击几率等
    public float revivalChance; // 重生几率，在死亡后有一定概率原地重生

    // 移动与控制属性
    [Header("移动与控制属性")] public float movementSpeed; // 移动速度，角色在地图上的移动速度
    public float dashDistance; // 冲刺距离，角色冲刺的最大距离
    public float slideDuration; // 滑行时间，冲刺后的滑行时间

    // 元素与能量属性
    [Header("元素与能量属性")] public int firePower; // 火焰能量，影响火焰技能的威力
    public int frostPower; // 冰霜能量，影响冰霜技能的威力
    public int lightningPower; // 雷电能量，影响雷电技能的威力
    public float poisonResistance; // 毒素抗性，减少来自毒素的持续伤害

    // 特殊职业属性
    [Header("特殊职业属性")] public int summoningPower; // 召唤生物能力，影响召唤生物的强度和持续时间
    public float divineBlessing; // 信仰加持，影响从信仰系统中获得的增益效果
    public int ragePoints; // 怒气值，通过受到攻击或攻击敌人累积的能量

    // 游戏机制相关属性
    [Header("游戏机制相关属性")] public float experienceGainBonus; // 经验值加成，增加击败敌人或完成任务获得的经验值
    public float lootDropBonus; // 掉落几率加成，增加敌人掉落物品的几率
    public float merchantDiscount; // 商人折扣，购买物品时获得的折扣比例
    public int itemCarryLimit; // 物品持有上限，角色能够携带的物品数量上限
}