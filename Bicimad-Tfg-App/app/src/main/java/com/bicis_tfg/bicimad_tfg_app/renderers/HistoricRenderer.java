package com.bicis_tfg.bicimad_tfg_app.renderers;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.bicis_tfg.bicimad_tfg_app.R;
import com.bicis_tfg.bicimad_tfg_app.models.History;
import com.pedrogomez.renderers.Renderer;

import org.joda.time.DateTime;
import org.joda.time.format.DateTimeFormat;
import org.joda.time.format.DateTimeFormatter;

import java.util.Locale;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * Created by JulioLopez on 4/6/16.
 */
public class HistoricRenderer extends Renderer<History> {


    @BindView(R.id.arrivalstation)
    TextView arrivalSText;

    @BindView(R.id.departurestation)
    TextView departureSText;

    @BindView(R.id.bikeident)
    TextView bikeIdText;

    @BindView(R.id.date)
    TextView dateText;


    @Override
    protected void setUpView(View rootView) {
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
        View inflatedView = inflater.inflate(R.layout.history_list_row, parent, false);
        ButterKnife.bind(this, inflatedView);
        return inflatedView;
    }

    @Override
    public void render() {
        History historial = getContent();
        this.arrivalSText.setText(historial.getArrivalStationUserName());
        this.departureSText.setText(historial.getDepartureStationUserName());
        this.bikeIdText.setText("Ident: " + historial.getBikeId());
        this.dateText.setText(formatTimeToString(historial.getCreatedDate()));
    }

    private String formatTimeToString(String time){
        time = time.substring(0,10);
        DateTimeFormatter fmt = DateTimeFormat.forPattern("yyyy-MM-dd").withLocale(Locale.FRANCE);
        DateTime t = fmt.parseDateTime(time);
        return t.toString(fmt);

    }
}
