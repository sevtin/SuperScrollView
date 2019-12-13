using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
    public class ListItem2 : MonoBehaviour
    {
        //标题名称
        public Text mNameText;
        //图标
        public Image mIcon;
        //星星
        public Image[] mStarArray;
        //描述
        public Text mDescText;
        //描述2
        public Text mDescText2;
        //红色
        public Color32 mRedStarColor = new Color32(249, 227, 101, 255);
        //白色
        public Color32 mGrayStarColor = new Color32(215, 215, 215, 255);
        public GameObject mContentRootObj;
        int mItemDataIndex = -1;
        public void Init()
        {
            for(int i = 0;i<mStarArray.Length;++i)
            {
                int index = i;
                ClickEventListener listener = ClickEventListener.Get(mStarArray[i].gameObject);
                listener.SetClickEventHandler(delegate (GameObject obj) { OnStarClicked(index); });
            }
            
        }

        void OnStarClicked(int index)
        {
            ItemData data = DataSourceMgr.Get.GetItemDataByIndex(mItemDataIndex);
            if(data == null)
            {
                return;
            }
            if(index == 0 && data.mStarCount == 1)
            {
                data.mStarCount = 0;
            }
            else
            {
                data.mStarCount = index + 1;
            }
            SetStarCount(data.mStarCount);
        }

        public void SetStarCount(int count)
        {
            int i = 0;
            for(; i<count;++i)
            {
                mStarArray[i].color = mRedStarColor;
            }
            for (; i < mStarArray.Length; ++i)
            {
                mStarArray[i].color = mGrayStarColor;
            }
        }
        //更新item数据和UI
        public void SetItemData(ItemData itemData,int itemIndex)
        {
            mItemDataIndex = itemIndex;
            mNameText.text = itemData.mName;
            mDescText.text = itemData.mFileSize.ToString() + "KB";
            mDescText2.text = itemData.mDesc;
            mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
            SetStarCount(itemData.mStarCount);
        }


    }
}
