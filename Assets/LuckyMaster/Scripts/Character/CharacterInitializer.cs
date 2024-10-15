using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using RootMotion.FinalIK;
using UnityEngine;

namespace LuckyMaster.Character
{
    // 用于保存各层级的引用以及初始化角色
    public class CharacterInitializer : MonoBehaviour
    {
        public AimIK aimIK; // 引用 AimIK 脚本
        public LookAtIK lookAtIK; // 引用 LookAtIK 脚本
        public CharacterHandleWeapon characterHandleWeapon; // 引用 CharacterHandleWeapon 脚本
        public void Awake()
        {
            // 确保组件引用不为空，如果为空则尝试获取组件
            aimIK ??= GetComponentInChildren<AimIK>();
            lookAtIK ??= GetComponentInChildren<LookAtIK>();
            characterHandleWeapon ??= GetComponentInChildren<CharacterHandleWeapon>();
        }
    }
}