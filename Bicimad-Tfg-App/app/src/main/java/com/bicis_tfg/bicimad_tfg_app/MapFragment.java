package com.bicis_tfg.bicimad_tfg_app;

import android.Manifest;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.Snackbar;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.view.View;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;

import javax.inject.Inject;


public class MapFragment extends com.google.android.gms.maps.MapFragment implements OnMapReadyCallback, GoogleMap.OnMarkerClickListener,
        GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {

    private static int MY_PERMISSIONS_REQUEST_ACESS_FINE_LOCATION;
    private GoogleApiClient mGoogleApiClient;
    private GoogleMap googleMap;
    private Location mCurrentLocation;

    @Inject BicimadApplication mApp;
    @Inject SharedPreferences sharedPref;

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);


        Log.v(getClass().getSimpleName(), "app before injection: " + mApp);
        BicimadApplication.inject(this);
        Log.v(getClass().getSimpleName(), "app after injection: " + mApp);
    }

    @Override
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);



        MapFragment map = (MapFragment) getFragmentManager()
                .findFragmentById(R.id.map);
        map.getMapAsync(this);
    }


    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        if (mGoogleApiClient == null) {
            mGoogleApiClient = new GoogleApiClient.Builder(getActivity().getApplicationContext())
                    .addConnectionCallbacks(this)
                    .addOnConnectionFailedListener(this)
                    .addApi(LocationServices.API)
                    .build();
        }



    }

    private void initCamera(Location location) {
        CameraPosition position = CameraPosition.builder()
                .target(new LatLng(location.getLatitude(),
                        location.getLongitude()))
                .zoom(16f)
                .bearing(0.0f)
                .tilt(0.0f)
                .build();

        googleMap.animateCamera(CameraUpdateFactory
                .newCameraPosition(position), null);

        googleMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);
        googleMap.setTrafficEnabled(true);
        googleMap.getUiSettings().setZoomControlsEnabled(true);
        googleMap.getUiSettings().setCompassEnabled(true);

        if (ContextCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED) {
            googleMap.setMyLocationEnabled(true);
        }


    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        this.googleMap = googleMap;

        if(!hasPermission(Manifest.permission.ACCESS_FINE_LOCATION))
            askPermission();

        if (mCurrentLocation == null) {
            mCurrentLocation = new Location("");
            mCurrentLocation.setLongitude(-3.7038);
            mCurrentLocation.setLatitude(40.4168d);
        }

        initCamera(mCurrentLocation);


    }

    @Override
    public boolean onMarkerClick(Marker marker) {
        return false;
    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {
        if (ContextCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED) {

            mCurrentLocation = LocationServices
                    .FusedLocationApi
                    .getLastLocation(mGoogleApiClient);
        }

    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }

    @Override
    public void onStart() {
        mGoogleApiClient.disconnect();
        super.onStart();

    }

    @Override
    public void onStop() {
        mGoogleApiClient.disconnect();
        super.onStop();
    }

    private void askPermission() {
        if (ActivityCompat.shouldShowRequestPermissionRationale(getActivity(),
                Manifest.permission.ACCESS_FINE_LOCATION)) {

            Snackbar.make(this.getView(), "We need location for Biking stuff", Snackbar.LENGTH_LONG)
                    .setAction("Action", null).show();

        } else {
            ActivityCompat.requestPermissions(getActivity(),
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                    MY_PERMISSIONS_REQUEST_ACESS_FINE_LOCATION);

             SharedPreferences.Editor editor = sharedPref.edit();
            editor.putBoolean("Manifest.permission.ACCESS_FINE_LOCATION",true);
            getMapAsync(this);

        }
    }

    private boolean hasPermission(String permission) {
        return (ContextCompat.checkSelfPermission(getContext(), permission) == PackageManager.PERMISSION_GRANTED);
    }


}

