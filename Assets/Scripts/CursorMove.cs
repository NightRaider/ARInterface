using UnityEngine;
using System.Collections;

public class CursorMove : MonoBehaviour {

    public Transform stick;
    public float screenScale=1.0f;
    public float smoothing=1.0f;
    public int startZ=2000;

	void Update ()
    {
        Vector3 projXY= new Vector3(stick.position.x, stick.position.y, 0);
        Vector3 targetZ = new Vector3(0, 0, startZ);
        if (Vector3.Distance(transform.position, screenScale * projXY+targetZ) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, screenScale * projXY + targetZ, smoothing*Time.deltaTime);
        }
    }
	
	// Update is called once per frame
	//IEnumerator MoveCursor (Transform target)
 //   {
 //       while (Vector3.Distance(transform.position, screenScale*(new Vector3(target.position.x,target.position.y,0.0f))) > 0.05f)
 //       {
 //           transform.position = Vector3.Lerp(transform.position, screenScale * (new Vector3(target.position.x, target.position.y, 0.0f)), Time.deltaTime);
 //           yield return null;
 //       }
 //   }
}
