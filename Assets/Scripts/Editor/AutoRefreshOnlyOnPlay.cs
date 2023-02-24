using UnityEditor;

namespace BlankRip.Tools
{
    [InitializeOnLoad]
	public class ChangeUnityAutoRefreshOption
	{
		private const string autoRefreshPrefKey = "kAutoRefresh";
		private const string menuPath = "BlankRip/Disable Autorefresh";

		private static bool isChecked = false;
		public static bool IsChecked {
			get { return isChecked; }
			private set {
				isChecked = value;
				Menu.SetChecked(menuPath, isChecked);
				EditorPrefs.SetBool(autoRefreshPrefKey, !isChecked);
			}
		}
		
		static ChangeUnityAutoRefreshOption() {
			//Populate the value after taking the information from the Unity settings
			EditorApplication.delayCall += () => {
                IsChecked = !EditorPrefs.GetBool(autoRefreshPrefKey, false);
            };
		}
		
		[MenuItem(menuPath)]
		private static void ToggleUnityAutoRefreshState()
		{
			IsChecked = !IsChecked;
		}
	}
	
	[InitializeOnLoad]
	public static class RefreshEditorOnPlay
	{
		static RefreshEditorOnPlay() {
			EditorApplication.playModeStateChanged += PlayRefresh;
		}
  
		private static void PlayRefresh(PlayModeStateChange state) {
			//Force the refresh when we press Play button on the Level Editor but just if we have the auto refresh option disabled.
			if(state == PlayModeStateChange.ExitingEditMode && ChangeUnityAutoRefreshOption.IsChecked) {
				AssetDatabase.Refresh();
			}
		}
	}
}