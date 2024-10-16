using System;
using System.Collections;
using System.Collections.Generic;
using LuckyMaster.Character;
using MoreMountains.TopDownEngine;
using RootMotion.FinalIK;
using UnityEngine;

namespace LuckyMaster.IK
{
    public class LMLookAtController : LookAtController
    {
        CharacterInitializer characterInitializer;
        WeaponAutoAim3D currentWeaponAutoAim3D;

        protected override void Start()
        {
            base.Start();
            characterInitializer = GetComponentInParent<CharacterInitializer>();
            currentWeaponAutoAim3D = characterInitializer.currentWeaponAutoAim3D;
        }

        private void Update()
        {
            // 场景中敌人或物体被销毁后是作为非活跃状态，而非销毁，所以需要判断目标是否活跃
            if (currentWeaponAutoAim3D.Target == null || !currentWeaponAutoAim3D.Target.gameObject.activeInHierarchy)
            {
                this.weight = 0;
            }
            else
            {
                this.weight = 1;
            }
        }
    }
}