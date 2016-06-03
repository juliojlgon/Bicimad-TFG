package com.bicis_tfg.bicimad_tfg_app;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.v4.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.bicis_tfg.bicimad_tfg_app.helpers.ResourcesHelper;

import javax.inject.Inject;

import services.IBiciMadServices;

/**
 * A placeholder fragment containing a simple view.
 */
public class MainActivityFragment extends Fragment {

    @Inject
    SharedPreferences sharedPreferences;

    @Inject
    ResourcesHelper rHelper;

    @Inject
    IBiciMadServices apiService;

    public MainActivityFragment() {
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        BicimadApplication.inject(this);

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_main, container, false);

    }
}
