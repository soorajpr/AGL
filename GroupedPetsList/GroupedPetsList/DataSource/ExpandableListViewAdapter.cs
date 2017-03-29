using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using GroupedPetsList.Shared;

namespace GroupedPetsList
{
    public class ExpandableListViewAdapter : BaseExpandableListAdapter
    {
        #region Local Declarations

        readonly Activity context;
        readonly List<PetsOwner> petsList;

        #endregion

        #region constructor

        public ExpandableListViewAdapter(Activity cntxt, List<PetsOwner> pets)
        {
            context = cntxt;
            petsList = pets;
        }

        #endregion

        #region Override Methods

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            var rootObjectList = petsList[groupPosition].Pets;
            return rootObjectList.Count;
        }

        /// <summary>
        /// Gets the child view.
        /// </summary>
        /// <returns>The child view.</returns>
        /// <param name="groupPosition">Group position.</param>
        /// <param name="childPosition">Child position.</param>
        /// <param name="isLastChild">If set to <c>true</c> is last child.</param>
        /// <param name="convertView">Convert view.</param>
        /// <param name="parent">Parent.</param>
        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.SortedPetsLayout, null);
            var textViewName = view.FindViewById<TextView>(Resource.Id.petsName);
            var petObject = petsList[groupPosition].Pets;
            textViewName.Text = petObject[childPosition].PetName;
            return view;

        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return groupPosition;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        /// <summary>
        /// Gets the group view.
        /// </summary>
        /// <returns>The group view.</returns>
        /// <param name="groupPosition">Group position.</param>
        /// <param name="isExpanded">If set to <c>true</c> is expanded.</param>
        /// <param name="convertView">Convert view.</param>
        /// <param name="parent">Parent.</param>
        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.PetsOwnerHeaderLayout, null);
            //to expand all childviews
            var expandableView = (ExpandableListView)parent;
            expandableView.ExpandGroup(groupPosition);
            var groupName = view.FindViewById<TextView>(Resource.Id.groupHeaderName);
            groupName.Text = petsList[groupPosition].Gender;
            return view;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }


        public override int GroupCount
        {
            get
            {
                return petsList.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}