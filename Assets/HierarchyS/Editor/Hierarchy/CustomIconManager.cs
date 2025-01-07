using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Shadowprofile.Utils.HierarchyS;
namespace Shadowprofile.HierarchyS
{
    public class CustomIconManager : EditorWindow
    {
        private static CustomIconData s_IconData;

        private TextField m_ComponentNameField;
        private ObjectField m_CustomIconField;
        private ScrollView m_AssignedIconsScrollView;

        [MenuItem("Tools/Custom Icon Manager")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<CustomIconManager>("Custom Icon Manager");
            wnd.minSize = new Vector2(600, 400);
        }

        private void OnEnable()
        {
            LoadOrCreateIconData();
            CreateUI();
        }

        private static void LoadOrCreateIconData()
        {
            s_IconData = ShadowProfileUtils.GetAssetFromRelativePath<CustomIconData>("HierarchyS/Editor");
            Debug.Log("Icon Data: " + s_IconData);
            if (s_IconData != null) return;

            s_IconData = CreateInstance<CustomIconData>();
            AssetDatabase.CreateAsset(s_IconData, ShadowProfileUtils.GetFullPathFromRelativePath("HierarchyS/Editor", "t:CustomIconData") + "/CustomIconData.asset");
            AssetDatabase.SaveAssets();
        }

        private void CreateUI()
        {
            var root = rootVisualElement;

            var visualTree = ShadowProfileUtils.GetAssetFromRelativePath<VisualTreeAsset>("HierarchyS/Editor/Layouts", "CustomIconManager");
            if (visualTree == null)
            {
                Debug.LogError("UXML file not found!");
                return;
            }

            visualTree.CloneTree(root);

            var styleSheet = ShadowProfileUtils.GetAssetFromRelativePath<StyleSheet>("HierarchyS/Editor/Layouts", "CustomIconManager");
            if (styleSheet == null)
            {
                Debug.LogError("USS file not found!");
                return;
            }

            root.styleSheets.Add(styleSheet);

            m_ComponentNameField = root.Q<TextField>("componentNameField");
            m_AssignedIconsScrollView = root.Q<ScrollView>("assignedIconsScrollView");

            if (m_ComponentNameField == null || m_AssignedIconsScrollView == null)
            {
                Debug.LogError("UI elements not found in UXML!");
                return;
            }

            m_ComponentNameField.AddToClassList("text-input");

            var assignBlock = root.Q<VisualElement>("assignBlock");
            m_CustomIconField = new ObjectField("Custom Icon")
            {
                objectType = typeof(Texture2D),
                allowSceneObjects = false
            };
            m_CustomIconField.AddToClassList("icon-input");
            assignBlock.Add(m_CustomIconField);
            m_CustomIconField.PlaceBehind(root.Q<Button>("assignIconButton"));

            root.Q<Button>("assignIconButton").clicked += AssignIcon;
            root.Q<Button>("clearAllIconsButton").clicked += ClearAllIcons;

            RefreshAssignedIcons();
        }

        private void AssignIcon()
        {
            var componentName = m_ComponentNameField.value;
            var customIcon = (Texture2D)m_CustomIconField.value;

            if (string.IsNullOrEmpty(componentName) || customIcon == null)
            {
                Debug.LogWarning("Component Name and Icon must be provided.");
                return;
            }

            var iconPath = AssetDatabase.GetAssetPath(customIcon);
            s_IconData.SetIconPath(componentName, iconPath);
            SaveIconData();
            RefreshAssignedIcons();
        }

        private void RefreshAssignedIcons()
        {
            m_AssignedIconsScrollView.Clear();

            foreach (var kvp in s_IconData.GetIconPaths())
            {
                var container = new VisualElement();
                container.style.flexDirection = FlexDirection.Row;

                var label = new Label(kvp.Key);
                label.style.flexGrow = 1;
                container.Add(label);

                var icon = new Image { image = AssetDatabase.LoadAssetAtPath<Texture2D>(kvp.Value) };
                icon.style.width = 16;
                icon.style.height = 16;
                container.Add(icon);

                var removeButton = new Button(() => RemoveIcon(kvp.Key)) { text = "Remove" };
                removeButton.style.width = 60;
                container.Add(removeButton);

                m_AssignedIconsScrollView.Add(container);
            }
        }

        private void RemoveIcon(string key)
        {
            s_IconData.RemoveIconPath(key);
            SaveIconData();
            RefreshAssignedIcons();
        }

        private void ClearAllIcons()
        {
            s_IconData.ClearIcons();
            SaveIconData();
            RefreshAssignedIcons();
        }

        private static void SaveIconData()
        {
            EditorUtility.SetDirty(s_IconData);
            AssetDatabase.SaveAssets();
        }
    }
}