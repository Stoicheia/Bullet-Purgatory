using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRenderer : MonoBehaviour
{
	public SpriteRenderer backgroundImage;
	public float scrollSpeed;

	const float SCROLL_LENIENCY = 1f;
    const float GAP_FILL = 0.01f;
    const float SPRITE_DIM = 0.5f;
	MeshCollider myCollider;
	List<SpriteRenderer> scrollingImages;

	void Awake(){
		myCollider = transform.parent.GetComponent<MeshCollider>();
    	scrollingImages = new List<SpriteRenderer>();
	}

    void Start()
    {
    	for(int i=-1; i<=1; i++){
	        SpriteRenderer myBackground = Instantiate(backgroundImage, transform.position, Quaternion.identity) as SpriteRenderer;
	        myBackground.transform.parent = transform;
	        float scale = myCollider.bounds.size.x/myBackground.bounds.size.x;
            Vector3 toScale = myBackground.transform.localScale;
	        toScale *= scale;
            myBackground.transform.localScale = toScale;
	        myBackground.transform.Translate(new Vector3(0,(i*(1-GAP_FILL))*myBackground.bounds.size.y,10));
            myBackground.color = new Color(SPRITE_DIM, SPRITE_DIM, SPRITE_DIM);
	        scrollingImages.Add(myBackground);
   		}
    }

    void Update()
    {
        foreach(SpriteRenderer si in scrollingImages){
        	si.transform.Translate(new Vector3(0,-scrollSpeed*Time.deltaTime,0));
        	if(si.bounds.max.y<myCollider.bounds.min.y-SCROLL_LENIENCY){
        		si.transform.Translate(new Vector3(0,3*si.bounds.size.y-4*GAP_FILL*si.bounds.size.y,0));
        	}
        }
    }
}
