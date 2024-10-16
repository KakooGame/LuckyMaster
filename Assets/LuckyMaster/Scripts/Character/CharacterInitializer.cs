using System;
using System.Collections;
using System.Collections.Generic;
using LuckyMaster.IK;
using MoreMountains.TopDownEngine;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LuckyMaster.Character
{
    // 用于保存各层级的引用以及初始化角色
    public class CharacterInitializer : MonoBehaviour
    {
        public AimIK aimIK; // 引用 AimIK 脚本
        public LookAtIK lookAtIK; // 引用 LookAtIK 脚本
        public CharacterHandleWeapon characterHandleWeapon; // 引用 CharacterHandleWeapon 脚本

        public LMLookAtController LmLookAtController; // 引用 LMLookAtController 脚本

        public WeaponAutoAim3D currentWeaponAutoAim3D; // 当前活跃的 WeaponAim3D 脚本引用

        public void Awake()
        {
            // 确保组件引用不为空，如果为空则尝试获取组件
            aimIK ??= GetComponentInChildren<AimIK>();
            lookAtIK ??= GetComponentInChildren<LookAtIK>();
            characterHandleWeapon ??= GetComponentInChildren<CharacterHandleWeapon>();
            LmLookAtController ??= GetComponentInChildren<LMLookAtController>();
        }

        public void Start()
        {
            currentWeaponAutoAim3D = characterHandleWeapon.CurrentWeapon?.GetComponent<WeaponAutoAim3D>();
        }

        // 当CharacterHandleWeapon变化时，更新当前活跃的 WeaponAim3D 脚本
        public void UpdateCurrentWeapon()
        {
            if (characterHandleWeapon != null)
            {
                currentWeaponAutoAim3D = characterHandleWeapon.CurrentWeapon?.GetComponent<WeaponAutoAim3D>();
            }
        }
    }
}