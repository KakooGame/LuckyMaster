using System;
using System.Collections;
using System.Collections.Generic;
using LuckyMaster.Character;
using MoreMountains.TopDownEngine;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.Serialization;

namespace LuckyMaster.IK
{
    public class AutoAimTargetLinker : MonoBehaviour
    {
        public CharacterInitializer characterInitializer; // 引用 CharacterInitializer 脚本
        public WeaponAutoAim3D currentWeaponAutoAim; // 当前活跃的 WeaponAutoAim3D 脚本引用

        private void Awake()
        {
            // 确保组件引用不为空，如果为空则尝试获取组件
            characterInitializer ??= GetComponentInParent<CharacterInitializer>();
        }

        private void Start()
        {
            currentWeaponAutoAim = characterInitializer.currentWeaponAutoAim3D;
        }

        private void Update()
        {
            // 如果当前的 WeaponAutoAim3D 有目标，则将其传递给 AimIK
            if (currentWeaponAutoAim != null && characterInitializer.aimIK && characterInitializer.lookAtIK != null &&
                currentWeaponAutoAim.Target != null)
            {
                characterInitializer.aimIK.solver.target = currentWeaponAutoAim.Target;
                characterInitializer.lookAtIK.solver.target = currentWeaponAutoAim.Target;
                Debug.Log("aimIK.solver.target = currentWeaponAutoAim.Target;");
            }
        }
    }
}