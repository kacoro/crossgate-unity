using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BattleUnit : MonoBehaviour
{
   [SerializeField] PetBase _base;
   [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;
    public Pet Pet {get;set;}

    Image image;
    Vector3 originalPos;
    Color originalColor;
    private void Awake() {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

   public void Setup(){
       Pet = new Pet(_base,level);
       if(isPlayerUnit){
           image.sprite = Pet.Base.Sprite;
       }else{
           image.sprite = Pet.Base.Sprite;
       }
       image.color = originalColor;
       PlayEnterAnimation();
   }

   public void PlayEnterAnimation(){
       if(isPlayerUnit){
           image.transform.localPosition = new Vector3(900f,-900f*47/64);
       }else{
           image.transform.localPosition = new Vector3(-900f,900f*47/64);
       }
       image.transform.DOLocalMove(originalPos,1f);
   }

   public void PlayAttackAnimation(){
       var sequence = DOTween.Sequence();
       if(isPlayerUnit){
          sequence.Append(image.transform.DOLocalMove(new Vector3(originalPos.x - 50f,originalPos.y+50*47/64,0f),0.25f));
       }
       else{
           sequence.Append(image.transform.DOLocalMove(new Vector3(originalPos.x + 50f,originalPos.y-50*47/64,0f),0.25f));
       }
       sequence.Append(image.transform.DOLocalMove(originalPos,0.25f));
   }
   public void PlayHitAnimation(){
       var sequence = DOTween.Sequence();
       sequence.Append(image.DOColor(Color.gray,0.1f));
       sequence.Append(image.DOColor(originalColor,0.1f));
   }

   public void PlayFaintAnimation(){
       var sequence = DOTween.Sequence();
       sequence.Append(image.transform.DOMoveY(originalPos.y -150f,0.5f));
       sequence.Join(image.DOFade(0f,0.5f));
   }
}
