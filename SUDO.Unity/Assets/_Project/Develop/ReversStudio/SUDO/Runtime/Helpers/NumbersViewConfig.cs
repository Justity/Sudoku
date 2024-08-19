using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ReversStudio.SUDO.Runtime.Helpers
{
    [CreateAssetMenu(fileName = "NumbersViewConfig", menuName = "Field/NumbersViewConfig", order = 1)]
    public class NumbersViewConfig : ScriptableObject
    {
        public Color defaultColor;

        public Color selectedColor;
        
        public List<NumberSettings> numberConfigs;

        [Button]
        private void Numerate()
        {
            for (int i = 0; i < numberConfigs.Count; i++)
            {
                numberConfigs[i].number = i + 1;
            }
        }
    }

    [Serializable]
    public class NumberSettings
    {
        public int number;
        public Color completedColor;
        public Material defaultMaterial;
        public Material userMaterial;
    }
}

