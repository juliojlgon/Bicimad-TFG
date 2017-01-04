package com.bicis_tfg.bicimad_tfg_app.renderers;

import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.bicis_tfg.bicimad_tfg_app.BicimadApplication;
import com.bicis_tfg.bicimad_tfg_app.R;
import com.bicis_tfg.bicimad_tfg_app.models.CurrentUser;
import com.bicis_tfg.bicimad_tfg_app.models.Reservation;
import com.bicis_tfg.bicimad_tfg_app.models.ReservationResult;
import com.bicis_tfg.bicimad_tfg_app.models.ValidResult;
import com.pedrogomez.renderers.Renderer;

import org.joda.time.DateTime;
import org.joda.time.format.DateTimeFormat;
import org.joda.time.format.DateTimeFormatter;

import java.util.Locale;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;

/**
 * Created by JulioLopez on 5/6/16.
 */
public class ReservationRenderer extends Renderer<Reservation> {
    @BindView(R.id.station)
    TextView stationText;

    @BindView(R.id.type)
    TextView typeText;

    @BindView(R.id.ident)
    TextView identText;

    @BindView(R.id.time)
    TextView timeText;


    @BindView(R.id.deleteThumbnail)
    ImageView deleteBtn;

    @Inject
    IBiciMadServices apiService;

    @Inject
    CurrentUser user;

    @Override
    protected void setUpView(View rootView) {
        BicimadApplication.inject(this);
        /*
         * Empty implementation substituted with the usage of ButterKnife library by Jake Wharton.
         */
    }

    @Override
    protected void hookListeners(View rootView) {
        /*
        * Empty implementation substituted with the usage of ButterKnife library by Jake Wharton.
        */
    }

    @Override
    protected View inflate(LayoutInflater inflater, ViewGroup parent) {
        View inflatedView = inflater.inflate(R.layout.reservation_list_row, parent, false);
        ButterKnife.bind(this, inflatedView);
        return inflatedView;
    }

    @OnClick(R.id.deleteThumbnail)
    void onDeleteClicked() {
        Reservation r = getContent();
        AlertDialog.Builder alert = new AlertDialog.Builder(getContext());
        alert.setTitle("Remove reservation with id " + r.getItemId());
        alert.setMessage("You are going to delete a reservation.");
        alert.setCancelable(true);
        alert.setPositiveButton("Delete", (dialog, which) -> {
            Observable<ReservationResult> obsdelete;
            if (r.isIsbike()) {
                obsdelete = apiService.removeBikeReservation(user.getId(), r.getStationId());
            } else {
                obsdelete = apiService.removeSlotReservation(user.getId(), r.getStationId());
            }

            obsdelete.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                    .subscribe(validResult -> {
                        if (validResult.isSuccess()) {
                            dialog.dismiss();
                        } else {
                            dialog.cancel();
                        }
                    });
        }).setNegativeButton("Cancel",(dialog1, which1) -> {dialog1.cancel();}).show();
    }


    @Override
    public void render() {
        Reservation reservations = getContent();
        stationText.setText(reservations.getStationName());
        typeText.setText(getTypeString(reservations.isIsbike()));
        identText.setText("Ident: " + reservations.getItemId());
        timeText.setText(formatTimeToString(reservations.getCreatedDate()));
    }

    private String formatTimeToString(String time) {
        time = time.substring(11, 16);
        DateTimeFormatter fmt = DateTimeFormat.forPattern("HH:mm").withLocale(Locale.FRANCE);
        DateTime t = fmt.parseDateTime(time);
        return t.toString(fmt);

    }

    private String getTypeString(boolean isBike) {
        return (isBike) ? "Bike" : "Slot";
    }
}
