using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class YourIcons : Editor
{
  static Texture2D yourIcon;
  static bool isInited = false;

  static YourIcons()
  {
    Initialize();
  }

  static void Initialize()
  {
    if (isInited)
    {
      // Only do this once.
      return;
    }

    // Find the current path we're running in
    DirectoryInfo rootDir = new DirectoryInfo(Application.dataPath);

    // Search the directory for this file
    FileInfo[] files = rootDir.GetFiles("YourIcons.cs", SearchOption.AllDirectories);

    // rework our file path into the Assets path
    string editorPath = Path.GetDirectoryName(files[0].FullName
                                .Replace("\\", "/")
                                .Replace(Application.dataPath, "Assets"));

    // Add the name of the folder with your icon
    string editorGUIPath = editorPath + "/GUI";

    // Load the icon
    yourIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(editorGUIPath + "/yourIcon.png");

    EditorApplication.hierarchyWindowItemOnGUI -= HierarchyIconsOnGUI;
    EditorApplication.hierarchyWindowItemOnGUI += HierarchyIconsOnGUI;
    isInited = true;
  }

  static void HierarchyIconsOnGUI(int instanceId, Rect selectionRect)
  {
    GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceId);

    if (!go)
    {
      // If this isn't a GameObject, then stop processing
      return;
    }

    // create a rectangle to hold the texture.
    Rect rect = new Rect(selectionRect.x - 25f, selectionRect.y + 2, 15f, 15f);

    // Check to see if the GameObject is one we care about
    /*if (go.GetComponent<YourComponent>())
    {
      // Position the rectangle off to the right of list, 15px in from the edge
      rect.x = selectionRect.x + selectionRect.width - 15f;
      GUI.Label(rect, yourIcon);
      return;
    }*/
  }
}