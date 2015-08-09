using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ComboEditorWindow : EditorWindow
{
    private ComboInfo comboInfo = new ComboInfo();
    private Animator animator;
    private List<string> animationTriggers = new List<string>();
    private List<bool> toggle = new List<bool>();
    private Vector2 scrollPosition = new Vector2(0, 0);

    [MenuItem("Window/Combo Editor")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(ComboEditorWindow));
        window.titleContent = new GUIContent("Combo Editor");
    }

    void OnFocus()
    {
    }

    void OnLostFocus()
    {
    }

    void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        {
            animator = EditorGUILayout.ObjectField(animator, typeof(Animator), true) as Animator;

            if (animator != null)
            {
                foreach (AnimatorControllerParameter parameter in animator.parameters)
                    if (parameter.type == AnimatorControllerParameterType.Trigger)
                        animationTriggers.Add(parameter.name);
            }
            else animationTriggers.Clear();
            EditorGUILayout.Separator();
            int editorPosition = 0;
            List<string> keys = new List<string>(comboInfo.Combos.Keys);
            foreach (string comboName in keys)
            {
                EditorGUILayout.BeginHorizontal();
                addComboForm(comboName, editorPosition++);
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Combo"))
                addNewCombo();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Open Combos Sheet"))
                    openCombosSheet();
                if (GUILayout.Button("Save Combos Sheet"))
                    saveCombosSheet();
            }
            EditorGUILayout.EndHorizontal();
            
        }
        EditorGUILayout.EndScrollView();
    }

    private void addComboForm(string comboName, int editorPosition)
    {
        Combo combo = comboInfo.Combos[comboName];
        string newComboName = EditorGUILayout.TextField(comboName);
        if (comboName != newComboName)
        {
            comboInfo.Combos.Remove(comboName);
            comboInfo.Combos.Add(newComboName, combo);
            string comboAnimation = comboInfo.CombosAnimation[comboName];
            comboInfo.CombosAnimation.Remove(comboName);
            comboInfo.CombosAnimation.Add(newComboName, comboAnimation);
            KeyValuePair<float, float> timeRequirements = comboInfo.CombosTimeRequirement[comboName];
            comboInfo.CombosTimeRequirement.Remove(comboName);
            comboInfo.CombosTimeRequirement.Add(newComboName, timeRequirements);
        }

        toggle[editorPosition] = EditorGUILayout.Toggle(toggle[editorPosition]);
        if (toggle[editorPosition])
        {
            
            EditorGUILayout.BeginVertical();
            {
                for (int i = 0; i < combo.ComboSequence.Count; ++i)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        combo.ComboSequence[i] = (AttackType)EditorGUILayout.EnumPopup(combo.ComboSequence[i]);
                        if (GUILayout.Button("Remove"))
                            combo.ComboSequence.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add"))
                    combo.Add(AttackType.WeakAttack);
            }
            EditorGUILayout.EndVertical();
        }
 
        float minTime = EditorGUILayout.FloatField("Minimun Time Before Correct Combo", comboInfo.CombosTimeRequirement[newComboName].Key);
        float maxTime = EditorGUILayout.FloatField("Minimun Time Untill Correct Combo", comboInfo.CombosTimeRequirement[newComboName].Value);
        comboInfo.CombosTimeRequirement[newComboName] = new KeyValuePair<float, float>(minTime, maxTime);

        if (animationTriggers.Count == 0)
            comboInfo.CombosAnimation[newComboName] = EditorGUILayout.TextField("Animation To Execute", comboInfo.CombosAnimation[newComboName]);
        else
        {
            int animationTriggerIndex = 
                EditorGUILayout.Popup(animationTriggers.FindIndex(p => p == comboInfo.CombosAnimation[newComboName]), animationTriggers.ToArray());
            comboInfo.CombosAnimation[newComboName] = animationTriggers[(animationTriggerIndex == -1) ? 0 : animationTriggerIndex];
        }

        if (GUILayout.Button("Remove"))
            comboInfo.removeCombo(comboName);
    }

    private void addNewCombo()
    {
        Combo combo = new Combo();
        string comboName = "Combo " + (comboInfo.Combos.Count + 1);
        combo.Add(AttackType.WeakAttack);
        comboInfo.Combos.Add(comboName, combo);
        comboInfo.CombosAnimation[comboName] = "";
        comboInfo.CombosTimeRequirement[comboName] = new KeyValuePair<float, float>(0, 0);
        toggle.Add(false);
    }

    private void openCombosSheet()
    {
        string path = EditorUtility.OpenFilePanel("Open Combos Sheet", "Assets/Resources/Combos Sheets", "txt");
        if (path == "") return;
        string serializedComboInfo = File.ReadAllText(path, System.Text.Encoding.UTF8);
        comboInfo = new ComboInfo(serializedComboInfo);
        toggle.Clear();
        for (int i = 0; i < comboInfo.CombosCount; ++i)
            toggle.Add(false);
    }

    private void saveCombosSheet()
    {
        string directory = EditorUtility.SaveFilePanel("Save Combos Sheet", "Assets/Resources/Combos Sheets", "CombosSheet", "txt");
        if (directory == "") return;
        File.WriteAllText(directory, comboInfo.serialize());
    }

    
}
