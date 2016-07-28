using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(Grid))]
public class GridEditor : Editor 
{

    Grid grid;

    private int oldIndex = 0;

    private Vector3 mouseBeginPos;
    private Vector3 mouseEndPos;

    void OnEnable()
    {
        grid = (Grid)target;
    }

    [MenuItem("Assets/Create/TileSet")]
    static void CreateTileSet()
    {
        var asset = ScriptableObject.CreateInstance<TileSet>();
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        } else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(path), "");
        } else
        {
            path += "/";
        }

        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "TileSet.asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        asset.hideFlags = HideFlags.DontSave;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        grid.width = createSlider("Width", grid.width);
        grid.height = createSlider("Height", grid.height);

        /*if(GUILayout.Button("Open Grid Window"))
        {
            GridWindow window = (GridWindow)EditorWindow.GetWindow(typeof(GridWindow));
            window.init();
        }*/

        //Tile Prefab
        EditorGUI.BeginChangeCheck();
        var newTilePrefab = (Transform)EditorGUILayout.ObjectField("Tile Prefab", grid.tilePrefab, typeof(Transform), false);
        if (EditorGUI.EndChangeCheck())
        {
            grid.tilePrefab = newTilePrefab;
            Undo.RecordObject(target, "Grid Changed");
        }

        //Tile Map
        EditorGUI.BeginChangeCheck();
        var newTileSet = (TileSet)EditorGUILayout.ObjectField("Tile Set", grid.tileSet, typeof(TileSet), false);
        if (EditorGUI.EndChangeCheck())
        {
            grid.tileSet = newTileSet;
            Undo.RecordObject(target, "Grid Changed");
        }

        if (grid.tileSet != null)
        {
            EditorGUI.BeginChangeCheck();
            var names = new string[grid.tileSet.prefabs.Length];
            var values = new int[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = grid.tileSet.prefabs[i] != null ? grid.tileSet.prefabs[i].name : "";
                values[i] = i;
            }

            var index = EditorGUILayout.IntPopup("Select Tile", oldIndex, names, values);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Grid Changed");
                if (oldIndex != index)
                {
                    oldIndex = index;
                    grid.tilePrefab = grid.tileSet.prefabs[index];

                    float width = grid.tilePrefab.GetComponent<Renderer>().bounds.size.x;
                    float height = grid.tilePrefab.GetComponent<Renderer>().bounds.size.y;

                    grid.width = width;
                    grid.height = height;
                }
            }
        }

        EditorGUI.BeginChangeCheck();

        bool draggable = EditorGUILayout.Toggle("Toggle Dragging: ", grid.draggable);
        if (EditorGUI.EndChangeCheck())
        {
            grid.draggable = draggable;
        }
    }

    private float createSlider(string labelName, float sliderPosition)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid " + labelName);
        sliderPosition = EditorGUILayout.Slider(sliderPosition, .01f, 100.00f, null);
        EditorGUILayout.EndHorizontal();
        return sliderPosition;
    }

    void OnSceneGUI()
    {
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        Event E = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(E.mousePosition.x, - E.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = ray.origin;

        if (E.isMouse && E.type == EventType.mouseDown & E.button == 0)
        {
            GUIUtility.hotControl = controlId;
            E.Use();

            if (grid.draggable)
            {
                mouseBeginPos = mousePos;
            }
            else {

                GameObject gameObject;
                Transform prefab = grid.tilePrefab;
                if (prefab)
                {
                    Undo.IncrementCurrentGroup();
                    gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab.gameObject);
                    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0.0f);

                    if (GetTransformFromPosition(aligned) != null) return;

                    gameObject.transform.position = aligned;
                    gameObject.transform.parent = grid.transform;
                    Undo.RegisterCreatedObjectUndo(gameObject, "Create" + gameObject.name);
                }
            }
        }
        
        if(E.isMouse & E.type == EventType.MouseDown && (E.button == 0 || E.button == 1))
        {
            GUIUtility.hotControl = controlId;
            E.Use();
            Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0.0f);
            Transform transform = GetTransformFromPosition(aligned);
            if (transform != null)
            {
                DestroyImmediate(transform.gameObject);
            }
        }

        if (E.isMouse && E.type == EventType.MouseUp && (E.button == 0 || E.button == 1))
        {
            if (grid.draggable && E.button == 0)
            {
                mouseEndPos = mousePos;
                FillArea(mouseBeginPos, mouseEndPos);

                mouseEndPos = Vector3.zero;
                mouseBeginPos = Vector3.zero;
            }
            GUIUtility.hotControl = 0;
        }
    }

    Transform GetTransformFromPosition(Vector3 aligned)
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Transform transform = grid.transform.GetChild(i);
            if(transform.position == aligned)
            {
                return transform;
            }
        }
        return null;
    }

    void FillArea(Vector3 _StartPosition, Vector3 _EndPosition)
    {

        Transform prefab = grid.tilePrefab;
        if (prefab == null)
        {
            Debug.LogError("No prefab attached to grid.");
        }

        _StartPosition.x = Mathf.Floor(_StartPosition.x / grid.width) * grid.width;
        _StartPosition.y = Mathf.Floor(_StartPosition.y / grid.height) * grid.height;

        _EndPosition.x = Mathf.Floor(_EndPosition.x / grid.width) * grid.width;
        _EndPosition.y = Mathf.Floor(_EndPosition.y / grid.height) * grid.height;

        Vector2 numberOfTilesToFill = new Vector2();


        // look if there is a drag from right to left or bottom to top
        // if so swap entries
        numberOfTilesToFill.x = Mathf.Abs(_StartPosition.x - _EndPosition.x);
        numberOfTilesToFill.y = Mathf.Abs(_StartPosition.y - _EndPosition.y);

        // swap to fill from left to right
        if (_StartPosition.x >= _EndPosition.x)
        {
            Vector3 tmp = new Vector3();
            tmp = _EndPosition;
            _StartPosition = _EndPosition;
            _EndPosition = tmp;
        }

        numberOfTilesToFill.x = numberOfTilesToFill.x / grid.width + 1.0f;
        numberOfTilesToFill.y = numberOfTilesToFill.y / grid.height + 1.0f;

        for (int i = 0; i < numberOfTilesToFill.x; i++)
        {
            for (int j = 0; j < numberOfTilesToFill.y; j++)
            {

                Vector3 realWorldPosition = new Vector3();
                GameObject gameObject;

                realWorldPosition.x = _StartPosition.x + (i * grid.width) + grid.width / 2.0f;
                realWorldPosition.y = _StartPosition.y - (j * grid.height) + grid.height / 2.0f;
                realWorldPosition.z = 0.0f;

                gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab.gameObject);
                gameObject.transform.position = realWorldPosition;
                gameObject.transform.parent = grid.transform;

            }

        }

    }

}
