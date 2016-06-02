using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Assignment_7
{
    public class ExpandableDataAdapter : IExpandableListAdapter
    {
        Activity context;
        List<ReceiptClass> items;

        public ExpandableDataAdapter(Activity context, List<ReceiptClass> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public int GroupCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool HasStableIds
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsEmpty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool AreAllItemsEnabled()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public long GetChildId(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public int GetChildrenCount(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = items[childPosition];
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.childLyt, null);
            view.FindViewById<TextView>(Resource.Id.tv_tax).Text = item.taxInvoice;
            view.FindViewById<TextView>(Resource.Id.tv_gst).Text = item.gstNum;

            return view;
        }

        public long GetCombinedChildId(long groupId, long childId)
        {
            throw new NotImplementedException();
        }

        public long GetCombinedGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public long GetGroupId(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var item = items[groupPosition];
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CostumRw, null);
            view.FindViewById<TextView>(Resource.Id.txtBsnssNm).Text = item.business;
            view.FindViewById<TextView>(Resource.Id.txtVwPrice).Text = item.productTotal.ToString();

            return view;
        }

        public bool IsChildSelectable(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public void OnGroupCollapsed(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public void OnGroupExpanded(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public void RegisterDataSetObserver(DataSetObserver observer)
        {
            throw new NotImplementedException();
        }

        public void UnregisterDataSetObserver(DataSetObserver observer)
        {
            throw new NotImplementedException();
        }
        ////Activity context;
        ////List<ReceiptClass> items;

        ////public ExpandableDataAdapter(Activity context, List<ReceiptClass> items) : base()
        ////{
        ////    this.context = context;
        ////    this.items = items;
        ////}

        ////public override int GroupCount
        ////{
        ////    get
        ////    {
        ////        throw new NotImplementedException();
        ////    }
        ////}

        ////public override bool HasStableIds
        ////{
        ////    get
        ////    {
        ////        throw new NotImplementedException();
        ////    }
        ////}

        ////public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public override long GetChildId(int groupPosition, int childPosition)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public override int GetChildrenCount(int groupPosition)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        ////{
        ////    var item = items[childPosition];
        ////    View view = convertView;

        ////    if (view == null)
        ////        view = context.LayoutInflater.Inflate(Resource.Layout.childLyt, null);
        ////    view.FindViewById<TextView>(Resource.Id.tv_tax).Text = item.taxInvoice;
        ////    view.FindViewById<TextView>(Resource.Id.tv_gst).Text = item.gstNum;

        ////    return view;
        ////}

        ////public override Java.Lang.Object GetGroup(int groupPosition)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public override long GetGroupId(int groupPosition)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        ////{
        ////    var item = items[groupPosition];
        ////    View view = convertView;

        ////    if (view == null)
        ////        view = context.LayoutInflater.Inflate(Resource.Layout.CostumRw, null);
        ////    view.FindViewById<TextView>(Resource.Id.txtBsnssNm).Text = item.business;
        ////    view.FindViewById<TextView>(Resource.Id.txtVwPrice).Text = item.productTotal.ToString();

        ////    return view;
        ////}

        ////public override bool IsChildSelectable(int groupPosition, int childPosition)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //        protected List<ReceiptClass> DataList { get; set; }

        //        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        //        {
        //            View header = convertView;
        //            if (header == null)
        //            {
        //                header = Context.LayoutInflater.Inflate(Resource.Layout.CostumRw, null);
        //            }
        //            header.FindViewById<TextView>(Resource.Id.txtBsnssNm).Text = DataList.business

        //            return header;
        //        }

        //        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        //        {
        //            View row = convertView;
        //            if (row == null)
        //            {
        //                row = Context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);
        //            }
        //            string newId = "", newValue = "";
        //            GetChildViewHelper(groupPosition, childPosition, out newId, out newValue);
        //            row.FindViewById<TextView>(Resource.Id.DataId).Text = newId;
        //            row.FindViewById<TextView>(Resource.Id.DataValue).Text = newValue;

        //            return row;
        //            //throw new NotImplementedException ();
        //        }

        //        public override int GetChildrenCount(int groupPosition)
        //        {
        //            char letter = (char)(65 + groupPosition);
        //            List<ReceiptClass> results = DataList.FindAll((ReceiptClass obj) => obj.Id[0].Equals(letter));
        //            return results.Count;
        //        }

        //        public override int GroupCount
        //        {
        //            get
        //            {
        //                return 26;
        //            }
        //        }

        //        private void GetChildViewHelper(int groupPosition, int childPosition, out string Id, out string Value)
        //        {
        //            char letter = (char)(65 + groupPosition);
        //            List<ReceiptClass> results = DataList.FindAll((ReceiptClass obj) => obj.Id[0].Equals(letter));
        //            Id = results[childPosition].Id;
        //            Value = results[childPosition].Value;
        //        }

        //        #region implemented abstract members of BaseExpandableListAdapter

        //        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public override long GetChildId(int groupPosition, int childPosition)
        //        {
        //            return childPosition;
        //        }

        //        public override Java.Lang.Object GetGroup(int groupPosition)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public override long GetGroupId(int groupPosition)
        //        {
        //            return groupPosition;
        //        }

        //        public override bool IsChildSelectable(int groupPosition, int childPosition)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public override bool HasStableIds
        //        {
        //            get
        //            {
        //                return true;
        //            }
        //        }

        //        #endregion

    }
}