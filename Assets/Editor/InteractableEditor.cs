using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UIElements;


[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable) target;
        if(target.GetType() == typeof(EventOnlyInteractable)){
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteractable can only use Events", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null){
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else{
            base.OnInspectorGUI();
            if(interactable.useEvents){
                if(interactable.GetComponent<InteractionEvent>() == null){
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else{
                if(interactable.GetComponent<InteractionEvent>() != null){
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
        
    }
}
