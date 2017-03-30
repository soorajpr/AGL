using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using Android.Net;
using Android.Views;
using GroupedPetsList.Shared;
using System;

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
            try
            {
                expandableList = FindViewById<ExpandableListView>(Resource.Id.groupedListView);
                progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
                txtNodataText = FindViewById<TextView>(Resource.Id.txtNodata);
            }
            catch (Exception ex)
            {
                var exception = new CustomException(ex);
                exception.MethodName = "InitializeView";
                ExceptionHandler.Instance.LogError(exception);
            }
        }


        /// <summary>
		/// Method for Get Pets.
		/// </summary>
		async void FetchPetsCollection()
        {
            try
            {
                progressBar.Visibility = ViewStates.Visible;

                if (IsNetworkAvilable())
                {
                    petsOwnerList = await PetsServiceHandler.Instance.GetPetsOwnerListAsync();
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
            catch (Exception ex)
            {
                txtNodataText.Text = GetString(Resource.String.NoData_message);
                txtNodataText.Visibility = ViewStates.Visible;
                progressBar.Visibility = ViewStates.Gone;
                var exception = new CustomException(ex);
                exception.MethodName = "FetchPetsCollection";
                ExceptionHandler.Instance.LogError(exception);
            }

        }

        /// <summary>
		/// Initializes the adapter.
		/// </summary>
		void InitializeAdapter(List<PetsOwner> petsList)
        {
            try
            {
                var petsListAdapter = new ExpandableListViewAdapter(this, petsList);
                expandableList.SetAdapter(petsListAdapter);
            }
            catch (Exception ex)
            {
                var exception = new CustomException(ex);
                exception.MethodName = "InitializeAdapter";
                ExceptionHandler.Instance.LogError(exception);
            }
        }


        /// <summary>
		/// Method to check the internert connectivity
		/// </summary>
		/// <returns><c>true</c> if this instance is network avilable the specified context; otherwise, <c>false</c>.</returns>
		public bool IsNetworkAvilable()
        {
            try
            {
                var context = Application.Context;
                var connectivity = (ConnectivityManager)context.GetSystemService(ConnectivityService);
                if (null == connectivity)
                    return false;
                var info = connectivity.GetAllNetworkInfo();
                return null != info && info.Any(t => t.GetState() == NetworkInfo.State.Connected);
            }
            catch(Exception ex)
            {
                var exception = new CustomException(ex);
                exception.MethodName = "IsNetworkAvilable";
                ExceptionHandler.Instance.LogError(exception);
            }
            return false;
        }

        #endregion
    }
}

