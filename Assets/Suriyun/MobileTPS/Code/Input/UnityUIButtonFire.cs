﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Suriyun.MobileTPS
{
    public class UnityUIButtonFire : UnityUIButton
    {
        [HideInInspector] public Agent agent;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            if (agent == null)
            {
                agent = GameObject.FindObjectOfType<Agent>();
            }

            agent.behaviour.StartFiring();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            agent.behaviour.StopFiring();
        }
    }
}