using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cardooo.core;

public class Graphics : MonoBehaviour
{
    public Main CurMain = null;

    public GameObject BlockCube = null;

    public Camera mainCamera = null;

    public List<BlockCube> list = new List<BlockCube>();

    MonoGobjPool pool = null;

    public void Init()
    {
        pool = MonoGobjPoolMgr.Instance.CreatePool("BlockCube", BlockCube, 1000);

        for (int i = 0, len = CurMain.Width; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                GameObject go = pool.Get();
                BlockCube blockCube = go.GetComponent<BlockCube>();
                blockCube.TargetBlock = CurMain.GetBlock(i, j);
                list.Add(blockCube);
                go.transform.position = new Vector3(i, 0, j);
                go.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BlockCube bc in list)
        {
            bc.SetLife();
        }

        Control();
    }

    private void OnDestroy()
    {
        foreach (BlockCube i in list)
        {
            pool.Recycle(i.gameObject);
        }
    }

    void Control()
    {
        EventSystem eventSystem = FindObjectOfType<EventSystem>();

        // there is no event system (no gui) or no gui element under the cursor
        if (eventSystem != null && eventSystem.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetKeyDown("mouse 0"))
        {
            Debug.Log("Smasher");
            if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                return;

            Debug.Log("Hit Gobj! ");
            hit.collider.transform.parent.GetComponent<BlockCube>().TargetBlock.Alive = !hit.collider.transform.parent.GetComponent<BlockCube>().TargetBlock.Alive;
        }
    }

    public void ZoomIn()
    {
        mainCamera.orthographicSize += 5;
    }

    public void ZoomOut()
    {
        mainCamera.orthographicSize -= 5;
    }
}
