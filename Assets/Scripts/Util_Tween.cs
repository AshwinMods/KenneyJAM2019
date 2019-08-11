using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Util_Tween : MonoBehaviour
{
    public enum TweenType
    {
        None, Move, MoveUI, Rotate, Scale, ColorMat, ColorVtx, Alpha,
    }

    public enum TweenPara
    {
        None, Zero_To_One, One_To_Zero,
        FloatA_To_FloatB, VectA_To_VectB,
        ColA_To_ColB, ViewA_To_ViewB,
    }

    [System.Serializable]
    public struct TweenConfig
    {
        public string name;
        public TweenType type;
        public TweenPara para;
        public LeanTweenType curve;
        public float delay;
        public float duration;
        [Space]
        [Header("Para")]
        public bool use_curVal_as_A;
        public float flt_A, flt_B;
        public Vector3 vec_A, vec_B;
        public Color col_A, col_B;
        public Vector3 vPrt_A, vPrt_B;
        [Space]
        public UnityEvent onStart;
        public UnityEvent onComplete;
    }

    public TweenConfig[] tweenConfigs;

    public static Camera Cam
    {
        get { return Camera.main; }
    }

    public void Execute_Effect(string name)
    {
        if (string.IsNullOrEmpty(effect_OnEnable) && tweenConfigs.Length < 1)
        {
            return;
        }

        int index = -1;

        for (int i = 0, iMax = tweenConfigs.Length; i < iMax; ++i)
        {
            if (tweenConfigs[i].name == name)
            {
                index = i;
                break;
            }
        }

        if (index < 0)
        {
            return;
        }

        LeanTween.delayedCall(gameObject, tweenConfigs[index].delay, () => {
            Apply_Effect(index);
        });
    }

    public void Apply_Effect(int index)
    {
        TweenConfig config = tweenConfigs[index];
        LTDescr tweenDescr = null;
        Vector3 v3a, v3b;
        switch (config.type)
        {
            case TweenType.None:
                break;
            case TweenType.Move:

                switch (config.para)
                {
                    case TweenPara.None:
                        break;
                    case TweenPara.Zero_To_One:
                        break;
                    case TweenPara.One_To_Zero:
                        break;
                    case TweenPara.FloatA_To_FloatB:
                        break;
                    case TweenPara.VectA_To_VectB:
                        if (!config.use_curVal_as_A)
                            transform.position = config.vec_A;
                        tweenDescr = LeanTween.move(gameObject, config.vec_B, config.duration);
                        break;
                    case TweenPara.ColA_To_ColB:
                        break;
                    case TweenPara.ViewA_To_ViewB:
                        if (!config.use_curVal_as_A)
                        {
                            v3a = Cam.ViewportToWorldPoint(config.vPrt_A);
                            v3a.z = transform.position.z;
                            transform.position = v3a;
                        }
                        v3b = Cam.ViewportToWorldPoint(config.vPrt_B);
                        v3b.z = transform.position.z;

                        tweenDescr = LeanTween.move(gameObject, v3b, config.duration).setEase(config.curve);
                        break;
                    default:
                        break;
                }

                break;
            case TweenType.MoveUI:
                RectTransform rt = GetComponent<RectTransform>();
                switch (config.para)
                {
                    case TweenPara.None:
                        break;
                    case TweenPara.Zero_To_One:
                        break;
                    case TweenPara.One_To_Zero:
                        break;
                    case TweenPara.FloatA_To_FloatB:
                        break;
                    case TweenPara.VectA_To_VectB:
                        if (!config.use_curVal_as_A)
                            transform.position = config.vec_A;
                        tweenDescr = LeanTween.move(gameObject, config.vec_B, config.duration);
                        break;
                    case TweenPara.ColA_To_ColB:
                        break;
                    case TweenPara.ViewA_To_ViewB:
                        CanvasScaler cs = GetComponentInParent<CanvasScaler>();
                        RectTransform crt = cs.GetComponent<RectTransform>();
                        if (!config.use_curVal_as_A)
                        {
                            v3a.x = crt.rect.width * config.vPrt_A.x * crt.localScale.x;
                            v3a.y = crt.rect.height * config.vPrt_A.y * crt.localScale.y;
                            v3a.z = transform.position.z;
                            transform.position = v3a;
                        }

                        v3b.x = crt.rect.width * config.vPrt_B.x * crt.localScale.x;
                        v3b.y = crt.rect.height * config.vPrt_B.y * crt.localScale.y;
                        v3b.z = transform.position.z;
                        tweenDescr = LeanTween.move(gameObject, v3b, config.duration).setEase(config.curve);
                        break;
                    default:
                        break;
                }

                break;
            case TweenType.Rotate:
                break;
            case TweenType.Scale:
                break;
            case TweenType.ColorMat:
                break;
            case TweenType.ColorVtx:
                break;
            default:
                break;
        }

        if (tweenDescr != null)
        {
            tweenDescr.setOnStart(() => { config.onStart.Invoke(); });
            tweenDescr.setOnComplete(() => { config.onComplete.Invoke(); });
        }
    }
    [SerializeField] string effect_OnEnable = "Open";
    [SerializeField] string effect_CloseDisable = "Close";

    private void OnEnable()
    {
        Execute_Effect(effect_OnEnable);
    }

    public void Close_And_Disable()
    {
        Execute_Effect(effect_CloseDisable);
    }
}
