using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class ComboInfo
    {
        private Dictionary<string, Combo> combos;
        private Dictionary<string, string> combosAnimation;
        private Dictionary<string, KeyValuePair<float, float>> combosTimeRequirement;

        public Dictionary<string, Combo> Combos
        {
            get { return combos; }
        }

        public Dictionary<string, string> CombosAnimation
        {
            get { return combosAnimation; }
        }

        public Dictionary<string, KeyValuePair<float, float>> CombosTimeRequirement
        {
            get { return combosTimeRequirement; }
        }

        public int CombosCount
        {
            get { return combos.Count; }
        }

        public ComboInfo()
        {
            combos = new Dictionary<string, Combo>();
            combosAnimation = new Dictionary<string, string>();
            combosTimeRequirement = new Dictionary<string, KeyValuePair<float, float>>();
        }

        public ComboInfo(string serializedInfo) : this()
        {
            deserializeInfo(serializedInfo);
        }

        public void removeCombo(string comboName)
        {
            combos.Remove(comboName);
            combosAnimation.Remove(comboName);
            combosTimeRequirement.Remove(comboName);
        }

        private void deserializeInfo(string seriealizedInfo)
        {
            foreach (string comboInfo in seriealizedInfo.Split('\n')) 
            {
                if (comboInfo != "")
                {
                    string[] comboAttributes = comboInfo.Split(';');
                    combos.Add(comboAttributes[0], new Combo(comboAttributes[1]));
                    combosAnimation.Add(comboAttributes[0], comboAttributes[2]);
                    initTimeRequirements(comboAttributes[0], comboAttributes[3]);
                }
            }
        }

        private void initTimeRequirements(string comboName, string serializadTimeRequirements)
        {
            char[] separators = new char[4] {' ', ']', '[', ','};
            string[] stringTimeRequirements = serializadTimeRequirements.Split(separators);
            combosTimeRequirement.Add(comboName, 
                new KeyValuePair<float, float>(float.Parse(stringTimeRequirements[1]), float.Parse(stringTimeRequirements[3])));
        }

        public string serialize()
        {
            string serializedList = "";
            foreach (string comboName in combos.Keys)
                serializedList += comboName + ";" + combos[comboName].ToString() + ";"
                    + combosAnimation[comboName] + ";" + combosTimeRequirement[comboName].ToString() + "\n";
            return serializedList;
        }
    }
