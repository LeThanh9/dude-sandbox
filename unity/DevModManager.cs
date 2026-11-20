// Language: C#
// File: DevModManager.cs
// Runtime: Unity 2022+ IL2CPP / Mono Android
// Target: Dude Theft Wars v0.9.1.1b

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevModManager : MonoBehaviour
{
    private static DevModManager _instance;
    public static DevModManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject host = new GameObject("DevModHost");
                DontDestroyOnLoad(host);
                _instance = host.AddComponent<DevModManager>();
            }
            return _instance;
        }
    }

    public static bool IsDevModeActive = false;
    private bool showMenu = false;
    private Rect windowRect = new Rect(20, 20, 320, 480);
    private bool godMode = false;
    private bool buildingsSpawned = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialize()
    {
        Instance.CheckPersistentDevState();
    }

    public void CheckPersistentDevState()
    {
        string savedName = PlayerPrefs.GetString("myName", "");
        int devFlag = PlayerPrefs.GetInt("DTW_DEV_MODE_ENABLED", 0);
        
        if (devFlag == 1 || savedName.Trim().ToLowerInvariant() == "dev")
        {
            ActivateDevMode();
        }
    }

    // Secret trigger hook called from NamePickGui / ProfileUIManager / PlayerNameInputField
    public static void OnPlayerNameChanged(string newName)
    {
        if (string.IsNullOrEmpty(newName)) return;

        // Strictly no suggestion or autocomplete; silent exact match
        if (newName.Trim().ToLowerInvariant() == "dev")
        {
            PlayerPrefs.SetInt("DTW_DEV_MODE_ENABLED", 1);
            PlayerPrefs.SetString("myName", "dev");
            PlayerPrefs.Save();
            Instance.ActivateDevMode();
        }
    }

    public void ActivateDevMode()
    {
        IsDevModeActive = true;
        godMode = true;

        // 1. Unlimited Cash
        GameManager gm = GameManager.Instance;
        if (gm != null)
        {
            gm.cash = 99999999;
            PlayerPrefs.SetInt("Cash", 99999999);
            gm.UnlimitedNos = true;
            gm.wantedLevel = 0;
        }

        // 2. Unlock Richie & VIP Characters
        if (DevTools.Instance != null)
        {
            DevTools.Instance.UnlockRichie();
        }

        // 3. Inject Custom Map Buildings & Structures
        SpawnCustomMapBuildings();

        Debug.Log("[DevMod] DEV MODE ACTIVATED: God Mode, Unlimited Cash, Custom Map Injected.");
    }

    private void Update()
    {
        if (!IsDevModeActive) return;

        // Enforce God Mode
        if (godMode && GameManager.Instance != null)
        {
            if (GameManager.Instance.dudeHealths != null && GameManager.Instance.dudeHealths.Length > 0)
            {
                GameManager.Instance.dudeHealths[0] = 999999f;
            }
            GameManager.Instance.wantedLevel = 0;
            GameManager.Instance.UnlimitedNos = true;
        }

        // Check if map buildings need spawning on new scene load
        if (!buildingsSpawned && GameManager.Instance != null)
        {
            SpawnCustomMapBuildings();
        }
    }

    public void SpawnCustomMapBuildings()
    {
        if (buildingsSpawned) return;

        GameObject mapRoot = new GameObject("DevCustomMap_Root");
        DontDestroyOnLoad(mapRoot);

        // --- 1. Dev Mega Skyscraper at (120, 0, 180) ---
        BuildSkyscraper(mapRoot.transform, new Vector3(120f, 0f, 180f));

        // --- 2. Dev Sky Arena & Stunt Ramp at (0, 85, 0) ---
        BuildSkyArena(mapRoot.transform, new Vector3(0f, 85f, 0f));

        // --- 3. Dev Military Bunker at (-220, 0, -140) ---
        BuildBunker(mapRoot.transform, new Vector3(-220f, 0f, -140f));

        buildingsSpawned = true;
        Debug.Log("[DevMod] All Custom Map Structures & Colliders Spawned Successfully.");
    }

    private void BuildSkyscraper(Transform parent, Vector3 basePos)
    {
        GameObject skyscraper = new GameObject("DevSkyscraper");
        skyscraper.transform.parent = parent;
        skyscraper.transform.position = basePos;

        // Foundation / Ground Floor
        CreateSolidBlock(skyscraper.transform, Vector3.zero, new Vector3(34f, 2f, 34f), Color.gray);

        // Main Tower Column (Height: 70m)
        CreateSolidBlock(skyscraper.transform, new Vector3(0f, 36f, 0f), new Vector3(26f, 70f, 26f), new Color(0.2f, 0.3f, 0.5f));

        // Helipad Roof
        CreateSolidBlock(skyscraper.transform, new Vector3(0f, 72f, 0f), new Vector3(30f, 2f, 30f), Color.yellow);

        // Vehicle Ramp up to 1st floor terrace
        GameObject ramp = CreateSolidBlock(skyscraper.transform, new Vector3(0f, 5f, 25f), new Vector3(8f, 1f, 24f), Color.red);
        ramp.transform.rotation = Quaternion.Euler(-25f, 0f, 0f);
    }

    private void BuildSkyArena(Transform parent, Vector3 basePos)
    {
        GameObject arena = new GameObject("DevSkyArena");
        arena.transform.parent = parent;
        arena.transform.position = basePos;

        // Huge Arena Floor (150m x 150m) with thick solid BoxCollider
        CreateSolidBlock(arena.transform, Vector3.zero, new Vector3(150f, 3f, 150f), new Color(0.15f, 0.15f, 0.15f));

        // Safety Guard Walls
        CreateSolidBlock(arena.transform, new Vector3(0f, 3f, 75f), new Vector3(150f, 6f, 2f), Color.cyan);
        CreateSolidBlock(arena.transform, new Vector3(0f, 3f, -75f), new Vector3(150f, 6f, 2f), Color.cyan);
        CreateSolidBlock(arena.transform, new Vector3(75f, 3f, 0f), new Vector3(2f, 6f, 150f), Color.cyan);
        CreateSolidBlock(arena.transform, new Vector3(-75f, 3f, 0f), new Vector3(2f, 6f, 150f), Color.cyan);

        // Sky Mega Ski-Jump Ramp pointing down to city
        GameObject jumpRamp = CreateSolidBlock(arena.transform, new Vector3(0f, -10f, -85f), new Vector3(16f, 2f, 80f), Color.magenta);
        jumpRamp.transform.rotation = Quaternion.Euler(30f, 0f, 0f);
    }

    private void BuildBunker(Transform parent, Vector3 basePos)
    {
        GameObject bunker = new GameObject("DevMilitaryBunker");
        bunker.transform.parent = parent;
        bunker.transform.position = basePos;

        // Floor
        CreateSolidBlock(bunker.transform, Vector3.zero, new Vector3(40f, 1.5f, 40f), Color.black);
        // Roof
        CreateSolidBlock(bunker.transform, new Vector3(0f, 8f, 0f), new Vector3(42f, 2f, 42f), Color.gray);
        // Walls
        CreateSolidBlock(bunker.transform, new Vector3(0f, 4f, 20f), new Vector3(40f, 8f, 2f), Color.gray);
        CreateSolidBlock(bunker.transform, new Vector3(20f, 4f, 0f), new Vector3(2f, 8f, 40f), Color.gray);
        CreateSolidBlock(bunker.transform, new Vector3(-20f, 4f, 0f), new Vector3(2f, 8f, 40f), Color.gray);
    }

    private GameObject CreateSolidBlock(Transform parent, Vector3 localPos, Vector3 localScale, Color color)
    {
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        block.transform.parent = parent;
        block.transform.localPosition = localPos;
        block.transform.localScale = localScale;

        // Ensure solid collision without falling through
        BoxCollider collider = block.GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = block.AddComponent<BoxCollider>();
        }
        collider.isTrigger = false;

        // Visual styling
        Renderer renderer = block.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            renderer.material.color = color;
        }

        return block;
    }

    private void OnGUI()
    {
        if (!IsDevModeActive) return;

        // Floating Toggle Button
        if (GUI.Button(new Rect(Screen.width - 140, 20, 120, 45), showMenu ? "Close Dev" : "[DEV MENU]"))
        {
            showMenu = !showMenu;
        }

        if (showMenu)
        {
            windowRect = GUI.Window(9999, windowRect, RenderDevMenu, "=== DEV MODE V1.0 ===");
        }
    }

    private void RenderDevMenu(int windowID)
    {
        GUILayout.Label("Status: <color=green>DEVELOPER MODE ACTIVE</color>");
        
        godMode = GUILayout.Toggle(godMode, " God Mode (Invincible)");

        if (GUILayout.Button("Add +10,000,000 Cash"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.cash += 10000000;
                PlayerPrefs.SetInt("Cash", GameManager.Instance.cash);
            }
        }

        if (GUILayout.Button("Spawn Lambo"))
        {
            if (GameManager.Instance != null) GameManager.Instance.OrderMuscleCar();
        }

        if (GUILayout.Button("Spawn Rampy"))
        {
            if (GameManager.Instance != null) GameManager.Instance.SpawnFormula();
        }

        if (GUILayout.Button("Teleport: Dev Skyscraper"))
        {
            TeleportPlayer(new Vector3(120f, 74f, 180f));
        }

        if (GUILayout.Button("Teleport: Dev Sky Arena"))
        {
            TeleportPlayer(new Vector3(0f, 90f, 0f));
        }

        if (GUILayout.Button("Teleport: Dev Bunker"))
        {
            TeleportPlayer(new Vector3(-220f, 3f, -140f));
        }

        if (GUILayout.Button("Re-inject Map Structures"))
        {
            buildingsSpawned = false;
            SpawnCustomMapBuildings();
        }

        GUI.DragWindow();
    }

    private void TeleportPlayer(Vector3 targetPos)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            player.transform.position = targetPos;
            if (cc != null) cc.enabled = true;
        }
    }
}
