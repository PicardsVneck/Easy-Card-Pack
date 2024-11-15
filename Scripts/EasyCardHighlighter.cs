using UnityEngine;

namespace EasyCardPack
{

public class EasyCardHighlighter : MonoBehaviour
{
    [Header("Backdrop Highlighting")]
    [SerializeField] private GameObject _highlightBackdrop;

    [Header("Face Highlighting")]
    [SerializeField] private bool _highlightFace = false;
    [SerializeField] private MeshRenderer _faceMeshRenderer;
    [SerializeField] [ColorUsage(true,true)]  private Color _faceHighlightColor = Color.white;
    private Color _faceOriginalColor;

    [Header("Back Highlighting")]
    [SerializeField] private bool _highlightBack = false;
    [SerializeField] private MeshRenderer _backMeshRenderer;
    [SerializeField] [ColorUsage(true,true)] private Color _backHighlightColor = Color.white;
    private Color _backOriginalColor;

    private void Start()
    {
        if (_highlightFace && _faceMeshRenderer != null)
        {
            _faceOriginalColor = _faceMeshRenderer.material.GetColor("_Background_Color");
        }

        if (_highlightBack && _backMeshRenderer != null)
        {
            _backOriginalColor = _backMeshRenderer.material.GetColor("_Background_Color");
        }
    }

    public void Highlight(bool enabled)
    {
        if (_highlightBackdrop != null)
        {
            _highlightBackdrop.SetActive(enabled);
        }

        if (_highlightFace && _faceMeshRenderer != null)
        {
            _faceMeshRenderer.material.SetColor("_Background_Color", enabled ? _faceHighlightColor : _faceOriginalColor);
        }

        if (_highlightBack && _backMeshRenderer != null)
        {
            _backMeshRenderer.material.SetColor("_Background_Color", enabled ? _backHighlightColor : _faceOriginalColor);
        }
    }
}

}
