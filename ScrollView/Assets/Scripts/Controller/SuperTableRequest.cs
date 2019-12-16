using UnityEngine;

namespace SuperScrollView
{
    public class SuperTableRequest : MonoBehaviour
    {
        public LoopListView2 mLoopListView;
        LoadingTipStatus mLoadingTipStatus = LoadingTipStatus.None;
        float mLoadingTipItemHeight = 100;
        int mLoadMoreCount = 20;

        void Start()
        {
            SuperDataSourceMgr.Get.RequestRefreshDataList(OnDataSourceRefreshFinished);
        }
        //更新item数据
        LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
        {
            if (index < 0)
            {
                return null;
            }
            LoopListViewItem2 item = null;
            //当index == 总的数据条数
            if (index == SuperDataSourceMgr.Get.TotalItemCount)
            {
                item = listView.NewListViewItem("ItemPrefab0");
                //更新加载栏状态
                UpdateLoadingTip(item);
                return item;
            }
            ItemData itemData = SuperDataSourceMgr.Get.GetItemDataByIndex(index);
            if (itemData == null)
            {
                return null;
            }
            item = listView.NewListViewItem("ItemPrefab1");
            ListItem2 itemScript = item.GetComponent<ListItem2>();
            //如果没有初始化，则进行初始化
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();
            }
            if (index == SuperDataSourceMgr.Get.TotalItemCount - 1)
            {
                item.Padding = 0;
            }
            //更新item数据和UI
            itemScript.SetItemData(itemData, index);
            return item;
        }
        /// <summary>
        /// 更新加载栏状态
        /// </summary>
        /// <param name="item"></param>
        void UpdateLoadingTip(LoopListViewItem2 item)
        {
            if (item == null)
            {
                return;
            }
            ListItem0 itemScript0 = item.GetComponent<ListItem0>();
            if (itemScript0 == null)
            {
                return;
            }
            if (mLoadingTipStatus == LoadingTipStatus.None)
            {
                itemScript0.mRoot.SetActive(false);
                item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            }
            else if (mLoadingTipStatus == LoadingTipStatus.WaitRelease)
            {
                itemScript0.mRoot.SetActive(true);
                itemScript0.mText.text = "Release to Load More";
                itemScript0.mArrow.SetActive(true);
                itemScript0.mWaitingIcon.SetActive(false);
                item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, mLoadingTipItemHeight);
            }
            else if (mLoadingTipStatus == LoadingTipStatus.WaitLoad)
            {
                itemScript0.mRoot.SetActive(true);
                itemScript0.mArrow.SetActive(false);
                itemScript0.mWaitingIcon.SetActive(true);
                itemScript0.mText.text = "Loading ...";
                item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, mLoadingTipItemHeight);
            }
        }

        void OnBeginDrag()
        {

        }
        void OnDraging()
        {
            if (mLoopListView.ShownItemCount == 0)
            {
                return;
            }
            if (mLoadingTipStatus != LoadingTipStatus.None && mLoadingTipStatus != LoadingTipStatus.WaitRelease)
            {
                return;
            }
            LoopListViewItem2 item = mLoopListView.GetShownItemByItemIndex(SuperDataSourceMgr.Get.TotalItemCount);
            if (item == null)
            {
                return;
            }
            LoopListViewItem2 item1 = mLoopListView.GetShownItemByItemIndex(SuperDataSourceMgr.Get.TotalItemCount - 1);
            if (item1 == null)
            {
                return;
            }
            float y = mLoopListView.GetItemCornerPosInViewPort(item1, ItemCornerEnum.LeftBottom).y;
            if (y + mLoopListView.ViewPortSize >= mLoadingTipItemHeight)
            {
                if (mLoadingTipStatus != LoadingTipStatus.None)
                {
                    return;
                }
                mLoadingTipStatus = LoadingTipStatus.WaitRelease;
                UpdateLoadingTip(item);
            }
            else
            {
                if (mLoadingTipStatus != LoadingTipStatus.WaitRelease)
                {
                    return;
                }
                mLoadingTipStatus = LoadingTipStatus.None;
                UpdateLoadingTip(item);
            }
        }
        void OnEndDrag()
        {
            if (mLoopListView.ShownItemCount == 0)
            {
                return;
            }
            if (mLoadingTipStatus != LoadingTipStatus.None && mLoadingTipStatus != LoadingTipStatus.WaitRelease)
            {
                return;
            }
            LoopListViewItem2 item = mLoopListView.GetShownItemByItemIndex(SuperDataSourceMgr.Get.TotalItemCount);
            if (item == null)
            {
                return;
            }
            mLoopListView.OnItemSizeChanged(item.ItemIndex);
            if (mLoadingTipStatus != LoadingTipStatus.WaitRelease)
            {
                return;
            }
            mLoadingTipStatus = LoadingTipStatus.WaitLoad;
            UpdateLoadingTip(item);
            //请求加载更多数据
            SuperDataSourceMgr.Get.RequestLoadMoreDataList(mLoadMoreCount, OnDataSourceLoadMoreFinished);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        void OnDataSourceRefreshFinished()
        {
            //OnGetItemByIndex：创建item和更新item数据
            mLoopListView.InitListView(SuperDataSourceMgr.Get.TotalItemCount + 1, OnGetItemByIndex);
            mLoopListView.mOnBeginDragAction = OnBeginDrag;
            mLoopListView.mOnDragingAction = OnDraging;
            mLoopListView.mOnEndDragAction = OnEndDrag;
        }

        /// <summary>
        /// 加载数据完成回调
        /// </summary>
        void OnDataSourceLoadMoreFinished()
        {
            if (mLoopListView.ShownItemCount == 0)
            {
                return;
            }
            if (mLoadingTipStatus == LoadingTipStatus.WaitLoad)
            {
                mLoadingTipStatus = LoadingTipStatus.None;
                mLoopListView.SetListItemCount(SuperDataSourceMgr.Get.TotalItemCount + 1, false);
                mLoopListView.RefreshAllShownItem();
            }
        }

        void OnJumpBtnClicked()
        {
            int itemIndex = 0;
            //int.TryParse(string s,out int i) 的参数： s是要转换的字符串，i 是转换的结果。
            if (int.TryParse("0", out itemIndex) == false)
            {
                return;
            }
            if (itemIndex < 0)
            {
                return;
            }
            mLoopListView.MovePanelToItemIndex(itemIndex, 0);
        }

    }

}
