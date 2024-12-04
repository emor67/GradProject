using UnityEngine;
using DG.Tweening;

public class CoinAnimDOTween : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.SetTweensCapacity(500,50);
        transform.DOLocalRotate(new Vector3(90,0,360), 4f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOScale(new Vector3(1.3f,0.1f,1.3f), 2f).SetEase(Ease.InOutSine).OnComplete(()=> {
            ScaleBack();
        });
    }
    private void ScaleBack(){
        transform.DOScale(new Vector3(1f,0.1f,1f), 2f).SetEase(Ease.InOutSine).OnComplete(()=> {
            Scale();
        });
    }
    
    private void Scale(){
        transform.DOScale(new Vector3(1.3f,0.1f,1.3f), 2f).SetEase(Ease.InOutSine).OnComplete(()=> {
            ScaleBack();
        });
    }
}
