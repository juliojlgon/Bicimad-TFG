package com.bicis_tfg.bicimad_tfg_app.helpers;

import android.app.Application;
import android.content.res.Resources;
import android.view.View;

import com.bicis_tfg.bicimad_tfg_app.R;

/**
 * Created by JulioLopez on 3/6/16.
 */
public class ResourcesHelper {

    private Application app;
    private Resources resources;

    public ResourcesHelper(Application app) {
        this.app = app;
        resources = app.getResources();

    }

    public String getUserKey(){
       return resources.getString(R.string.UserKey);
    }

    public String getStationKey(){
        return resources.getString(R.string.StationKey);
    }

}
