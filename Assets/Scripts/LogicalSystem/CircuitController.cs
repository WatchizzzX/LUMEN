using System;
using System.Collections.Generic;
using LogicalSystem.Interfaces;
using UnityEngine;

namespace LogicalSystem
{
    [DisallowMultipleComponent]
    public class CircuitController : MonoBehaviour
    {
        [SerializeField] private List<LogicalComponent> logicalComponents;
        [SerializeField] private List<SourceComponent> sourceComponents;

        private void Awake()
        {
            throw new NotImplementedException();
        }
    }
}