using System;
using System.Collections.Generic;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Entities;
using Sonder.Scripts.Systems;
using Sonder.Scripts.Ui.UnityConnectStubs;
using TMPro;
using UnityEngine;

namespace Sonder.Scripts.Ui {
    public class ItemSearchUi : BaseEntity {
        public Body Body;
        public Disabable Disabable;
        
        public TextMeshPro header;
        public GameObject content;
        public GameObject listItemGameObject;
        public List<GameObject> textList;
        
        public static ItemSearchUi New(EcsSonderGameWorld world, 
            GameObject uiObject,
            GameObject listItem
            ) {
            var ui = CreateThis<ItemSearchUi>(world);
            
            ui.Disabable = ui.AddComponent<Disabable>(world);
            ui.Body = ui.AddComponent<Body>(world);
            ui.header = uiObject.transform.Find("container_name").gameObject.GetComponent<TextMeshPro>();
            ui.content = uiObject.GetComponentsInChildren<ContentTag>()[0].gameObject;
            ui.listItemGameObject = listItem;
            ui.textList = new List<GameObject>();
            
            ui.Body.init(uiObject);
            ui.Disabable.init(uiObject);
            ui.Disabable.SetActive(false);

            return ui;
        }

        public void SetHeader(String s) {
            header.text = s;
        }

        public void AddTextToContent(EcsSonderGameWorld world, String s) {
            var listItem = ObjectSpawnExtensions.SpawnToParent(listItemGameObject, content);
            var inventoryItem = InventoryItemUi.New(world, listItem);
            inventoryItem.Body.Tr.localPosition = new Vector3(0,-textList.Count,0);
            inventoryItem.SetText(s); 
            textList.Add(listItem);
        }

        public void ClearContent() {
            for (var i = 0; i < textList.Count; i++) {
                ObjectSpawnExtensions.Despawn(textList[i]);
            }
            textList.Clear();
        }
    }
}