package com.bicis_tfg.bicimad_tfg_app;

import android.Manifest;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.content.res.Resources;
import android.graphics.Typeface;
import android.location.Location;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.Snackbar;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.view.Gravity;
import android.view.InflateException;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.annimon.stream.Optional;
import com.annimon.stream.Stream;
import com.bicis_tfg.bicimad_tfg_app.models.Station;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptor;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.List;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.plugins.RxJavaErrorHandler;
import rx.plugins.RxJavaPlugins;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;


public class MapFragment extends Fragment implements OnMapReadyCallback, GoogleMap.OnMarkerClickListener,
        GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {

    private static int MY_PERMISSIONS_REQUEST_ACESS_FINE_LOCATION;
    @BindView(R.id.TakeBike)
    Button mTakeActionButton;
    @BindView(R.id.ReturnBike)
    Button mBookActionButton;
    @Inject
    BicimadApplication mApp;
    @Inject
    SharedPreferences sharedPref;
    @Inject
    IBiciMadServices apiService;
    @Inject
    Resources resources;
    private GoogleApiClient mGoogleApiClient;
    private GoogleMap googleMap;
    private Location mCurrentLocation;
    private List<Station> stations;
    private Station station;
    private Marker previousMarker;

    private View root;


    @Override
    public void onAttach(Context context) {
        super.onAttach(context);


        Log.v(getClass().getSimpleName(), "app before injection: " + mApp);
        BicimadApplication.inject(this);
        Log.v(getClass().getSimpleName(), "app after injection: " + mApp);
    }


    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
            if (root!= null) {
                ViewGroup parent = (ViewGroup) root.getParent();
                if (parent != null)
                    parent.removeView(root);
            }
            try {
                root= inflater.inflate(R.layout.fragment_map, null, false);
            } catch (InflateException e) {
        /* map is already there, just return view as it is */
            }


        SupportMapFragment mapFragment = (SupportMapFragment) this.getChildFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        return root;
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
        Log.v(getClass().getSimpleName(), "app before butterinjection: " + mTakeActionButton);
        ButterKnife.bind(this, getActivity());
        Log.v(getClass().getSimpleName(), "app after butterinjection: " + mTakeActionButton);
        this.googleMap = googleMap;

        if (!hasPermission(Manifest.permission.ACCESS_FINE_LOCATION))
            askPermission();

        if (mCurrentLocation == null) {
            mCurrentLocation = new Location("");
            mCurrentLocation.setLongitude(-3.7038);
            mCurrentLocation.setLatitude(40.4168d);
        }
        googleMap.setInfoWindowAdapter(new GoogleMap.InfoWindowAdapter() {

            @Override
            public View getInfoWindow(Marker arg0) {
                return null;
            }

            @Override
            public View getInfoContents(Marker marker) {

                Context context = getContext(); //or getActivity(), YourActivity.this, etc.

                LinearLayout info = new LinearLayout(context);
                info.setOrientation(LinearLayout.VERTICAL);

                TextView title = new TextView(context);
                title.setTextColor(resources.getColor(R.color.primaryText));
                title.setGravity(Gravity.CENTER);
                title.setTypeface(null, Typeface.BOLD);
                title.setText(marker.getTitle());

                TextView snippet = new TextView(context);
                snippet.setTextColor(resources.getColor(R.color.secondaryText));
                snippet.setText(marker.getSnippet());

                info.addView(title);
                info.addView(snippet);

                return info;
            }
        });
        try {
            RxJavaPlugins.getInstance().registerErrorHandler(new RxJavaErrorHandler() {
                @Override
                public void handleError(Throwable e) {
                    Log.w("Error", e);
                }
            });
        } catch (Exception e) {
            Log.e("RxJavaPlugins_Error", "Error controlado.");
        }

        Observable<List<Station>> stationsObs = apiService.getStations();
        stationsObs.doOnError(throwable -> throwable.printStackTrace()).subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(stations -> {
                    this.stations = stations;
                    for (Station station : stations) {
                        googleMap.addMarker(
                                new MarkerOptions()
                                        .position(new LatLng(Double.parseDouble(station.getLatitude()), Double.parseDouble(station.getLongitude())))
                                        .title(station.getStationName() + appendTitle(station.getIsBikeBooked()))
                                        .icon(getIcon(station.getIsBikeBooked(), station.getFreeBikes() / (double) station.getBikeNum()))
                                        .snippet(new StringBuilder().append("Free Bikes: ").append(station.getFreeBikes())
                                                .append("\nFree Slots: ").append(station.getAvailSlots())
                                                .append("\nMetro: ").append(station.getMetro())
                                                .append("\nBus lines: ").append(station.getBus()).toString())
                        );
                    }
                });

        googleMap.setOnMarkerClickListener(this);
        initCamera(mCurrentLocation);


    }

    private String appendTitle(boolean isBooked) {
        if (isBooked)
            return "*Reserva*";
        else
            return "";
    }

    @NonNull
    private BitmapDescriptor getIcon(boolean b, double numero) {
        if (b)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_VIOLET);
        else if (numero > 0.75)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_GREEN);
        else if (numero <= 0)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_CYAN);
        else if (numero < 0.25)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_RED);
        else
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_ORANGE);

    }

    @Override
    public boolean onMarkerClick(Marker marker) {
        //If we selected a icon before return to its original state.
        if (previousMarker != null) {
            Station station = getStationByName(previousMarker.getTitle());
            BitmapDescriptor oldIcon = getIcon(station.getIsBikeBooked(), station.getFreeBikes() / (double) station.getBikeNum());
            previousMarker.setIcon(oldIcon);
        }
        previousMarker = marker;
        String name = marker.getTitle();
        station = getStationByName(name);
        Log.d("Station selected", station.getStationName());
        BitmapDescriptor mIcon = BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_YELLOW);
        marker.setIcon(mIcon);
        return false;
    }

    private Station getStationByName(String name) {
        Optional<Station> op = Stream.of(stations).filter(s -> s.getStationName().equals(name)).findFirst();
        station = (op.isPresent()) ? op.get() : null;
        return station;
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
            editor.putBoolean("Manifest.permission.ACCESS_FINE_LOCATION", true);

        }
    }

    private boolean hasPermission(String permission) {
        return (ContextCompat.checkSelfPermission(getContext(), permission) == PackageManager.PERMISSION_GRANTED);
    }


}

