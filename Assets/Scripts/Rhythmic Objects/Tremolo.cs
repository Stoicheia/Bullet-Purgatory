using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhythmicObject))]
public class Tremolo : MonoBehaviour
{
   	public float horzTremoloDistance;
   	public float horzTremoloSpeed;

   	public float vertTremoloDistance;
   	public float vertTremoloSpeed;

   	Vector3 startPosition;

   	float extremeLeft;
   	float extremeRight;
   	float extremeUp;
   	float extremeDown;


   	void Start(){
   		startPosition = transform.localPosition;
   	}

    void Update()
    {
    	extremeLeft = startPosition.x + horzTremoloDistance/2;
   		extremeRight = startPosition.x - horzTremoloDistance/2;
   		extremeUp = startPosition.y + vertTremoloDistance/2;
   		extremeDown = startPosition.y - vertTremoloDistance/2;
    	float horizontalPos = Mathf.Lerp(extremeLeft,extremeRight,HarmonicOscillation(Time.time,horzTremoloSpeed));
    	float verticalPos = Mathf.Lerp(extremeUp,extremeDown,HarmonicOscillation(Time.time,horzTremoloSpeed));
        transform.localPosition = new Vector3(horizontalPos,verticalPos,0);
    }

    float HarmonicOscillation(float t, float s){
    	return 0.5f+Mathf.Sin(Time.time*horzTremoloSpeed)/2;
    }
}
