using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class BackgroundRenderer : MonoBehaviour
{
	public SpriteRenderer backgroundImage;
	public float scrollSpeed;
	const float SCROLL_LENIENCY = 1f;
	MeshCollider myCollider;
	List<SpriteRenderer> scrollingImages;

	void Awake(){
		myCollider = GetComponent<MeshCollider>();
    	scrollingImages = new List<SpriteRenderer>();
	}

    void Start()
    {
    	for(int i=-1; i<=1; i++){
	        SpriteRenderer myBackground = Instantiate(backgroundImage, transform.position, Quaternion.identity) as SpriteRenderer;
	        myBackground.transform.parent = transform;
	        float scale = myCollider.bounds.size.x/myBackground.bounds.size.x;
	        myBackground.transform.localScale *= scale;
	        myBackground.transform.Translate(new Vector3(0,i*myBackground.bounds.size.y,0));
	        scrollingImages.Add(myBackground);
   		}
    }

    void Update()
    {
        foreach(SpriteRenderer si in scrollingImages){
        	si.transform.Translate(new Vector3(0,-scrollSpeed*Time.deltaTime,0));
        	if(si.bounds.max.y<myCollider.bounds.min.y-SCROLL_LENIENCY){
        		si.transform.Translate(new Vector3(0,3*si.bounds.size.y,0));
        	}
        }
    }
}
