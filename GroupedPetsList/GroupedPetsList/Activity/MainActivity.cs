using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using Android.Net;
using Android.Views;
using GroupedPetsList.Shared;

namespace GroupedPetsList
{
    [Activity(Label = "GroupedPetsList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        #region local declarations

        ExpandableListView expandableList;
        ProgressBar progressBar;
        List<PetsOwner> petsOwnerList;
        TextView txtNodataText;
        #endregion

        #region override methods
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            InitializeView();
        }

        /// <summary>
        /// Ons the start.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
            if (petsOwnerList == null)
                FetchPetsCollection();
        }
        #endregion

        #region private methods
        /// <summary>
        /// Initializes the views.
        /// </summary>
        void InitializeView()
        {
            expandableList = FindViewById<ExpandableListView>(Resource.Id.groupedListView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            txtNodataText= FindViewById<TextView>(Resource.Id.txtNodata);
        }


        /// <summary>
		/// Method for Get Pets.
		/// </summary>
		async void FetchPetsCollection()
        {
            progressBar.Visibility = ViewStates.Visible;

            if (IsNetworkAvilable())
            {
                petsOwnerList = await PetsServiceHandler.Instance.GetPetListAsync();
                if (null != petsOwnerList)
                    InitializeAdapter(petsOwnerList);
                else
                {
                    txtNodataText.Text = GetString(Resource.String.NoData_message);
                    txtNodataText.Visibility = ViewStates.Visible;
                }
            }
            else
            {
                txtNodataText.Text = GetString(Resource.String.network_message);
                txtNodataText.Visibility = ViewStates.Visible;
            }

            progressBar.Visibility = ViewStates.Gone;
        }

        /// <summary>
		/// Initializes the adapter.
		/// </summary>
		void InitializeAdapter(List<PetsOwner> petsList)
        {
            var petsListAdapter = new ExpandableListViewAdapter(this, petsList);
            expandableList.SetAdapter(petsListAdapter);
        }


        /// <summary>
		/// Method to check the internert connectivity
		/// </summary>
		/// <returns><c>true</c> if this instance is network avilable the specified context; otherwise, <c>false</c>.</returns>
		public bool IsNetworkAvilable()
        {
            var context = Application.Context;
            var connectivity = (ConnectivityManager)context.GetSystemService(ConnectivityService);
            if (null == connectivity)
                return false;
            var info = connectivity.GetAllNetworkInfo();
            return null != info && info.Any(t => t.GetState() == NetworkInfo.State.Connected);
        }

        #endregion
    }
}

