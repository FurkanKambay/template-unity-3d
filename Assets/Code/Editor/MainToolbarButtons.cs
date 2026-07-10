using System.Diagnostics;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FK.Game.Editor
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class MainToolbarButtons
    {
        private const string protoScenePath = "Proto";

#region Buttons
        [MainToolbarElement("Open Scene/Proto", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement OpenProtoSceneButton() => GetSceneButton("🚧", protoScenePath);

        [MainToolbarElement("Open Project Settings", defaultDockPosition = MainToolbarDockPosition.Left)]
        public static MainToolbarElement ProjectSettingsButton()
        {
            var icon = EditorGUIUtility.IconContent("SettingsIcon").image as Texture2D;
            var content = new MainToolbarContent(icon, "Project Settings");
            return new MainToolbarButton(content, () => SettingsService.OpenProjectSettings());
        }

#if UNITY_EDITOR_WIN
        [MainToolbarElement("Open Builds in File Explorer", defaultDockPosition = MainToolbarDockPosition.Left)]
        public static MainToolbarElement BuildsButton()
        {
            var content = new MainToolbarContent("Builds", "Open Builds in File Explorer");
            return new MainToolbarButton(content, () => Process.Start("explorer.exe", "Builds"));
        }
#endif
#endregion

        private static MainToolbarButton GetSceneButton(string text, string sceneSubPath)
        {
            var content = new MainToolbarContent(text, $"Open \"{sceneSubPath}.unity\"");
            return new MainToolbarButton(content, () => TryOpenScene(sceneSubPath));
        }

        private static bool TryOpenScene(string sceneSubPath, OpenSceneMode openSceneMode = OpenSceneMode.Single)
        {
            if (Application.isPlaying || !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return false;

            Scene scene = EditorSceneManager.OpenScene($"Assets/Scenes/{sceneSubPath}.unity", openSceneMode);
            return scene.isLoaded;
        }
    }
}
