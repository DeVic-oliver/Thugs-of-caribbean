using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDebugger : MonoBehaviour
{
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - obj.transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f * 10f * Time.deltaTime;
        obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
