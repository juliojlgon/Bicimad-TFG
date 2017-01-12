package com.bicis_tfg.bicimad_tfg_app;

import android.Manifest;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.Drawable;
import android.location.Location;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.StringRes;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
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
import com.bicis_tfg.bicimad_tfg_app.models.BookResult;
import com.bicis_tfg.bicimad_tfg_app.models.CurrentUser;
import com.bicis_tfg.bicimad_tfg_app.models.ReservationResult;
import com.bicis_tfg.bicimad_tfg_app.models.Station;
import com.github.rahatarmanahmed.cpv.CircularProgressView;
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

import java.util.ArrayList;
import java.util.List;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import permissions.dispatcher.NeedsPermission;
import permissions.dispatcher.OnNeverAskAgain;
import permissions.dispatcher.OnPermissionDenied;
import permissions.dispatcher.OnShowRationale;
import permissions.dispatcher.PermissionRequest;
import permissions.dispatcher.RuntimePermissions;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.plugins.RxJavaErrorHandler;
import rx.plugins.RxJavaPlugins;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;

@RuntimePermissions
public class MapFragment extends Fragment implements OnMapReadyCallback, GoogleMap.OnMarkerClickListener,
        GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {

    private static int MY_PERMISSIONS_REQUEST_ACESS_FINE_LOCATION;
    @BindView(R.id.TakeBike)
    Button mTakeActionButton;
    @BindView(R.id.BookBike)
    Button mBookActionButton;
    @BindView(R.id.changeView)
    Button mChangeViewActionButton;
    @BindView(R.id.progress_view)
    CircularProgressView progressView;
    @Inject
    BicimadApplication mApp;
    @Inject
    SharedPreferences sharedPref;
    @Inject
    IBiciMadServices apiService;
    @Inject
    Resources resources;
    @Inject
    CurrentUser currentUser;
    private GoogleApiClient mGoogleApiClient;
    private GoogleMap googleMap;
    private Location mCurrentLocation;
    private List<Station> stations;
    private Station station;
    private Marker previousMarker;

    private  ProgressDialog progress;

    private View root;

    private boolean isTakeState = true;

    private ArrayList<Marker> markerList = new ArrayList<>();




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


       progress = new ProgressDialog(getContext());
        progress.setTitle("Loading");
        progress.setMessage("Loading...");
        progress.setCancelable(false); // disable dismiss by tapping outside of the dialog

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

    @NeedsPermission({Manifest.permission.ACCESS_FINE_LOCATION})
    public void initCamera(Location location) {
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
        stationsObs.doOnError(Throwable::printStackTrace).subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(stations -> {
                    this.stations = stations;
                    for (Station station : stations) {
                        boolean type = (station.getDiscType() == 0);
                        String discount = (type)? String.format("%s€", station.getDiscConst()) : String.format("%s%%", station.getDiscPorc());
                        markerList.add(googleMap.addMarker(
                                new MarkerOptions()
                                        .position(new LatLng(Double.parseDouble(station.getLatitude()), Double.parseDouble(station.getLongitude())))
                                        .title(station.getStationName())
                                        .icon(getIcon(station.getIsBikeBooked(), station.getFreeBikes() / (double) station.getBikeNum()))
                                        .snippet(new StringBuilder().append(appendTitle(station.getIsBikeBooked())).append("Free Bikes: ").append(station.getFreeBikes())
                                                .append("\nFree Slots: ").append(station.getAvailSlots())
                                                .append("\nDiscount: ").append(discount)
                                                .append("\nMetro: ").append(station.getMetro())
                                                .append("\nBus lines: ").append(station.getBus()).toString())
                        ));
                    }
                });

        googleMap.setOnMarkerClickListener(this);
        MapFragmentPermissionsDispatcher.initCameraWithCheck(this, mCurrentLocation);


    }

    private String appendTitle(boolean isBooked) {
        if (isBooked)
            return "You have a reservation here.\n";
        else
            return "";
    }

    @NonNull
    private BitmapDescriptor getIcon(boolean b, double numero) {
        //TODO: Try to change the Cyan for a grey when it's full.
        if (b)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_VIOLET);
        else if (numero > 0.75)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_GREEN);
        else if (numero <= 0)
        {
            Drawable circleDrawable = resources.getDrawable(R.drawable.circle_shape);
            return getMarkerIconFromDrawable(circleDrawable);
        }
        else if (numero < 0.25)
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_RED);
        else
            return BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_ORANGE);

    }

    private BitmapDescriptor getMarkerIconFromDrawable(Drawable drawable) {
        Canvas canvas = new Canvas();
        Bitmap bitmap = Bitmap.createBitmap(drawable.getIntrinsicWidth(), drawable.getIntrinsicHeight(), Bitmap.Config.ARGB_8888);
        canvas.setBitmap(bitmap);
        drawable.setBounds(0, 0, drawable.getIntrinsicWidth(), drawable.getIntrinsicHeight());
        drawable.draw(canvas);
        return BitmapDescriptorFactory.fromBitmap(bitmap);
    }


    @Override
    public boolean onMarkerClick(Marker marker) {
        //If we selected a icon before return to its original state.
        if (previousMarker != null) {
            Station station = getStationByName(previousMarker.getTitle());
            BitmapDescriptor oldIcon = null;
            if(!isTakeState) {
                double percent = station.getAvailSlots() / (double) station.getBikeNum();
                boolean booked = station.getIsSlotBooked();
                oldIcon = getIcon(booked,percent);
            }else {
                double percent = station.getFreeBikes() / (double) station.getBikeNum();
                boolean booked = station.getIsBikeBooked();
                oldIcon = getIcon(booked,percent);
            }
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


    @OnClick(R.id.BookBike)
    void bookBikeOrSlot(){
        //TODO: Crear un dialog con el texto de reservar bici o slot. al aceptar se ejecuta la acción.
        if(isTakeState){
            bookBike();
        }else{
            bookSlot();
        }
        //TODO: Actualizar el marker de la bicicleta que coges.
        getStation();

    }

    @OnClick(R.id.TakeBike)
    void takeOrReturnBike(){
        //TODO: Crear un dialog con el texto de coger o dejar bici. al aceptar se ejecuta la acción.
        if(isTakeState){
            takeBike();
        }else{
            returnBike();
        }
        getStation();
    }

    private void  getStation(){
        Observable<List<Station>> stationsObs = apiService.getStations();
        stationsObs.doOnError(Throwable::printStackTrace).subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(stations -> {
                    this.stations.clear();
                    this.stations = stations;
                    Double percent;
                    boolean booked = false;
                    for(Marker marker: markerList){
                        marker.hideInfoWindow();
                        Station s = getStationByName(marker.getTitle());
                        if(isTakeState) {
                            percent = s.getAvailSlots() / (double) s.getBikeNum();
                            booked = s.getIsSlotBooked();
                            mTakeActionButton.setText(resources.getString(R.string.return_bike));
                            mBookActionButton.setText(resources.getString(R.string.book_slot));
                            mChangeViewActionButton.setText(resources.getString(R.string.return_mode));
                        }else {
                            percent = s.getFreeBikes() / (double) s.getBikeNum();
                            booked = s.getIsBikeBooked();
                            mTakeActionButton.setText(resources.getString(R.string.take_bike));
                            mBookActionButton.setText(resources.getString(R.string.book_bike));
                            mChangeViewActionButton.setText(resources.getString(R.string.take_mode));

                        }
                        boolean type = (s.getDiscType() == 0);
                        String discount = (type)? String.format("%s€", s.getDiscConst()) : String.format("%s%%", s.getDiscPorc());
                        marker.setSnippet(new StringBuilder().append(appendTitle(s.getIsBikeBooked())).append("Free Bikes: ").append(s.getFreeBikes())
                                                .append("\nFree Slots: ").append(s.getAvailSlots())
                                                .append("\nDiscount: ").append(discount)
                                                .append("\nMetro: ").append(s.getMetro())
                                                .append("\nBus lines: ").append(s.getBus()).toString());
                        marker.setIcon(getIcon(booked,percent));
                    }
                    isTakeState = !isTakeState;
                    progress.hide();
                });
    }

    @OnClick(R.id.changeView)
    void changeView(){
        progress.show();
        station = null;
        getStation();
    }

    private void takeBike(){
        if(station == null){
            Snackbar snackbar = Snackbar.make(getView(), "Choose a station first.", Snackbar.LENGTH_LONG)
                    .setAction("Action", null);
            View snackbarView = snackbar.getView();
            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
            textView.setTextColor(Color.RED);
            snackbar.show();
        }else {
            progressView.startAnimation();
            progressView.setVisibility(View.VISIBLE);
            Observable<BookResult> result = apiService.takeBike(currentUser.getId(), station.getId());
            result.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                    .subscribe(bookResult -> {
                        progressView.stopAnimation();
                        progressView.setVisibility(View.GONE);
                        if (bookResult.isSuccess()) {
                            Snackbar snackbar = Snackbar.make(getView(), "Bike successful taken. " + bookResult.getBikeId(), Snackbar.LENGTH_LONG)
                                    .setAction("Action", null);
                            snackbar.show();
                        } else {
                            Snackbar snackbar = Snackbar.make(getView(), bookResult.getError(), Snackbar.LENGTH_LONG)
                                    .setAction("Action", null);
                            View snackbarView = snackbar.getView();
                            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
                            textView.setTextColor(Color.RED);
                            snackbar.show();
                        }
                    });
        }
    }

    private void bookBike(){
        if(station == null){
            Snackbar snackbar = Snackbar.make(getView(), "Choose a station first.", Snackbar.LENGTH_LONG)
                    .setAction("Action", null);
            View snackbarView = snackbar.getView();
            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
            textView.setTextColor(Color.RED);
            snackbar.show();
        }else{
            progressView.startAnimation();
            progressView.setVisibility(View.VISIBLE);
        Observable<BookResult> result = apiService.bookBike(currentUser.getId(),station.getId());
        result.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(bookResult -> {
                    progressView.stopAnimation();
                    progressView.setVisibility(View.GONE);
                    if(bookResult.isSuccess()){
                        Snackbar snackbar = Snackbar.make(getView(), "Bike successful booked. " + bookResult.getBikeId(), Snackbar.LENGTH_LONG)
                                .setAction("Action", null);
                        snackbar.show();
                    }else{
                        Snackbar snackbar = Snackbar.make(getView(), bookResult.getError(), Snackbar.LENGTH_LONG)
                                .setAction("Action", null);
                        View snackbarView = snackbar.getView();
                        TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
                        textView.setTextColor(Color.RED);
                        snackbar.show();
                    }
                });
        }
    }

    private void bookSlot(){
        if(station == null){
            Snackbar snackbar = Snackbar.make(getView(), "Choose a station first.", Snackbar.LENGTH_LONG)
                    .setAction("Action", null);
            View snackbarView = snackbar.getView();
            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
            textView.setTextColor(Color.RED);
            snackbar.show();
        }else {
            progressView.startAnimation();
            progressView.setVisibility(View.VISIBLE);
            Observable<BookResult> result = apiService.bookSlot(currentUser.getId(), station.getId());
            result.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                    .subscribe(bookResult -> {
                        progressView.stopAnimation();
                        progressView.setVisibility(View.GONE);
                        if (bookResult.isSuccess()) {
                            Snackbar snackbar = Snackbar.make(getView(), "Slot successful booked. " + bookResult.getBikeId(), Snackbar.LENGTH_LONG)
                                    .setAction("Action", null);
                            snackbar.show();
                        } else {
                            Snackbar snackbar = Snackbar.make(getView(), bookResult.getError(), Snackbar.LENGTH_LONG)
                                    .setAction("Action", null);
                            View snackbarView = snackbar.getView();
                            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
                            textView.setTextColor(Color.RED);
                            snackbar.show();
                        }
                    });
        }
    }

    private void returnBike(){
        if(station == null){
            Snackbar snackbar = Snackbar.make(getView(), "Choose a station first.", Snackbar.LENGTH_LONG)
                    .setAction("Action", null);
            View snackbarView = snackbar.getView();
            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
            textView.setTextColor(Color.RED);
            snackbar.show();
        }else {
            progressView.startAnimation();
            progressView.setVisibility(View.VISIBLE);
            Observable<ReservationResult> result = apiService.returnBike(currentUser.getId(), station.getId());
            result.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                    .subscribe(bookResult -> {
                        progressView.stopAnimation();
                        progressView.setVisibility(View.GONE);
                        if (bookResult.isSuccess()) {
                            Snackbar snackbar = Snackbar.make(getView(), "Bike successful returned.", Snackbar.LENGTH_LONG)
                                    .setAction("Action", null);
                            snackbar.show();
                        } else {
                            Snackbar snackbar = Snackbar.make(getView(), bookResult.getError(), Snackbar.LENGTH_LONG)
                                    .setAction("Action", null);
                            View snackbarView = snackbar.getView();
                            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
                            textView.setTextColor(Color.RED);
                            snackbar.show();
                        }
                    });
        }
    }





    @OnShowRationale({Manifest.permission.ACCESS_FINE_LOCATION})
    void showRationaleForContact(PermissionRequest request) {
        // NOTE: Show a rationale to explain why the permission is needed, e.g. with a dialog.
        // Call proceed() or cancel() on the provided PermissionRequest to continue or abort
        showRationaleDialog(R.string.permission_location_rationale, request);
    }
    @OnPermissionDenied(Manifest.permission.ACCESS_FINE_LOCATION)
    void onLocationDenied() {
        // NOTE: Deal with a denied permission, e.g. by showing specific UI
        // or disabling certain functionality
        Snackbar.make(root, R.string.permission_location_denied, Snackbar.LENGTH_LONG)
                .setAction("Action", null).show();
    }

    @OnNeverAskAgain(Manifest.permission.ACCESS_FINE_LOCATION)
    void onLocationNeverAskAgain() {
        Snackbar.make(root, R.string.permission_location_never_askagain, Snackbar.LENGTH_LONG)
                       .setAction("Action", null).show();
    }

    private void showRationaleDialog(@StringRes int messageResId, final PermissionRequest request) {
        new AlertDialog.Builder(getContext())
                .setPositiveButton(R.string.button_allow, (dialog, which) -> {
                    request.proceed();
                })
                .setNegativeButton(R.string.button_deny, (dialog, which) -> {
                    request.cancel();
                })
                .setCancelable(false)
                .setMessage(messageResId)
                .show();
    }
    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        // NOTE: delegate the permission handling to generated method
        MapFragmentPermissionsDispatcher.onRequestPermissionsResult(this, requestCode, grantResults);
    }
}


