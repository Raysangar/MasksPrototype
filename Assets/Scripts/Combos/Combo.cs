using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class Combo
    {

        private List<AttackType> comboSequence;

        public Combo()
        {
            comboSequence = new List<AttackType>();
        }

        public Combo(string serializedCombo) : this()
        {
            foreach (string stringAttack in serializedCombo.Split('|'))
                if (stringAttack != "")
                    comboSequence.Add((AttackType)System.Enum.Parse(typeof(AttackType), stringAttack));
        }

        public List<AttackType> ComboSequence 
        { 
            get { return comboSequence; }
        }

        public void Add(AttackType attackType)
        {
            comboSequence.Add(attackType);
        }

        public void Clear()
        {
            comboSequence.Clear();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Combo);
        }

        public bool Equals(Combo obj)
        {
            if (obj == null || obj.ComboSequence.Count != comboSequence.Count)
                return false;
            int i = 0;
            while (i < comboSequence.Count && comboSequence[i] == obj.ComboSequence[i])
                ++i;
            return (i == comboSequence.Count);
        }

        public override string ToString()
        {
            string serialized = "";
            foreach (AttackType attack in comboSequence)
                serialized += "|" + attack;
            return serialized;
        }
    }
