using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ComboEditorWindow : EditorWindow
{
    private ComboInfo comboInfo = new ComboInfo();
    private Animator animator;
    private List<string> animationTriggers = new List<string>();
    private List<bool> toggle = new List<bool>();
    private Vector2 scrollPosition = new Vector2(0, 0);
    private GameObject lastSelection;

    [MenuItem("Tibi Games/Combo Editor")]
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
            if (Selection.activeGameObject != null && Selection.activeGameObject != lastSelection) {
                lastSelection = Selection.activeGameObject;
                CombosManager selectedCombosManager = lastSelection.GetComponent<CombosManager>();
                if (selectedCombosManager != null)
                    initComboInfoWith(selectedCombosManager.ComboSheet);
                animator = lastSelection.GetComponent<Animator>();
            }
            animator = EditorGUILayout.ObjectField(animator, typeof(Animator), true) as Animator;
            
            if (animator != null) {
                foreach (AnimatorControllerParameter parameter in animator.parameters)
                    if (parameter.type == AnimatorControllerParameterType.Trigger)
                        animationTriggers.Add(parameter.name);
            }
            else animationTriggers.Clear();
            EditorGUILayout.Separator();
            int editorPosition = 0;
            List<string> keys = new List<string>(comboInfo.Combos.Keys);
            foreach (string comboName in keys) {
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
            TimeRange timeRequirements = comboInfo.CombosTimeRequirement[comboName];
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

        if (combo.ComboSequence.Count > 1) {
            if (comboInfo.CombosTimeRequirement[newComboName].isFalsey())
                comboInfo.CombosTimeRequirement[newComboName] = new TimeRange(0, 0);

            float minTime = EditorGUILayout.FloatField("Minimun Time For Correct Combo", comboInfo.CombosTimeRequirement[newComboName].MinTime);
            float maxTime = EditorGUILayout.FloatField("Maximun Time For Correct Combo", comboInfo.CombosTimeRequirement[newComboName].MaxTime);
            comboInfo.CombosTimeRequirement[newComboName] = new TimeRange(minTime, maxTime);
        } else
            comboInfo.CombosTimeRequirement[newComboName] = TimeRange.FalseyTimeRange();

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
        comboInfo.CombosTimeRequirement[comboName] = null;
        toggle.Add(false);
    }

    private void openCombosSheet()
    {
        string path = EditorUtility.OpenFilePanel("Open Combos Sheet", "Assets/Resources/Combos Sheets", "txt");
        if (path != "") {
            initComboInfoWith(File.ReadAllText(path, System.Text.Encoding.UTF8));
        }
    }

    private void initComboInfoWith(string serializedComboInfo) {
        comboInfo = new ComboInfo(serializedComboInfo);
        toggle.Clear();
        toggle.AddRange(new bool[comboInfo.CombosCount]);
    }

    private void saveCombosSheet()
    {
        string directory = EditorUtility.SaveFilePanel("Save Combos Sheet", "Assets/Resources/Combos Sheets", "CombosSheet", "txt");
        if (directory == "") return;
        File.WriteAllText(directory, comboInfo.serialize());
    }

    
}
