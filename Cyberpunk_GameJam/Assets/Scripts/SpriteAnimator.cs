using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {
    public SpriteAnimation[] spriteAnimations;

    public IEnumerator PlayAnimation(string animationName, bool loop){
        foreach (SpriteAnimation animation in spriteAnimations) {
            if(animation.animationName == animationName){

                while (true) {
                    int frames = 0;
                    while (frames < animation.spriteList.Length) {

                        GetComponent<SpriteRenderer>().sprite = animation.spriteList[frames];
                        yield return new WaitForSeconds(animation.frameTimes[frames]); 

                        frames += 1;

                        if (loop == false) {
                            yield break;
                        }
                    }
                }
            }
        }
    }

}

[System.Serializable]
public class SpriteAnimation {
    public string animationName;
    public Sprite[] spriteList;
    public float[]  frameTimes;
}
